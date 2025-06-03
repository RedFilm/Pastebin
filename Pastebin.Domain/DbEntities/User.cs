namespace Pastebin.Domain.DbEntities
{
	public class User
	{
        public Guid Id { get; set; }
        public string? UserName { get; set; }

        public ICollection<Paste>? Pastes { get; set; }
    }
}
