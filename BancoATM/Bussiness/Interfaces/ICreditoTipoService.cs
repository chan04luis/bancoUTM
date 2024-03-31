public interface ICreditoTipoService
{
    Task<List<CreditoTipoDTO>> GetAllCreditosTipo();
    Task<CreditoTipoDTO> GetCreditoTipoById(int id);
    Task<CreditoTipoDTO> AddCreditoTipo(CreditoTipoDTO creditoTipoDTO);
    Task<CreditoTipoDTO> UpdateCreditoTipo(CreditoTipoDTO creditoTipoDTO);
    Task<int> DeleteCreditoTipo(int id);
}