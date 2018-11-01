using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCore.Common.Utils;
using System.Collections.Generic;

namespace NetCore.Common.Mvc
{
    public static class ModelStateUtils
    {
        public static void AddFormError(this ModelStateDictionary modelState, string errorMessage) => modelState.AddModelError("", errorMessage);

        public static void RemoveErrors(this ModelStateDictionary modelState, params string[] fieldNames) => fieldNames.ForEach(fieldName => modelState.Remove(fieldName));

        public static Dictionary<string, IList<string>> ToErrorMap(this ModelStateDictionary modelState)
        {
            var map = new Dictionary<string, IList<string>>();

            modelState.Keys.ForEach(key =>
            {
                var entry = modelState[key];
                var list = new List<string>(entry.Errors.Count);

                entry.Errors.ForEach(error =>
                {
                    list.Add(error.ErrorMessage);
                });

                map[key] = list;
            });
            return map;
        }
    }
}
