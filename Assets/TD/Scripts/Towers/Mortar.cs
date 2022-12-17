using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class Mortar : ATower, IPickerGhost, ICellChild
{
	[SerializeField]
	protected MortarWeaponController _mortarWeaponController = null;

	private void Update()
	{
		if (_damageableDetector.HasAnyDamageableInRange() == true)
		{
		_mortarWeaponController.target = _damageableDetector.GetDamageableSortedByOrder()[0];
			if (_mortarWeaponController.target.GetAimPosition() != null)
			{
				_mortarWeaponController.Fire();
			}
		}
	}
}
