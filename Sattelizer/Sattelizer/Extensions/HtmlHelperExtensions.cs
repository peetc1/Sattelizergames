using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Sattelizer.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Formats link with an image.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="actionResult">The action result.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString ImageLink(this HtmlHelper html, ActionResult actionResult)
        {
            return new MvcHtmlString("");
        }

        /// <summary>
        /// Formats the text as HTML.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="text">The text.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString FormatTextAsHtml(this HtmlHelper html, string text)
        {
            // string builder
            var sb = new StringBuilder();
            
            // split on spaces
            var splitData = text.Split(' ');

            // regex
            const string twitterRegex = @"^@[\w]";

            // replace urls with actual links
            foreach (var str in splitData)
            {
                Uri uri;
                if (Uri.TryCreate(str, UriKind.Absolute, out uri))
                {
                    sb.AppendFormat("<a href =\"{0}\" target=\"_blank\">{0}</a> ", str);
                    continue;
                }

                if (Regex.IsMatch(str, twitterRegex))
                {
                    sb.AppendFormat("<a href =\"https://twitter.com/{0}\" target=\"_blank\">{1}</a> ", str.Remove(0, 1), str);
                    continue;
                }

                sb.Append(str + " ");
            }

            return new MvcHtmlString(sb.ToString().Trim());
        }

        public static MvcHtmlString GetTimeFromNow(this HtmlHelper html, DateTime date)
        {
            var diff = DateTime.Now.Subtract(date);

            if (diff.Duration().TotalHours > 23)
            {
                return new MvcHtmlString(String.Format("{0:dd MM}", date));
            }


            return diff.Duration().TotalMinutes > 59
                ? new MvcHtmlString(String.Format("{0:##}h", diff.Duration().TotalHours))
                : new MvcHtmlString(String.Format("{0:##}m", diff.Duration().TotalMinutes));


        }
    }
}