using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationQuest : Quest {

	[SerializeField]
	QuestLocation location;
	
	bool collected = false;
	// Update is called once per frame
	void Update () {
		
	}

	void Start () {
		AssignQuestLocation();
		if (questEnabled){
			EnableQuest();
		}
		if (questEnabledTrigger != ""){
			TriggerManager.AddTrigger(questEnabledTrigger, questEnabled);
		}
		if (questStartedTrigger != ""){
			TriggerManager.AddTrigger(questStartedTrigger, questStarted);
		}
		if (questCompletedTrigger != ""){
			TriggerManager.AddTrigger(questCompletedTrigger, questCompleted);
		}
	}

	protected void AssignQuestLocation(){
		location.AssignQuest(this);
	}

	public bool EnterArea(){
		if (questEnabled && questStarted && !collected){
			collected = true;
			CompleteQuest();
			return true;
		}
		return false;
	}
}
