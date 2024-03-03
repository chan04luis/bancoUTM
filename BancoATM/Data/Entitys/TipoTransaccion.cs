using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("tipo_transaction")]
public class TipoTransaccion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombres { get; set; }

    [Required]
    public int Edo_Cuenta { get; set; }

    [Required]
    public int Estatus { get; set; } = 1;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}
