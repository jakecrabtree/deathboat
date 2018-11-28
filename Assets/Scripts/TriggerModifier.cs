using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerModifier : MonoBehaviour {
	
	[SerializeField]
	private string triggerName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E)){
            if (other.CompareTag("Player")){
				TriggerManager.UpdateTrigger(triggerName, true);
            }
        }
    } 
}
