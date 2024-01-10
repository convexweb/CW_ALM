using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using CW_ALM.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CW_ALM.Infra.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly CW_ALMContext _context;

        public GrupoRepository(CW_ALMContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Grupo model)
        {
            _context.Grupos.Add(model);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }

        public async Task<List<Grupo>> GetAllAsync()
        {
            return await _context.Grupos.ToListAsync();
        }

        public async Task<List<Grupo>> GetAllByStatusAsync(bool status)
        {
            return await _context.Grupos.Where(a => a.Status.Equals(status)).ToListAsync();
        }

        public Grupo GetByUID(Guid uID)
        {
            return _context.Grupos.FirstOrDefault(a => a.UID.Equals(uID));
        }

        public async Task<Grupo> GetByUIDAsync(Guid uID)
        {
            return await _context.Grupos.FirstOrDefaultAsync(a => a.UID.Equals(uID));
        }

        public Grupo GetByUIDComLstUsuarios(Guid uID)
        {
            return _context.Grupos.Include(a => a.LstGruposUsuarios).FirstOrDefault(a => a.UID.Equals(uID));
        }

        public async Task UpdateAsync(Grupo model)
        {
            _context.Grupos.Update(model);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }
    }
}
