using UnityEngine;

public interface ICoolTimeChecker
{
    bool CheckCoolTime();
    void SetCoolTime(float cooltime);
    void UpdateCoolTime();
}


public class CoolTimeChecker : ICoolTimeChecker
{
    private float _coolTime;

    public bool CheckCoolTime()
    {
        return _coolTime <= 0;
    }

    public void SetCoolTime(float cooltime)
    {
        _coolTime = cooltime;
    }

    public void UpdateCoolTime()
    {
        _coolTime -= Time.deltaTime;
    }
}