using LinkShorteningService.BusinessLogic.DTO;
using LinkShorteningService.BusinessLogic.Interfaces;
using LinkShorteningService.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkShorteningService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly ILinkService _linkService;

        public LinksController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        [Authorize(Policy = "Api")]
        [Authorize(Policy = "User")]
        [HttpPost]
        [Route("AddLink")]
        public IActionResult AddLink([FromBody] LinkDto link)
        {
            _linkService.AddLink(link);

            return Ok();
        }

        [HttpGet]
        [Route("GetGroup")]
        public IActionResult GetGroup(int id)
        {
            var group = _linkService.GetGroup(id);
            return Ok(group);
        }

        [HttpGet]
        [Route("GetLinkUrl")]
        public IActionResult GetLinkUrl(string id)
        {
            var link = _linkService.GetLinkUrl(id);
            if (string.IsNullOrEmpty(link))
            {
                return NotFound();
            }
            _linkService.IncLinkClicks(id);

            return Ok(new { Url = link });
        }

        [Authorize(Policy = "Api")]
        [Authorize(Policy = "User")]
        [HttpPost]
        [Route("AddLinksGroup")]
        public IActionResult AddLinksGroup([FromBody] GroupDto group)
        {
            group.UserEmail = User.GetEmailFromUser();
            _linkService.AddLinksGroup(group);

            return Ok();
        }

        [Authorize(Policy = "Api")]
        [Authorize(Policy = "User")]
        [HttpPost]
        [Route("GetGroups")]
        public IActionResult GetGroups([FromBody] GroupsPageRequestDto request)
        {
            request.Email = User.GetEmailFromUser();
            var groups = _linkService.GetGroups(request);

            return Ok(groups);
        }

        [Authorize(Policy = "Api")]
        [Authorize(Policy = "User")]
        [HttpPost]
        [Route("GetGroupLinks")]
        public IActionResult GetGroupLinks([FromBody] GroupLinksPageRequestDto request)
        {
            var group = _linkService.GetGroup(request.GroupId);
            var links = _linkService.GetGroupLinks(request);

            return Ok(new { Links = links, Group = group });
        }

        [Authorize(Policy = "Api")]
        [Authorize(Policy = "User")]
        [HttpPost]
        [Route("GetUserLinks")]
        public IActionResult GetUserLinks([FromBody] UserLinksPageRequestDto request)
        {
            request.Email = User.GetEmailFromUser();
            var links = _linkService.GetUserLinks(request);

            return Ok(links);
        }
    }
}
