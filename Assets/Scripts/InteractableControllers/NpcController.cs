public class NPCController : ClickableController
{
    public string npcName; 

    protected override void Start()
    {
        base.Start();
    }

    void OnMouseDown()
    {
        GameController.Instance.PlayDialogue(npcName);
    }
}
