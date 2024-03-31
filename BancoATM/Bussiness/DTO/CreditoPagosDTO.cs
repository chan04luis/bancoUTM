public class CreditoPagosDTO
{
    public int Id { get; set; }
    public int NumPago { get; set; }
    public decimal Pago { get; set; }
    public DateTime Fecha { get; set; }
    public DateTime? FechaPago { get; set; }
    public decimal Capital { get; set; }
    public decimal Interes { get; set; }
    public int Estatus { get; set; }
    public int IdCredito { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaActualizado { get; set; }
}