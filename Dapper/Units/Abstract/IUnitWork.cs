using Dapper.DAL.Repositories.Abstract;
using Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Units.Abstract
{
    public interface IUnitWork : IDisposable
    {
        public IRepository<Category> CategoryRepository { get; }
        public IRepository<Product> ProductRepository { get; }
        int Complete();
    }
}
