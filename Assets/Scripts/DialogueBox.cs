using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour {

	[SerializeField]
	private Text dialogueText;
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text nextButtonText;

	[SerializeField]
	private Text quitButtonText;

	[SerializeField]
	private DialogueTree tree;

	void Start(){
		nextButtonText = transform.GetChild(1).GetComponentInChildren<Text>();
		quitButtonText = transform.GetChild(2).GetComponentInChildren<Text>();
		dialogueText = transform.GetChild(3).GetComponent<Text>();
		nameText = transform.GetChild(4).GetComponent<Text>();
		gameObject.SetActive(false);
	}

	public void UseDialogueTree(DialogueTree tree, string name){
		this.nameText.text = name;
		this.tree = tree;
		gameObject.SetActive(true);
		ShowNextDialogue();
	}

	void ShowCurrentDialogue(){
		this.dialogueText.text = tree.current();
	}

	public bool ShowNextDialogue(){
		if(tree.isEnd()){
			EndDialogue();
			return true;
		}
		else{
			this.dialogueText.text = tree.next();
			return false;
		}
	}

	public void EndDialogue(){
		tree.reset();
		gameObject.SetActive(false);
	}
}
