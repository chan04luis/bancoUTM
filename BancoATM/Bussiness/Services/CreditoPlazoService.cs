public class CreditoPlazoService : ICreditoPlazoService
{
    private readonly ICreditoPlazoRepository _creditoPlazoRepository;
    private readonly ICreditoPlazoMapper _creditoPlazoMapper;

    public CreditoPlazoService(ICreditoPlazoRepository creditoPlazoRepository, ICreditoPlazoMapper creditoPlazoMapper)
    {
        _creditoPlazoRepository = creditoPlazoRepository;
        _creditoPlazoMapper = creditoPlazoMapper;
    }

    public async Task<List<CreditoPlazoDTO>> GetAllCreditosPlazo()
    {
        var creditosPlazo = await _creditoPlazoRepository.GetAll();
        return creditosPlazo.Select(creditoPlazo => _creditoPlazoMapper.MapToDTO(creditoPlazo)).ToList();
    }

    public async Task<List<CreditoPlazoDTO>> GetAllCreditosPlazoByTipo(int idTipo)
    {
        var creditosPlazo = await _creditoPlazoRepository.GetAllByTipo(idTipo);
        return creditosPlazo.Select(creditoPlazo => _creditoPlazoMapper.MapToDTO(creditoPlazo)).ToList();
    }

    public async Task<CreditoPlazoDTO> GetCreditoPlazoById(int id)
    {
        var creditoPlazo = await _creditoPlazoRepository.GetById(id);
        return _creditoPlazoMapper.MapToDTO(creditoPlazo);
    }

    public async Task<CreditoPlazoDTO> AddCreditoPlazo(CreditoPlazoDTO creditoPlazoDTO)
    {
        var creditoPlazo = _creditoPlazoMapper.MapToEntity(creditoPlazoDTO);
        var addedCreditoPlazo = await _creditoPlazoRepository.Add(creditoPlazo);
        return _creditoPlazoMapper.MapToDTO(addedCreditoPlazo);
    }

    public async Task<CreditoPlazoDTO> UpdateCreditoPlazo(CreditoPlazoDTO creditoPlazoDTO)
    {
        var creditoPlazo = _creditoPlazoMapper.MapToEntity(creditoPlazoDTO);
        var updatedCreditoPlazo = await _creditoPlazoRepository.Update(creditoPlazo);
        return _creditoPlazoMapper.MapToDTO(updatedCreditoPlazo);
    }

    public async Task<int> DeleteCreditoPlazo(int id)
    {
        return await _creditoPlazoRepository.Delete(id);
    }
}