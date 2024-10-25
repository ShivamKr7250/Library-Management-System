using LMS.DataAccess.Data;
using LMS.DataAccess.Repository.IRepository;

namespace LMS.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IBooksRepository Books { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Books = new BookRepository(_db);
            Category = new CategoryRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
