using Bulky2_DataAccess.Data;
using Bulky2_DataAccess.Repository.IRepository;
using Bulky2_Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Bulky2_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;


        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product produect)
        {
            _db.SaveChanges();
        }
    }
}
