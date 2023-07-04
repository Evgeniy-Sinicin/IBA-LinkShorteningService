using AutoMapper;
using LinkShorteningService.DataAccess.Interfaces;
using LinkShorteningService.DataAccess.Models;
using LinkShorteningService.DataAccess.Repositories.EFRepositories.Models;
using System.Linq;

namespace LinkShorteningService.DataAccess.Repositories
{
    public class GroupRepository : IRepository<Group>
    {
        protected readonly LinksContext _db;
        protected readonly IMapper _mapper;

        public GroupRepository(LinksContext db)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Group, EfGroup>();
                cfg.CreateMap<EfGroup, Group>();
            });
            _mapper = config.CreateMapper();
            _db = db;
        }

        public void Create(Group item)
        {
            _db.Groups.Add(_mapper.Map<EfGroup>(item));
        }

        public void Delete(object id)
        {
            var group = _db.Groups.Find(id);
            if (group == null)
            {
                return;
            }

            _db.Groups.Remove(group);
        }

        public void DeleteAll()
        {
            _db.Groups.RemoveRange(_db.Groups);
        }

        public Group Get(object id)
        {
            return _mapper.Map<Group>(_db.Groups.Find(id));
        }

        public IQueryable<Group> GetAll()
        {
            return _db.Groups.Select(group => new Group { Id = group.Id, Name = group.Name, UserEmail = group.UserEmail });
        }

        public void Update(Group item)
        {
            var group = _db.Groups.FirstOrDefault(us => us.Id == item.Id);
            group.Name = item.Name;
            group.UserEmail = item.UserEmail;
            _db.Groups.Update(group);
        }
    }
}
