using CompanyEmployees.Entities.Models;
using CompanyEmployees.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CompanyEmployees.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class AsistenciaController : Controller
  {

    private RepositoryContext _context;
    public AsistenciaController(RepositoryContext context)
    {
      _context = context;
    }


    [Authorize]
    [HttpGet]
    [Route("")]
    public IEnumerable<RegistroAsistencias> getRegistroAsistencias() => _context.RegistroAsistencias
      .Include(ra => ra.VentaSuscripcion)
      .Where(ts => ts.Estado)
      .ToList();



    [Authorize]
    [HttpGet]
    [Route("consultar/{ci}")]
    public IActionResult Consultar(string ci) {


      try
      {
        DateTime fechaActual = DateTime.UtcNow.ToUniversalTime();
        DateTime fechaHaceUnMes = DateTime.UtcNow.AddMonths(2).ToUniversalTime();

        var fecha = DateTime.UtcNow;
        var registro = _context.VentaSuscripcion
            .Where(e => e.Ci == ci)
            .ToList();

        return new JsonResult(registro);

      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status200OK, new { message = error.Message });

      }
      
    }


    [HttpPost]
    [Route("create")]
    public IActionResult Crear([FromBody] RegistroAsistencias objeto)
    {
      try
      {


        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        // Configuración de la clave secreta
        var claveSecreta = "CodeMazeSecretKey"; // Reemplaza con tu clave secreta
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));

        // Configuración de la validación del token
        var tokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateIssuer = false, // Puedes cambiar esto según tus necesidades
          ValidateAudience = false, // Puedes cambiar esto según tus necesidades
          ClockSkew = TimeSpan.Zero
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;

        // Acceder al campo idUser
        var idUser = jwtToken.Claims.First(c => c.Type == "idUser").Value;
        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

        RegistroAsistencias registro = new RegistroAsistencias();
        registro.IdVentaSuscripcion = objeto.IdVentaSuscripcion;
        registro.UsuarioModifico = idUser.ToString();
        registro.IpModificacion = ipAddress;
        registro.TipoAccion = "create";
        registro.FechaModificacion = DateTime.UtcNow;


        _context.RegistroAsistencias.Add(registro);
        _context.SaveChanges();
        return StatusCode(StatusCodes.Status200OK, new { message = "OK" });
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status200OK, new { message = error.Message });

      }
    }



    [Authorize]
    [HttpPut]
    [Route("update/{id}")]
    public IActionResult update(int id, [FromBody] RegistroAsistencias objeto)
    {
      try
      {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        // Configuración de la clave secreta
        var claveSecreta = "CodeMazeSecretKey"; // Reemplaza con tu clave secreta
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));

        // Configuración de la validación del token
        var tokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateIssuer = false, // Puedes cambiar esto según tus necesidades
          ValidateAudience = false, // Puedes cambiar esto según tus necesidades
          ClockSkew = TimeSpan.Zero
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
        var jwtToken = (JwtSecurityToken)validatedToken;

        // Acceder al campo idUser
        var idUser = jwtToken.Claims.First(c => c.Type == "idUser").Value;


        var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

        var RegistroAsistenciasExiste = _context.RegistroAsistencias.Find(id);
        if (RegistroAsistenciasExiste == null)
        {
          return NotFound(); // Puedes retornar un 404 si el objeto no se encuentra
        }
        RegistroAsistenciasExiste.UsuarioModifico = idUser.ToString();
        RegistroAsistenciasExiste.IpModificacion = ipAddress;
        RegistroAsistenciasExiste.TipoAccion = "update";
        RegistroAsistenciasExiste.FechaModificacion = DateTime.Now;
        RegistroAsistenciasExiste.ValoresAnteriores = JsonConvert.SerializeObject(RegistroAsistenciasExiste);






        _context.SaveChanges();
        return StatusCode(StatusCodes.Status200OK, new { message = "Registro Actualizado" });
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
      }
    }

    [Authorize]
    [HttpDelete]
    [Route("delete/{id}")]
    public IActionResult delete(int id)
    {
      try
      {
        var tipoSuscripcionExistente = _context.TipoSuscripcion.Find(id);
        if (tipoSuscripcionExistente == null)
        {
          return NotFound(); // Puedes retornar un 404 si el objeto no se encuentra
        }
        tipoSuscripcionExistente.Estado = false;
        _context.SaveChanges();
        return StatusCode(StatusCodes.Status200OK, new { message = "Registro Eliminado" });
      }
      catch (Exception error)
      {
        return StatusCode(StatusCodes.Status200OK, new { message = error.Message });
      }
    }
  }
}
