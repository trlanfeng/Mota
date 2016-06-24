using UnityEngine;
using System.Collections;

public class DialoguerTestSceneMain : MonoBehaviour {
	
	public UnityDefaultGuiManager unityDialogue;
	public OldSchoolRPGDialogueGUI oldRpgDialogue;
	public NextGenRpgDialogueGui nextGenRpgDialogue;
	
	private readonly string GLOBAL_VARIABLE_SAVE_KEY = "serialized_global_variable_state";
	
	//private string returnedString = string.Empty;
	
	void Start () {
		
		// Initialize the Dialoguer
		Dialoguer.Initialize();
		
		// If the Global Variables state already exists, LOAD it into Dialoguer
		if(PlayerPrefs.HasKey(GLOBAL_VARIABLE_SAVE_KEY)){
			Dialoguer.SetGlobalVariablesState(PlayerPrefs.GetString(GLOBAL_VARIABLE_SAVE_KEY));
			//returnedString = PlayerPrefs.GetString(GLOBAL_VARIABLE_SAVE_KEY);
		}
		//		This can be saved anywhere, and loaded from anywhere the user wishes
		//		To save the Global Variable State, get it with Dialoguer.GetGlobalVariableState() and save it where you wish
	}
	
	void Update () {
		//returnedString = Dialoguer.GetGlobalVariablesState();
	}
	
	void OnGUI(){
		GUI.depth = -10;
		
		if(GUI.Button (new Rect(25, 25, 125, 30), "Unity GUI")){
			Dialoguer.events.ClearAll();
			unityDialogue.addDialoguerEvents();
			Dialoguer.StartDialogue(0);	// Generic
		}
		
		if(GUI.Button (new Rect(25, 25 + 30 + 10, 125, 30), "Old School RPG")){
			Dialoguer.events.ClearAll();
			oldRpgDialogue.addDialoguerEvents();
			Dialoguer.StartDialogue(DialoguerDialogues.OldSchoolRPG);	// Old SchoolRPG
		}
		
		if(GUI.Button (new Rect(25, 25 + 60 + 20, 125, 30), "NextGen RPG")){
			Dialoguer.events.ClearAll();
			nextGenRpgDialogue.addDialoguerEvents();
			Dialoguer.StartDialogue(2);	// Old SchoolRPG
		}
	}
}
