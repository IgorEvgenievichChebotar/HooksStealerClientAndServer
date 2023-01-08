namespace KeyboardHookReceiver.Dto;

public abstract class BaseInputInfo
{
    public string? AccountName { get; set; }
    public DateTime DateTime { get; set; }
    public string? Program { get; set; }
}