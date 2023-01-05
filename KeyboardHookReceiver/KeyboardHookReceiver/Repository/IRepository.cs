using KeyboardHookReceiver.Dto;

namespace KeyboardHookReceiver.Repository;

public interface IRepository
{
    Task CreateTableByAccountNameAsync(string accountName);
    Task AddKeyboardLogToTableAsync(KeyboardInputDto log);
    Task AddMouseLogToTableAsync(MouseClickPosInputDto log);
}