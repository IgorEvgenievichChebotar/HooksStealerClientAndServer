namespace KeyboardHookReceiver.Models;

public class InputAction
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string? Program { get; set; }
    public int KeyCode { get; set; }
    public string? Click { get; set; }
}