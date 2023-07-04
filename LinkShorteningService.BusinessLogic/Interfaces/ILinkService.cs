

using LinkShorteningService.BusinessLogic.DTO;
using System.Collections.Generic;

namespace LinkShorteningService.BusinessLogic.Interfaces
{
	public interface ILinkService
	{
		void AddLinksGroup(GroupDto group);
		GroupDto GetGroup(int id);
		void AddLink(LinkDto link);
		void IncLinkClicks(string id);
		string GetLinkUrl(string id);
		IEnumerable<GroupDto> GetGroups(GroupsPageRequestDto request);
		IEnumerable<LinkDto> GetGroupLinks(GroupLinksPageRequestDto request);
		IEnumerable<LinkDto> GetUserLinks(UserLinksPageRequestDto request);
	}
}
