using LinkShorteningService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinkShorteningService.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Link> Links { get; }
        IRepository<Group> Groups { get; }
        //IRepository<User> Users { get; }
        void Save();
    }
}
