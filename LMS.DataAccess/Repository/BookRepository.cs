using LMS.DataAccess.Data;
using LMS.DataAccess.Repository.IRepository;
using LMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DataAccess.Repository
{
    public class BookRepository : Repository<Books>, IBooksRepository
    {
        private ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public void Update(Books obj)
        {
            var objFromDb = _db.Books.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.BookName = obj.BookName;
                objFromDb.AuthorName = obj.AuthorName;
                objFromDb.SerialName = obj.SerialName;
                objFromDb.IsBookAvailable = obj.IsBookAvailable;
            }
        }
    }
}
