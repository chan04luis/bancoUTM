using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ATM")]
public class ATM
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int Denominacion { get; set; }

    public int Cantidad { get; set; }

    public int Estatus { get; set; } = 1;

    public int Tipo { get; set; } = 1;


    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}
