using System;
using System.Collections.Generic;

public interface IATMRepository
{
    List<ATM> GetAll();
    ATM GetById(int id);
    void Add(ATM atm);
    void Update(ATM atm);
    void Delete(ATM atm);
}
