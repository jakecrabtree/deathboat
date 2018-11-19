using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
	
	private static DialogueManager instance;

	[SerializeField]
	private static Dictionary<string, bool> triggers;

	private void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
		}
		else{
			instance = this;
		}
	}
	
	public static void SetTrigger(string trigger, bool val){
		if (triggers.ContainsKey(trigger)){
			triggers[trigger] = val;
		}
		else {
			Debug.Log("Trigger " + trigger + " not found.");
		}
	}

	public static bool GetTrigger(string trigger){
		return triggers[trigger];
	}

	public static void AddTrigger(string trigger, bool val){
		if (!triggers.ContainsKey(trigger)){
			triggers.Add(trigger,val);
		}
		else{
			Debug.Log("Trigger " + trigger + " is already a trigger.");
		}
	}
}
