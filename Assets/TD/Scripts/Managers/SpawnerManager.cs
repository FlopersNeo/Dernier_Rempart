namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;

	public enum SpawnerIndex
	{
		Spawner00,
		Spawner01,
		Spawner02,
	}

	public enum SpawnerStatus
	{
		Inactive = 0,
		WaveRunning
	}

	public class SpawnerManager : MonoBehaviour
	{
		[SerializeField]
		private List<EntitySpawner> _spawners = null;

		[SerializeField]
		public int _moneyEarnedByWave = 0;

		[SerializeField]
		private bool _autoStartNextWaves = false;

		[SerializeField]
		public int _numberOfWaves = 0;

		[System.NonSerialized]
		private int _currentWaveSetIndex = -1;

		[System.NonSerialized]
		private int _currentWaveRunning = 0;

		public UnityEvent<SpawnerManager, SpawnerStatus, int> WaveStatusChanged_UnityEvent;

		public delegate void SpawnerEvent(SpawnerManager sender, SpawnerStatus status, int runningWaveCount);
		public event SpawnerEvent WaveStatusChanged = null;

		[ContextMenu("Start waves")]
		public void StartWaves()
		{
			// Start a new wave set only if there are no currently a wave running
			if (_currentWaveRunning <= 0)
			{
				StartNewWaveSet();
			}
		}

		public void StartNewWaveSet()
		{
			if(_numberOfWaves != 0)
            {
				ThunasseManager.Instance.EarnMoney(_moneyEarnedByWave);
			}
			ThunasseManager.Instance.EarnMoney(_moneyEarnedByWave);
			_numberOfWaves++;
			_currentWaveSetIndex += 1;
			var waveDatabase = DatabaseManager.Instance.WaveDatabase;

			if (waveDatabase.Waves.Count > _currentWaveSetIndex)
			{
				WaveSet waveSet = waveDatabase.Waves[_currentWaveSetIndex];
				List<Wave> waves = waveSet.Waves;

				for (int i = 0, length = _spawners.Count; i < length; i++)
				{
					if (waves.Count <= i)
					{
						Debug.LogWarningFormat("{0}.StartNewWaveSet() There are more wave ({1}) than available spawner ({2})", GetType().Name, waves.Count, i);
						break;
					}
					if (waves[i] == null)
					{
						Debug.LogWarningFormat("{0}.StartNewWaveSet() Null reference found in WaveSet at index {1}, ignoring.", GetType().Name, i);
						break;
					}
					_currentWaveRunning += 1;
					var spawner = _spawners[i];
					spawner.StartWave(waves[i]);
					spawner.WaveEnded.RemoveListener(Spawner_OnWaveEnded);
					spawner.WaveEnded.AddListener(Spawner_OnWaveEnded);

					WaveStatusChanged?.Invoke(this, SpawnerStatus.WaveRunning, _currentWaveRunning);
					WaveStatusChanged_UnityEvent?.Invoke(this, SpawnerStatus.WaveRunning, _currentWaveRunning);
				}
			}
			else
			{
				// No waves left : end game
			}
		}

		private void Spawner_OnWaveEnded(EntitySpawner entitySpawner, Wave wave)
		{
			entitySpawner.WaveEnded.RemoveListener(Spawner_OnWaveEnded);

			_currentWaveRunning -= 1;

			WaveStatusChanged?.Invoke(this, SpawnerStatus.Inactive, _currentWaveRunning);
			WaveStatusChanged_UnityEvent?.Invoke(this, SpawnerStatus.Inactive, _currentWaveRunning);

			// should we run a new wave?
			if (_autoStartNextWaves == true && _currentWaveRunning <= 0)
			{
				StartNewWaveSet();
			}
		}
	}
}