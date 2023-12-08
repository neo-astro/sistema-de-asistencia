using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CompanyEmployees.Entities.Models
{
  public class Suscripcion
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdSuscripcion { get; set; }

    public int IdTipoSuscripcion { get; set; }
    [ForeignKey("IdTipoSuscripcion")]
    public virtual TipoSuscripcion? TipoSuscripcion { get; set; }

    public string Nombre { get; set; }
    public int    Duracion { get; set; }
    public float  Precio { get; set; }
    public int    Descuento { get; set; }

    [DefaultValue(true)] // Esta anotación solo es válida para ASP.NET Core 6
    public bool Estado { get; set; } = true;
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public string? UsuarioModifico { get; set; }
    public string? TipoAccion { get; set; }
    public string? ValoresAnteriores { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string? IpModificacion { get; set; }
  }
}
