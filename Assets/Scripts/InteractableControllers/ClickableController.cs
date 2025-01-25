using UnityEngine;

public class ClickableController : MonoBehaviour
{
    public bool clickable;
    protected Renderer npcRenderer;
    protected Color originalColor;

    protected virtual void Start()
    {
        npcRenderer = GetComponent<Renderer>();

        if (npcRenderer != null)
        {
            originalColor = npcRenderer.material.color;
        }
    }

    void OnMouseEnter()
    {
        if (npcRenderer != null && GameController.Instance != null && clickable)
        {
            npcRenderer.material.color = Color.yellow;
            GameController.Instance.canAttack = false;
        }
    }

    void OnMouseExit()
    {
        if (npcRenderer != null && GameController.Instance != null && clickable)
        {
            npcRenderer.material.color = originalColor; 
            GameController.Instance.canAttack = true;
        }
    }
}
