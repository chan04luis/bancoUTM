public class CreditoPagosService : ICreditoPagosService
{
    private readonly ICreditoPagosRepository _creditoPagosRepository;
    private readonly ICreditoPagosMapper _creditoPagosMapper;

    public CreditoPagosService(ICreditoPagosRepository creditoPagosRepository, ICreditoPagosMapper creditoPagosMapper)
    {
        _creditoPagosRepository = creditoPagosRepository;
        _creditoPagosMapper = creditoPagosMapper;
    }

    public async Task<CreditoPagosDTO> GetCreditoPagosById(int id)
    {
        var creditoPagos = await _creditoPagosRepository.GetById(id);
        return _creditoPagosMapper.MapToDTO(creditoPagos);
    }

    public async Task<CreditoPagosDTO> UpdateCreditoPagos(CreditoPagosDTO creditoPagosDTO)
    {
        var creditoPagos = _creditoPagosMapper.MapToEntity(creditoPagosDTO);
        var updatedCreditoPagos = await _creditoPagosRepository.Update(creditoPagos);
        return _creditoPagosMapper.MapToDTO(updatedCreditoPagos);
    }

    public async Task<int> DeleteCreditoPagos(int id)
    {
        return await _creditoPagosRepository.Delete(id);
    }

    public async Task<List<CreditoPagosDTO>> GetCreditoPagosByCreditoId(int idCredito)
    {
        var creditoPagosList = await _creditoPagosRepository.GetByCreditoId(idCredito);
        return creditoPagosList.Select(cp => _creditoPagosMapper.MapToDTO(cp)).ToList();
    }
}