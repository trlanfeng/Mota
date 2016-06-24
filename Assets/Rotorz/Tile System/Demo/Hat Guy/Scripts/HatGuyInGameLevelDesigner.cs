// Copyright (c) Rotorz Limited. All rights reserved.

using Rotorz.Tile;
using UnityEngine;

namespace Rotorz.Demos.HatGuyDemo {

	public class HatGuyInGameLevelDesigner : MonoBehaviour {

		// Large scale tile system to paint on
		public TileSystem largeSystem;
		// Small scale tile system to paint on
		public TileSystem smallSystem;

		// Brush to use when painting with large system
		public Brush largeBrush;
		// Brush to use when painting with small system
		public Brush smallBrush;

		// Material to use for large-scale grid
		public Material largeGridMaterial;
		// Material to use for small-scale grid
		public Material smallGridMaterial;

		// Indicates if large system is active
		private bool _usingLarge;
		// Reference to active tile system
		private TileSystem _currentSystem;
		// Reference to active brush
		private Brush _brush;

		// Index of last tile painted (to avoid overpaint)
		private TileIndex _lastPainted;

		// Gets or sets a value indicating whether large tile system is active.
		public bool largeSystemActive {
			get { return _usingLarge; }
			set {
				if (value == _usingLarge)
					return;

				var renderer = GetComponent<Renderer>();

				_usingLarge = value;
				if (_usingLarge) {
					// Switch to large tile system
					_currentSystem = largeSystem;
					renderer.material = largeGridMaterial;

					// Use large brush
					_brush = largeBrush;
				}
				else {
					// Switch to small tile system
					_currentSystem = smallSystem;
					renderer.material = smallGridMaterial;

					// Use small brush
					_brush = smallBrush;
				}
			}
		}

		private void Start() {
			// Use large system be default
			_currentSystem = largeSystem;
			_usingLarge = true;
			_brush = largeBrush;

			// Ignore last painted state
			_lastPainted = new TileIndex(-1, -1);
		}

		private void Update() {
			bool leftButton = Input.GetMouseButton(0);
			bool rightButton = Input.GetMouseButton(1);

			// Switch between grids when space key is pressed
			if (Input.GetKeyDown(KeyCode.Space))
				largeSystemActive = !largeSystemActive;

			// Respond to mouse event?
			if (leftButton || rightButton) {
				// Find mouse position in 3D space
				Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
				TileIndex index = _currentSystem.ClosestTileIndexFromRay(mouseRay);

				if (leftButton && _lastPainted != index) {
					// Ignore if this tile was painted last!
					_lastPainted = index;

					// Paint with left mouse button
					_brush.Paint(_currentSystem, index.row, index.column);
					_currentSystem.RefreshSurroundingTiles(index.row, index.column);
				}
				else if (rightButton) {
					// Ignore last painted state
					_lastPainted = new TileIndex(-1, -1);

					// Erase with right mouse button
					_currentSystem.EraseTile(index.row, index.column);
					_currentSystem.RefreshSurroundingTiles(index.row, index.column);
				}
			}
		}

		private void OnGUI() {
			GUILayout.Label("Paint - Left mouse\nErase - Right mouse\nToggle Brush - Space bar");
		}

	}

}
