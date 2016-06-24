// Copyright (c) Rotorz Limited. All rights reserved.

using UnityEngine;

namespace Rotorz.Demos.HatGuyDemo {

	public class HatGuyJoystick : MonoBehaviour {

		public Rect pixelInset;							// Position and size on screen

		public Vector2 touchZoneSize = Vector2.zero;	// Size of screen to use for touch pad
		public Vector2 padding = Vector2.zero;			// Padding can be used to make it easier to get
		// 100% on axis

		public bool normalize = true;					// Normalize magnitude within range -1 to +1

		[HideInInspector]
		public Vector2 position = Vector2.zero;			// Position (0, 0) indicates no movement
		[HideInInspector]
		public Vector2 previousPosition = Vector2.zero;	// Previous position value
		public float motionThreshold = 0.0f;			// Threshold that breaks initial motion constraint
		// (Zero never breaks)

		public AnimationCurve motionCurve;				// Curve to smooth movement axis
		// (Only applies to normalized axis)
		public bool useMotionCurve = false;				// Indicates if motion curve should be used

		private int _latchedFingerId = -1;				// Identifier associated with latched finger
		private Rect _touchZone;						// Touch zone rectangle
		private Rect _paddedZone;						// Padded zone rectangle (smaller zone)

		private Vector2 _touchDownPosition;				// Position where touch started
		private bool _brokenThreshold = false;			// Indicates if initial motion threshold has broken

		private void Start() {
			float scale = 1.0f;

			int posX, posY;

			if (SystemInfo.deviceModel == "iPad") {
				pixelInset.x = 29;
				pixelInset.y = 189;
				//padding.x *= 2;
				//padding.y *= 2;
				scale = 0.7f;
			}
			else {
				scale = Mathf.Min(1.0f, Screen.width / 960.0f);
			}

			pixelInset.x *= scale;
			pixelInset.y *= scale;
			pixelInset.width *= scale;
			pixelInset.height *= scale;

			posX = (int)pixelInset.x;
			posY = Screen.height - (int)pixelInset.y - (int)pixelInset.height;

			// Transform scale of D-Pad texture
			float midX = posX + pixelInset.width * 0.5f;
			float midY = Screen.height - (posY + pixelInset.height * 0.5f);

			// Apply device specific scale to padding
			padding.x *= scale;
			padding.y *= scale;
			// Apply device specific scale to touch zone size
			touchZoneSize.x *= scale;
			touchZoneSize.y *= scale;

			// Calculate coordinates of touch zone
			_touchZone = new Rect(
				midX - touchZoneSize.x * 0.5f - 10.0f,
				midY - touchZoneSize.y * 0.5f,
				touchZoneSize.x,
				touchZoneSize.y
			);

			// Calculate coordinates of padded zone
			_paddedZone = new Rect(
				_touchZone.x + padding.x,
				_touchZone.y + padding.y,
				_touchZone.width - padding.x * 2,
				_touchZone.height - padding.y * 2
			);
		}

		public Vector2 TransformToPosition(Vector2 position) {
			position.x = Mathf.Clamp(position.x, _paddedZone.xMin, _paddedZone.xMax) - _paddedZone.xMin - (_paddedZone.width / 2.0f);
			position.y = Mathf.Clamp(position.y, _paddedZone.yMin, _paddedZone.yMax) - _paddedZone.yMin - (_paddedZone.height / 2.0f);

			position.x = (2.0f * position.x) / _paddedZone.width;
			position.y = (2.0f * position.y) / _paddedZone.height;

			if (normalize && position.magnitude > 1.0f)
				position.Normalize();

			if (useMotionCurve) {
				position.x *= motionCurve.Evaluate(Mathf.Abs(position.x));
				position.y *= motionCurve.Evaluate(Mathf.Abs(position.y));
			}

			if (Mathf.Abs(position.x) < 0.01f)
				position.x = 0.0f;
			if (Mathf.Abs(position.y) < 0.01f)
				position.y = 0.0f;

			return position;
		}

		private void Update() {
			// Skip when game is paused
			if (Time.timeScale == 0.0f)
				return;

			Touch? latchedTouch = null;

			// If already latched to finger, determine whether still latched
			foreach (var touch in Input.touches)
				if (touch.fingerId == _latchedFingerId && _touchZone.Contains(touch.position)) {
					latchedTouch = touch;
					break;
				}

			// If not latched to finger...
			if (latchedTouch == null) {
				// Release latch of this user interface?
				if (_latchedFingerId != -1)
					ResetJoystick();

				// Search for finger to latch with
				foreach (var touch in Input.touches) {
					// Should finger latch to user interface?
					if (_touchZone.Contains(touch.position)) {
						// Latched to touch!
						_latchedFingerId = touch.fingerId;
						latchedTouch = touch;

						// Initial touch position
						_touchDownPosition = TransformToPosition(touch.position);
						position = _touchDownPosition;

						// Has broken threshold if threshold is not being used!
						_brokenThreshold = motionThreshold == 0.0f;
						// Has threshold already been broken?
						if (!_brokenThreshold) {
							// Already broken if both axis have exceeded threshold (diagonal) ?
							if ((Mathf.Abs(position.x) > motionThreshold && Mathf.Abs(position.y) > motionThreshold)
								|| (Mathf.Abs(position.x) < 0.45f && Mathf.Abs(position.y) < 0.45f)) {
								_brokenThreshold = true;
							}
							else {
								if (Mathf.Abs(position.x) < motionThreshold)
									position.x = 0.0f;
								if (Mathf.Abs(position.y) < motionThreshold)
									position.y = 0.0f;
							}
						}
						break;
					}
				}
			}

			// If latched, update position!
			if (latchedTouch != null) {
				// Calculate new position
				previousPosition = position;
				position = TransformToPosition(latchedTouch.Value.position);

				if (!_brokenThreshold) {
					// Restore previous position if initial motion threshold constraint not broken
					if (Vector2.Distance(previousPosition, position) > motionThreshold)
						_brokenThreshold = true;
					else
						position = previousPosition;
				}
			}
		}

		private void ResetJoystick() {
			previousPosition = Vector2.zero;
			position = Vector2.zero;

			_touchDownPosition = Vector2.zero;
			_latchedFingerId = -1;
			_brokenThreshold = false;
		}

		public void ReleaseLatchOf(int fingerId) {
			if (_latchedFingerId == fingerId)
				ResetJoystick();
		}

		public bool isLatched {
			get { return _latchedFingerId != -1; }
		}
		public int latchedFingerId {
			get { return _latchedFingerId; }
		}

	}

}
