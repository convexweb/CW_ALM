namespace CW_ALM.Domain.Entities
{
    public partial class CasoDeTesteFollowUpImage : Entity
    {
        public Guid CasoDeTesteFollowUPUID { get; set; }
        public string? ImgPath { get; set; }

        public virtual CasoDeTesteFollowUp CasoDeTesteFollowUp { get; set; } = null!;
    }
}
