using SomeStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeStore.Models.Concrete
{
    public class EFDBContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
