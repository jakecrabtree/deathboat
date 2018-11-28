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
	private DialogueTree tree;

	void Start(){
		nextButtonText = transform.GetChild(1).GetComponentInChildren<Text>();
		dialogueText = transform.GetChild(2).GetComponent<Text>();
		nameText = transform.GetChild(3).GetComponent<Text>();
		gameObject.SetActive(false);
	}

	public void UseDialogueTree(DialogueTree tree){
		this.tree = tree;
		gameObject.SetActive(true);
		ShowNextDialogue();
	}

	void ShowCurrentDialogue(){
		this.dialogueText.text = tree.current();
	}

	public void ShowNextDialogue(){
		if(tree.isEnd()){
			EndDialogue();
		}
		else{
			this.dialogueText.text = tree.next();
		}
	}

	void EndDialogue(){
		tree.reset();
		gameObject.SetActive(false);
	}
}
