using System;
using System.Collections.Generic;

public class ATMService : IATMService
{
    private readonly IATMRepository _atmRepository;

    public ATMService(IATMRepository atmRepository)
    {
        _atmRepository = atmRepository;
    }

    public List<ATM> GetAllATMs()
    {
        return _atmRepository.GetAll();
    }

    public ATM GetATMById(int id)
    {
        return _atmRepository.GetById(id);
    }

    public void AddATM(ATM atm)
    {
        _atmRepository.Add(atm);
    }

    public void UpdateATM(ATM atm)
    {
        _atmRepository.Update(atm);
    }

    public void DeleteATM(int id)
    {
        var atm = _atmRepository.GetById(id);
        if (atm != null)
        {
            _atmRepository.Delete(atm);
        }
    }
}
