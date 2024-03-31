using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("credito_tipo")]
public class CreditoTipo
{
    [Key]
    public int Id { get; set; }

    public string Nombre { get; set; }

    [Column("tasa_anul", TypeName = "float")]
    public float TasaAnual { get; set; }

    [Column("edad_min")]
    public int EdadMinima { get; set; }

    [Column("edad_max")]
    public int EdadMaxima { get; set; }

    [Column("cred_min")]
    public int CreditoMinimo { get; set; }

    [Column("cred_max")]
    public int CreditoMaximo { get; set; }

    public int Estatus { get; set; } = 1;

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Column("fecha_actualizado")]
    public DateTime FechaActualizado { get; set; } = DateTime.Now;
}
