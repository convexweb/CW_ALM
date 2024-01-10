using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class CasosDeTestes
{
    public Guid UID { get; set; }

    public Guid TesteUID { get; set; }

    public Guid SituacaoUID { get; set; }

    public string DescricaoPTBR { get; set; } = null!;

    public string? DescricaoENUS { get; set; }

    public string? DescricaoESCO { get; set; }

    public string? DescricaoESPE { get; set; }

    public DateOnly DataInicio { get; set; }

    public DateOnly DataTermino { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual ICollection<CasosDeTestesFollowUPs> CasosDeTestesFollowUPs { get; set; } = new List<CasosDeTestesFollowUPs>();

    public virtual ICollection<CasosDeTestes_Testers> CasosDeTestes_Testers { get; set; } = new List<CasosDeTestes_Testers>();

    public virtual Enumeracoes SituacaoU { get; set; } = null!;

    public virtual Testes TesteU { get; set; } = null!;
}
