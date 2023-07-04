using AutoMapper;
using LinkShorteningService.DataAccess.Interfaces;
using LinkShorteningService.DataAccess.Models;
using LinkShorteningService.DataAccess.Repositories.EFRepositories.Models;
using System.Linq;

namespace LinkShorteningService.DataAccess.Repositories
{
    public class LinkRepository : IRepository<Link>
    {
        protected readonly LinksContext _db;
        protected readonly IMapper _mapper;

        public LinkRepository(LinksContext db)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Link, EfLink>();
                cfg.CreateMap<EfLink, Link>();
            });
            _mapper = config.CreateMapper();
            _db = db;
        }

        public void Create(Link item)
        {
            _db.Links.Add(_mapper.Map<EfLink>(item));
        }

        public  void Delete(object id)
        {
            var link = _db.Links.Find(id);

            if (link != null)
            {
                _db.Links.Remove(link);
            }
        }

        public void DeleteAll()
        {

            if(!_db.Links.Any())
            {
                return;
            }

            _db.Links.RemoveRange(_db.Links);
        }

        public Link Get(object id)
        {
            return _mapper.Map<Link>(_db.Links.Find(id));
        }

        public  IQueryable<Link> GetAll()
        {
            return _db.Links.Select(link => new Link 
                { 
                    ClickCount = link.ClickCount,
                    GroupId = link.GroupId,
                    Id = link.Id,
                    Url = link.Url,
                    Name = link.Name 
                }
            );
        }

        public void Update(Link item)
        {
            var link = _db.Links.FirstOrDefault(us => us.Id == item.Id);
            link.Url = item.Url;
            link.ClickCount = item.ClickCount;
            link.Name = item.Name;
            link.GroupId = item.GroupId;
            _db.Links.Update(link);
        }
    }
}
