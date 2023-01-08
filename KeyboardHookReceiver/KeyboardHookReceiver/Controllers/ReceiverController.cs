using KeyboardHookReceiver.Dto;
using KeyboardHookReceiver.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KeyboardHookReceiver.Controllers;

[ApiController]
public class ReceiverController : Controller
{
    private readonly IRepository _repository;

    public ReceiverController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpPost, Route("/keyboard")]
    public async Task ReceiveKeyboardActions(KeyboardInputDto json)
    {
        Console.WriteLine($"{json.DateTime} | {json.AccountName} pressed key[{json.KeyCode}] in {json.Program}");

        await _repository.CreateTableByAccountNameAsync(json.AccountName!);
        await _repository.AddKeyboardLogToTableAsync(json);
    }

    [HttpPost, Route("/mouse")]
    public async Task ReceiveMouseActions(MouseClickPosInputDto json)
    {
        Console.WriteLine(
            $"{json.DateTime} | {json.AccountName} {json.clickSide}Clicked at pos({json.X}, {json.Y}) in {json.Program}");

        await _repository.CreateTableByAccountNameAsync(json.AccountName!);
        await _repository.AddMouseLogToTableAsync(json);
    }

    [HttpGet, Route("/actions")]
    public async Task<IActionResult> GetAccountActionsByTimeInterval(string account, DateTime from, DateTime until)
    {
        var actions = await _repository
            .GetActionsByAccountInTimeInterval(account, from, until);

        return Ok(actions);
    }
}