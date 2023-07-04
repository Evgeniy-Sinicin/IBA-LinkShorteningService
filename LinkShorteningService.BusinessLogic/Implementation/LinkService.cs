using AutoMapper;
using LinkShorteningService.BusinessLogic.DTO;
using LinkShorteningService.BusinessLogic.Interfaces;
using LinkShorteningService.DataAccess.Interfaces;
using LinkShorteningService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkShorteningService.BusinessLogic.Implementation
{
    public class LinkService : ILinkService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unit;

        public LinkService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Group, GroupDto>();
                cfg.CreateMap<GroupDto, Group>();
                cfg.CreateMap<Link, LinkDto>();
                cfg.CreateMap<LinkDto, Link>();
            });
            _mapper = config.CreateMapper();
        }

        public void AddLink(LinkDto link)
        {
            do
            {
                link.Id = GenerateId();
            } while (_unit.Links.Get(link.Id) != null) ;

            _unit.Links.Create(_mapper.Map<Link>(link));
            _unit.Save();
        }

        private string GenerateId()
        {
            string id = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            return id;
        }

        public void AddLinksGroup(GroupDto group)
        {
            _unit.Groups.Create(_mapper.Map<Group>(group));
            _unit.Save();
        }

        public GroupDto GetGroup(int id)
        {
            var group = _unit.Groups.GetAll()
                .Where(group => group.Id == id)
                .Select(group => new GroupDto
                    {
                        Id = group.Id,
                        Name = group.Name,
                        LinksCount = _unit.Links
                                .GetAll()
                                .Count(link => link.GroupId == group.Id),
                        RedirectCount = _unit.Links
                                .GetAll()
                                .Where(link => link.GroupId == group.Id)
                                .Sum(link => link.ClickCount),
                    }
                )
                .FirstOrDefault();
            return group;
        }

        public IEnumerable<LinkDto> GetGroupLinks(GroupLinksPageRequestDto request)
        {
            var result = _unit.Links.GetAll()
                .Where(link => link.GroupId == request.GroupId)
                .Skip(request.Skip)
                .Take(request.Size)
                .ToList()
                .Select(link => _mapper.Map<LinkDto>(link));
            return result;
        }

        public IEnumerable<GroupDto> GetGroups(GroupsPageRequestDto request)
        {
            var result = _unit.Groups.GetAll()
                .Where(group => group.UserEmail.Equals(request.Email))
                .Skip(request.Skip)
                .Take(request.Size)
                .Select(group => new GroupDto
                    {
                        Id = group.Id,
                        Name = group.Name,
                        LinksCount = _unit.Links
                            .GetAll()
                            .Count(link => link.GroupId == group.Id),
                        RedirectCount = _unit.Links
                            .GetAll()
                            .Where(link => link.GroupId == group.Id)
                            .Sum(link => link.ClickCount),
                    }
                );
            return result;
        }

        public string GetLinkUrl(string id)
        {
            return _unit.Links.Get(id)?.Url;
        }

        public void IncLinkClicks(string id)
        {
            Link link = _unit.Links.Get(id);
            link.ClickCount++;
            _unit.Links.Update(link);
            _unit.Save();
        }

        public IEnumerable<LinkDto> GetUserLinks(UserLinksPageRequestDto request)
        {
            IQueryable<int> groupsIds = _unit.Groups
                .GetAll()
                .Where(group => group.UserEmail == request.Email)
                .Select(group => group.Id);
            var result = _unit.Links
            .GetAll()
            .Where(link => groupsIds.Contains(link.GroupId))
            .Skip(request.Skip)
            .Take(request.Size)
            .ToList().Select(link => _mapper.Map<LinkDto>(link));

            return result;
        }
    }
}
