﻿// Copyright (c) Rotorz Limited. All rights reserved.

//
// Integrates Rotorz Tile System into the Unity user interface.
//
// WARNING - Automatically generated upon changing 'Preferred Language'.
//

using UnityEditor;
using UnityEngine;

namespace Rotorz.Tile.Editor.Internal {

	[InitializeOnLoad]
	internal static class UnityIntegration {

		static UnityIntegration() {
			UnityIntegrationUtility.CheckPreferredLanguage(LanguageCultureName, LanguageVer);
		}

		// Force assembly reload upon changing language even if unchanged!
		// 635588541859862162

		private static readonly string LanguageCultureName = "en-US";
		private static readonly string LanguageVer = "2.4.0";

		#region Tool Menu Commands

		[MenuItem("CONTEXT/_RTS_TOOLS_/Create Tile System...", false, 0)]
		[MenuItem("GameObject/Create Other/Rotorz Tile System...")]
		private static void ToolMenu_CreateTileSystem() {
			UnityIntegrationUtility.ToolMenu_CreateTileSystem();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Rotorz Brush or Tileset...", false, 0)]
		[MenuItem("Assets/Create/Rotorz Brush or Tileset...")]
		private static void ToolMenu_CreateBrushOrTileset() {
			UnityIntegrationUtility.ToolMenu_CreateBrushOrTileset();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Use as Prefab Offset", false, 100)]
		private static void ToolMenu_UseAsPrefabOffset() {
			UnityIntegrationUtility.ToolMenu_UseAsPrefabOffset();
		}
		[MenuItem("CONTEXT/_RTS_TOOLS_/Use as Prefab Offset", true)]
		private static bool ToolMenu_UseAsPrefabOffset_Validate() {
			return UnityIntegrationUtility.ToolMenu_UseAsPrefabOffset_Validate();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Replace by Brush...", false, 100)]
		private static void ToolMenu_ReplaceByBrush() {
			UnityIntegrationUtility.ToolMenu_ReplaceByBrush();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Build Scene...", false, 100)]
		private static void ToolMenu_BuildScene() {
			UnityIntegrationUtility.ToolMenu_BuildScene();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Rescan Brushes", false, 200)]
		private static void ToolMenu_RescanBrushes() {
			UnityIntegrationUtility.ToolMenu_RescanBrushes();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Editor Windows/Designer", false, 5000)]
		private static void ToolMenu_EditorWindows_Designer() {
			UnityIntegrationUtility.ToolMenu_EditorWindows_Designer();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Editor Windows/Scene", false, 5000)]
		private static void ToolMenu_EditorWindows_Scene() {
			UnityIntegrationUtility.ToolMenu_EditorWindows_Scene();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Editor Windows/Brushes", false, 5000)]
		private static void ToolMenu_EditorWindows_Brushes() {
			UnityIntegrationUtility.ToolMenu_EditorWindows_Brushes();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/Home Page", false, 5000)]
		private static void ToolMenu_OnlineResources_HomePage() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_HomePage();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/User Guide", false, 5100)]
		private static void ToolMenu_OnlineResources_UserGuide() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_UserGuide();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/API Reference", false, 5100)]
		private static void ToolMenu_OnlineResources_API() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_API();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/Release Notes", false, 5100)]
		private static void ToolMenu_OnlineResources_ReleaseNotes() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_ReleaseNotes();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/YouTube Channel", false, 5200)]
		private static void ToolMenu_OnlineResources_YouTubeChannel() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_YouTubeChannel();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/Twitter Profile", false, 5200)]
		private static void ToolMenu_OnlineResources_TwitterProfile() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_TwitterProfile();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Online Resources/Facebook Profile", false, 5200)]
		private static void ToolMenu_OnlineResources_FacebookProfile() {
			UnityIntegrationUtility.ToolMenu_OnlineResources_FacebookProfile();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/Preferences...", false, 5000)]
		private static void ToolMenu_Preferences() {
			UnityIntegrationUtility.ToolMenu_Preferences();
		}

		[MenuItem("CONTEXT/_RTS_TOOLS_/About...", false, 5001)]
		private static void ToolMenu_About() {
			UnityIntegrationUtility.ToolMenu_About();
		}

		#endregion

		#region Context Menu Commands - TileSystem Component

		[MenuItem("CONTEXT/TileSystem/Toggle Lock", false, 2000)]
		private static void ContextMenu_TileSystem_ToggleLock(MenuCommand command) {
			UnityIntegrationUtility.ContextMenu_TileSystem_ToggleLock(command);
		}

		#endregion

		#region Context Menu Commands - Transform Component

		[MenuItem("CONTEXT/Transform/Select Tile System", false, 2000)]
		private static void ContextMenu_Transform_SelectTileSystem() {
			UnityIntegrationUtility.ContextMenu_Transform_SelectTileSystem();
		}

		[MenuItem("CONTEXT/Transform/Select Tile System", true)]
		private static bool ContextMenu_Transform_SelectTileSystem_Validate() {
			return UnityIntegrationUtility.ContextMenu_Transform_SelectTileSystem_Validate();
		}

		[MenuItem("CONTEXT/Transform/Use as Prefab Offset", false, 2000)]
		private static void ContextMenu_Transform_UseAsPrefabOffset() {
			UnityIntegrationUtility.ContextMenu_Transform_UseAsPrefabOffset();
		}

		[MenuItem("CONTEXT/Transform/Use as Prefab Offset", true)]
		private static bool ContextMenu_Transform_UseAsPrefabOffset_Validate() {
			return UnityIntegrationUtility.ContextMenu_Transform_UseAsPrefabOffset_Validate();
		}

		#endregion

		#region Project Settings

		[MenuItem("Edit/Project Settings/Rotorz Tile System")]
		private static void Edit_ProjectSettings_RotorzTileSystem() {
			Selection.objects = new Object[] { ProjectSettings.Instance };
		}

		#endregion

	}

}
