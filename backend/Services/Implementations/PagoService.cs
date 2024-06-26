using System.Data;
using backend.services.interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.services.implementations;

public class PagoService : IPagoService
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    public PagoService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<Pago> GetAll()
    {
        var pagos = new List<Pago>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ObtenerPagos", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    pagos.Add(new Pago
                    {
                        PagoID = (int)reader["PagoID"],
                        TarjetaID = (int)reader["TarjetaID"],
                        FechaPago = (DateTime)reader["FechaPago"],
                        Monto = (decimal)reader["Monto"]
                    });
                }
            }
        }
        return pagos;
    }

    public Pago GetById(int id)
    {
        Pago pago = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ObtenerPagoPorID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PagoID", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    pago = new Pago
                    {
                        PagoID = (int)reader["PagoID"],
                        TarjetaID = (int)reader["TarjetaID"],
                        FechaPago = (DateTime)reader["FechaPago"],
                        Monto = (decimal)reader["Monto"]
                    };
                }
            }
        }
        return pago;
    }

    public void Create(Pago pago)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_InsertarPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TarjetaID", pago.TarjetaID);
            command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
            command.Parameters.AddWithValue("@Monto", pago.Monto);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Update(Pago pago)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ActualizarPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PagoID", pago.PagoID);
            command.Parameters.AddWithValue("@TarjetaID", pago.TarjetaID);
            command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
            command.Parameters.AddWithValue("@Monto", pago.Monto);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_EliminarPago", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PagoID", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
