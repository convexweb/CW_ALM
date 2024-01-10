using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class CasosDeTestesFollowUPsImages
{
    public Guid UID { get; set; }

    public Guid CasoDeTesteFollowUPUID { get; set; }

    public string? ImgPath { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual CasosDeTestesFollowUPs CasoDeTesteFollowUPU { get; set; } = null!;
}
