using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCasinoGame : MonoBehaviour {

	[SerializeField]
    private DialogueBox dialogueBox;
    [SerializeField]
    private DialogueTree tree;

    [SerializeField]
    string gameName = "Slot Machine";

	[SerializeField]
	public float winChance = 0.2f;

	[SerializeField]
	public int winAmount = 10;

	[SerializeField]
	public int loseAmount = 5;

    [SerializeField]
    string winTriggerString = "slot_win";

    [SerializeField]
    string loseTriggerString = "slot_lose";


	GameManager manager;

    bool started = false;
    bool played = false;
    

    void Start(){
        tree = GetComponent<DialogueTree>();
		manager = GameManager.instance;
        TriggerManager.AddTrigger(winTriggerString, false);
        TriggerManager.AddTrigger(loseTriggerString, false);
    }

    void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E) &&!started){
            if (other.CompareTag("Player")){
                dialogueBox.UseDialogueTree(tree, gameName);
                started = true;
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && started){
            if (other.CompareTag("Player")){
                bool res = !dialogueBox.ShowNextDialogue();
                if (!played){
                    PlayGame();
                    played = true;
                }
                if (dialogueBox.ShowNextDialogue()){
                    played = false;
                    started = false;
                }
            }
        }else if (Input.GetKeyDown(KeyCode.Escape) && started){
            if (other.CompareTag("Player")){
                started = false;
                dialogueBox.EndDialogue();
                ResetTriggers();
            }
        }
    } 

	void PlayGame(){
		float roll = Random.Range(0,1.0f);
        if (roll <= winChance){
            manager.AddScore(winAmount);
            TriggerManager.UpdateTrigger(winTriggerString, true);
        }
        else{
            manager.SubtractScore(loseAmount);
            TriggerManager.UpdateTrigger(loseTriggerString, true); 
        }
	}

    void ResetTriggers(){
        TriggerManager.UpdateTrigger(winTriggerString, false);
        TriggerManager.UpdateTrigger(loseTriggerString, false);
    }
}
