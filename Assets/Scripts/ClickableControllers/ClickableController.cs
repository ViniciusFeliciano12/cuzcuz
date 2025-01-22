using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickableController : MonoBehaviour
{
    public bool clickable;
    protected GameController gameController;
    protected Renderer npcRenderer;
    protected Color originalColor;

    protected virtual void Start()
    {
        npcRenderer = GetComponent<Renderer>();
        gameController = FindObjectOfType<GameController>();

        if (npcRenderer != null)
        {
            originalColor = npcRenderer.material.color;
        }
    }

    void OnMouseEnter()
    {
        if (npcRenderer != null && gameController != null && clickable)
        {
            npcRenderer.material.color = Color.yellow;
            gameController.canAttack = false;
        }
    }

    void OnMouseExit()
    {
        if (npcRenderer != null && gameController != null && clickable)
        {
            npcRenderer.material.color = originalColor; 
            gameController.canAttack = true;
        }
    }
}
