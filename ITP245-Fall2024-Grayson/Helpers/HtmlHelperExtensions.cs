using System.Web.Mvc;
using ITP245_Fall2024_GraysonModel;
using ITP245_Fall2024_Grayson.Helpers;

namespace ITP245_Fall2024_Grayson.Helpers
{
    public static class HtmlHelperExtensions
    {
        // Extension method for HtmlHelper to use TeamButton
        public static MvcHtmlString TeamButton(this HtmlHelper htmlHelper, Team team, string buttonClass = "btn btn-primary", string onClickAction = null)
        {
            return ButtonHelper.TeamButton(team, buttonClass, onClickAction);
        }
    }
}
