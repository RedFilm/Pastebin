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
		/// Получение поста по его url hash.
		/// </summary>
		/// <param name="urlHash"></param>
		/// <returns></returns>
		public Task<Paste?> GetByUrlHashAsync(string urlHash);

		/// <summary>
		/// Создание поста.
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


		/// <summary>
		/// Проверка на наличие поста с указанным url hash.
		/// </summary>
		/// <param name="urlHash"></param>
		/// <returns></returns>
		public Task<bool> AnyAsync(string urlHash);
	}
}
