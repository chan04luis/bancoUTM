public class TarjetaDTO
{
    public int Id { get; set; }
    public string Tarjeta { get; set; }
    public decimal Limite { get; set; }
    public int? Tipo { get; set; }
    public string Nip { get; set; }
    public decimal? Saldo { get; set; }
    public ClienteDTO? Cliente { get; set; }
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    public DateTime Fecha_Actualizado { get; set; } = DateTime.Now;
}