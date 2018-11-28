using System.Collections;
using System.Collections.Generic;
using UnityEngine; 


[System.Serializable]
public class Edges{
    public List<int> edges;
    public Edges(){
        edges = new List<int>();
    }
}

public class DialogueEdgeList{

    public List<DialogueNode> nodes;

    public HashSet<KeyValuePair<int,int>> edges;
    
    public DialogueEdgeList(){
        nodes = new List<DialogueNode>();
        edges = new HashSet<KeyValuePair<int,int>>();
    }
    public DialogueEdgeList(List<DialogueNode> nodes, HashSet<KeyValuePair<int,int>> edges){
        this.nodes = nodes;
        this.edges = edges;
    }
}
[System.Serializable]
public class DialogueNode {
    [SerializeField]
    public string text;
    [SerializeField]
    string trigger;
    [SerializeField]
    int priority;
    [SerializeField]
    int id;
    [SerializeField]
    Rect editorBox;
    public DialogueNode(string text, int id = -1, string trigger = null, int priority = 0){
        this.text = text;
        this.trigger = trigger;
        this.priority = priority;
        this.id = id;
    }

    public DialogueNode(NodeWindow window){
        text = window.text;
        trigger = window.triggerString;
        priority = window.triggerPriority;
        id = window.id;
        editorBox = window.window;
    }

    public NodeWindow toNodeWindow(){
        /* if (id == 0){
            editorBox = new Rect(NodeWindow.DEFAULT_WINDOW_X_POS, NodeWindow.DEFAULT_WINDOW_Y_POS, 
                            NodeWindow.DEFAULT_WINDOW_WIDTH, NodeWindow.DEFAULT_WINDOW_HEIGHT/2);
        }*/
        return new NodeWindow(editorBox, id, text, trigger!="", trigger, priority);
    }
}

public class DialogueTree : MonoBehaviour{
    
    [SerializeField]
    private List<DialogueNode> nodes = new List<DialogueNode>();

    [SerializeField]
    private List<Edges> adjList = new List<Edges>();

    private int root = 0;
    private int curr = 0;

    public string current(){
        return nodes[curr].text;
    }

    public string next(){
        if (!isEnd()){
            curr = selectChild();
        }
        return current();
    }

    private int selectChild(){
        return adjList[curr].edges[0]; 
    }

    public bool isEnd(){
        return (adjList.Count < curr) || adjList[curr].edges.Count == 0;
    }

    public void reset(){
        curr = root;
    }

    public void AddNode(DialogueNode node){
        nodes.Add(node);
    }

    public DialogueEdgeList toEdgeList(){
        DialogueEdgeList list = new DialogueEdgeList();
        list.nodes = nodes;
        int count = 0;
        foreach(Edges edges in adjList){
            foreach(int index in edges.edges){
                list.edges.Add(new KeyValuePair<int, int>(count, index));
            }
            ++count;
        }
        return list;
    }

    public void fromEdgeList(DialogueEdgeList edgeList){
        this.nodes = edgeList.nodes;
        adjList = new List<Edges>();
        foreach (DialogueNode node in nodes){
            adjList.Add(new Edges());
        }
        foreach(KeyValuePair<int,int> edge in edgeList.edges){
            this.adjList[edge.Key].edges.Add(edge.Value);
        } 
    }

}