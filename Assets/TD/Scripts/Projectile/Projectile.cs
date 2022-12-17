namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class Projectile : AProjectile
	{
		[SerializeField]
		private float _moveSpeed = 1f;

		public float projectileSpeed => _moveSpeed;

		protected virtual void Update()
		{
			MoveForward();
		}

		private void MoveForward()
		{
			transform.position = transform.position + _moveSpeed * Time.deltaTime * transform.forward;
		}

		public void IntitializeTimer()
        {

        }

        protected override void OnTriggerEnter(Collider other)
        {
			base.OnTriggerEnter(other);
        }
    }
}