// Copyright (c) Rotorz Limited. All rights reserved.

using UnityEngine;

namespace Rotorz.Demos.HatGuyDemo {

	public class HatGuyDiamond : MonoBehaviour {

		private void OnTriggerEnter(Collider collider) {
			if (collider.CompareTag("Player"))
				Destroy(gameObject);
		}

	}

}
