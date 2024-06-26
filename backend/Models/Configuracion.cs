using System.ComponentModel.DataAnnotations;

public class Configuracion
{
    [Key]
    public int ConfiguracionID { get; set; }

    [Required]
    public decimal PorcentajeInteres { get; set; }

    [Required]
    public decimal PorcentajeSaldoMinimo { get; set; }
}
