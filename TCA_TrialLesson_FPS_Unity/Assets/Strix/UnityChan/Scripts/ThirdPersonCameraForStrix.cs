using UnityEngine;

namespace UnityChan
{
	public class ThirdPersonCameraForStrix : MonoBehaviour
	{
		[SerializeField] private float _smooth = 3.0f;

		private Transform _target = null;
		private bool _isQuickSwitch = false;

		private void Start()
		{
			_target = GameObject.Find("CamPos").transform;
			transform.position = _target.position;
			transform.forward = _target.forward;
		}
	
		private void FixedUpdate()
		{
			SetCameraPositionNormalView();
		}

		private void SetCameraPositionNormalView()
		{
			if (!_isQuickSwitch)
			{
				var time = Time.fixedDeltaTime * _smooth;
				transform.position = Vector3.Lerp(transform.position, _target.position, time);
				transform.forward = Vector3.Lerp(transform.forward, _target.forward, time);
			}
			else
			{
				transform.position = _target.position;	
				transform.forward = _target.forward;
				_isQuickSwitch = false;
			}
		}
	}
}
