namespace GSGD1
{
	using System.Collections;
	using System.Collections.Generic;
	using Unity.Collections;
	using UnityEngine;

	public class PathFollower : MonoBehaviour
	{
		[SerializeField]
		private Path _path = null;

		[SerializeField]
		private float _moveSpeed = 1f;

		public float moveSpeed => _moveSpeed;

		private float _speedMultiplier = 1f;

		public float speedMultiplier
        {
            get
            {
				return _speedMultiplier;
            }
            set
            {
				_speedMultiplier = value;
            }
        }

		[SerializeField]
		private float _rotateSpeed = 1f;

		[SerializeField]
		private float _distanceThreshold = 0.5f;

		[System.NonSerialized]
		private int _currentPathIndex = 0;

		private Vector3 _direction = Vector3.zero;

		public Vector3 velocity => _direction;

		Timer slowTimer = null;

		public void SetCanMove(bool canMove)
		{
			this.enabled = canMove;
		}

		public void SetPath(Path path, bool teleportToFirstWaypoint = true)
		{
			_path = path;
			if (teleportToFirstWaypoint == true)
			{
				Transform firstWaypoint = _path.FirstWaypoint;
				if (firstWaypoint != null)
				{
					transform.position = firstWaypoint.position;
				}
			}
		}
        private void OnEnable()
        {
			slowTimer = new Timer();
			slowTimer.OnEndCallback -= OnSlowTimerEnded;
			slowTimer.OnEndCallback += OnSlowTimerEnded;
		}

		public void SlowPathFollower(float duration, float slowAmount)
        {
			_speedMultiplier = slowAmount;
			slowTimer.Set(duration, true);	
        }

		private void OnSlowTimerEnded()
        {
			_speedMultiplier = 1f;
			slowTimer.OnEndCallback -= OnSlowTimerEnded;
        }



		private void Update()
		{
			slowTimer.Update();

			if (_path == null || _currentPathIndex >= _path.Waypoints.Count)
			{
				//Destroy(gameObject);
				return;
			}

			Vector3 nextDestination = _path.Waypoints[_currentPathIndex].position;

			if (Vector3.Distance(transform.position, nextDestination) < _distanceThreshold)
			{
				_currentPathIndex = _currentPathIndex + 1;
				return;
			}

			MoveTo(nextDestination);
			LookAt(nextDestination);
		}

		private void MoveTo(Vector3 position)
		{
			Vector3 movement = (position - transform.position).normalized * _moveSpeed * _speedMultiplier * Time.deltaTime;
			_direction = (position - transform.position).normalized * _moveSpeed * _speedMultiplier * Time.deltaTime;
			transform.position += movement;
		}

		private void LookAt(Vector3 position)
		{
			//transform.LookAt(position, Vector3.up);

			Vector3 direction = position - transform.position;
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
			transform.rotation =  Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
		}
	}
}