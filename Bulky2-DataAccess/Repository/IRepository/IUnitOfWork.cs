using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky2_DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
       ICategoryRepository Category { get; }
       IProductRepository Product { get; }
        void Save();
    }
}
