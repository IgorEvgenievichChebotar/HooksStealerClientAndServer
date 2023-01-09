namespace KeyboardHookReceiver.Dto;

public class MouseClickActionDto : BaseActionDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public string? clickSide { get; set; }
}