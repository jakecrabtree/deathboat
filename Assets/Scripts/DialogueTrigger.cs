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
    private QuestItem item;

    [SerializeField]
    private string speakerName;

    bool started = false;

    void Start(){
        tree = GetComponent<DialogueTree>();
        quest = GetComponent<Quest>();
        item = GetComponent<QuestItem>();
    }

    void OnTriggerStay(Collider other){
         if (Input.GetKeyDown(KeyCode.E) &&!started){
            if (other.CompareTag("Player")){
                dialogueBox.UseDialogueTree(tree, speakerName);
                started = true;
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && started){
            if (other.CompareTag("Player")){
                bool res = !dialogueBox.ShowNextDialogue();
                if (!res && quest != null){
                    quest.StartQuest();
                    quest.TurnIn();
                    started = false;
                }
                if (!res && item!=null){
                    item.Collect();
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
