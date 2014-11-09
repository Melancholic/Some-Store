using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeStore.WebUI.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using SomeStore.WebUI.Models;
namespace SomeStore.WebUI.Helpers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        [TestMethod()]
        public void PageLinksTest()
        {
            //init
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 3,
                TotalItems = 23,
                ItemsPerPage = 5
            };

            Func<int, string> urlDelegate = i => ("Page" + i);
            //action
            MvcHtmlString response= myHelper.PageLinks(pagingInfo,urlDelegate);
            //Assert
            Assert.AreEqual(response.ToString(), @"<a href=""Page1"">1</a><a href=""Page2"">2</a><a class=""selected"" href=""Page3"">3</a><a href=""Page4"">4</a><a href=""Page5"">5</a>");
        }
    }
}
