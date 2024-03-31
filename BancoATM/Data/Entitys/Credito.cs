using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("credito")]
public class Credito
{
    [Key]
    public int Id { get; set; }
    public int Estatus { get; set; } = 1;

    [ForeignKey("CreditoTipo")]
    [Column("id_tipo_credito")]
    public int IdTipoCredito { get; set; }

    [Column("plazo_meses")]
    public int PlazoMeses { get; set; }
    [Column("tasa_interes", TypeName = "float")]
    public float TasaInteres { get; set; }
    [Column("credito_otorgado")]
    public decimal CreditoOtorgado { get; set; }
    [Column("fecha_inicio")]
    public DateTime FechaInicio { get; set; }

    [Column("fecha_final")]
    public DateTime FechaFinal { get; set; }

    [ForeignKey("Cliente")]
    [Column("id_cliente")]
    public int IdCliente { get; set; }

    public decimal Pago { get; set; }
    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    [Column("fecha_actualizado")]
    public DateTime FechaActualizado { get; set; } = DateTime.Now;

    // Navegación a CreditoTipo y Cliente
    public CreditoTipo CreditoTipo { get; set; }
    public Cliente Cliente { get; set; }
}
