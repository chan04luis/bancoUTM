using System;
using System.Collections.Generic;

public interface IATMService
{
    Task<List<ATMDTO>> GetAllATMs();
    Task<ATMDTO> GetATMById(int id);
    Task<ATMDTO> AddATM(ATM atm);
    Task<ATMDTO> UpdateATM(ATMDTO atm);
    Task<int> DeleteATM(int id);
}
