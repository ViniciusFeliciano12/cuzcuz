using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    private List<Dialogue> SarahDialogue1 = new List<Dialogue>();
    private List<Dialogue> SarahDialogue2 = new List<Dialogue>();
    private List<Dialogue> SarahDialogue3 = new List<Dialogue>();



    private List<Dialogue> CurrentDialogue;
    public GameController gameController;
    public Canvas dialogueCanvas; 
    private TextMeshProUGUI nameText;  
    private TextMeshProUGUI messageText;
    private int currentDialogueIndex = 0; 
    private bool isDialogueActive = false;

    private void populateDialogues(){

        SarahDialogue1.Add(new Dialogue("Você", "Bom dia senhora, estou perdido nessas florestas e preciso prosseguir. Como eu poderia atravessar esse abismo?"));
        SarahDialogue1.Add(new Dialogue("Sarah", "Olá viajante. Muitos anos atrás, um monstro atacou essa floresta. Ele foi selado nas profundezas dela por um mágico poderoso, em uma caverna."));
        SarahDialogue1.Add(new Dialogue("Sarah", "Esse mago também foi derrotado no duelo, e a criatura foi fatalmente ferida e pegou sua varinha antes de ser selada."));
        SarahDialogue1.Add(new Dialogue("Sarah", "A unica forma de passar através do abismo é recolhendo a varinha mágica que o monstro guarda. Ela permite que você se transporte entre os grimórios do mago, como aquele do outro lado."));
        SarahDialogue1.Add(new Dialogue("Sarah", "Se for bravo o suficiente para enfrenta-lo e mata-lo, ajudará essas terras e poderá prosseguir."));
        SarahDialogue1.Add(new Dialogue("Sarah", "Boa sorte!"));

        SarahDialogue2.Add(new Dialogue("Sarah", "Derrote o monstro nas profundezas da floresta, viajante!"));

        SarahDialogue3.Add(new Dialogue("Sarah", "Parabéns! Você conseguiu matar ele! Muito obrigada. Agora conseguirá passar pelo abismo!"));
    }
    void Start()
    {
        populateDialogues();
        dialogueCanvas.enabled = false;

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
        currentDialogueIndex = 0;
        isDialogueActive = false;
        gameController.playerActive = true;
        CurrentDialogue[0].DialogueAlreadyUsed = true;
    }
    
    private void SelectDialogue(string name){
        if (name == "Sarah")
        {
            if (!gameController.wandCatched)
            {
                CurrentDialogue = SarahDialogue1.First().DialogueAlreadyUsed ? SarahDialogue2 : SarahDialogue1;
            }
            else
            {
                CurrentDialogue = SarahDialogue3;
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
        if (currentDialogueIndex < CurrentDialogue.Count)
        {
            Dialogue currentDialogue = CurrentDialogue[currentDialogueIndex];
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
