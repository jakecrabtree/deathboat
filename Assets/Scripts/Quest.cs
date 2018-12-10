using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour {

	private static string questIndicatorPrefabPath = "exclamation";

	private static Vector3 questIndicatorPositionOffset = new Vector3(0, 1.85f, 0);


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
	protected bool defaultDialogueAfterTurnIn = false;

	protected GameObject questIndicator;

	protected void CreateQuestIndicator(){
		questIndicator = Instantiate<GameObject>(Resources.Load<GameObject>(questIndicatorPrefabPath), gameObject.transform.position + questIndicatorPositionOffset, gameObject.transform.rotation);
	}

	protected void ChangeQuestIndicatorColor(Color color){
		Renderer[] renderers = questIndicator.GetComponentsInChildren<Renderer>();
		foreach(Renderer renderer in renderers){
			renderer.material.color = color;
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
		if (!questTurnedIn && questEnabled){
			ChangeQuestIndicatorColor(Color.yellow);
			questStarted = true;
		}
	}

	public bool TurnIn(){
		if (questCompleted){
			GameManager.instance.AddScore(karmaValue);
			DisableQuest();
			if (defaultDialogueAfterTurnIn){
				TriggerManager.UpdateTrigger(questCompletedTrigger, false);
			}
			questTurnedIn = true;
			return true;
		}
		return false;
	}

	public abstract bool CollectItem(QuestItem item);
	protected abstract void CompleteQuest();


}
