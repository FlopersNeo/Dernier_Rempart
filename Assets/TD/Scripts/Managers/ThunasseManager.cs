//Code by Néo Grapin

using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ThunasseManager : Singleton<ThunasseManager>
{
    public int _currentMoney = 0;

    public int currentMoney => _currentMoney;

    [SerializeField]
    private int _maxMoney= 200;

    public void EarnMoney(int moneyAdded)
    {
        _currentMoney += moneyAdded;
        if(_currentMoney > _maxMoney)
        {
            _currentMoney = _maxMoney;
        }
    }

    public void SpentMoney(int moneySpent)
    {
        if(moneySpent > _currentMoney)
        {
            _currentMoney = 0;
        }
        else
        {
        _currentMoney -= moneySpent;
        }
    }
}
