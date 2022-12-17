using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;
using System;

public class BaseManager : Singleton<BaseManager>
{
    private DefenseObjective[] _defenseObjectivesList = null;

    private int _totalBasesHealth = 0;

    private int _totalBasesMaxHealth = 0;
    public int totalBasesMaxHealth { get { return _totalBasesMaxHealth; } }

    public delegate void BasesHealth(int currentTotalHealth);
    public event BasesHealth OnBasesHealthChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        _defenseObjectivesList = FindObjectsOfType<DefenseObjective>();

        foreach (var item in _defenseObjectivesList)
        {
            item.OnDamaged -= OnDefenseObjectiveDamaged;
            item.OnDamaged += OnDefenseObjectiveDamaged;

            item.OnBaseDestroyed -= OnDefenseObjectiveDestroyed;
            item.OnBaseDestroyed += OnDefenseObjectiveDestroyed;

            _totalBasesHealth += item.health;
            _totalBasesMaxHealth = _totalBasesHealth;
        }
    }

    private void OnDefenseObjectiveDamaged(int health, int damageTaken)
    {
        _totalBasesHealth -= damageTaken;
        if (_totalBasesHealth <= 0)
        {
            OnTotalHealthDefeated();
        }
        OnBasesHealthChanged?.Invoke(_totalBasesHealth);
    }

    private void OnDefenseObjectiveDestroyed(DefenseObjective defenseObjective)
    {
        bool defeated = true;
        foreach (var item in _defenseObjectivesList)
        {
            if (item.baseDestroyed == false)
            {
                defeated = false;
            }
        }
        if (defeated == true)
        {
            OnAllBasesDestroyed();
        }
    }


    private void OnAllBasesDestroyed()
    {
        Defeated();
    }

    private void OnTotalHealthDefeated()
    {
        Defeated();
    }

    private void Defeated()
    {
        Time.timeScale = 0f;
        UIManager.Instance.DefeatUi();
    }
}
