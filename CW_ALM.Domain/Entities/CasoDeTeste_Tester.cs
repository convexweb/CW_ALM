namespace CW_ALM.Domain.Entities
{
    public partial class CasoDeTeste_Tester
    {
        public Guid CasoDeTesteUID { get; set; }
        public Guid TesterUID { get; set; }
        public DateTime Criado { get; set; }

        public virtual CasoDeTeste CasoDeTeste { get; set; } = null!;
        public virtual Usuario Tester { get; set; } = null!;
    }
}
