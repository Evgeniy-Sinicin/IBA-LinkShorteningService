using LinkShorteningService.DataAccess.Models;
using System.Collections.Generic;

namespace LinkShorteningService.DataAccess.Repositories.EFRepositories.Models
{
    public class EfGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string UserEmail { get; set; }
        public IEnumerable<EfLink> Links { get; set; }
    }
}
