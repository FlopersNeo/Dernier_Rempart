using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSGD1;

public class ATower : MonoBehaviour, IPickerGhost, ICellChild
{
	[SerializeField]
	protected DamageableDetector _damageableDetector = null;

	protected Damageable _target = null;

	public Damageable target { get { return _target; } }

	[SerializeField]
	public int _cost = 0;

	private bool _canBeSell = false;

	private bool _canBeUpgrade = false;

    protected virtual void Awake()
	{
		enabled = false;
	}

	public virtual void Enable(bool isEnabled)
	{
		enabled = isEnabled;
	}

	// Interfaces
	public Transform GetTransform()
	{
		return transform;
	}

	public void OnSetChild()
	{
		ThunasseManager.Instance.SpentMoney(_cost);
		_canBeUpgrade = true;
		_canBeSell = true;
		Enable(true);
	}
}
