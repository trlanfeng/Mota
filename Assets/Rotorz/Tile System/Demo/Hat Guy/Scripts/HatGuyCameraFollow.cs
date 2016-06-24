// Copyright (c) Rotorz Limited. All rights reserved.

using UnityEngine;

namespace Rotorz.Demos.HatGuyDemo {

	public class HatGuyCameraFollow : MonoBehaviour {

		// Transform of target object, defaults to player
		public Transform target;
		// Time in seconds for smoothing
		public float smoothTime = 0.5f;
		// Distance from target
		public float distance = 5.0f;

		// Velocity of camera smoothing
		private Vector3 _smoothVelocity;

		private void Start() {
			if (target == null) {
				// Point at player by default
				var playerGameObject = GameObject.FindWithTag("Player");
				if (playerGameObject != null)
					target = playerGameObject.transform;
			}
		}

		private void Update() {
			if (target != null) {
				// Point camera towards target
				Vector3 targetPosition = target.position;
				targetPosition.z -= distance;

				transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _smoothVelocity, smoothTime);
			}
		}
	}

}
