using NerdDinner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerdDinner.Controllers
{
    public static class ControllerHelpers
    {
        public static void AddRuleViolations(this ModelStateDictionary modelState, IEnumerable<RuleViolation> errors)
        {
            foreach (RuleViolation issue in errors)
            {
                modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
            }
        }
    }

    public class DinnersController : Controller
    {
        readonly IDinnerRepository _dinnerRepository;

        public DinnersController() : this(new DinnerRepository())
        {
            
        }

        public DinnersController(IDinnerRepository repository)
        {
            _dinnerRepository = repository;
        }

        //
        // GET: /Dinners/
        public ActionResult Index(int? page)
        {
           const int pageSize = 10;

            var dinners = _dinnerRepository.FindUpcomingDinners().ToList();
            //var paginatedDinners = new PaginatedList<Dinner> (dinners, page ?? 0, pageSize);
            //return View(paginatedDinners);


            return View(dinners);
        }
        //
        // GET: /Dinners/Details/2
        public ActionResult Details(int id)
        {
            Dinner dinner = _dinnerRepository.GetDinner(id);
            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }

        //
        // GET: /Dinners/Edit/2
        [Authorize]
        public ActionResult Edit(int id)
        {
            Dinner dinner = _dinnerRepository.GetDinner(id);
            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            return View(new DinnerFormViewModel(dinner));
        }
        //
        // POST: /Dinners/Edit/2
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            Dinner dinner = _dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            try
            {
                UpdateModel(dinner);
                _dinnerRepository.Save();
                return RedirectToAction("Details", new { id = dinner.DinnerID });
            }
            catch
            {
                ModelState.AddRuleViolations(dinner.GetRuleViolations());
                return View(new DinnerFormViewModel(dinner));
            }
        }

        // GET: /Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            Dinner dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            return View(new DinnerFormViewModel(dinner));
        }

        // POST: /Dinners/Create
        [AcceptVerbs(HttpVerbs.Post), Authorize]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dinner.HostedBy = User.Identity.Name;

                    RSVP rsvp = new RSVP();
                    rsvp.AttendeeName = User.Identity.Name;
                    dinner.RSVPs.Add(rsvp);

                    _dinnerRepository.Add(dinner);
                    _dinnerRepository.Save();

                    return RedirectToAction("Details", new { id = dinner.DinnerID });
                }
                catch
                {
                    ModelState.AddRuleViolations(dinner.GetRuleViolations());
                }
            }
            return View(new DinnerFormViewModel(dinner));
        }

        // HTTP GET: /Dinners/Delete/1
        public ActionResult Delete(int id)
        {
            
            Dinner dinner = _dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }
            if (dinner == null)
                return View("NotFound");
            else if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            else
                return View(dinner);
        }

        // HTTP POST: /Dinners/Delete/1
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int id, string confirmButton)
        {
            Dinner dinner = _dinnerRepository.GetDinner(id);
            if (dinner == null)
                return View("NotFound");
            else if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");
            else
                _dinnerRepository.Delete(dinner);
                _dinnerRepository.Save();
                return View("Deleted");
        }
    }
}
