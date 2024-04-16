public class DepositCheckDTO
{
    public int id {  get; set; }
    public int monto { get; set; }
    public List<DepositDTO?>? depositDTOs { get; set; }
}