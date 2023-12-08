using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CompanyEmployees.Entities.Models
{
  public class RegistroAsistencias
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdRegistroAsistencias { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime fechaAsistencia { get; set; } = DateTime.UtcNow;
    public int IdVentaSuscripcion { get; set; }

    [ForeignKey("IdVentaSuscripcion")]
    public virtual VentaSuscripcion? VentaSuscripcion { get; set; }

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
