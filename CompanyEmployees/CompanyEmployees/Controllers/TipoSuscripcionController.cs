using AutoMapper;
using CompanyEmployees.Contracts;
using CompanyEmployees.Entities.Models;
using CompanyEmployees.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CompanyEmployees.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  //[Authorize]

  public class TipoSuscripcionController : ControllerBase
  {
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private RepositoryContext _context;

    public TipoSuscripcionController(IRepositoryManager repository, IMapper mapper, RepositoryContext context)
    {
      _context  = context;
      _repository = repository;
      _mapper = mapper;
    }



    [Authorize]
    [HttpGet]
    [Route("")]
    public IEnumerable<TipoSuscripcion> getTipoSuscripcion()=> _context.TipoSuscripcion
       .Where(ts => ts.Estado)
       .ToList();



    [HttpPost]
    [Route("create")]
    public IActionResult Crear([FromBody] TipoSuscripcion objeto)
    {
      try
      {
        _context.TipoSuscripcion.Add(objeto);
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
    public IActionResult update(int id,[FromBody] TipoSuscripcion objeto)
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

          var tipoSuscripcionExistente = _context.TipoSuscripcion.Find(id);
          if (tipoSuscripcionExistente == null)
          {
            return NotFound(); // Puedes retornar un 404 si el objeto no se encuentra
          }
          tipoSuscripcionExistente.Nombre = objeto.Nombre;
          tipoSuscripcionExistente.UsuarioModifico= idUser.ToString();
          tipoSuscripcionExistente.IpModificacion = ipAddress;
          tipoSuscripcionExistente.TipoAccion = "update";
          tipoSuscripcionExistente.FechaModificacion = DateTime.UtcNow;
          tipoSuscripcionExistente.ValoresAnteriores = JsonConvert.SerializeObject(tipoSuscripcionExistente);






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
