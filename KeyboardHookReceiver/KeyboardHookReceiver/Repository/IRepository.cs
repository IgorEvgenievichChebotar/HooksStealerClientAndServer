using KeyboardHookReceiver.Dto;
using KeyboardHookReceiver.Models;

namespace KeyboardHookReceiver.Repository;

public interface IRepository
{
    Task CreateTableByAccountNameAsync(string accountName);
    Task AddKeyboardLogToTableAsync(KeyboardInputDto log);
    Task AddMouseLogToTableAsync(MouseClickPosInputDto log);
    Task<ICollection<InputAction>> GetActionsByAccountInTimeInterval(string accountName, DateTime from, DateTime until);
}