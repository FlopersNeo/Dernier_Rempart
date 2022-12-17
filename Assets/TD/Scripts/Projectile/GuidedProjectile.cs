using GSGD1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedProjectile : AProjectile
{
    [SerializeField]
    private float _moveSpeed = 1f;

    private Damageable _target = null;

    private bool _initialized = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target != null)
        {
            MoveTracked(_target);
        }
        else
        {
            if (_initialized == true)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Initialize(Damageable target)
    {
        base.Initialize();
        _initialized = true;
        _target = target;
    }

    private void MoveTracked(Damageable damageable)
    {
        Vector3 direction = Vector3.Normalize(damageable.transform.position - transform.position);
        transform.position += _moveSpeed * Time.deltaTime * direction;
    }
}
