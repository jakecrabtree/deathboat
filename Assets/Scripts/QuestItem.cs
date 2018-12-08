using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

	Quest quest;
	
	[SerializeField]
	string itemName = "";

	[SerializeField]
	string triggerName;
	bool collected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E)){
            if (other.CompareTag("Player") && !collected){
				TriggerManager.UpdateTrigger(triggerName, true);
            }
        }
    }

	public void AssignQuest(Quest quest){
		this.quest = quest;
	}
}
