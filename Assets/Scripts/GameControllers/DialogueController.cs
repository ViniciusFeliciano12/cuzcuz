using System.Linq;
using TMPro;
using UnityEngine;
using static GameData;

public class DialogueController : MonoBehaviour
{
    public Canvas dialogueCanvas; 
    private TextMeshProUGUI nameText;  
    private TextMeshProUGUI messageText;
    
    private NpcDialogue[] dialogueData;  
    private Dialogues CurrentDialogue;
    private int currentDialogueIndex = 0; 


    void Start()
    {
        dialogueData = GameController.Instance.GetDialogueData();

        nameText = dialogueCanvas.transform.Find("Name").GetComponent<TextMeshProUGUI>();
        messageText = dialogueCanvas.transform.Find("Message").GetComponent<TextMeshProUGUI>();

        if (nameText == null || messageText == null)
        {
            Debug.LogError("Não foi possível encontrar os componentes 'Name' ou 'Message' no Canvas!");
        }

        StartInitialDialogue();
    }

    public void StartInitialDialogue(){
        if (!dialogueData.FirstOrDefault(dialogue => dialogue.NpcName == "Voce").NpcDialogues[0].AlreadyUsed){
            PlayDialogue("Voce", "InitialDialogue");
        }
    }

    public void PlayDialogue(string key, string specificDialogue = null){
        SelectDialogue(key, specificDialogue);
        GameController.Instance.playerActive = false;
        dialogueCanvas.enabled = true;
    }
    private void EndDialogue()
    {
        dialogueCanvas.enabled = false;
        GameController.Instance.playerActive = true;
        CurrentDialogue.AlreadyUsed = true;
        currentDialogueIndex = 0;
    }
    
    private void SelectDialogue(string key, string specificDialogue = null){
        var dialogues = dialogueData.FirstOrDefault(item => item.NpcName == key).NpcDialogues;

        if (dialogues != null){
            if (key == "Sarah")
            {
                if (!GameController.Instance.VerifyFlag(GameFlags.SpaceWand))
                {
                    if (dialogueData.FirstOrDefault(dialogue => dialogue.NpcName == "Voce").NpcDialogues[1].AlreadyUsed && !dialogues[5].AlreadyUsed){
                        if (GameController.Instance.VerifyFlag(GameFlags.Coins, 10)){
                            CurrentDialogue = dialogues[5];
                        }else{
                            CurrentDialogue = dialogues[3].AlreadyUsed ? dialogues[4] : dialogues[3];
                        }
                    }else{
                        CurrentDialogue = dialogues[0].AlreadyUsed ? dialogues[1] : dialogues[0];
                    }
                }
                else
                {
                    CurrentDialogue = dialogues[2];
                }
            }

            if(key == "Voce"){
                if (specificDialogue != null){
                    if (specificDialogue == "InitialDialogue"){
                        CurrentDialogue = dialogues[0];
                    }

                    if(specificDialogue == "Darkness"){
                        CurrentDialogue = dialogues[1].AlreadyUsed ? dialogues[2] : dialogues[1];
                    }
                }
                ShowNextDialogue();
            }
        }
    }

    void LateUpdate()
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
                GameController.Instance.InvokeEvent(currentDialogue.EventKey);
            }
        }
        else
        {
            EndDialogue();
        }
    }
}
