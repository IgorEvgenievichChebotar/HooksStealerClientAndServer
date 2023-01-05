namespace KeyboardHookReceiver.Dto;

public class MouseClickPosInputDto : BaseInputInfo
{
    public int X { get; set; }
    public int Y { get; set; }
    public string? clickSide { get; set; }
}