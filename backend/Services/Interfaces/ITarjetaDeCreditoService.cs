using backend.Models;
namespace backend.services.interfaces;
public interface ITarjetaDeCreditoService
{
    IEnumerable<TarjetaDeCredito> GetAll();
    TarjetaDeCredito GetById(int id);
    void Create(TarjetaDeCreditoModel TarjetaDeCreditoModel);
    void Update(TarjetaDeCredito tarjetaDeCredito);
    void Delete(int id);
}
