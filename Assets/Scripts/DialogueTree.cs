using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode {
    List<DialogueNode> children;
    string text;
    string trigger;
    int priority;
    int id;

    public DialogueNode(string text, string trigger = null, int priority = 0){
        this.text = text;
        this.children = new List<DialogueNode>();
        this.trigger = trigger;
        this.priority = priority;
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

    public void addChild(DialogueNode node){
        children.Add(node);
        children.Sort((x,y)=> x.priority.CompareTo(y.priority));
    }

    public bool empty(){
        return children.Count == 0;
    }
}



[ExecuteInEditMode]
public class DialogueTree : MonoBehaviour{
    DialogueNode root;
    DialogueNode curr;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    public void Awake()
    {
        this.root = new DialogueNode("root"); 
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

}