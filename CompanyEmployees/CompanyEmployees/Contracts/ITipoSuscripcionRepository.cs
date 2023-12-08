using CompanyEmployees.Entities.Models;

namespace CompanyEmployees.Contracts
{
  public interface ITipoSuscripcionRepository
  {
    IEnumerable<TipoSuscripcion> GetAllTipoSuscripcion(bool trackChanges);
  }
}
