using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickableNpc : MonoBehaviour
{
    public string npcName; // Nome do NPC
    private GameController gameController;
    private DialogueController dialogueController;
    private Renderer npcRenderer;
    private Color originalColor;

    void Start()
    {
        npcRenderer = GetComponent<Renderer>();
        gameController = FindObjectOfType<GameController>();
        dialogueController = FindObjectOfType<DialogueController>();

        if (npcRenderer != null)
        {
            originalColor = npcRenderer.material.color;
        }
    }
    void OnMouseDown()
    {
        dialogueController.PlayDialogue(npcName);
    }

    void OnMouseEnter()
    {
        if (npcRenderer != null && gameController != null)
        {
            npcRenderer.material.color = Color.yellow;
            gameController.canAttack = false;
        }
    }

    void OnMouseExit()
    {
        if (npcRenderer != null && gameController != null)
        {
            npcRenderer.material.color = originalColor; 
            gameController.canAttack = true;
        }
    }
}
