using CompanyEmployees.Entities.Models;
using CompanyEmployees.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CompanyEmployees.Controllers
{


  [Route("api/[controller]")]
  [ApiController]
  public class SuscripcionController : Controller
  {
    private RepositoryContext _context;
    public SuscripcionController(RepositoryContext context) {
    _context  = context;
    }





    [Authorize]
    [HttpGet]
    [Route("")]
    public IEnumerable<Suscripcion> getSuscripcion() => _context.Suscripcion
       .Where(ts => ts.Estado)
       .ToList();


    [Authorize]
    [HttpPost]
    [Route("create")]
    public IActionResult create([FromBody] Suscripcion objeto)
    {
      try
      {
        _context.Suscripcion.Add(objeto);
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
    public IActionResult update(int id, [FromBody] Suscripcion objeto)
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

        var suscripcionExistente = _context.Suscripcion.Find(id);
        if (suscripcionExistente == null)
        {
          return NotFound(); // Puedes retornar un 404 si el objeto no se encuentra
        }
        suscripcionExistente.Nombre = objeto.Nombre;
        suscripcionExistente.IdTipoSuscripcion = objeto.IdTipoSuscripcion;
        suscripcionExistente.Duracion = objeto.Duracion;
        suscripcionExistente.Precio = objeto.Precio;
        suscripcionExistente.Descuento = objeto.Descuento;


        suscripcionExistente.UsuarioModifico = idUser.ToString();
        suscripcionExistente.IpModificacion = ipAddress;
        suscripcionExistente.TipoAccion = "update";
        suscripcionExistente.FechaModificacion = DateTime.UtcNow;
        suscripcionExistente.ValoresAnteriores = JsonConvert.SerializeObject(suscripcionExistente);






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
        var suscripcionExistente = _context.Suscripcion.Find(id);
        if (suscripcionExistente == null)
        {
          return NotFound(); // Puedes retornar un 404 si el objeto no se encuentra
        }
        suscripcionExistente.Estado = false;
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
