using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.Common.Utils;

namespace NetCore.Common.Mvc
{
    public static class ModelStateUtils
    {
        public static void AddFormError(this ModelStateDictionary modelState, string errorMessage) => modelState.AddModelError("", errorMessage);

        public static void RemoveErrors(this ModelStateDictionary modelState, params string[] fieldNames) => fieldNames.ForEach(fieldName => modelState.Remove(fieldName));
    }
}
