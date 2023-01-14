using KeyboardHookReceiver.Dto;
using KeyboardHookReceiver.Models;
using Microsoft.AspNetCore.Mvc;

namespace KeyboardHookReceiver.Repository;

public interface IRepository
{
    Task AddKeyboardActionAsync(KeyboardActionDto log);
    Task AddMouseActionAsync(MouseClickActionDto log);
    Task<ICollection<InputAction>> GetActionsAsync(string accountName, DateTime from, DateTime until);
    Task<ActionResult<IEnumerable<string>>> GetListenedAccountsAsync();
}