using System;
using System.Collections.Generic;

public interface IATMRepository
{
    Task<List<ATM>> GetAll();
    Task<ATM> GetById(int id);
    Task<ATM> Add(ATM atm);
    Task<ATM> Update(ATM atm);
    Task<int> Delete(ATM atm);
}
