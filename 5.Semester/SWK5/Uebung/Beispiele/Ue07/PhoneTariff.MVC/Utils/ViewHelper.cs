using System.Collections.Generic;
using System.Web.Mvc;

namespace PhoneTariff.MVC.Utils
{
    public class ViewHelper
    {
        public static IEnumerable<SelectListItem> GetPercentList (int increment, int selected)
        {
            IList<SelectListItem> list = new List<SelectListItem>();

            for (int i = 0; i <= 100; i += increment)
            {
                list.Add(new SelectListItem() {
                       Value = i.ToString(),
                       Text = i.ToString() + " %",
                       Selected = i == selected
                });
            }

            return list;
        }
    }
}