using System;
using System.Collections.Generic;
using System.Diagnostics;

public class ATMService : IATMService
{
    private readonly IATMRepository _atmRepository;

    public ATMService(IATMRepository atmRepository)
    {
        _atmRepository = atmRepository;
    }

    public async Task<List<ATMDTO>> GetAllATMs()
    {

        var atms = await _atmRepository.GetAll();
        var atmDTOList = new List<ATMDTO>();

        foreach (var atm in atms)
        {
            var atmDTO = Mapper.MapATMToDTO(atm);

            atmDTOList.Add(atmDTO);
        }

        return atmDTOList;
    }

    public async Task<ATMDTO> GetATMById(int id)
    {
        return Mapper.MapATMToDTO(await _atmRepository.GetById(id));
    }

    public async Task<ATMDTO> AddATM(ATM atm)
    {
        return Mapper.MapATMToDTO(await _atmRepository.Add(atm));
    }

    public async Task<ATMDTO> UpdateATM(ATMDTO atm)
    {
        return Mapper.MapATMToDTO(await _atmRepository.Update(Mapper.MapATMToDTO(atm)));
    }

    public async Task<int> DeleteATM(int id)
    {
        var atm = await _atmRepository.GetById(id);
        if (atm != null)
        {
            return await _atmRepository.Delete(atm);
        }
        return 0;
    }
}
