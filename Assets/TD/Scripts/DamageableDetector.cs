//
//Created by Leo CUSSERNE
//
namespace GSGD1
{
	using System.Collections.Generic;
	using UnityEngine;

	public class DamageableDetector : MonoBehaviour
	{
		//[System.NonSerialized]
		[SerializeField]
		private List<Damageable> _damageablesInRange = new List<Damageable>();

		public bool HasAnyDamageableInRange()
		{
			return _damageablesInRange.Count > 0;
		}
		public List<Damageable> GetDamageableSortedByOrder()
		{
			if (HasAnyDamageableInRange() == true)
			{
				return _damageablesInRange;
			}
			else
			{
				return null;
			}
		}

		public List<Damageable> GetDamageableSortedByDistanceAscending()
		{
			List<Damageable> damageablesInRangeSortingGroup = _damageablesInRange;

			damageablesInRangeSortingGroup.Sort(
                delegate (Damageable x, Damageable y) 
				{
                    if (((x.transform.position - transform.position).sqrMagnitude) < ((y.transform.position - transform.position).sqrMagnitude))
                    {
						return -1;
                    }

					if (((x.transform.position - transform.position).sqrMagnitude) > ((y.transform.position - transform.position).sqrMagnitude))
                    {
						return 1;
                    }
                    else
                    {
						return 0;
                    }
				}
			);
			return damageablesInRangeSortingGroup;
        }

		public List<Damageable> GetDamageableSortedByHealthDescending()
        {
			List<Damageable> damageablesInRangeSortingGroup = _damageablesInRange;

			damageablesInRangeSortingGroup.Sort(
				delegate (Damageable x, Damageable y)
				{
					if (x.health > y.health)
					{
						return -1;
					}

					if (x.health < y.health)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			);
			return damageablesInRangeSortingGroup;
		}

		public List<Damageable> GetDamageableSortedByHealthAscending()
		{
			List<Damageable> damageablesInRangeSortingGroup = _damageablesInRange;

			damageablesInRangeSortingGroup.Sort(
				delegate (Damageable x, Damageable y)
				{
					if (x.health < y.health)
					{
						return -1;
					}

					if (x.health > y.health)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			);
			return damageablesInRangeSortingGroup;
		}

		public List<Damageable> GetDamageableSortedBySpeedDescending()
		{
			List<Damageable> damageablesInRangeSortingGroup = _damageablesInRange;

			damageablesInRangeSortingGroup.Sort(
				delegate (Damageable x, Damageable y)
				{
					if (x.GetComponentInParent<PathFollower>()?.moveSpeed > y.GetComponentInParent<PathFollower>()?.moveSpeed)
					{
						return -1;
					}

					if (x.GetComponentInParent<PathFollower>()?.moveSpeed < y.GetComponentInParent<PathFollower>()?.moveSpeed)
					{
						return 1;
					}
					else
					{
						return 0;
					}
				}
			);
			return damageablesInRangeSortingGroup;
		}

		private void OnTriggerEnter(Collider other)
		{
			Damageable damageable = other.GetComponentInParent<Damageable>();

			if (damageable != null && _damageablesInRange.Contains(damageable) == false)
			{
				damageable.DamageTaken -= Damageable_OnDamageTaken;
				damageable.DamageTaken += Damageable_OnDamageTaken;
				_damageablesInRange.Add(damageable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			Damageable damageable = other.GetComponentInParent<Damageable>();

			if (damageable != null && _damageablesInRange.Contains(damageable) == true)
			{
				damageable.DamageTaken -= Damageable_OnDamageTaken;
				_damageablesInRange.Remove(damageable);
			}
		}

		private void Damageable_OnDamageTaken(Damageable caller, int currentHealth, int damageTaken)
		{
			if (currentHealth <= 0)
			{
				_damageablesInRange.Remove(caller);
			}
		}

	}
}