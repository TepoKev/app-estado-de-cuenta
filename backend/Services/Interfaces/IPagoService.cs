namespace backend.services.interfaces;
public interface IPagoService
{
    IEnumerable<Pago> GetAll();
    Pago GetById(int id);
    void Create(Pago pago);
    void Update(Pago pago);
    void Delete(int id);
}
