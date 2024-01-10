using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class Usuarios
{
    public Guid UID { get; set; }

    public string UsuarioAD { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public bool Ativo { get; set; }

    public bool SenhaAtualizada { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual ICollection<CasosDeTestesFollowUPs> CasosDeTestesFollowUPs { get; set; } = new List<CasosDeTestesFollowUPs>();

    public virtual ICollection<CasosDeTestes_Testers> CasosDeTestes_Testers { get; set; } = new List<CasosDeTestes_Testers>();

    public virtual ICollection<GruposUsuarios> GruposUsuarios { get; set; } = new List<GruposUsuarios>();
}
