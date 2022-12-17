using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class DefenseObjective : MonoBehaviour
{
    [SerializeField]
    private bool _destroyable = false;

    [SerializeField]
    private int _health = 1;
    public int health { get { return _health; } }

    private int _maxHealth = 0;

    private bool _baseDestroyed = false;

    public bool baseDestroyed => _baseDestroyed;

    public delegate void DestroyedState(DefenseObjective defenceObjective);
    public event DestroyedState OnBaseDestroyed;
    public delegate void HealthState(int Health, int DamageTaken);
    public event HealthState OnDamaged;

    private void OnEnable()
    {
        _maxHealth = _health;
    }

    private void OnDisable()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseDamager baseDamager = other.GetComponentInParent<BaseDamager>();

        if (baseDamager != null)
        {
            TakeDamage(baseDamager.damageToBase);

            var baseDamageable = baseDamager.GetComponentInParent<Damageable>();
            if (baseDamageable != null)
            {
                baseDamageable.TakeDamage(9999);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        OnDamaged.Invoke(_health, damageAmount);
        if (_destroyable == true)
        {
            if (_health <= 0)
            {
                DestroyBase();
            }
        }
    }

    public void DestroyBase()
    {
        _baseDestroyed = true;
        OnBaseDestroyed.Invoke(this);
        Destroy(gameObject);
    }
}
