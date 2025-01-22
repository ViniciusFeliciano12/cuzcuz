public class Dialogue{
    public string Name { get; set; }
    public string Text { get; set; }
    public bool DialogueAlreadyUsed { get; set; } = false;
    
    public Dialogue(string name, string text) {
        Name = name;
        Text = text;
    }
}