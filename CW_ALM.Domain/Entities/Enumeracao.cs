namespace CW_ALM.Domain.Entities
{
    public partial class Enumeracao : Entity
    {
        public string DBField { get; set; } = null!;
        public string DBValue { get; set; } = null!;
        public string NomePTBR { get; set; } = null!;
        public string? NomeENUS { get; set; }
        public string? NomeESCO { get; set; }
        public string? NomeESPE { get; set; }

        public virtual ICollection<CasoDeTeste> LstCasosDeTestes { get; set; } = new List<CasoDeTeste>();
        public virtual ICollection<CasoDeTesteFollowUp> LstCasosDeTestesFollowUpsResultado { get; set; } = new List<CasoDeTesteFollowUp>();
        public virtual ICollection<CasoDeTesteFollowUp> LstCasosDeTestesFollowUpsStatus { get; set; } = new List<CasoDeTesteFollowUp>();
        public virtual ICollection<Teste> LstTestesSituacao { get; set; } = new List<Teste>();
        public virtual ICollection<Teste> LstTestesStatus { get; set; } = new List<Teste>();
    }
}
