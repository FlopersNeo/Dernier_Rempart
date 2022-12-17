using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class MortarWeaponController : MonoBehaviour
{
    [SerializeField]
    private MortarRoundLauncher _MortarRoundLauncher = null;

    private Damageable _target = null;

    public Damageable target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }

    public void Fire()
    {
        _MortarRoundLauncher.Fire();
    }
}
