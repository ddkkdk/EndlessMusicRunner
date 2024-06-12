using UnityEngine;

public class SheildBuster : MonoBehaviour
{
    const string Name = "Healing_{0}";
    public static ISkillClass _skillClass;

    public static async void Create(St_Skill st_Skill, int healing)
    {
        _skillClass = SkillClass.CreateClass(_skillClass);
        var name = string.Format(Name, st_Skill.objnum);
        var result = await Skill.Create<SheildBuster>(st_Skill, name, _skillClass);
    }
}