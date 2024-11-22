using System.Text;
using System.Web.Mvc;
using ITP245_Fall2024_GraysonModel;

namespace ITP245_Fall2024_Grayson.Helpers
{
    public static class ButtonHelper
    {
        /// <summary>
        /// Generates a customizable button for each team with required attributes.
        /// </summary>
        /// <param name="team">The team object to be displayed on the button.</param>
        /// <param name="buttonClass">The CSS class for styling the button (optional).</param>
        /// <param name="onClickAction">JavaScript action for button click (optional).</param>
        /// <returns>An MvcHtmlString containing the generated button HTML.</returns>
        public static MvcHtmlString TeamButton(Team team, string buttonClass = "btn btn-primary", string onClickAction = null)
        {
            StringBuilder htmlString = new StringBuilder();

            // Create the button
            TagBuilder buttonTag = new TagBuilder("button");
            buttonTag.AddCssClass(buttonClass); // Add default button class
            buttonTag.MergeAttribute("id", "team-button-" + team.TeamId); // ID based on TeamId
            buttonTag.MergeAttribute("value", team.TeamId.ToString()); // Set value to TeamId

            if (!string.IsNullOrEmpty(onClickAction))
            {
                buttonTag.MergeAttribute("onclick", onClickAction); // Set custom onClick action
            }

            buttonTag.SetInnerText(team.Name); // Set the team name as the button text

            htmlString.Append(buttonTag.ToString());

            return MvcHtmlString.Create(htmlString.ToString());
        }
    }
}
