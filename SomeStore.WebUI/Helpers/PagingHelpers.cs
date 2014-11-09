using SomeStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SomeStore.WebUI.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int, string> pageUrl)
        {
            if (pagingInfo.TotalPages == 1)
            {
                return MvcHtmlString.Empty;
            }
            StringBuilder res = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; ++i)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                }
                res.Append(tag.ToString());
            }
            return MvcHtmlString.Create(res.ToString());
        }
    }
}