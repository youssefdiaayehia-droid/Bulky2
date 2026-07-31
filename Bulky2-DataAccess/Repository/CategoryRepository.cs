using Bulky2_DataAccess.Data;
using Bulky2_DataAccess.Repository.IRepository;
using Bulky2_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bulky2_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;


        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Category category)
        {
            _db.categories.Update(category);
        }
    }
}
