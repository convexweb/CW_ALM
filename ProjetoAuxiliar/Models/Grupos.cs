using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class Grupos
{
    public Guid UID { get; set; }

    public string Nome { get; set; } = null!;

    public bool Ativo { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual ICollection<GruposUsuarios> GruposUsuarios { get; set; } = new List<GruposUsuarios>();
}
