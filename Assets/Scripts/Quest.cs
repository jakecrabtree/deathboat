using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour {

	[SerializeField]
	private QuestItem[] items;

	// Use this for initialization, MUST CALL base.Start();
	void Start () {
		foreach(QuestItem item in items){
			if (item != null){
				item.AssignQuest(this);
			}
			else{
				Debug.Log("Quest cannot contain null items");
			}
		}
	}

	

	public abstract bool CollectItem(QuestItem item);
}
