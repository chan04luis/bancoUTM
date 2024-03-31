using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("credito_pagos")]
public class CreditoPagos
{
    [Key]
    public int Id { get; set; }
    [Column("num_pago")]
    public int NumPago { get; set; }
    public decimal Pago { get; set; }
    public DateTime Fecha { get; set; }
    [Column("fecha_pago")]
    public DateTime? FechaPago { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public int Estatus { get; set; } = 1;

    [ForeignKey("Credito")]
    [Column("id_credito")]
    public int IdCredito { get; set; }

    [Column("fecha_registro")]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;
    [Column("fecha_actualizado")]
    public DateTime FechaActualizado { get; set; } = DateTime.Now;

    // Navegación a Credito
    public Credito Credito { get; set; }
}