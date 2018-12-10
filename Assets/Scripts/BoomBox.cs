using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (other.CompareTag("Player"))
            {
                GameObject.Find("SoundManager").GetComponent<SoundManager>().playEyeOfTheTiger();
            }
        }
	}
}
