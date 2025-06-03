using Pastebin.Domain.DbEntities;

namespace Pastebin.Domain.Interfaces.Repository
{
	public interface IPasteRepository
	{
		/// <summary>
		/// Получение поста по его id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<Paste?> GetByIdAsync(long id);

		/// <summary>
		/// Сохранение нового поста.
		/// </summary>
		/// <param name="paste"></param>
		/// <returns></returns>
		public Task<long> AddAsync(Paste paste);

		/// <summary>
		/// Обновление поста.
		/// </summary>
		/// <param name="paste"></param>
		/// <returns></returns>
		public Task<bool> UpdateAsync(Paste paste);

		/// <summary>
		/// Удаление поста.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Task<bool> DeleteAsync(long id);
	}
}
