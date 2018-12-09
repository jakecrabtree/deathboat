using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCasinoGame : MonoBehaviour {

	[SerializeField]
    private DialogueBox dialogueBox;
    [SerializeField]
    private DialogueTree tree;

	[SerializeField]
	public float winChance = 0.2f;

	[SerializeField]
	public int winAmount = 10;

	[SerializeField]
	public int loseAmount = 5;

    [SerializeField]
    string winTriggerStrimg = "slot_win";

    [SerializeField]
    string loseTriggerStrimg = "slot_lose";


	GameManager manager;

    bool started = false;
    bool played = false;
    

    void Start(){
        tree = GetComponent<DialogueTree>();
		manager = GameManager.instance;
        TriggerManager.AddTrigger(winTriggerStrimg, false);
        TriggerManager.AddTrigger(loseTriggerStrimg, false);
    }

    void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E)){
            if (other.CompareTag("Player")){
                dialogueBox.UseDialogueTree(tree);
                started = true;
            }
        }else if (Input.GetKeyDown(KeyCode.Return) && started){
            if (other.CompareTag("Player")){
                if (!played){
                    PlayGame();
                    played = true;
                }
                if (dialogueBox.ShowNextDialogue()){
                    played = false;
                    started = false;
                }
                ResetTriggers();
            }
        }
    } 

	void PlayGame(){
		float roll = Random.Range(0,1.0f);
        if (roll <= winChance){
            manager.AddScore(winAmount);
            TriggerManager.UpdateTrigger(winTriggerStrimg, true);
        }
        else{
            manager.SubtractScore(loseAmount);
            TriggerManager.UpdateTrigger(loseTriggerStrimg, true); 
        }
	}

    void ResetTriggers(){
        TriggerManager.UpdateTrigger(winTriggerStrimg, false);
        TriggerManager.UpdateTrigger(loseTriggerStrimg, false);
    }
}
