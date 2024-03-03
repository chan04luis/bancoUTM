using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("servicios")]
public class Servicio
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Nombre { get; set; }

    [ForeignKey("Tarjeta")]
    public int Id_Tarjeta { get; set; }
    public Tarjeta Tarjeta { get; set; }

    [Required]
    public int Estatus { get; set; } = 1;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}
