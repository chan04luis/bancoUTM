public class CalculoAmortizacionDTO
{
    public int IdTipoCredito { get; set; }
    public DateTime FechaInicio { get; set; }
    public int PlazoMeses { get; set; }
    public decimal MontoCredito { get; set; }
}