using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeWindow {
	public Rect window;
	public string text;
	public bool hasTrigger;
	public string triggerString;
	public int triggerPriority;

	public int id;

	public static readonly int DEFAULT_WINDOW_WIDTH = 100;
	public static readonly int DEFAULT_WINDOW_HEIGHT = 150;
	public static readonly int DEFAULT_WINDOW_X_POS = 10;
	public static readonly int DEFAULT_WINDOW_Y_POS = 10;



	public NodeWindow(Rect window, int id = -1, string text = "", bool hasTrigger = false, string triggerString = "", int triggerPriority  = 0){
		this.window = window;
		this.text = text;
		this.hasTrigger = hasTrigger;
		this.triggerString = triggerString;
		this.triggerPriority = triggerPriority;
		this.id = id;
	}

	public DialogueNode toDialogueNode(){
		return new DialogueNode(this);
	}
}
