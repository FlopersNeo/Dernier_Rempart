using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;
public class MissileLauncher : AWeapon
{
    [SerializeField]
    private BaseTower _tower = null;

    [SerializeField]
    private GuidedProjectile _missile = null;

    [SerializeField]
    private Transform _projectileAnchor = null;
    protected override void DoFire()
    {
        var instance = Instantiate(_missile, _projectileAnchor.position, _projectileAnchor.rotation);
        instance.Initialize(_tower.target);
    }
}
