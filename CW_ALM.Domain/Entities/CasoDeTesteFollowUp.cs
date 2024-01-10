namespace CW_ALM.Domain.Entities
{
    public partial class CasoDeTesteFollowUp : Entity
    {
        public Guid CasoDeTesteUID { get; set; }
        public Guid TesterUID { get; set; }
        public Guid StatusUID { get; set; }
        public Guid ResultadoUID { get; set; }
        public string? Feedback { get; set; }
        
        public virtual CasoDeTeste CasoDeTeste { get; set; } = null!;
        public virtual Enumeracao Resultado { get; set; } = null!;
        public virtual Enumeracao Status { get; set; } = null!;
        public virtual Usuario Tester { get; set; } = null!;
        public virtual ICollection<CasoDeTesteFollowUpImage> LstCasosDeTestesFollowUpsImages { get; set; } = new List<CasoDeTesteFollowUpImage>();
    }
}
