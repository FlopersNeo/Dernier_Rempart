namespace GSGD1
{
	using UnityEngine;

	public class Damageable : MonoBehaviour
	{
		[SerializeField]
		private int _health = 1;

		public int health => _health;

		private int _maxHealth = 0;

		[SerializeField]
		private bool _destroyIfKilled = true;

		[SerializeField]
		private int _moneyEarnedByKill = 0;

		[SerializeField]
		private Transform _aimPosition = null;

		[SerializeField]
		private ParticleSystem _deathParticle = null;

		private Vector3 lastPosition = Vector3.zero;

		public delegate void DamageableEvent(Damageable caller, int currentHealth, int maxHealth);
		private event DamageableEvent _damageTaken = null;

        public event DamageableEvent DamageTaken
		{
			add
			{
				_damageTaken -= value;
				_damageTaken += value;
			}
			remove
			{
				_damageTaken -= value;
			}
		}


		public Vector3 GetAimPosition()
		{
			return _aimPosition.position;
		}

		public void TakeDamage(int damage)
		{
			_health -= damage;

			_damageTaken?.Invoke(this, _health, _maxHealth);
			if (_health <= 0)
			{
				if (_destroyIfKilled == true)
				{
					DoDestroy();
				}
			}
		}

		private void OnEnable()
        {
			_maxHealth = _health;
        }
		private void DoDestroy()
		{
			ThunasseManager.Instance.EarnMoney(_moneyEarnedByKill);
			var particle = Instantiate(_deathParticle);
			particle.transform.position = transform.position;
			Destroy(gameObject);
		}
    }
}