namespace CW_ALM.Domain.Entities
{
    public partial class Grupo : Entity
    {
        public string Nome { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<GrupoUsuario> LstGruposUsuarios { get; set; } = new List<GrupoUsuario>();

        public Grupo(string nome)
        {
            Nome = nome;
            Status = true;
            LstGruposUsuarios = new List<GrupoUsuario>();
        }

        public void Update(string nome)
        {
            Nome = nome;
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
