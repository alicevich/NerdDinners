using NerdDinner.Models;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
public static class ControllerHelpers
{
    public static void AddRuleViolations(this ModelStateDictionary modelState,
    IEnumerable<RuleViolation> errors)
    {
        foreach (RuleViolation issue in errors)
        {
            modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
        }
    }
}