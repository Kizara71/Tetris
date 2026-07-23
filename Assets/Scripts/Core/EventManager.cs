using System;

public class EventManager : Singleton<EventManager>
{
    public event Action OnPieceLocked;
    public void TriggerPieceLocked() => OnPieceLocked?.Invoke();

    public event Action<int> OnLinesCleared;
    public void TriggerLinesCleared(int lines) => OnLinesCleared?.Invoke(lines);

    public event Action OnGameOver;
    public void TriggerGameOver() => OnGameOver?.Invoke();
}
