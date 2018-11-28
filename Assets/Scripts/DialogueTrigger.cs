﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField]
    private DialogueBox dialogueBox;
    [SerializeField]
    private DialogueTree tree;

    void Start(){
        tree = GetComponent<DialogueTree>();
    }

    void OnTriggerStay(Collider other){
        if (Input.GetKeyDown(KeyCode.E)){
            if (other.CompareTag("Player")){
                dialogueBox.UseDialogueTree(tree);
            }
        }
    }    
}
