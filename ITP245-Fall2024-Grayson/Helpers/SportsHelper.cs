using ITP245_Fall2024_GraysonModel; // Ensure this namespace is correct
using System.Text;  // For StringBuilder
using System.Web.Mvc;

namespace ITP245_Fall2024_Grayson.Helpers
{
    public static class SportsHelper
    {
        /// <summary>
        /// Generates a customizable HTML heading for ISports objects with default conventions and action context.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper instance used to access route data.</param>
        /// <param name="sports">An object implementing ISports interface.</param>
        /// <param name="headingLevel">The HTML heading level (default is "h2").</param>
        /// <param name="id">The id attribute of the heading tag (optional).</param>
        /// <param name="cssClass">The class attribute of the heading tag (optional).</param>
        /// <returns>An MvcHtmlString containing the generated heading.</returns>
        public static MvcHtmlString NameHeader(
            this HtmlHelper htmlHelper,
            ISports sports,
            string headingLevel = "h2",
            string id = null,
            string cssClass = null)
        {
            // Get the current action name from route data if not passed as a parameter
            string action = htmlHelper.ViewContext.RouteData.Values["action"]?.ToString();
            return NameHeader(sports, action, headingLevel, id, cssClass, GetPrefix(sports), GetSuffix(sports));
        }

        /// <summary>
        /// Generates a fully customizable HTML heading for ISports objects.
        /// </summary>
        public static MvcHtmlString NameHeader(
            ISports sports,
            string action,
            string headingLevel,
            string id,
            string cssClass,
            string prefix,
            string suffix)
        {
            if (sports == null) return MvcHtmlString.Empty;

            StringBuilder html = new StringBuilder();

            // Default to the action name if it's not provided
            if (string.IsNullOrEmpty(action))
            {
                action = "View"; // Default action name if not provided
            }

            // Create the tag dynamically based on the headingLevel
            TagBuilder headerTag = new TagBuilder(headingLevel);

            // Add optional id and class attributes
            if (!string.IsNullOrEmpty(id))
                headerTag.MergeAttribute("id", id);

            if (!string.IsNullOrEmpty(cssClass))
                headerTag.MergeAttribute("class", cssClass);

            // Set the inner text of the header, including action context
            headerTag.SetInnerText($"{action} {prefix}{sports.Name}{suffix}");

            // Return the HTML string
            html.Append(headerTag.ToString());
            return new MvcHtmlString(html.ToString());
        }

        /// <summary>
        /// Determines a default prefix based on the ISports object type.
        /// </summary>
        private static string GetPrefix(ISports sports)
        {
            if (sports is Team) return "Team: ";
            if (sports is Player) return "Player: ";
            if (sports is Field) return "Field: ";
            if (sports is Game) return "Game: ";
            return string.Empty; // Default for unknown types
        }

        /// <summary>
        /// Determines a default suffix based on the ISports object type.
        /// </summary>
        private static string GetSuffix(ISports sports)
        {
            if (sports is Game) return " (Upcoming)";
            return string.Empty; // Default for other types
        }
    }
}
