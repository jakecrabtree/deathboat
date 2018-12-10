using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// starts animation clip "Anim" at a random position
public class RandomAnim : MonoBehaviour {

	void Start () {
		GetComponent<Animator>().Play("Anim", -1, Random.Range(0f, 1f));
	}
}
