using System.Collections.Generic;
using System.Data;
using backend.services.interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace backend.services.implementations;
public class CompraService : ICompraService
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    public CompraService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<Compra> GetAll()
    {
        var compras = new List<Compra>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ObtenerCompras", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    compras.Add(new Compra
                    {
                        CompraID = (int)reader["CompraID"],
                        TarjetaID = (int)reader["TarjetaID"],
                        FechaCompra = (DateTime)reader["FechaCompra"],
                        Descripcion = reader["Descripcion"].ToString(),
                        Monto = (decimal)reader["Monto"]
                    });
                }
            }
        }
        return compras;
    }

    public Compra GetById(int id)
    {
        Compra compra = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ObtenerCompraPorID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CompraID", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    compra = new Compra
                    {
                        CompraID = (int)reader["CompraID"],
                        TarjetaID = (int)reader["TarjetaID"],
                        FechaCompra = (DateTime)reader["FechaCompra"],
                        Descripcion = reader["Descripcion"].ToString(),
                        Monto = (decimal)reader["Monto"]
                    };
                }
            }
        }
        return compra;
    }

    public void Create(Compra compra)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_InsertarCompra", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TarjetaID", compra.TarjetaID);
            command.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);
            command.Parameters.AddWithValue("@Descripcion", compra.Descripcion);
            command.Parameters.AddWithValue("@Monto", compra.Monto);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Update(Compra compra)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ActualizarCompra", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CompraID", compra.CompraID);
            command.Parameters.AddWithValue("@TarjetaID", compra.TarjetaID);
            command.Parameters.AddWithValue("@FechaCompra", compra.FechaCompra);
            command.Parameters.AddWithValue("@Descripcion", compra.Descripcion);
            command.Parameters.AddWithValue("@Monto", compra.Monto);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_EliminarCompra", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CompraID", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
