using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class CasosDeTestesFollowUPs
{
    public Guid UID { get; set; }

    public Guid CasoDeTesteUID { get; set; }

    public Guid TesterUID { get; set; }

    public Guid StatusUID { get; set; }

    public Guid ResultadoUID { get; set; }

    public string? Feedback { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual CasosDeTestes CasoDeTesteU { get; set; } = null!;

    public virtual ICollection<CasosDeTestesFollowUPsImages> CasosDeTestesFollowUPsImages { get; set; } = new List<CasosDeTestesFollowUPsImages>();

    public virtual Enumeracoes ResultadoU { get; set; } = null!;

    public virtual Enumeracoes StatusU { get; set; } = null!;

    public virtual Usuarios TesterU { get; set; } = null!;
}
