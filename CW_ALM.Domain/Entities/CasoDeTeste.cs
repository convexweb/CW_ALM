namespace CW_ALM.Domain.Entities
{
    public partial class CasoDeTeste : Entity
    {
        public Guid TesteUID { get; set; }
        public Guid SituacaoUID { get; set; }
        public string DescricaoPTBR { get; set; } = null!;
        public string? DescricaoENUS { get; set; }
        public string? DescricaoESCO { get; set; }
        public string? DescricaoESPE { get; set; }
        public DateOnly DataInicio { get; set; }
        public DateOnly DataTermino { get; set; }

        public virtual Enumeracao Situacao { get; set; } = null!;
        public virtual Teste Teste { get; set; } = null!;
        public virtual ICollection<CasoDeTesteFollowUp> LstCasosDeTestesFollowUps { get; set; } = new List<CasoDeTesteFollowUp>();
        public virtual ICollection<CasoDeTeste_Tester> LstCasosDeTestes_Testers { get; set; } = new List<CasoDeTeste_Tester>();
    }
}
