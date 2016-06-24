using UnityEngine;
using UnityEditor;
using System.Collections;

public class DialogueEditorHelp : Editor {

	public const int PRIORITY = 2000;

	[MenuItem ("Dialoguer/Official Website", false, PRIORITY)]
	static void Website(){
		Application.OpenURL("http://www.dialoguer.info");
	}

	[MenuItem ("Dialoguer/Documentation", false, PRIORITY)]
	static void Documentation(){
		Application.OpenURL("http://www.dialoguer.info/docs.php");
	}

	[MenuItem ("Dialoguer/Nodes", false, PRIORITY)]
	static void Nodes(){
		Application.OpenURL("http://www.dialoguer.info/nodes.php");
	}

	[MenuItem ("Dialoguer/Code", false, PRIORITY)]
	static void Code(){
		Application.OpenURL("http://www.dialoguer.info/doce.php");
	}

	[MenuItem ("Dialoguer/Contact", false, PRIORITY)]
	static void Contact(){
		Application.OpenURL("mailto:email@dialoguer.info");
	}
}
