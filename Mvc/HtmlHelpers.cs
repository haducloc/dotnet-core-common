using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NetCore.Common.Mvc
{
    public static class HtmlHelpers
    {
        public static IHtmlContent Id(this IHtmlHelper html, string id)
        {
            return html.Raw("id=\"" + id + "\"");
        }

        public static IHtmlContent IIf(this IHtmlHelper html, bool expr, string rawTrueString, string rawFalseString)
        {
            if (expr)
            {
                return html.Raw(rawTrueString);
            }
            return html.Raw(rawFalseString);
        }

        public static IHtmlContent Checked(this IHtmlHelper html, bool? expr)
        {
            if (expr == null || !expr.Value) return HtmlString.Empty;
            return html.Raw("checked='checked'");
        }

        public static IHtmlContent Readonly(this IHtmlHelper html, bool? expr)
        {
            if (expr == null || !expr.Value) return HtmlString.Empty;
            return html.Raw("readonly='readonly'");
        }

        public static IHtmlContent Disabled(this IHtmlHelper html, bool? expr)
        {
            if (expr == null || !expr.Value) return HtmlString.Empty;
            return html.Raw("disabled='disabled'");
        }

        public static IHtmlContent Selected(this IHtmlHelper html, bool? expr)
        {
            if (expr == null || !expr.Value) return HtmlString.Empty;
            return html.Raw("selected='selected'");
        }

        public static IHtmlContent SelectedIndex(this IHtmlHelper html, string selectSelector, object curValue)
        {
            if (curValue != null) return HtmlString.Empty;
            return html.Raw(selectSelector + ".prop(\"selectedIndex\", -1);");
        }
    }
}
