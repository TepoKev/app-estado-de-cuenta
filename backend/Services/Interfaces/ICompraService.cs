namespace backend.services.interfaces;
public interface ICompraService
{
    IEnumerable<Compra> GetAll();
    Compra GetById(int id);
    void Create(Compra compra);
    void Update(Compra compra);
    void Delete(int id);
}