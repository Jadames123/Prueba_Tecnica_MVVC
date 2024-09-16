using System;
using System.Collections.Generic;

namespace MVC_Prueba_Tecnica.Models;

public partial class TablaComprobantesFiscale
{
    public int IdComprobante { get; set; }

    public string? RncCedula { get; set; }

    public string? NCF { get; set; }

    public decimal? Monto { get; set; }

    public decimal? Itbis18 { get; set; }

    public virtual TablaContribuyente? RncCedulaNavigation { get; set; }
}
