namespace Pastebin.Domain.DbEntities
{
	public class Paste
	{
		public long Id { get; set; }
        public Guid UserId { get; set; }

        public string ContentPath { get; set; } = string.Empty;
        public string? UrlHash { get; set; }

        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }

        public virtual User User { get; set; } = null!;
    }
}