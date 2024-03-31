public class CreditoTipoService : ICreditoTipoService
{
    private readonly ICreditoTipoRepository _creditoTipoRepository;
    private readonly ICreditoTipoMapper _creditoTipoMapper;

    public CreditoTipoService(ICreditoTipoRepository creditoTipoRepository, ICreditoTipoMapper creditoTipoMapper)
    {
        _creditoTipoRepository = creditoTipoRepository;
        _creditoTipoMapper = creditoTipoMapper;
    }

    public async Task<List<CreditoTipoDTO>> GetAllCreditosTipo()
    {
        var creditosTipo = await _creditoTipoRepository.GetAll();
        return creditosTipo.Select(creditoTipo => _creditoTipoMapper.MapToDTO(creditoTipo)).ToList();
    }

    public async Task<CreditoTipoDTO> GetCreditoTipoById(int id)
    {
        var creditoTipo = await _creditoTipoRepository.GetById(id);
        return _creditoTipoMapper.MapToDTO(creditoTipo);
    }

    public async Task<CreditoTipoDTO> AddCreditoTipo(CreditoTipoDTO creditoTipoDTO)
    {
        var creditoTipo = _creditoTipoMapper.MapToEntity(creditoTipoDTO);
        var addedCreditoTipo = await _creditoTipoRepository.Add(creditoTipo);
        return _creditoTipoMapper.MapToDTO(addedCreditoTipo);
    }

    public async Task<CreditoTipoDTO> UpdateCreditoTipo(CreditoTipoDTO creditoTipoDTO)
    {
        var creditoTipo = _creditoTipoMapper.MapToEntity(creditoTipoDTO);
        var updatedCreditoTipo = await _creditoTipoRepository.Update(creditoTipo);
        return _creditoTipoMapper.MapToDTO(updatedCreditoTipo);
    }

    public async Task<int> DeleteCreditoTipo(int id)
    {
        var item = await _creditoTipoRepository.GetById(id);
        return await _creditoTipoRepository.Delete(item);
    }
}