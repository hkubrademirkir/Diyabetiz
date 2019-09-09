using Diyabetiz.DAL.DbContexts;
using Diyabetiz.DAL.Repository.Concrete;
using Diyabetiz.Entities.Entities;
using System;

namespace Diyabetiz.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DiyabetizDbContext _context;
        public UnitOfWork()
        {
            this._context = new DiyabetizDbContext();
        }
        
        private GenericRepository<News, int> _newsRepository;
        private GenericRepository<Food, int> _foodRepository;
        private GenericRepository<Event, int> _eventRepository;
        private GenericRepository<CarbohydrateCounting, int> _carbohydrateCountingRepository;
        private GenericRepository<DiabetesEducation, int> _diabetesEducationRepository;
       
        private GenericRepository<ContactUs, int> _contactUsRepository;
      

        private bool _disposed = false;

        public GenericRepository<News, int> NewsRepository
        {
            get
            {
                if (_newsRepository == null)
                {
                    _newsRepository = new GenericRepository<News, int>(_context);
                }
                return _newsRepository;
            }

        }
        public GenericRepository<Food, int> FoodRepository
        {
            get
            {
                if (_foodRepository == null)
                {
                    _foodRepository = new GenericRepository<Food, int>(_context);
                }
                return _foodRepository;
            }

        }
        public GenericRepository<Event, int> EventRepository
        {
            get
            {
                if (_eventRepository == null)
                {
                    _eventRepository = new GenericRepository<Event, int>(_context);
                }
                return _eventRepository;
            }

        }
        public GenericRepository<CarbohydrateCounting, int> CarbohydrateCountingRepository
        {
            get
            {
                if (_carbohydrateCountingRepository == null)
                {
                    _carbohydrateCountingRepository = new GenericRepository<CarbohydrateCounting, int>(_context);
                }
                return _carbohydrateCountingRepository;
            }

        }
        public GenericRepository<DiabetesEducation, int> DiabetesEducationRepository
        {
            get
            {
                if (_diabetesEducationRepository == null)
                {
                    _diabetesEducationRepository = new GenericRepository<DiabetesEducation, int>(_context);
                }
                return _diabetesEducationRepository;
            }

        }
       
        public GenericRepository<ContactUs, int> ContactUsRepository
        {
            get
            {
                if (_contactUsRepository == null)
                {
                    _contactUsRepository = new GenericRepository<ContactUs, int>(_context);
                }
                return _contactUsRepository;
            }

        }
   

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

            }
            catch (Exception) { }
        }
    }
}

