using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charon : MonoBehaviour {
 	
	[SerializeField]
    private DialogueBox dialogueBox;

    [SerializeField]
    private DialogueTree tree;

    [SerializeField]
    private Quest quest;

	[SerializeField]
    private string[] nextTriggerStrings;


    [SerializeField]
    private string speakerName;

    bool started = false;

    int currTriggerString = 0;



    void Start(){
        tree = GetComponent<DialogueTree>();
        quest = GetComponent<Quest>();
        foreach(string triggerString in nextTriggerStrings){
            TriggerManager.AddTrigger(triggerString, false);
        }
        TriggerManager.UpdateTrigger(nextTriggerStrings[currTriggerString], true);
    }

    void OnTriggerStay(Collider other){
         if (Input.GetKeyDown(KeyCode.E)){
            if (other.CompareTag("Player")){
                dialogueBox.UseDialogueTree(tree, speakerName);
                started = true;
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && started){
            if (other.CompareTag("Player")){
                started = !dialogueBox.ShowNextDialogue();
                if (!started && quest != null){
                    TriggerManager.UpdateTrigger(nextTriggerStrings[currTriggerString], false);
                    if(currTriggerString < nextTriggerStrings.Length - 1){
                        TriggerManager.UpdateTrigger(nextTriggerStrings[++currTriggerString], true);
                    }
                    else{
                        quest.CompleteQuest();
                    }
                    quest.StartQuest();
                    quest.TurnIn();
                }
            }
        }else if (Input.GetKeyDown(KeyCode.Escape) && started){
            if (other.CompareTag("Player")){
                started = false;
                dialogueBox.EndDialogue();
            }
        }
    }  
}
