using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace CompanyEmployees.Entities.Models
{
  public class VentaSuscripcion
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdVentaSuscripcion { get; set; }

    public string NombreCliente { get; set; }
    public string Ci { get; set; }
    public int Iva { get; set; }
    public float Total { get; set; }
    public DateTime FechaFinSuscripcion { get; set; }


    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime FechaInicioSuscripcion { get; set; } = DateTime.UtcNow;
    public int IdSuscripcion { get; set; }
    [ForeignKey("IdSuscripcion")]
    public Suscripcion Suscripcion { get; set; }



    [DefaultValue(true)] // Esta anotación solo es válida para ASP.NET Core 6
    public bool Estado { get; set; } = true;
    public string? UsuarioModifico { get; set; }
    public string? TipoAccion { get; set; }
    public string? ValoresAnteriores { get; set; }
    public DateTime? FechaModificacion { get; set; }
    public string? IpModificacion { get; set; }



  }
}
