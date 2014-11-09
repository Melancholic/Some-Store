using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SomeStore.Models.Abstract;
using SomeStore.Models.Concrete;
using Moq;

namespace SomeStore.WebUI.Infrastructure
{
    public class NinjectControllerFactory:DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext reqContext,  Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }
            else
            {
                return (IController)ninjectKernel.Get(controllerType);
            }
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            /*Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>{
                    new Product{ Name="product1",Price=25},
                    new Product{ Name="product2",Price=50},
                    new Product{ Name="product3",Price=125},
                    new Product{ Name="product4",Price=150},
                    new Product{ Name="product5",Price=185},
                    new Product{ Name="product6",Price=250},

            }.AsQueryable);
            ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);
        */
       }
    }
}