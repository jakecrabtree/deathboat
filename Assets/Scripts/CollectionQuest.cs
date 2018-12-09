using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectionQuest : Quest {

	[SerializeField]
	protected QuestItem[] items;

	[SerializeField]
	int remaining; 
	// Use this for initialization
	void Start () {
		AssignQuestItems();
		remaining = items.Length;
		if (questEnabledTrigger != ""){
			TriggerManager.AddTrigger(questEnabledTrigger, questEnabled);
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
	
	public override bool CollectItem(QuestItem collectedItem){
		/* for(int i = 0; i < items.Length; ++i){
			if (items[i] == collectedItem){
				if (--remaining == 0){
					CompleteQuest();
				}
				return true;
			}
		}*/
		if (--remaining == 0){
			CompleteQuest();
		}
		return true;
	}

	protected override void CompleteQuest(){
		TriggerManager.UpdateTrigger(questCompletedTrigger, true);
		TriggerManager.UpdateTrigger(questEnabledTrigger, false);
		questEnabled = true;
	}
}
