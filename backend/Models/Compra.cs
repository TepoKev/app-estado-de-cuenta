using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Compra
{
    [Key]
    public int CompraID { get; set; }

    [Required]
    public int TarjetaID { get; set; }

    [ForeignKey("TarjetaID")]
    public TarjetaDeCredito TarjetaDeCredito { get; set; }

    [Required]
    public DateTime FechaCompra { get; set; }

    [Required]
    [StringLength(255)]
    public string Descripcion { get; set; }

    [Required]
    public decimal Monto { get; set; }
}
