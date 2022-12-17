namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class ProjectileLauncher : AWeapon
	{
		[SerializeField]
		protected Projectile _projectile = null;

		public Projectile projectile => _projectile;

		[SerializeField]
		protected Transform _projectileAnchor = null;

		public Transform projectileAnchor => _projectileAnchor;

		protected override void DoFire()
		{
			var instance = Instantiate(_projectile, _projectileAnchor.position, _projectileAnchor.rotation);
		}
	}
}