namespace GSGD1
{
	using UnityEngine;
	using LeoCusserneAPI;

	public class WeaponController : MonoBehaviour
	{
		[SerializeField]
		protected ProjectileLauncher _projectileLauncher = null;

		[SerializeField]
		private float _rotationSpeed = 5f;

		[SerializeField]
		private float _minAngleToFire = 10f;

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

		[System.NonSerialized]
		private Quaternion _lastLookRotation = Quaternion.identity;

		TurretUtilityFunctions turretUtilityFunctions = new TurretUtilityFunctions();

		public void LookAt(Vector3 position)
		{
			Vector3 direction = turretUtilityFunctions.PredictShotDirection(_target.GetAimPosition(),_target.GetComponentInParent<PathFollower>().velocity, _projectileLauncher.projectileAnchor.position, _projectileLauncher.projectile.projectileSpeed);
			_lastLookRotation = Quaternion.LookRotation(direction, Vector3.up);
			transform.rotation = Quaternion.Slerp(transform.rotation, _lastLookRotation, _rotationSpeed * Time.deltaTime);
		}

		public void Fire()
		{
			_projectileLauncher.Fire();
		}

		public void LookAtAndFire(Vector3 position)
		{
			LookAt(position);
			if (Quaternion.Angle(_lastLookRotation, transform.rotation) < _minAngleToFire)
			{
				Fire();
			}
		}

		private void LateUpdate()
		{
			_lastLookRotation = transform.rotation;
		}
	}
}