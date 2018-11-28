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

	private DialogueTree tree;

	void Start(){

	}

	public void UseDialogueTree(DialogueTree tree){
		this.tree = tree;
		gameObject.SetActive(true);
		ShowCurrentDialogue();
	}

	void ShowCurrentDialogue(){
		this.dialogueText.text = tree.current();
	}

	void ShowNextDialogue(){
		if(tree.isEnd()){
			EndDialogue();
		}
		this.dialogueText.text = tree.next();
	}

	void EndDialogue(){
		tree.reset();
		gameObject.SetActive(false);
	}
}
