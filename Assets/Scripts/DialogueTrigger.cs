using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField]
    private DialogueBox dialogueBox;
    [SerializeField]
    private DialogueTree tree;

    [SerializeField]
    private Quest quest;

    [SerializeField]
    private string speakerName;

    bool started = false;

    void Start(){
        tree = GetComponent<DialogueTree>();
        quest = GetComponent<Quest>();
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
