namespace CW_ALM.Domain.Entities
{
    public partial class GrupoUsuario
    {
        public Guid GrupoUID { get; set; }
        public Guid UsuarioUID { get; set; }
        public DateTime Criado { get; set; }

        public virtual Grupo Grupo { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = null!;

        public GrupoUsuario(Guid grupoUID, Guid usuarioUID)
        {
            GrupoUID = grupoUID;
            UsuarioUID = usuarioUID;
            Criado = DateTime.Now;
        }
    }
}
