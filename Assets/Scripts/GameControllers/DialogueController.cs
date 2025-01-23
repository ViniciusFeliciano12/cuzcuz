using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static GameData;

public class DialogueController : MonoBehaviour
{
    public GameData gameData;
    private NpcDialogue[] dialogueData;  
    private Dialogues CurrentDialogue;
    public GameController gameController;
    public Canvas dialogueCanvas; 
    private TextMeshProUGUI nameText;  
    private TextMeshProUGUI messageText;
    private int currentDialogueIndex = 0; 
    private bool isDialogueActive = false;

    void Start()
    {
        dialogueData = gameController.gameData.NpcsDialogues;

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
        isDialogueActive = true;
        gameController.playerActive = false;
    }
    private void EndDialogue()
    {
        dialogueCanvas.enabled = false;
        isDialogueActive = false;
        gameController.playerActive = true;
        CurrentDialogue.AlreadyUsed = true;
        currentDialogueIndex = 0;
    }
    
    private void SelectDialogue(string name){
        if (name == "Sarah")
        {
            var sarahDialogues = dialogueData.FirstOrDefault(item => item.NpcName == "Sarah").NpcDialogues;

            if (!gameController.gameData.Player.getSpaceWand)
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
        if (Input.GetMouseButtonDown(0) && isDialogueActive)
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
        }
        else
        {
            EndDialogue();
        }
    }
}
