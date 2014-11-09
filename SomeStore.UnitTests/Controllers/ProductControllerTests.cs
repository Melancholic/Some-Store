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
                    new Product{ Name="product1",Price=25,Category="cat1"},
                    new Product{ Name="product2",Price=50, Category="cat1"}, 
                    new Product{ Name="product3",Price=125, Category="cat1"},
                    new Product{ Name="product4",Price=150, Category="cat1"},
                    new Product{ Name="product5",Price=185, Category="cat2"},
                    new Product{ Name="product6",Price=250,Category="cat3"},

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
            ProductsListViewModel result= (ProductsListViewModel)controller.List(null,2).Model;
            //Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.IsTrue(pageInfo.CurrentPage == 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems,6 );
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Filter_Categories_For_Products()
        {
            //Init
            ProductController controller = new ProductController(mock.Object);
            //Action
            Product[] result = ((ProductsListViewModel)controller.List("cat1").Model).Products.ToArray();
            //Assert
            Assert.IsTrue(result.Length == 3);
            Assert.IsTrue(result[1].Name == "product2" && result[1].Category == "cat1");
            Assert.IsTrue(result[2].Name == "product3" && result[2].Category == "cat1");
        }

        [TestMethod]
        public void Create_Categories()
        {
            //Init
            NavigationController target = new NavigationController(mock.Object);
            //Action
            String[] result = ((IEnumerable<string>)target.Menu().Model).ToArray();
            //Assert
            Assert.IsTrue(result.Length == 3);
            Assert.IsTrue(result[0] == "cat1");
            Assert.IsTrue(result[1] == "cat2");
            Assert.IsTrue(result[2] == "cat3");
        }
        [TestMethod]
        public void Indicate_Current_Category()
        {
            //Init
            NavigationController target = new NavigationController(mock.Object);
            string current = "cat1";
            //Action
            string result = target.Menu(current).ViewBag.CurrentCat;
            //Assert
            Assert.AreEqual(current, result);
        }

        [TestMethod]
        public void Test_Correct_Pagination_With_Categories()
        {
            //Init
            ProductController target = new ProductController(mock.Object);
            //Action
            int res1 = ((ProductsListViewModel)target.List("cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)target.List("cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)target.List("cat3").Model).PagingInfo.TotalItems;
            int res4 = ((ProductsListViewModel)target.List("cat0").Model).PagingInfo.TotalItems;
            int res5 = ((ProductsListViewModel)target.List(null).Model).PagingInfo.TotalItems;
            //Assert
            Assert.AreEqual(4,res1);
            Assert.AreEqual(1,res2);
            Assert.AreEqual(1,res3);
            Assert.AreEqual(0,res4);
            Assert.AreEqual(6,res5);
        }
    }
}
