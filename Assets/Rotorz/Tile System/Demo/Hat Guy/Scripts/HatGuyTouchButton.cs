// Copyright (c) Rotorz Limited. All rights reserved.

using UnityEngine;

namespace Rotorz.Demos.HatGuyDemo {

	public class HatGuyTouchButton : MonoBehaviour {

		[HideInInspector]
		public bool isButtonDown = false;			// Indicates if button was just pressed down
		[HideInInspector]
		public bool isPressed = false;				// Indicates if button is pressed

		private void Update() {
			// Assume button is not pressed and then attempt to prove otherwise
			isButtonDown = false;
			isPressed = false;

			for (int i = 0; i < Input.touchCount; ++i) {
				var touch = Input.GetTouch(i);

				// Button represents second half of screen
				if (touch.position.x > (float)Screen.width / 2.0f) {
					isButtonDown = touch.phase == TouchPhase.Began;
					isPressed = true;
					break;
				}
			}
		}

	}

}
