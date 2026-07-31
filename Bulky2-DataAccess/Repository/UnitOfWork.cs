using Bulky2_DataAccess.Data;
using Bulky2_DataAccess.Repository;
using Bulky2_DataAccess.Repository.IRepository;
using Bulky2_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky2_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Category {  get;private set; }
        public IProductRepository Product { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
