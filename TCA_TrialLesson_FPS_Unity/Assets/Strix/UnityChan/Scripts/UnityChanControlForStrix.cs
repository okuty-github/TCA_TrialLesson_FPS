using System;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

namespace UnityChan
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class UnityChanControlForStrix : StrixBehaviour
	{
		// アニメーター各ステートへの参照
		private static int IDLE_STATE = Animator.StringToHash("Base Layer.Idle");
		private static int LOCO_STATE = Animator.StringToHash("Base Layer.Locomotion");
		private static int JUMP_STATE = Animator.StringToHash("Base Layer.Jump");
		private static int RESET_STATE = Animator.StringToHash("Base Layer.Rest");

		[SerializeField] private float _animSpeed = 1.5f;		// アニメーション再生速度設定
		[SerializeField] private bool _useCurves = true;		// Mecanimでカーブ調整を使うか設定する
		[SerializeField] private float _useCurvesHeight = 0.5f;	// カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）
		[SerializeField] private float _forwardSpeed = 7.0f;	// 前進速度
		[SerializeField] private float _backwardSpeed = 2.0f;	// 後退速度
		[SerializeField] private float _rotateSpeed = 2.0f;		// 旋回速度
		[SerializeField] private float _jumpPower = 3.0f;		// ジャンプ威力
		[SerializeField] private Transform _returnPoint = null;	// 復帰ポイント
		[SerializeField] private float _fallJudgmentY = -20.0f;	// 落下判定距離（Y座標）

		private CapsuleCollider _collider;
		private Rigidbody _rigidbody;
		private Vector3 _velocity;
		private float _orgColHight;
		private Vector3 _orgVectColCenter;
		private Animator _animator;
		private AnimatorStateInfo _currentBaseState;
		private Dictionary<int, Action>	_updateMap;
		
		private void Start()
		{
			_animator = GetComponent<Animator>();
			_collider = GetComponent<CapsuleCollider>();
			_rigidbody = GetComponent<Rigidbody>();
			_orgColHight = _collider.height;
			_orgVectColCenter = _collider.center;

			_updateMap = new Dictionary<int, Action>();
			_updateMap[IDLE_STATE] = UpdateIdle;
			_updateMap[LOCO_STATE] = UpdateLoco;
			_updateMap[JUMP_STATE] = UpdateJump;
			_updateMap[RESET_STATE] = UpdateReset;
		}

		private void FixedUpdate()
		{
			if (!isLocal)
			{
				return;
			}

			var horizontal = Input.GetAxis("Horizontal");
			var vertical = Input.GetAxis("Vertical");
			_animator.SetFloat("Speed", vertical);
			_animator.SetFloat("Direction", horizontal);
			_animator.speed = _animSpeed;
			_currentBaseState = _animator.GetCurrentAnimatorStateInfo(0);
			_rigidbody.useGravity = true;

			// 落下判定
			if (transform.position.y <= _fallJudgmentY)
			{
				transform.position = _returnPoint.position;
			}

			// 移動処理
			_velocity = new Vector3(0.0f, 0.0f, vertical);
			_velocity = transform.TransformDirection(_velocity);
			if (vertical > 0.1f)
			{
				_velocity *= _forwardSpeed;
			} else if (vertical < -0.1f) 
			{
				_velocity *= _backwardSpeed;
			}
		
			if (Input.GetButtonDown("Jump"))
			{
				if (_currentBaseState.fullPathHash == LOCO_STATE)
				{
					if (!_animator.IsInTransition(0))
					{
						_rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);
						_animator.SetBool("Jump", true);
					}
				}
			}

			transform.localPosition += _velocity * Time.fixedDeltaTime;
			transform.Rotate(0, horizontal * _rotateSpeed, 0.0f);

			var state = _currentBaseState.fullPathHash;
			if (_updateMap.ContainsKey(state))
			{
				var updateFunc = _updateMap[state];
				updateFunc.Invoke();
			}
		}

		private void UpdateIdle()
		{
			if (_useCurves)
			{
				ResetCollider();
			}
			if (Input.GetButtonDown("Jump"))
			{
				_animator.SetBool("Rest", true);
			}
		}

		private void UpdateLoco()
		{
			if (_useCurves)
			{
				ResetCollider();
			}
		}

		private void UpdateJump()
		{
			if (!_animator.IsInTransition(0))
			{
				if (_useCurves)
				{
					var jumpHeight = _animator.GetFloat("JumpHeight");
					var gravityControl = _animator.GetFloat("GravityControl");
					if (gravityControl > 0.0f)
					{
						_rigidbody.useGravity = false;
					}

					var ray = new Ray(transform.position + Vector3.up, -Vector3.up);
					var hitInfo = new RaycastHit();

					if (Physics.Raycast(ray, out hitInfo))
					{
						if (hitInfo.distance > _useCurvesHeight)
						{
							_collider.height = _orgColHight - jumpHeight;
							var adjCenterY = _orgVectColCenter.y + jumpHeight;
							_collider.center = new Vector3(0.0f, adjCenterY, 0.0f);
						}
						else
						{
							ResetCollider();
						}
					}
				}
				_animator.SetBool("Jump", false);
			}
		}

        private void UpdateReset()
        {
			if (!_animator.IsInTransition(0))
			{
				_animator.SetBool("Rest", false);
			}
		}

        private void ResetCollider()
		{
			_collider.height = _orgColHight;
			_collider.center = _orgVectColCenter;
		}
	}
}
