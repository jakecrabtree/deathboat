using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spooky : MonoBehaviour {

	GameManager manager;
	Renderer[] renderers;
	List<Color> startColors;

	public Color endColor = new Color(0.33f, 0.745f, 0.216f, 0.588f);

	// Use this for initialization
	void Start () {
		manager = GameManager.instance;
		renderers = GetComponentsInChildren<Renderer>();
		startColors = new List<Color>();
		foreach(Renderer renderer in renderers){
			startColors.Add(renderer.material.color);
		}
	}
	
	// Update is called once per frame
	void Update () {
        float lerp = Mathf.PingPong((manager.totalTime - manager.timeRemaining) / manager.totalTime, manager.totalTime);
		int count = 0;
		foreach(Renderer renderer in renderers){
        	renderer.material.color = Color.Lerp(startColors[count++], endColor, lerp);
		}
	}
}
