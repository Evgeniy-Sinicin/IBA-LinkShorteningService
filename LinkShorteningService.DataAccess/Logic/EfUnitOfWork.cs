using LinkShorteningService.DataAccess.Interfaces;
using LinkShorteningService.DataAccess.Models;
using LinkShorteningService.DataAccess.Repositories;
using LinkShorteningService.DataAccess.Repositories.EFRepositories.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LinkShorteningService.DataAccess.Logic
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private LinksContext db;
        private IRepository<Link> linkRepository;
        private IRepository<Group> groupRepository;

        public EfUnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LinksContext>();
            optionsBuilder.UseSqlServer(connectionString);
            db = new LinksContext(optionsBuilder.Options);
        }

        public IRepository<Link> Links
        {
            get
            {
                if (linkRepository == null)
                {
                    linkRepository = new LinkRepository(db);
                }

                return linkRepository;
            }
        }

        public IRepository<Group> Groups
        {
            get
            {
                if (groupRepository == null)
                {
                    groupRepository = new GroupRepository(db);
                }

                return groupRepository;
            }
        }

        //public IRepository<User> Users
        //{
        //    get
        //    {
        //        if (userRepository == null)
        //        {
        //            userRepository = new UserRepository(db);
        //        }

        //        return userRepository;
        //    }
        //}

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
