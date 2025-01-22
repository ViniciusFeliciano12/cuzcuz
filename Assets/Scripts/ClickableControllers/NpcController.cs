using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCController : ClickableController
{
    public string npcName; // Nome do NPC
    private DialogueController dialogueController;

    protected override void Start()
    {
        base.Start();
        dialogueController = FindObjectOfType<DialogueController>();
    }

    void OnMouseDown()
    {
        dialogueController.PlayDialogue(npcName);
    }
}
