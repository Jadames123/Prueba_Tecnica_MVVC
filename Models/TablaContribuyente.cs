using System;
using System.Collections.Generic;

namespace MVC_Prueba_Tecnica.Models;

public partial class TablaContribuyente
{
    public int IdContribuyente { get; set; }

    public string RncCedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Tipo { get; set; }

    public string? Estatus { get; set; }

    public virtual ICollection<TablaComprobantesFiscale> TablaComprobantesFiscales { get; set; } = new List<TablaComprobantesFiscale>();
}
