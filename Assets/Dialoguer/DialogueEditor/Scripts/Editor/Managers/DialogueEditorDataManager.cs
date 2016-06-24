using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DialoguerEditor{
	public class DialogueEditorDataManager{
		private static DialogueEditorDataManager __instance;
		private static DialogueEditorMasterObject __data;
		
		private DialogueEditorDataManager() {}
		
		public static DialogueEditorMasterObject data{
			get{
				if(__data == null){
					load();
				}
				return __data;
			}
			
			private set{
				__data = value;
			}
		}
		
		public static void save(){
			bool outputFolderExists = System.IO.Directory.Exists(@DialogueEditorFileStatics.ASSETS_FOLDER_PATH+"/"+DialogueEditorFileStatics.OUTPUT_FOLDER_PATH);
			if(!outputFolderExists) AssetDatabase.CreateFolder(DialogueEditorFileStatics.ASSETS_FOLDER_PATH, DialogueEditorFileStatics.OUTPUT_FOLDER_PATH);
			bool resourcesFolderExists = System.IO.Directory.Exists(@DialogueEditorFileStatics.ASSETS_FOLDER_PATH+"/"+DialogueEditorFileStatics.OUTPUT_FOLDER_PATH+"/"+DialogueEditorFileStatics.OUTPUT_RESOURCES_FOLDER_PATH);
			if(!resourcesFolderExists) AssetDatabase.CreateFolder(DialogueEditorFileStatics.ASSETS_FOLDER_PATH+"/"+DialogueEditorFileStatics.OUTPUT_FOLDER_PATH, DialogueEditorFileStatics.OUTPUT_RESOURCES_FOLDER_PATH);
				
			AssetDatabase.DeleteAsset(DialogueEditorFileStatics.PATH + DialogueEditorFileStatics.DIALOGUE_DATA_FILENAME);
			XmlSerializer serializer = new XmlSerializer(typeof(DialogueEditorMasterObject));
			TextWriter textWriter = new StreamWriter(@DialogueEditorFileStatics.PATH + DialogueEditorFileStatics.DIALOGUE_DATA_FILENAME);
			serializer.Serialize(textWriter, data);
			textWriter.Close();
			AssetDatabase.Refresh();
		}
		
		private static void load(){
			bool assetExists = System.IO.File.Exists(@DialogueEditorFileStatics.PATH + DialogueEditorFileStatics.DIALOGUE_DATA_FILENAME);
			if(assetExists){
				//Debug.Log ("Ouput Folder Exists: Loading Data");
				data = null;
				XmlSerializer deserializer = new XmlSerializer(typeof(DialogueEditorMasterObject));
				TextReader textReader = new StreamReader(@DialogueEditorFileStatics.PATH + DialogueEditorFileStatics.DIALOGUE_DATA_FILENAME);
				data = (DialogueEditorMasterObject)deserializer.Deserialize(textReader);
				textReader.Close();
			}else{
				//Debug.Log("Output Folder Does Not Exist: Creating New Folders");
				data = new DialogueEditorMasterObject();
				save();
				AssetDatabase.Refresh();
			}
		}
		
		
		// REMOVE THIS
		public static void debugLoad(){
			load();
		}
	}
}