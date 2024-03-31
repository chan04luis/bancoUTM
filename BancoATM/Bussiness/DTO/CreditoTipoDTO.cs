public class CreditoTipoDTO
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public float TasaAnual { get; set; }
    public int EdadMinima { get; set; }
    public int EdadMaxima { get; set; }
    public int CreditoMinimo { get; set; }
    public int CreditoMaximo { get; set; }
    public int Estatus { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaActualizado { get; set; }
}
