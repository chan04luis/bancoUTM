using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("credito_plazos")]
public class CreditoPlazo
{
    [Key]
    public int Id { get; set; }

    [Column("plazo_meses")]
    public int PlazoMeses { get; set; }

    public int Estatus { get; set; } = 1;

    [ForeignKey("CreditoTipo")]
    [Column("id_tipo_credito")]
    public int IdTipoCredito { get; set; }
    public CreditoTipo CreditoTipo { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Column("fecha_actualizado")]
    public DateTime FechaActualizado { get; set; } = DateTime.Now;
}
