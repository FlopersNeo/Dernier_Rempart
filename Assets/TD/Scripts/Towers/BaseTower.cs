namespace GSGD1
{
	using GSGD1;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Facade for Tower subsystems
	/// </summary>
	public class BaseTower : ATower, IPickerGhost, ICellChild
	{
		[SerializeField]
		protected WeaponController _weaponController = null;

		protected virtual void Update()
		{
			if (_damageableDetector.HasAnyDamageableInRange() == true)
			{
				Damageable damageableTarget = _damageableDetector.GetDamageableSortedByDistanceAscending()[0];
				_target = damageableTarget;
                //_weaponController.LookAt(damageableTarget.GetAimPosition());
                //_weaponController.Fire();
                if (damageableTarget.GetAimPosition() != null)
                {
					_weaponController.LookAtAndFire(damageableTarget.GetAimPosition());
                }
			}
		}
	}
}