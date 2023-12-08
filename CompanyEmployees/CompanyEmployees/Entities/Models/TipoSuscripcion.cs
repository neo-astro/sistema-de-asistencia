
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace CompanyEmployees.Entities.Models
{
  public class TipoSuscripcion
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int      IdTipoSuscripcion { get; set; }
    public string   Nombre{ get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [DefaultValue(true)] // Esta anotación solo es válida para ASP.NET Core 6
    public bool Estado { get; set; } = true;
    public string? UsuarioModifico { get; set; }
    public string   ?TipoAccion { get; set; }
    public string   ?ValoresAnteriores { get; set; }
    public DateTime ?FechaModificacion { get; set; }
    public string   ?IpModificacion { get; set; }
  }
}
