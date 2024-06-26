using System.Collections.Generic;
using System.Data;
using backend.Models;
using backend.services.interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace backend.services.implementations;

public class TarjetaDeCreditoService : ITarjetaDeCreditoService
{
    private readonly ApplicationDbContext _context;
    private readonly string _connectionString;

    public TarjetaDeCreditoService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<TarjetaDeCredito> GetAll()
    {
        var tarjetas = new List<TarjetaDeCredito>();
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ObtenerTarjetasDeCredito", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tarjetas.Add(new TarjetaDeCredito
                    {
                        TarjetaID = (int)reader["TarjetaID"],
                        NombreTitular = reader["NombreTitular"].ToString(),
                        NumeroTarjeta = reader["NumeroTarjeta"].ToString(),
                        SaldoActual = (decimal)reader["SaldoActual"],
                        LimiteCredito = (decimal)reader["LimiteCredito"],
                        SaldoDisponible = (decimal)reader["SaldoDisponible"]
                    });
                }
            }
        }
        return tarjetas;
    }

    public TarjetaDeCredito GetById(int id)
    {
        TarjetaDeCredito tarjeta = null;
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ObtenerTarjetaDeCreditoPorID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TarjetaID", id);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    tarjeta = new TarjetaDeCredito
                    {
                        TarjetaID = (int)reader["TarjetaID"],
                        NombreTitular = reader["NombreTitular"].ToString(),
                        NumeroTarjeta = reader["NumeroTarjeta"].ToString(),
                        SaldoActual = (decimal)reader["SaldoActual"],
                        LimiteCredito = (decimal)reader["LimiteCredito"],
                        SaldoDisponible = (decimal)reader["SaldoDisponible"]
                    };
                }
            }
        }
        return tarjeta;
    }

    public void Create(TarjetaDeCreditoModel TarjetaDeCreditoModel)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_InsertarTarjetaDeCredito", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@NombreTitular", TarjetaDeCreditoModel.NombreTitular);
            command.Parameters.AddWithValue("@NumeroTarjeta", TarjetaDeCreditoModel.NumeroTarjeta);
            command.Parameters.AddWithValue("@SaldoActual", TarjetaDeCreditoModel.SaldoActual);
            command.Parameters.AddWithValue("@LimiteCredito", TarjetaDeCreditoModel.LimiteCredito);
            command.Parameters.AddWithValue("@SaldoDisponible", TarjetaDeCreditoModel.SaldoActual);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Update(TarjetaDeCredito tarjetaDeCredito)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_ActualizarTarjetaDeCredito", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TarjetaID", tarjetaDeCredito.TarjetaID);
            command.Parameters.AddWithValue("@NombreTitular", tarjetaDeCredito.NombreTitular);
            command.Parameters.AddWithValue("@NumeroTarjeta", tarjetaDeCredito.NumeroTarjeta);
            command.Parameters.AddWithValue("@SaldoActual", tarjetaDeCredito.SaldoActual);
            command.Parameters.AddWithValue("@LimiteCredito", tarjetaDeCredito.LimiteCredito);
            command.Parameters.AddWithValue("@SaldoDisponible", tarjetaDeCredito.SaldoDisponible);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("sp_EliminarTarjetaDeCredito", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TarjetaID", id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
