using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class Enumeracoes
{
    public Guid UID { get; set; }

    public string DBField { get; set; } = null!;

    public string DBValue { get; set; } = null!;

    public string NomePTBR { get; set; } = null!;

    public string? NomeENUS { get; set; }

    public string? NomeESCO { get; set; }

    public string? NomeESPE { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual ICollection<CasosDeTestes> CasosDeTestes { get; set; } = new List<CasosDeTestes>();

    public virtual ICollection<CasosDeTestesFollowUPs> CasosDeTestesFollowUPsResultadoU { get; set; } = new List<CasosDeTestesFollowUPs>();

    public virtual ICollection<CasosDeTestesFollowUPs> CasosDeTestesFollowUPsStatusU { get; set; } = new List<CasosDeTestesFollowUPs>();

    public virtual ICollection<Testes> TestesSituacaoU { get; set; } = new List<Testes>();

    public virtual ICollection<Testes> TestesStatusU { get; set; } = new List<Testes>();
}
