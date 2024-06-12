using System.Threading.Tasks;
using UnityEngine;

public struct St_Skill
{
    public int combo;
    public float activetime;
    public float cooltime;
    public int objnum;

    public St_Skill(int combo, float activetime, float cooltime, int objnum)
    {
        this.combo = combo;
        this.activetime = activetime;
        this.cooltime = cooltime;
        this.objnum = objnum;
    }
}

public class Skill : MonoBehaviour
{
    //생성 코드
    public static async Task<T> Create<T>(St_Skill sk, string key,ISkillClass skillclass) where T : Object
    {
        skillclass.SetClass();
        if (!skillclass.ComboChecker.CheckComboCondition(sk.combo) || !skillclass.CoolTimeChecker.CheckCoolTime())
        {
            return null;
        }
        var result = await key.CreateOBJ<Skill>();
        result.Setup(sk.activetime, sk.cooltime, skillclass);

        return result.GetComponent<T>();
    }

    private float _activeTime;

    protected ISkillClass skillClass;

    protected virtual void Update()
    {
        if (!SetActive())
        {
            return;
        }
    }

    //셋팅 함수
    public virtual void Setup(float activetime, float cooltime, ISkillClass skillclass)
    {
        skillClass = skillclass;
        _activeTime = activetime;
        skillClass.CoolTimeChecker.SetCoolTime(cooltime);
        skillClass.ActiveChecker.SetActive(true);
    }

    //지속시간 확인
    protected virtual bool SetActive()
    {
        _activeTime -= Time.deltaTime;
        if (_activeTime <= 0)
        {
            Destroy(this.gameObject);
            return false;
        }
        return true;
    }

    private void OnDestroy()
    {
        skillClass.ActiveChecker.SetActive(false);
        print("오프");
    }
}