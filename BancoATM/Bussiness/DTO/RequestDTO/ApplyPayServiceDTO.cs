public class ApplyPayServiceDTO
{
    public int Id { get; set; }
    public int PayId { get; set; }
    public List<DepositDTO>? Billetes { get; set; }
    public decimal Monto { get; set; }
    public string? Referencia { get; set;}
}