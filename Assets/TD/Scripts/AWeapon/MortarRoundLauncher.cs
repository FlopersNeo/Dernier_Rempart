using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class MortarRoundLauncher : AWeapon
{
    [SerializeField]
    protected MortarProjectile _projectile = null;

    [SerializeField]
    protected Transform _projectileAnchor = null;

    [SerializeField]
    private MortarWeaponController _weaponController = null;

    [SerializeField]
    private float heightProjectileLimit = 0f;

    protected override void DoFire()
    {
		var instance = Instantiate(_projectile, _projectileAnchor.position, _projectileAnchor.rotation);
        instance.InititalizeProjectile(_weaponController.target, heightProjectileLimit, ref _projectile);
        instance.IntitializeTimer(heightProjectileLimit);
    }
}
