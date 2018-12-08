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
		AssignQuests();
		remaining = items.Length;
		if (triggerName != ""){
			TriggerManager.AddTrigger(triggerName, false);
		}
	}

	protected void AssignQuests(){
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
		for(int i = 0; i < items.Length; ++i){
			if (items[i] == collectedItem){
				if (--remaining == 0){
					CompleteQuest();
				}
				return true;
			}
		}
		return false;
	}

	protected override void CompleteQuest(){
		TriggerManager.UpdateTrigger(triggerName, true);
	}
}
