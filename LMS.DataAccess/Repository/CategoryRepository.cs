﻿using LMS.DataAccess.Data;
using LMS.DataAccess.Repository.IRepository;
using LMS.Models;

namespace LMS.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
           _db.Categories.Update(obj);
        }
    }
}
