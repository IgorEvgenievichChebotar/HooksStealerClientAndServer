using KeyboardHookReceiver.Dto;
using KeyboardHookReceiver.Models;
using KeyboardHookReceiver.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KeyboardHookReceiver.Controllers;

[ApiController]
public class ActionController : Controller
{
    private readonly IRepository _repository;

    public ActionController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpPost, Route("/keyboard")]
    public async Task ReceiveKeyboardActions(KeyboardActionDto json)
    {
        Console.WriteLine($"{json.DateTime} | {json.AccountName} pressed key[{json.KeyCode}] in {json.Program}");

        await _repository.CreateTableByAccountNameAsync(json.AccountName!);
        await _repository.AddKeyboardActionAsync(json);
    }

    [HttpPost, Route("/mouse")]
    public async Task ReceiveMouseActions(MouseClickActionDto json)
    {
        Console.WriteLine(
            $"{json.DateTime} | {json.AccountName} {json.clickSide}Clicked at pos({json.X}, {json.Y}) in {json.Program}");

        await _repository.CreateTableByAccountNameAsync(json.AccountName!);
        await _repository.AddMouseActionAsync(json);
    }

    [HttpGet, Route("/actions")]
    public async Task<ActionResult<IEnumerable<InputAction>>> GetAccountActionsByTimeInterval(
        string account,
        DateTime from, DateTime until)
    {
        var actions = await _repository
            .GetActionsAsync(account, from, until);

        return Ok(actions);
    }

    [HttpGet, Route("/accounts")]
    public async Task<ActionResult<IEnumerable<string>>> GetListenedAccounts()
    {
        return await _repository.GetListenedAccountsAsync();
    }
}