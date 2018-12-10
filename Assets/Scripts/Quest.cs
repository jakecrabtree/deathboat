﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour {

	private static string questIndicatorPrefabPath = "exclamation";

	[SerializeField]
	private Vector3 questIndicatorPositionOffset = new Vector3(0, 1.85f, 0);


	[SerializeField]
	protected string questName = "";

	[SerializeField]
	protected string questCompletedTrigger = "";

	[SerializeField]
	protected string questStartedTrigger = "";

	[SerializeField]
	protected string questEnabledTrigger = "";

	[SerializeField]
	protected int karmaValue = 0;

	[SerializeField]
	protected bool questEnabled = false;

	[SerializeField]
	protected bool questStarted = false;

	[SerializeField]
	protected bool questCompleted = false;

	[SerializeField]
	protected bool questTurnedIn = false;

	[SerializeField]
	protected bool secretQuest = false;



	[SerializeField]
	protected bool defaultDialogueAfterTurnIn = false;

	protected GameObject questIndicator;

	private static SoundManager soundMng;

	void Awake(){
		if(soundMng == null){
			soundMng = GameObject.Find("SoundManager").GetComponent<SoundManager>();
		}
	}

	protected void CreateQuestIndicator(){
		if (!secretQuest){
			questIndicator = Instantiate<GameObject>(Resources.Load<GameObject>(questIndicatorPrefabPath), gameObject.transform.position + questIndicatorPositionOffset, Quaternion.identity);
		}
	}

	protected void ChangeQuestIndicatorColor(Color color){
		if (!secretQuest){
			Renderer[] renderers = questIndicator.GetComponentsInChildren<Renderer>();
			foreach(Renderer renderer in renderers){
				renderer.material.color = color;
			}
		}
	}

	public void EnableQuest(){
		CreateQuestIndicator();
		questEnabled = true;
	}

	public void DisableQuest(){
		Destroy(questIndicator);
		questEnabled = false;
	}

	public void StartQuest(){
		if (!questTurnedIn && questEnabled && !questStarted){
			if(soundMng != null){
				soundMng.playQuestAccepted();
			}
			ChangeQuestIndicatorColor(Color.yellow);
			questStarted = true;
			TriggerManager.UpdateTrigger(questStartedTrigger, true);
		}
	}

	public bool TurnIn(){
		if (questCompleted && !questTurnedIn){
			GameManager.instance.AddScore(karmaValue);
			DisableQuest();
			if (defaultDialogueAfterTurnIn){
				TriggerManager.UpdateTrigger(questCompletedTrigger, false);
				TriggerManager.UpdateTrigger(questEnabledTrigger, false);
				TriggerManager.UpdateTrigger(questStartedTrigger, false);
			}
			questTurnedIn = true;
			return true;
		}
		return false;
	}

	
	public void CompleteQuest(){
		if (!questCompleted){
			TriggerManager.UpdateTrigger(questCompletedTrigger, true);
			TriggerManager.UpdateTrigger(questEnabledTrigger, false);
			TriggerManager.UpdateTrigger(questStartedTrigger, false);
			ChangeQuestIndicatorColor(Color.green);
			questCompleted = true;
		}
	}


}
