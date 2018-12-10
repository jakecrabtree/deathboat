using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

	List<CollectionQuest> quests = new List<CollectionQuest>();
	
	[SerializeField]
	string itemName = "";

	[SerializeField]
	string triggerName = "";

	[SerializeField]
	bool disappearOnCollect = true;

	[SerializeField]
	bool hasDialogue = false;

	public bool collected = false;

	static SoundManager soundMng;

	void Start(){
		if (triggerName != ""){
			TriggerManager.AddTrigger(triggerName, false);
		}

		if(soundMng == null){
			soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		}
		hasDialogue = GetComponent<DialogueTree>() != null;
	}

	void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player") && !hasDialogue){
			Collect();
        }
    }

	public void Collect(){
		foreach(CollectionQuest quest in quests){
			if (quest.CollectItem(this)){
				if(soundMng != null){
					soundMng.playPickup();
				}
				if (triggerName != ""){
					TriggerManager.UpdateTrigger(triggerName, true);
				}
				if(disappearOnCollect){
					gameObject.SetActive(false);
				}
			}
		}
	}

	public void AssignQuest(CollectionQuest quest){
		this.quests.Add(quest);
	}
}
