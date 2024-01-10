namespace CW_ALM.Domain.Entities
{
    public partial class Teste : Entity
    {
        public Guid SituacaoUID { get; set; }
        public Guid StatusUID { get; set; }
        public string NomePTBR { get; set; } = null!;
        public string? NomeENUS { get; set; }
        public string? NomeESCO { get; set; }
        public string? NomeESPE { get; set; }
        public string URLSistema { get; set; } = null!;
        public DateOnly DataInicio { get; set; }
        public DateOnly DataTermino { get; set; }

        public virtual Enumeracao Situacao { get; set; } = null!;
        public virtual Enumeracao Status { get; set; } = null!;
        public virtual ICollection<CasoDeTeste> LstCasosDeTestes { get; set; } = new List<CasoDeTeste>();
    }
}
