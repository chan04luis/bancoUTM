using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("tarjetas")]
public class Tarjeta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(16)]
    public string tarjeta { get; set; }

    [Required]
    [StringLength(4)]
    public string NIP { get; set; }

    [Required]
    public decimal Limite { get; set; } = 0;

    public decimal? Saldo { get; set; }

    public int? Tipo { get; set; }

    [ForeignKey("Cliente")]
    public int Id_Cliente { get; set; }
    public Cliente Cliente { get; set; }

    [Required]
    public int Estatus { get; set; } = 0;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}