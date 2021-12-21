using Dapper.DAL.Conxtext;
using Dapper.DAL.Repositories.Abstract;
using Dapper.DAL.Repositories.Concrete;
using Dapper.Entities;
using Dapper.Units.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Units.Concrete
{
    public class UnitWork : IUnitWork
    {
        private ProductContext _productContext;
        public IRepository<Category> CategoryRepository { get; private set; }
        public IRepository<Product> ProductRepository { get; private set; }

        public UnitWork(ProductContext productContext)
        {
            _productContext = productContext;
            CategoryRepository = new Repository<Category>(productContext);
            ProductRepository = new Repository<Product>(productContext);
        }


        public int Complete()
        {
            return _productContext.SaveChanges();
        }

        public void Dispose()
        {
            _productContext.Dispose();
        }
    }
}
