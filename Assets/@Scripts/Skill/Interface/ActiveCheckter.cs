public interface IActiveChecker
{
    bool CheckActive();
    void SetActive(bool active);
}

public class ActiveCheckter : IActiveChecker
{
    bool Active;
    public bool CheckActive()
    {
        return Active;
    }
    public void SetActive(bool active)
    {
        Active = active;
    }
}

