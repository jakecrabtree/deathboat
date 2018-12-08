using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour {

	[SerializeField]
	protected string questName = "";

	[SerializeField]
	protected string triggerName = "";

	[SerializeField]
	protected int karmaValue = 0;
	public abstract bool CollectItem(QuestItem item);
	protected abstract void CompleteQuest();
}
