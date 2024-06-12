using TMPro;
using UnityEngine;

public class ScoreBooster : Skill
{
    const string Name = "ScoreBootser_{0}";
    public static ISkillClass _skillClass;
    public static async void Create(St_Skill st_Skill, int addscore)
    {
        _skillClass = SkillClass.CreateClass(_skillClass);
        var name = string.Format(Name, st_Skill.objnum);
        var result = await Skill.Create<ScoreBooster>(st_Skill, name, _skillClass);
        result?.SetAddScore(addscore);
    }

    [SerializeField] TextMeshProUGUI T_AddScore;

    //스코어 추가
    public void SetAddScore(int addscore)
    {
        T_AddScore.text = "+" + addscore;
        ScoreManager.instance.SetCurrentScore(addscore, true);
    }
}