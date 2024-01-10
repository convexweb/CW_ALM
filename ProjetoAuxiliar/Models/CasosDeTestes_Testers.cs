using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class CasosDeTestes_Testers
{
    public Guid CasoDeTesteUID { get; set; }

    public Guid TesterUID { get; set; }

    public DateTime Criado { get; set; }

    public virtual CasosDeTestes CasoDeTesteU { get; set; } = null!;

    public virtual Usuarios TesterU { get; set; } = null!;
}
