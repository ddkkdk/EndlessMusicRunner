using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;

public class FerverTimeSystem : Skill
{
    const string Name = "FeverTime_{0}";
    public static ISkillClass _skillClass;

    public static async void Create(St_Skill st_Skill)
    {
        _skillClass = SkillClass.CreateClass(_skillClass);
        var name = string.Format(Name, st_Skill.objnum);
        await Skill.Create<FerverTimeSystem>(st_Skill, name, _skillClass);
    }

    //피버타임 발동 시 스코어 두배
    public static int SetFeverScore(int currentScore)
    {
        if (_skillClass == null || _skillClass.ActiveChecker == null)
        {
            return currentScore;
        }

        return _skillClass.ActiveChecker.CheckActive() ? currentScore * 2 : currentScore;
    }
}