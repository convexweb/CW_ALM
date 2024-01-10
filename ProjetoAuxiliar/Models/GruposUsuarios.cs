using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class GruposUsuarios
{
    public Guid GrupoUID { get; set; }

    public Guid UsuarioUID { get; set; }

    public DateTime Criado { get; set; }

    public virtual Grupos GrupoU { get; set; } = null!;

    public virtual Usuarios UsuarioU { get; set; } = null!;
}
