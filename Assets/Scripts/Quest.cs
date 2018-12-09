using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour {

	[SerializeField]
	protected string questName = "";

	[SerializeField]
	protected string questCompletedTrigger = "";

	[SerializeField]
	protected string questEnabledTrigger = "";

	[SerializeField]
	protected int karmaValue = 0;

	[SerializeField]
	protected bool questEnabled = false;

	[SerializeField]
	protected bool questCompleted = false;

	public void EnableQuest(){
		questEnabled = true;
	}

	public void DisableQuest(){
		questEnabled = false;
	}

	public abstract bool CollectItem(QuestItem item);
	protected abstract void CompleteQuest();

	public bool TurnIn(){
		if (questCompleted){
			GameManager.instance.AddScore(karmaValue);
			questEnabled = false;
			return true;
		}
		return false;
	}
}
