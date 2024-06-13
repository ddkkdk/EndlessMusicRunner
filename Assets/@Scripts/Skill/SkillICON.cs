using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SkillICON : MonoBehaviour
{
    const string Name = "UI_SkillICON";

    public static async Task<SkillICON> Create(int skillid)
    {
        var result = await Name.CreateOBJ<SkillICON>(SkillSystem.instance.Tr_SkillICONCreate);
        result.SetUp(skillid);
        return result;
    }

    [SerializeField] Image[] Img_ICON;
    ISkillClass M_SkillClass;


    public void SetUp(int skillid)
    {
        SetICON(skillid);
    }

    private void Update()
    {
        SetViewCoolTime();
    }

    //아이콘 셋팅
    async void SetICON(int skillid)
    {
        var icon = await skillid.GetSkillICON();
        foreach (var item in Img_ICON)
        {
            item.sprite = icon;
        }
    }


    //이미지에 쿨타임 표기
    void SetViewCoolTime()
    {
        if (M_SkillClass == null || M_SkillClass.CoolTimeChecker == null)
        {
            return;
        }

        var max = M_SkillClass.CoolTimeChecker.GetMaxCoolTime();
        var cur = M_SkillClass.CoolTimeChecker.GetCurrentCoolTime();

        Img_ICON[1].fillAmount = cur / max;
    }

    public void SetCoolTime(ISkillClass skillClass)
    {
        M_SkillClass = skillClass;
    }
}