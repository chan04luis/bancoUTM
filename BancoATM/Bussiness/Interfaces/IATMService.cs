using System;
using System.Collections.Generic;

public interface IATMService
{
    List<ATM> GetAllATMs();
    ATM GetATMById(int id);
    void AddATM(ATM atm);
    void UpdateATM(ATM atm);
    void DeleteATM(int id);
}
