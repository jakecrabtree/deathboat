using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

	Quest quest;
	
	[SerializeField]
	string itemName = "";

	[SerializeField]
	string triggerName = "";

	[SerializeField]
	bool disappearOnCollect = true;

	public bool collected = false;



	void Start(){
		if (triggerName != ""){
			TriggerManager.AddTrigger(triggerName, false);
		}
	}

	void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player")){
			Collect();
        }
    }

	void Collect(){
		if (!collected && quest != null && quest.CollectItem(this)){
			if (triggerName != ""){
				TriggerManager.UpdateTrigger(triggerName, true);
			}
			collected = true;
			if(disappearOnCollect){
				gameObject.SetActive(false);
			}
		}
	}

	public void AssignQuest(Quest quest){
		this.quest = quest;
	}
}
