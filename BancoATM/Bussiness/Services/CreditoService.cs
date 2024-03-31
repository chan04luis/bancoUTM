public class CreditoService : ICreditoService
{
    private readonly ICreditoRepository _creditoRepository;
    private readonly ICreditoPagosRepository _creditoPagosRepository;
    private readonly ICreditoMapper _creditoMapper;
    private readonly ICreditoPagosMapper _creditoPagosMapper;

    public CreditoService(ICreditoRepository creditoRepository, 
        ICreditoMapper creditoMapper, 
        ICreditoPagosMapper creditoPagosMapper, ICreditoPagosRepository creditoPagosRepository)
    {
        _creditoRepository = creditoRepository;
        _creditoMapper = creditoMapper;
        _creditoPagosMapper = creditoPagosMapper;
        _creditoPagosRepository = creditoPagosRepository;
    }

    public async Task<List<CreditoDTO>> GetAllCreditos()
    {
        var creditos = await _creditoRepository.GetAll();
        List< CreditoDTO> list = creditos.Select(credito => _creditoMapper.MapToDTO(credito)).ToList();
        foreach (var item in list)
        {
            var items= await _creditoPagosRepository.GetByCreditoId(item.Id);

            item.creditoPagosDTO = items.Select(x=> _creditoPagosMapper.MapToDTO(x)).ToList();
        }
        return list;
    }

    public async Task<CreditoDTO> GetCreditoById(int id)
    {
        var credito = await _creditoRepository.GetById(id);
        return _creditoMapper.MapToDTO(credito);
    }

    public async Task<CreditoDTO> CreateCredito(CreditoDTO creditoDTO, List<CreditoPagosDTO> creditoPagosDTO)
    {
        var credito = _creditoMapper.MapToEntity(creditoDTO);
        var creditoPagos = creditoPagosDTO.Select(cp => _creditoPagosMapper.MapToEntity(cp)).ToList();

        var newCredito = await _creditoRepository.Insert(credito, creditoPagos);
        return _creditoMapper.MapToDTO(newCredito);
    }

    public async Task<CreditoDTO> UpdateCredito(CreditoDTO creditoDTO)
    {
        var credito = _creditoMapper.MapToEntity(creditoDTO);
        var updatedCredito = await _creditoRepository.Update(credito);
        return _creditoMapper.MapToDTO(updatedCredito);
    }

    public async Task<int> DeleteCredito(int id)
    {
        return await _creditoRepository.Delete(id);
    }
}