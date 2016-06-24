// Copyright (c) Rotorz Limited. All rights reserved.

using UnityEngine;

namespace Rotorz.Demos.HatGuyDemo {

	public class HatGuyCharacterController : MonoBehaviour {
		
		// Virtual joystick for movement
		public HatGuyJoystick virtualJoystick;
		// Virtual jump button
		public HatGuyTouchButton virtualButton;

		// Walking speed
		public float walkSpeed = 3.5f;
		// Minimum speed to apply when walking
		public float minimumWalkSpeed = 1.5f;
		// Force to apply upon jumping
		public float jumpForce = 5.9f;
		// Speed to turn from side to side
		public float turnSpeed = 3.5f;

		// Indicates if player is facing right
		private bool _facingRight = true;
		// Indicates if player is turning
		private bool _turning = false;

		private Quaternion _leftRotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
		private Quaternion _rightRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
		private float _rotationAmount;

		private Rigidbody _rigidbody;
		private Animator _animator;

		private void Start() {
			_rigidbody = GetComponent<Rigidbody>();
			_animator = GetComponent<Animator>();

			// Automatically rotate player to initial direction
			if (_facingRight) {
				transform.rotation = _rightRotation;
				_rotationAmount = +1.0f;
			}
			else {
				transform.rotation = _leftRotation;
				_rotationAmount = -1.0f;
			}

			var virtualJoystickGameObject = GameObject.Find("Virtual Joystick");
			if (virtualJoystickGameObject != null) {
				virtualJoystick = virtualJoystickGameObject.GetComponent<HatGuyJoystick>();
				virtualButton = virtualJoystickGameObject.GetComponent<HatGuyTouchButton>();
			}

#if !(UNITY_IPHONE || UNITY_ANDROID)
			// Virtual joystick not needed for non-touch devices!
			if (virtualJoystick != null)
				Destroy(virtualJoystick.gameObject);
#endif
		}

		private void Update() {
			Vector3 eulerAngles = transform.eulerAngles;

			// Velocity will be zero unless proven otherwise
			float velocity = 0.0f;

			float horizontal = Input.GetAxis("Horizontal");
#if (UNITY_IPHONE || UNITY_ANDROID)
			if (virtualJoystick != null)
				horizontal += virtualJoystick.position.x;
#endif

			if (horizontal != 0.0f) {
				// Does player need to turn around?
				if (horizontal > 0.0f && eulerAngles.y != 90.0f) {
					// Begin turning if not facing right direction
					if (!_facingRight) {
						_turning = true;
						_facingRight = true;
					}
				}
				else if (horizontal < 0.0f && eulerAngles.y != -90.0f) {
					// Begin turning if not facing right direction
					if (_facingRight) {
						_turning = true;
						_facingRight = false;
					}
				}

				// Only attempt to move player if there is nothing in the way!
				if (!Physics.Raycast(transform.position, transform.forward, 0.4f)) {
					// Calculate movement velocity
					velocity = Mathf.Max(minimumWalkSpeed, Mathf.Abs(horizontal) * walkSpeed) * Time.deltaTime;
					if (horizontal < 0.0f)
						velocity = -velocity;

					// Move player forwards
					transform.Translate(velocity, 0.0f, 0.0f, Space.World);
				}
			}

			if (_turning) {
				float t, rotateSpeed = turnSpeed * Time.deltaTime;

				// Perform smooth rotation
				if (_facingRight) {
					// Rotate from left to right
					t = Mathf.Min(1.0f, (_rotationAmount + 1.0f) / 2.0f + rotateSpeed);
					_rotationAmount = t * 2.0f - 1.0f;
					transform.rotation = Quaternion.Lerp(_leftRotation, _rightRotation, t);
				}
				else {
					// Rotate from right to left
					t = Mathf.Min(1.0f, (2.0f - (_rotationAmount + 1.0f)) / 2.0f + rotateSpeed);
					_rotationAmount = (2.0f - (t * 2.0f)) - 1.0f;
					transform.rotation = Quaternion.Lerp(_rightRotation, _leftRotation, t);
				}

				if (t == 1.0f)
					_turning = false;
			}

#if (UNITY_IPHONE || UNITY_ANDROID)
			bool jumpButtonDown = virtualButton.isButtonDown;
#else
			bool jumpButtonDown = Input.GetButtonDown("Fire1");
#endif

			// Jump?
			if (jumpButtonDown) {
				// Can only jump if is touching ground!
				if (Physics.Raycast(transform.position, Vector3.down, 0.5f)) {
					// Apply force to rigidbody
					_rigidbody.AddForce(0.0f, jumpForce, 0.0f, ForceMode.VelocityChange);
				}
			}

			// Reflect current velocity in animator controller.
			_animator.SetFloat("Velocity", Mathf.Abs(velocity));
		}
	}

}
