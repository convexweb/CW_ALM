using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using CW_ALM.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CW_ALM.Infra.Repositories
{
    public class GrupoUsuarioRepository : IGrupoUsuarioRepository
    {
        private readonly CW_ALMContext _context;

        public GrupoUsuarioRepository(CW_ALMContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(GrupoUsuario model)
        {
            _context.GruposUsuarios.Add(model);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }

        public async Task DeleteAsync(GrupoUsuario model)
        {
            _context.GruposUsuarios.Remove(model);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }

        public List<GrupoUsuario> GetByGrupoUID(Guid grupoUID)
        {
            return _context.GruposUsuarios.Include(a => a.Usuario).Where(a => a.GrupoUID.Equals(grupoUID)).ToList();
        }

        public List<GrupoUsuario> GetByUsuarioUID(Guid usuarioUID)
        {
            return _context.GruposUsuarios.Where(a => a.UsuarioUID.Equals(usuarioUID)).ToList();
        }
    }
}
