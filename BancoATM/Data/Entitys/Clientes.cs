using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


[Table("clientes")]
public class Cliente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Nombres { get; set; }

    [Required]
    [StringLength(50)]
    public string Apellidos { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string Telefono { get; set; }

    [Required]
    public int Estatus { get; set; } = 1;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}