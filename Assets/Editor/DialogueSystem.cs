using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor; 
using UnityEditor.SceneManagement;

public class DialogueSystem : EditorWindow {

    HashSet<int> windowsToAttach;
	HashSet<int> windowsToDetach;
	HashSet<KeyValuePair<int,int>> connectedEdges;
	List<NodeWindow> windows;
	DialogueTree currentTree;
	
	string attachString;
	string detachString;
	int nodeBeingAttached;
	int nodeBeingDetached;
 
    [MenuItem("Window/Speakeasy")]
    static void ShowEditor() {
        DialogueSystem editor = EditorWindow.GetWindow<DialogueSystem>();
		editor.Init();
    }

	void ResetTree(){
		windowsToAttach = new HashSet<int>();
		windowsToDetach = new HashSet<int>();
		connectedEdges = new HashSet<KeyValuePair<int, int>>();
		windows = new List<NodeWindow>();
		attachString = "Attach";
		detachString = "Detach";
		nodeBeingAttached = -1;
		nodeBeingDetached = -1;
	}

	void LoadFromSelection(){
		foreach (GameObject obj in Selection.gameObjects){
			if (currentTree = obj.GetComponent<DialogueTree>()){
				Debug.Log("found tree");
				LoadFromTree(currentTree);
				break;
			}
		}
	}
 
	void Init(){
		ResetTree();
		LoadFromSelection();
	}

	void OnDestroy()
	{
		WriteToTree(currentTree);
	}

	void OnSelectionChange(){
		Debug.Log("Selection changed");
		WriteToTree(currentTree);
		ResetTree();
		LoadFromSelection();
	}
 
    void OnGUI() {
        if (windowsToAttach.Count == 2) {
			List<int> windowsList = windowsToAttach.ToList();
			KeyValuePair<int,int> pair = new KeyValuePair<int, int>(windowsList[0], windowsList[1]);
			KeyValuePair<int,int> pairReverse = new KeyValuePair<int, int>(windowsList[1], windowsList[0]);
			if (connectedEdges.Contains(pair) || connectedEdges.Contains(pairReverse)){
				Debug.Log("in set");
			}
			else{
				connectedEdges.Add(pair);
			}
			windowsToAttach.Clear();
        }
		if (windowsToDetach.Count == 2){
			List<int> windowsList = windowsToDetach.ToList();
			connectedEdges.RemoveWhere(x => x.Key == windowsList[0] && x.Value == windowsList[1]);
			connectedEdges.RemoveWhere(x => x.Key == windowsList[1] && x.Value == windowsList[0]);
			windowsToDetach.Clear();
		}

		if (connectedEdges.Count >= 1) {
			foreach(KeyValuePair<int,int> edge in connectedEdges){
				Debug.Log("Drawing:" + edge.Key + edge.Value);
				DrawNodeCurve(windows[edge.Key].window, windows[edge.Value].window);
			}
        }
 
        BeginWindows();
 
		if (currentTree){
        	if (GUILayout.Button("Create Dialogue Node")) {
            	MakeWindow(NodeWindow.DEFAULT_WINDOW_WIDTH,NodeWindow.DEFAULT_WINDOW_HEIGHT);
        	}
			if (GUILayout.Button("Save Tree")) {
            	WriteToTree(currentTree);
        	}
		}
		else{
			GUILayout.Label("Selection does not contain a Dialogue Tree Component");
		}
 
        for (int i = 0; i < windows.Count; i++) {
			string name;
			if (i==0){
				name = "Root Node";
			}
			else{
				name = "Dialogue Node " + i;
			}
            windows[i].window = GUI.Window(i, windows[i].window, DrawNodeWindow, name);
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
		windows.Add(new NodeWindow(new Rect(NodeWindow.DEFAULT_WINDOW_X_POS, NodeWindow.DEFAULT_WINDOW_Y_POS, width, height), windows.Count));
	}

	void DeleteNode(int id){
		windows.RemoveAt(id);
		windowsToAttach.Remove(id);
		windowsToDetach.Remove(id);
		connectedEdges.RemoveWhere(x => x.Key == id || x.Value == id);
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

	void LoadFromTree(DialogueTree tree){
		ResetTree();
		if (tree == null){
			Debug.Log("Null Tree");
			return;
		}
		DialogueEdgeList list = tree.toEdgeList();
		foreach(DialogueNode node in list.nodes){
			windows.Add(node.toNodeWindow());
		}
		if (list.nodes.Count == 0){
			windows.Add(new NodeWindow(new Rect(NodeWindow.DEFAULT_WINDOW_X_POS, NodeWindow.DEFAULT_WINDOW_Y_POS, 
                            NodeWindow.DEFAULT_WINDOW_WIDTH, NodeWindow.DEFAULT_WINDOW_HEIGHT/2), windows.Count, "Don't Forget to Add Real Text Here!"));
		}
		connectedEdges = list.edges;
	}

	void WriteToTree(DialogueTree tree){
		if (tree == null){
			return;
		}
		List<DialogueNode> nodes = new List<DialogueNode>();
		foreach (NodeWindow window in windows){
			nodes.Add(window.toDialogueNode());
		}
		Undo.RecordObject(currentTree, "Tree Written");
		currentTree.fromEdgeList(new DialogueEdgeList(nodes, connectedEdges));
		EditorUtility.SetDirty(currentTree);
		EditorSceneManager.MarkAllScenesDirty();
	}
}
