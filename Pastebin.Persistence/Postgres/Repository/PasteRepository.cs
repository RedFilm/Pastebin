﻿using Microsoft.EntityFrameworkCore;
using Pastebin.Domain.DbEntities;
using Pastebin.Domain.Interfaces.Repository;
using Pastebin.Persistence.Postgres;

namespace Pastebin.Persistence.Postgress.Repository
{
    public class PasteRepository : IPasteRepository
    {
        private readonly PastebinDbContext _context;

        public PasteRepository(PastebinDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddAsync(Paste paste)
        {
            await _context.AddAsync(paste);
            await _context.SaveChangesAsync();

            return paste.Id;
        }

        public async Task<Paste?> GetByIdAsync(long id)
        {
            return await _context.Pastes.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var paste = await _context.Pastes.FindAsync(id);

            if (paste is not null)
                _context.Pastes.Remove(paste);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Paste paste)
        {
            _context.Pastes.Update(paste);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Paste?> GetByUrlHashAsync(string urlHash)
        {
            return await _context.Pastes.FirstOrDefaultAsync(p => p.UrlHash == urlHash);
        }

        public async Task<bool> AnyAsync(string urlHash) =>
            await _context.Pastes.AnyAsync(p => p.UrlHash == urlHash);
    }
}
