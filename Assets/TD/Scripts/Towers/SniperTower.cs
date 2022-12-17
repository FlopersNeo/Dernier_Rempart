//
//Created by Leo CUSSERNE
//
namespace GSGD1
{
	using GSGD1;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	/// <summary>
	/// Facade for Tower subsystems
	/// </summary>
	public class SniperTower : BaseTower, IPickerGhost, ICellChild
	{
		protected override void Update()
		{
			if (_damageableDetector.HasAnyDamageableInRange() == true)
			{
				List<Damageable> damageableTarget = _damageableDetector.GetDamageableSortedByHealthDescending();
                    if (damageableTarget[0] != null)
                    {
						if (damageableTarget[0].GetAimPosition() != null)
                        {
							_weaponController.target = damageableTarget[0];
							_weaponController.LookAtAndFire(damageableTarget[0].GetAimPosition());
                        }
                    }
			}
		}
	}
}