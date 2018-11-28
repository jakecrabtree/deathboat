using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class DialogueEdgeList{
    public HashSet<DialogueNode> nodes;
    public HashSet<KeyValuePair<int,int>> edges;

    public DialogueEdgeList(){
        nodes = new HashSet<DialogueNode>();
        edges = new HashSet<KeyValuePair<int,int>>();
    }
}

public class DialogueNode {
    List<DialogueNode> children = new List<DialogueNode>();

    string text;
    string trigger;
    int priority;
    int id;
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

    private DialogueNode selectChild(){
        foreach(DialogueNode child in children){
            if(child.trigger != null && DialogueManager.GetTrigger(child.trigger)){
                return child;
            }else if (child.trigger == null){
                return child;
            }
        }
        return null;
    }

    public DialogueNode next(){
        return (empty()) ? null : selectChild();
    }

    public DialogueEdgeList BFSTraverse(){
        DialogueEdgeList ret = new DialogueEdgeList();
        Queue<DialogueNode> nodeQueue = new Queue<DialogueNode>();

        nodeQueue.Enqueue(this);
        DialogueNode curr;
        while (nodeQueue.Count > 0){
            curr = nodeQueue.Dequeue();
            ret.nodes.Add(curr);
            foreach(DialogueNode node in curr.children){
                if (!ret.nodes.Contains(node)){
                    nodeQueue.Enqueue(node);
                }
                ret.edges.Add(new KeyValuePair<int, int>(curr.id, node.id));
            }
        }
        return ret;
    }

    public void addChild(DialogueNode node){
        children.Add(node);
        children.Sort((x,y)=> x.priority.CompareTo(y.priority));
    }

    public bool empty(){
        return children.Count == 0;
    }

    public NodeWindow toNodeWindow(){
        if (id == 0){
            editorBox = new Rect(NodeWindow.DEFAULT_WINDOW_X_POS, NodeWindow.DEFAULT_WINDOW_Y_POS, 
                            NodeWindow.DEFAULT_WINDOW_WIDTH, NodeWindow.DEFAULT_WINDOW_HEIGHT/2);
        }
        return new NodeWindow(editorBox, id, text, trigger!="", trigger, priority);
    }
}



[ExecuteInEditMode]
public class DialogueTree : MonoBehaviour{
    DialogueNode root = new DialogueNode("root", 0); 
    DialogueNode curr;

    
    public void Awake(){
        this.curr = root; 
    }

    public DialogueNode current(){
        return curr;
    }

    public DialogueNode next(){
        if (!isEnd()){
            curr = curr.next();
        }
        return curr;
    }

    public bool isEnd(){
        return curr.empty();
    }

    public void reset(){
        curr = root;
    }

    public void addChild(DialogueNode node){
        if (curr != null){
            curr.addChild(node);
        }   
    }

    public DialogueEdgeList toEdgeList(){
        return root.BFSTraverse();
    }

    public void setRoot(DialogueNode root){
        this.root = root;
        reset();
    }

}