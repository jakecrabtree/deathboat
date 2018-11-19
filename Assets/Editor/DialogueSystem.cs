using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
 

public class NodeWindow {
	public Rect window;
	public string text;
	public bool hasTrigger;
	public string triggerString;
	public int triggerPriority;

	public NodeWindow(Rect window, string text = "", bool hasTrigger = false, string triggerString = "", int triggerPriority  = 0){
		this.window = window;
		this.text = text;
		this.hasTrigger = hasTrigger;
		this.triggerString = triggerString;
		this.triggerPriority = triggerPriority;
	}
}

public class DialogueSystem : EditorWindow {

    HashSet<int> windowsToAttach = new HashSet<int>();
	HashSet<int> windowsToDetach = new HashSet<int>();
    List<int> attachedWindows = new List<int>();
	List<NodeWindow> windows = new List<NodeWindow>();
	string attachString = "Attach";
	string detachString = "Detach";
	int nodeBeingAttached = -1;
	int nodeBeingDetached = -1;
 
    [MenuItem("Window/Speakeasy")]
    static void ShowEditor() {
        DialogueSystem editor = EditorWindow.GetWindow<DialogueSystem>();
		editor.Init();
    }
 
	void Init(){
		MakeWindow(100,75);
	}
 
    void OnGUI() {
        if (windowsToAttach.Count == 2) {
            foreach(int window in windowsToAttach){
				attachedWindows.Add(window);
			}
			windowsToAttach.Clear();
        }
        if (attachedWindows.Count >= 2) {
            for (int i = 0; i < attachedWindows.Count; i += 2) {
				if (windowsToDetach.Contains(attachedWindows[i]) && 
					windowsToDetach.Contains(attachedWindows[i+1])){
						attachedWindows.RemoveRange(i, 2);
						i-=2;
				}else{
                	DrawNodeCurve(windows[attachedWindows[i]].window, windows[attachedWindows[i + 1]].window);
				}
            }
        }

		if (windowsToDetach.Count == 2){
			windowsToDetach.Clear();
		}
 
        BeginWindows();
 
        if (GUILayout.Button("Create Dialogue Node")) {
            MakeWindow(100,150);
        }
 
		windows[0].window = GUI.Window(0, windows[0].window, DrawNodeWindow, "Root Node");
        for (int i = 1; i < windows.Count; i++) {
            windows[i].window = GUI.Window(i, windows[i].window, DrawNodeWindow, "Dialogue Node " + i);
        }
 
        EndWindows();
    }
 
 
    void DrawNodeWindow(int id) {
		bool deleted = false;
		NodeWindow window = windows[id];
		if (id == nodeBeingAttached){
			if (GUILayout.Button("Cancel")){
				CancelAttach(id);
			}
		}
		else {
			if (GUILayout.Button(attachString)){
				Attach(id);
			}
		}

		if (id == nodeBeingDetached){
			if (GUILayout.Button("Cancel")){
				CancelDetach(id);
			}
		}
		else {
			if (GUILayout.Button(detachString)){
				Detach(id);
			}
		}

		if (id != 0){
			if (GUILayout.Button("Delete")){
				deleted = true;
			}
			GUILayout.Label("Dialogue Text");
			window.text = GUILayout.TextField(window.text);
			bool prevHasTrigger = window.hasTrigger;
			if (window.hasTrigger = GUILayout.Toggle(window.hasTrigger, "Trigger")){
				if (window.hasTrigger != prevHasTrigger){
					windows[id].window.height += 75;
				}
				GUILayout.Label("Trigger Name");
				window.triggerString = GUILayout.TextField(window.triggerString, 20);
				GUILayout.Label("Priority");
				int val;
				if (int.TryParse(GUILayout.TextField(window.triggerPriority.ToString(), 1), out val)){
					window.triggerPriority = val;
				}
				else{
					Debug.Log("Priority must be an int");
				}
			} 
			else if (window.hasTrigger != prevHasTrigger){
				windows[id].window.height -= 75;
			}
		}

		if (deleted){
			DeleteNode(id);
		}

        GUI.DragWindow();
    }

	void Attach(int id){
		windowsToAttach.Add(id);
		if (windowsToAttach.Count == 1){
			attachString = "Attach to " + id;
			nodeBeingAttached = id;
		}else if (windowsToAttach.Count == 2){
			attachString = "Attach";
			nodeBeingAttached = -1;
		}
	}

	void CancelAttach(int id){
		windowsToAttach.Clear();
		nodeBeingAttached = -1;
		attachString = "Attach";
	}

	void Detach(int id){
		windowsToDetach.Add(id);
		if (windowsToDetach.Count == 1){
			detachString = "Detach from " + id;
			nodeBeingDetached = id;
		}else if (windowsToDetach.Count == 2){
			detachString = "Detach";
			nodeBeingDetached = -1;
		}
	}

	void CancelDetach(int id){
		windowsToDetach.Clear();
		nodeBeingDetached = -1;
		detachString = "Detach";
	}


	void MakeWindow(int width, int height){
		windows.Add(new NodeWindow(new Rect(10, 10, width, height)));
	}

	void DeleteNode(int id){
		windows.RemoveAt(id);
		windowsToAttach.Remove(id);
		windowsToDetach.Remove(id);
		for (int i = 0; i < attachedWindows.Count; i += 2) {
			if (attachedWindows[i] == id || 
				attachedWindows[i+1] == id){
					attachedWindows.RemoveRange(i, 2);
					i-=2;
			}
		}
	}
 
 
    void DrawNodeCurve(Rect start, Rect end) {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
 
        for (int i = 0; i < 3; i++) {// Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }
 
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }
}
