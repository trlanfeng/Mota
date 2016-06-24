using UnityEngine;
using System.Collections;

public class UnityDefaultGuiManager : MonoBehaviour {

	public const float HEIGHT = 200;
	public const float WIDTH = 500;

	private bool _showing = false;
	private bool _windowShowing = false;
	private bool _selectionClicked = false;
	
	//dialoguer information
	private string _windowText = string.Empty;
	private string[] _choices;
	
	// Use this for initialization
	void Start () {
		addDialoguerEvents();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI(){
		if(!_showing) return;
		if(!_windowShowing) return;
		
		GUI.depth = 10;
		
		Rect dialogueBoxRect = new Rect((Screen.width*0.5f) - (WIDTH * 0.5f), Screen.height - HEIGHT - 100, WIDTH, HEIGHT);
		
		Rect dialogueBackBoxRect = new Rect(dialogueBoxRect.x, dialogueBoxRect.y, dialogueBoxRect.width, dialogueBoxRect.height - (45*_choices.Length));
		GUI.Box(dialogueBackBoxRect, string.Empty);
		GUI.Label(new Rect(dialogueBackBoxRect.x + 10, dialogueBackBoxRect.y + 10, dialogueBackBoxRect.width - 20, dialogueBackBoxRect.height - 20), _windowText);
		
		if(_selectionClicked) return;
		
		for(int i = 0; i<_choices.Length; i+=1){
			Rect buttonRect = new Rect(dialogueBoxRect.x, dialogueBoxRect.yMax - (45*(_choices.Length - i)) + 5 , dialogueBoxRect.width, 40);
			if(GUI.Button(buttonRect, _choices[i])){
				_selectionClicked = true;
				Dialoguer.ContinueDialogue(i);
			}
		}
		
	}
	
	public void addDialoguerEvents(){
		Dialoguer.events.onStarted += onStartedHandler;
		Dialoguer.events.onEnded += onEndedHandler;
		Dialoguer.events.onInstantlyEnded += onInstantlyEndedHandler;
		Dialoguer.events.onTextPhase += onTextPhaseHandler;
		Dialoguer.events.onWindowClose += onWindowCloseHandler;
	}
	
	private void onStartedHandler(){
		//Debug.Log ("[GUI Manager] Started");
		_showing = true;
	}
	
	private void onEndedHandler(){
		//Debug.Log ("[GUI Manager] Ended");
		_showing = false;
	}
	
	private void onInstantlyEndedHandler(){
		_showing = true;
		_windowShowing = false;
		_selectionClicked = false;
	}
	
	private void onTextPhaseHandler(DialoguerTextData data){
		//Debug.Log ("[GUI Manager] Text Phase");
		_windowText = data.text;
		if(data.windowType == DialoguerTextPhaseType.Text){
			_choices = new string[1] {"Continue"};
		}else{
			_choices = data.choices;
		}
		_windowShowing = true;
		_selectionClicked = false;
	}
	
	private void onWindowCloseHandler(){
		//Debug.Log ("[GUI Manager] Window Closed");
		_windowShowing = false;
		_selectionClicked = false;
	}
}
