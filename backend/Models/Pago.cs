using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Pago
{
    [Key]
    public int PagoID { get; set; }

    [Required]
    public int TarjetaID { get; set; }

    [ForeignKey("TarjetaID")]
    public TarjetaDeCredito TarjetaDeCredito { get; set; }

    [Required]
    public DateTime FechaPago { get; set; }

    [Required]
    public decimal Monto { get; set; }
}
