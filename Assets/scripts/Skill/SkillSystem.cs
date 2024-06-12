using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public static SkillSystem instance;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //피버 쿨타임
        FerverTimeSystem.SetCooltime();
    }

    //피버타임
    public void SetFeverTime()
    {
        var combo = 0;
        var activetime = 5;
        var cooltime = 30;
        var feverbacknum = 0;
        FerverTimeSystem.Create(combo, activetime, cooltime, feverbacknum);
    }
}