using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class TarjetaDeCredito
{
    [Key]
    public int TarjetaID { get; set; }

    [Required]
    [StringLength(100)]
    public string NombreTitular { get; set; }

    [Required]
    [StringLength(16)]
    public string NumeroTarjeta { get; set; }

    [Required]
    public decimal SaldoActual { get; set; }

    [Required]
    public decimal LimiteCredito { get; set; }

    [Required]
    public decimal SaldoDisponible { get; set; }

    public ICollection<Compra> Compras { get; set; } = new List<Compra>();
    public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
