using System.Diagnostics;

public interface ISkillClass
{
    IComboChecker ComboChecker { get; set; }
    ICoolTimeChecker CoolTimeChecker { get; set; }
    IActiveChecker ActiveChecker { get; set; }

    void SetClass();
}

public class SkillClass : ISkillClass
{
    public IComboChecker ComboChecker { get; set; }
    public ICoolTimeChecker CoolTimeChecker { get; set; }
    public IActiveChecker ActiveChecker { get; set; }

    //클래스 생성
    public static ISkillClass CreateClass(ISkillClass data)
    {
        if (data == null)
        {
            data = new SkillClass();
        }
        return data;
    }

    public void SetClass()
    {
        if (ComboChecker != null)
        {
            return;
        }
        ComboChecker = new ComboChecker();
        CoolTimeChecker = new CoolTimeChecker();
        ActiveChecker = new ActiveCheckter();
        SkillSystem.instance.SetCoolTime(CoolTimeChecker.UpdateCoolTime);
    }
}