using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdDinner.Models;

namespace NerdDinner.Controllers
{
    public class JsonDinner
    {
        public int DinnerId { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RsvpCount { get; set; }
    }

    public class SearchController : Controller
    {
        DinnerRepository dinnerRepository = new DinnerRepository();

        //
        // AJAX: /Search/SearchByLocation
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchByLocation(float longitude, float latitude)
        {
            var dinners = dinnerRepository.FindByLocation(latitude, longitude);
            var jsonDinners = from dinner in dinners
                              select new JsonDinner
                              {
                                  DinnerId = dinner.DinnerID,
                                  Latitude = dinner.Latitude,
                                  Longitude = dinner.Longitude,
                                  Title = dinner.Title,
                                  Description = dinner.Description,
                                  RsvpCount = dinner.RSVPs.Count
                              };
            return Json(jsonDinners.ToList());
        }
    }
}
