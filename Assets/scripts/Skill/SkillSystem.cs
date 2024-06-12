using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public static SkillSystem instance;

    System.Action Ac_CoolTime;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Ac_CoolTime?.Invoke();
    }

    //쿨타임 적용
    public void SetCoolTime(System.Action action)
    {
        Ac_CoolTime += action;
    }

    //피버타임
    public void SetFeverTime()
    {
        St_Skill st_Skill = new St_Skill(0, 10, 10, 0);
        FerverTimeSystem.Create(st_Skill);
    }

    //스코어부스터
    public void SetScoreBooster()
    {
        St_Skill st_Skill = new St_Skill(0, 2, 10, 0);
        ScoreBooster.Create(st_Skill, 1000);
    }

    public void SetHealing()
    {
        St_Skill st_Skill = new St_Skill(0, 2, 0, 0);
        Healing.Create(st_Skill, 100);
    }

    public void SetShield()
    {
        St_Skill st_Skill = new St_Skill(0, 2, 0, 0);
        ShieldBuster.Create(st_Skill);
    }
}