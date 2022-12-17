using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class TeslaTower : MonoBehaviour
{
	public class SniperTower : ATower, IPickerGhost, ICellChild
	{
		[SerializeField]
		protected WeaponController _weaponController = null;

		[SerializeField]
		protected int _numberOfEnnemies = 0;

		[SerializeField]
		private float _slowDuration = 2f;

		[SerializeField]
		private float _slowMultiplier = 0.8f;

		protected void Update()
		{
			if (_damageableDetector.HasAnyDamageableInRange() == true)
			{
				List<Damageable> damageableTarget = _damageableDetector.GetDamageableSortedBySpeedDescending();
				for (int i = 0; i < _numberOfEnnemies; i++)
				{
					damageableTarget[i].GetComponentInParent<PathFollower>().SlowPathFollower(_slowDuration, _slowMultiplier);
                }
			}
		}
	}
}
