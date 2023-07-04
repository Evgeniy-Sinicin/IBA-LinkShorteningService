using System;
using System.Collections.Generic;
using System.Text;

namespace LinkShorteningService.BusinessLogic.DTO
{
    public class LinkDto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int ClickCount { get; set; }
        public int GroupId { get; set; }
    }
}
