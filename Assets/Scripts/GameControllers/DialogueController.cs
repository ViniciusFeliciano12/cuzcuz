using System.Linq;
using TMPro;
using UnityEngine;
using static GameData;

public class DialogueController : MonoBehaviour
{
    public Canvas dialogueCanvas; 
    private TextMeshProUGUI nameText;  
    private TextMeshProUGUI messageText;
    
    private GameController gameController;

    private NpcDialogue[] dialogueData;  
    private Dialogues CurrentDialogue;
    private int currentDialogueIndex = 0; 


    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        dialogueData = gameController.GetDialogueData();

        nameText = dialogueCanvas.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        messageText = dialogueCanvas.transform.Find("Message").GetComponent<TextMeshProUGUI>();

        if (nameText == null || messageText == null)
        {
            Debug.LogError("Não foi possível encontrar os componentes 'Name' ou 'Message' no Canvas!");
        }
    }

    public void PlayDialogue(string name){
        SelectDialogue(name);
        dialogueCanvas.enabled = true;
        gameController.playerActive = false;
    }
    private void EndDialogue()
    {
        dialogueCanvas.enabled = false;
        gameController.playerActive = true;
        CurrentDialogue.AlreadyUsed = true;
        currentDialogueIndex = 0;
    }
    
    private void SelectDialogue(string name){
        if (name == "Sarah")
        {
            var sarahDialogues = dialogueData.FirstOrDefault(item => item.NpcName == "Sarah").NpcDialogues;

            if (!gameController.VerifyFlag(GameFlags.SpaceWand))
            {
                CurrentDialogue = sarahDialogues[0].AlreadyUsed ? sarahDialogues[1] : sarahDialogues[0];
            }
            else
            {
                CurrentDialogue = sarahDialogues[2];
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialogueCanvas.enabled)
        {
            ShowNextDialogue();
        }
    }

    public void ShowNextDialogue()
    {
        if (currentDialogueIndex < CurrentDialogue.lines.Count())
        {
            DialogueLine currentDialogue = CurrentDialogue.lines[currentDialogueIndex];
            nameText.text = currentDialogue.Name;
            messageText.text = currentDialogue.Text;
            currentDialogueIndex++;
            if (!string.IsNullOrEmpty(currentDialogue.EventKey)){
                gameController.InvokeEvent(currentDialogue.EventKey);
            }
        }
        else
        {
            EndDialogue();
        }
    }
}
