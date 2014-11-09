using SomeStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomeStore.Models.Entities;
namespace SomeStore.Models.Concrete
{
    public class EFProductRepository:IProductRepository
    {
        private EFDBContext context = new EFDBContext();
        public IQueryable<Product> Products
        {
            get { return context.Products; }
        }

    }
}
