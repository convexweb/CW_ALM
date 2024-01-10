using CW_ALM.Domain.Core;

namespace CW_ALM.Domain.Entities
{
    public partial class Usuario : Entity
    {
        public string UsuarioAD { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public bool Status { get; set; }
        public bool SenhaAtualizada { get; set; }

        public virtual ICollection<CasoDeTesteFollowUp> LstCasosDeTestesFollowUps { get; set; } = new List<CasoDeTesteFollowUp>();
        public virtual ICollection<CasoDeTeste_Tester> LstCasosDeTestes_Testers { get; set; } = new List<CasoDeTeste_Tester>();
        public virtual ICollection<GrupoUsuario> LstGruposUsuarios { get; set; } = new List<GrupoUsuario>();

        public Usuario()
        {
        }

        public Usuario(string usuarioAD, string nome, string email, string senha)
        {
            UsuarioAD = usuarioAD;
            Nome = nome;
            Email = email;
            Senha = senha;
            Status = true;
            SenhaAtualizada = false;
            LstGruposUsuarios = new List<GrupoUsuario>();
        }

        public void Create(string usuarioAD, string nome, string email, string senha)
        {
            UsuarioAD = usuarioAD;
            Nome = nome;
            Email = email;
            Senha = SecurityCipherStrings.ConvertString2MD5(senha);
            Status = true;
            SenhaAtualizada = false;
            LstGruposUsuarios = new List<GrupoUsuario>();
        }

        public void Update(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            if(senha.Trim().ToLower().Length > 0)
            {
                Senha = SecurityCipherStrings.ConvertString2MD5(senha);
                SenhaAtualizada = false;
            }
        }

        public void UpdateSenha(string senha)
        {
            Senha = SecurityCipherStrings.ConvertString2MD5(senha);
            SenhaAtualizada = true;
        }

        public void Inactivate()
        {
            Status = false;
        }
        public void Reactivate()
        {
            Status = true;
        }
    }
}
