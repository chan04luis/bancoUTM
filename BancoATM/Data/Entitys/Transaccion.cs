using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("transactions")]
public class Transaccion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int? Edo_cuenta { get; set; }

    [ForeignKey("TipoTransaccion")]
    public int Id_Tipo { get; set; }
    public TipoTransaccion TipoTransaccion { get; set; }
    public int CompareTo(Transaccion other)
    {
        // Comparación por nombre
        return this.Importe.CompareTo(other.Importe);
    }

    [Required]
    public int Estatus { get; set; } = 1;

    public string Descripcion { get; set; }

    public decimal Importe { get; set; }

    [StringLength(200)]
    public string Referencia { get; set; }

    [ForeignKey("Cuenta")]
    public int Id_Cuenta { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}
