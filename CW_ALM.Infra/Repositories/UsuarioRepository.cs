using CW_ALM.Domain.Core;
using CW_ALM.Domain.Entities;
using CW_ALM.Domain.Repositories;
using CW_ALM.Infra.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CW_ALM.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CW_ALMContext _context;

        public UsuarioRepository(CW_ALMContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Usuario model)
        {
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<List<Usuario>> GetAllByStatusAsync(bool status)
        {
            return await _context.Usuarios.Where(a => a.Status.Equals(status)).ToListAsync();
        }

        public Usuario GetByUID(Guid uID)
        {
            return _context.Usuarios.FirstOrDefault(a => a.UID.Equals(uID));
        }

        public async Task<Usuario> GetByUIDAsync(Guid uID)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(a => a.UID.Equals(uID));
        }

        public Usuario GetByUIDComLstGrupos(Guid uID)
        {
            return _context.Usuarios.Include(a => a.LstGruposUsuarios).FirstOrDefault(a => a.UID.Equals(uID));
        }

        public async Task<Usuario> GetUsuarioByEmailSenhaAsync(string email, string senha)
        {
            string senhaMD5 = SecurityCipherStrings.ConvertString2MD5(senha).Trim().ToLower();
            return await _context
                .Usuarios
                .Include(a => a.LstGruposUsuarios)
                    .ThenInclude(b => b.Grupo)
                .FirstOrDefaultAsync(a => a.Email.Trim().ToLower().Equals(email.Trim().ToLower()) && a.Senha.Trim().ToLower().Equals(senhaMD5));
        }

        public async Task UpdateAsync(Usuario model)
        {
            _context.Usuarios.Update(model);
            await _context.SaveChangesAsync(cancellationToken: CancellationToken.None);
        }
    }
}
