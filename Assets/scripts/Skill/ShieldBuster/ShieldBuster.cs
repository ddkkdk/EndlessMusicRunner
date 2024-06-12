using UnityEngine;

public class ShieldBuster : Skill
{
    const string Name = "ShieldBuster_{0}";
    public static ISkillClass _skillClass;

    public static async void Create(St_Skill st_Skill)
    {
        _skillClass = SkillClass.CreateClass(_skillClass);
        var name = string.Format(Name, st_Skill.objnum);
        var result = await Skill.Create<ShieldBuster>(st_Skill, name, _skillClass);
    }

    //쉴드 작동 체크
    public static bool CheckShield()
    {
        if (_skillClass == null || _skillClass.ActiveChecker == null)
        {
            return false;
        }
        return _skillClass.ActiveChecker.CheckActive();
    }
}