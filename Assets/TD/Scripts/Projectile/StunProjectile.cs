using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class StunProjectile : AProjectile
{
    [SerializeField]
    private float _stunDuration = 0.5f;

    private Damageable _stunnedTarget = null;
    protected override void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponentInParent<Damageable>();

        if (damageable != null)
        {
            StunTarget(damageable);
        }
    }

    private void StunTarget(Damageable target)
    {
        _stunnedTarget = target;
        Timer timer = new Timer(_stunDuration);
        timer.OnEndCallback += OnStunEnd;
        target.GetComponent<PathFollower>().SetCanMove(false);
    }

    private void OnStunEnd()
    {
        _stunnedTarget.GetComponent<PathFollower>().SetCanMove(true);
    }
}
