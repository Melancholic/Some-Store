using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SomeStore.Models.Abstract;
using SomeStore.Models.Entities;
using SomeStore.Models.Concrete;
using Moq;
using System.Web.Routing;
using SomeStore.WebUI.Models;

namespace SomeStore.WebUI.Controllers.Tests
{
    [TestClass()]
    public class ProductControllerTests
    {
        private Mock<IProductRepository> mock;
        [TestInitialize]
        public void init()
        {
             mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                    new Product{ Name="product1",Price=25},
                    new Product{ Name="product2",Price=50},
                    new Product{ Name="product3",Price=125},
                    new Product{ Name="product4",Price=150},
                    new Product{ Name="product5",Price=185},
                    new Product{ Name="product6",Price=250},

            }.AsQueryable);
        }

        [TestCleanup]
        public void free()
        {
            mock = null;
        }

        [TestMethod]
        public void ListTest()
        {
            //Init

            ProductController controller = new ProductController(mock.Object);
            //Action
            ProductsListViewModel result= (ProductsListViewModel)controller.List(2).Model;
            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.IsTrue(pageInfo.CurrentPage == 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems,6 );
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

    }
}
