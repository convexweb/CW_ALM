namespace CW_ALM.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            UID = Guid.Empty;
            Criado = DateTime.Now;
            Alterado = null;
        }

        public Guid UID { get; set; }
        public DateTime Criado { get; set; }
        public DateTime? Alterado { get; set; }
    }
}
