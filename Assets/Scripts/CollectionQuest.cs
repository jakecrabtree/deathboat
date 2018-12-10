using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectionQuest : Quest {

	[SerializeField]
	protected QuestItem[] items;

	[SerializeField]
	int remaining; 

	bool[] collected;
	// Use this for initialization
	void Start () {
		AssignQuestItems();
		if (remaining == 0){
			remaining = items.Length;
		}
		if (items.Length > 0){
			collected = new bool[items.Length];
		}
		if (questEnabled){
			EnableQuest();
		}
		if (questEnabledTrigger != ""){
			TriggerManager.AddTrigger(questEnabledTrigger, questEnabled);
			Debug.Log(questEnabledTrigger + TriggerManager.GetTrigger(questEnabledTrigger));
		}
		if (questStartedTrigger != ""){
			TriggerManager.AddTrigger(questStartedTrigger, questStarted);
		}
		if (questCompletedTrigger != ""){
			TriggerManager.AddTrigger(questCompletedTrigger, questCompleted);
		}
	}

	protected void AssignQuestItems(){
		foreach(QuestItem item in items){
			if (item != null){
				item.AssignQuest(this);
			}
			else{
				Debug.Log("Quest cannot contain null items");
			}
		}
	}
	
	public bool CollectItem(QuestItem collectedItem){
		if (questEnabled && questStarted){
			for (int i = 0; i < items.Length; ++i){
				if (collectedItem == items[i] && !collected[i]){
					collected[i] = true;
					if (--remaining == 0){
						CompleteQuest();
					}
					return true;
				}
			}
		}
		return false;
	}
}
