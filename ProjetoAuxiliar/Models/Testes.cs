using System;
using System.Collections.Generic;

namespace ProjetoAuxiliar.Models;

public partial class Testes
{
    public Guid UID { get; set; }

    public Guid SituacaoUID { get; set; }

    public Guid StatusUID { get; set; }

    public string NomePTBR { get; set; } = null!;

    public string? NomeENUS { get; set; }

    public string? NomeESCO { get; set; }

    public string? NomeESPE { get; set; }

    public string URLSistema { get; set; } = null!;

    public DateOnly DataInicio { get; set; }

    public DateOnly DataTermino { get; set; }

    public DateTime Criado { get; set; }

    public DateTime? Alterado { get; set; }

    public virtual ICollection<CasosDeTestes> CasosDeTestes { get; set; } = new List<CasosDeTestes>();

    public virtual Enumeracoes SituacaoU { get; set; } = null!;

    public virtual Enumeracoes StatusU { get; set; } = null!;
}
