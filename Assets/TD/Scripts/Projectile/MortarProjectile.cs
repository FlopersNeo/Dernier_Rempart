//
// Created by Leo CUSSERNE
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class MortarProjectile : Projectile
{
    Timer timer = new Timer();
    /// <summary>
    /// Will start a timer with a duration based on the targetHeight and the projectile's speed
    /// </summary>
    /// <param name="heightWhenDestroyed">The height at which the projectile is destroyed</param>

    [SerializeField]
    private int _blowDamage = 0;

    public void IntitializeTimer(float heightWhenDestroyed)
    {
        float timerDuration = heightWhenDestroyed / projectileSpeed;
        Debug.Log(timerDuration);
        timer.OnEndCallback -= OnTimerEnded;
        timer.OnEndCallback += OnTimerEnded;
        timer.Set(timerDuration, true).Start();
    }

    public void InititalizeProjectile(Damageable target, float heightLimit, ref MortarProjectile projectilePrefab)
    {
        Vector3 projectileOrigin = new Vector3(target.transform.position.x, heightLimit, target.transform.position.z);
        projectileOrigin += target.GetComponentInParent<PathFollower>().velocity.normalized * (target.GetComponentInParent<PathFollower>().velocity.magnitude * (heightLimit / projectileSpeed));
        var instance = Instantiate<MortarProjectile>(projectilePrefab, projectileOrigin, Quaternion.identity);
        instance.transform.LookAt(new Vector3(instance.transform.position.x, instance.transform.position.y - 1, instance.transform.position.z));
    }

    private void ApplyDamageToDamageableInRange(int damages)
    {
        List<Damageable> damageables = GetDamageableInSphereRadius(transform.position, 5);
        for (int i = 0; i < damageables.Count; i++)
        {
            damageables[i].TakeDamage(damages);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ApplyDamageToDamageableInRange(_blowDamage);
    }

    private List<Damageable> GetDamageableInSphereRadius(Vector3 atPosition, float radius)
    {
        Ray ray = new Ray(atPosition, Vector3.forward);
        RaycastHit[] hit = Physics.SphereCastAll(ray, radius);
        List<Damageable> damageables = new List<Damageable>();
        for (int i = 0; i < hit.Length; i++)
        {
            Damageable damageable = hit[i].transform.GetComponentInParent<Damageable>();
            if (damageable != null)
            {
                damageables.Add(damageable);
            }
        }
        return damageables;
    }

    private void OnTimerEnded()
    {
        Debug.Log("TimerEnded");
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        timer.Update();
    }
}
