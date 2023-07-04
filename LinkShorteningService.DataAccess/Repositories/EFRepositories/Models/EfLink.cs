namespace LinkShorteningService.DataAccess.Repositories.EFRepositories.Models
{
    public class EfLink
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int ClickCount { get; set; }

        public int GroupId { get; set; }
        public EfGroup Group { get; set; }
    }
}
