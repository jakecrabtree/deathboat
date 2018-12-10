using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TriggerManager {
	
	//private static DialogueManager instance;

	//[SerializeField]
	private static Dictionary<string, bool> triggers = new Dictionary<string,bool>();

	/* private void Awake(){
		if (instance != null && instance != this){
			Destroy(this.gameObject);
		}
		else{
			instance = this;
			triggers = new Dictionary<string,bool>();
		}
	}*/
	
	public static void UpdateTrigger(string trigger, bool val){
		if (triggers.ContainsKey(trigger)){
			Debug.Log("Set: " + trigger + " " +val);
			triggers[trigger] = val;
		}
		else {
			Debug.Log("Trigger " + trigger + " not found.");
		}
	}

	public static bool GetTrigger(string trigger){
		if (triggers.ContainsKey(trigger)){
			Debug.Log("Get: " + trigger + " " +triggers[trigger]);
			return triggers[trigger];
		}
		else {
			Debug.Log("Trigger " + trigger + " not found.");
			return false;
		}
	}

	public static void AddTrigger(string trigger, bool val){
		if (trigger.Length == 0){

		}
		else if (!triggers.ContainsKey(trigger)){
			triggers.Add(trigger,val);
		}
		else{
			triggers[trigger] = val;
		}
	}
}
