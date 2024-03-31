public class CreditoDTO
{
    public int Id { get; set; }
    public int Estatus { get; set; }
    public int IdTipoCredito { get; set; }
    public int PlazoMeses { get; set; }
    public float TasaInteres { get; set; }
    public decimal CreditoOtorgado { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFinal { get; set; }
    public int IdCliente { get; set; }
    public decimal Pago { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime FechaActualizado { get; set; }
    public List<CreditoPagosDTO?>? creditoPagosDTO { get; set; }
    public CreditoTipoDTO? creditoTipoDTO { get; set; }
    public ClienteDTO? clienteDTO {get; set; }
}