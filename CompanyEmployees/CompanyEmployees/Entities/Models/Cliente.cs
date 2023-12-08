using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyEmployees.Entities.Models
{
  public class Cliente
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCliente { get; set; }    
    public int Celular { get; set; }
    public string Email { get; set; }
    public int Cedula{ get; set; }


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
