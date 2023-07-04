using System;
using System.Collections.Generic;
using System.Text;

namespace LinkShorteningService.BusinessLogic.DTO
{
    public class GroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string UserEmail { get; set; }
        public int LinksCount { get; set; }
        public int RedirectCount { get; set; }
    }
}
