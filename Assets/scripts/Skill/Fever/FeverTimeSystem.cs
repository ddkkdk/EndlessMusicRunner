using UnityEngine;


public class FerverTimeSystem : MonoBehaviour
{
    const string BackGroundName = "FeverTime_{0}";

    //쿨타임
    static float CoolTime;
    public static bool Active_FeverTime;

    //피버타임 발동
    public static async void Create(int combo, float activetime, float cooltime, int backnum)
    {
        //조건 확인
        var checkcombo = CheckComboCondition(combo);
        var cooltimecheck = CheckCoolTime();
        if (!checkcombo || !cooltimecheck)
        {
            return;
        }

        var name = string.Format(BackGroundName, backnum);
        var result = await name.CreateOBJ<FerverTimeSystem>();
        result.Setup(activetime, cooltime);
    }


    //콤보 확인
    static bool CheckComboCondition(int combo)
    {
        return ScoreManager._instance.CurrentCombo >= combo;
    }

    //쿨타임 확인
    static bool CheckCoolTime()
    {
        return CoolTime <= 0;
    }

    //점수 정산
    public static int SetFeverScore(int currentscore)
    {
        if (!Active_FeverTime)
        {
            return currentscore;
        }

        return currentscore * 2;
    }

    //쿨타임 감소
    public static void SetCooltime()
    {
        CoolTime -= Time.deltaTime;
        //쿨타임 UI적용
    }


    //#################################### 위 static형식

    //지속시간
    float ActiveTime;

    private void Update()
    {
        var active = SetActive();
        if (!active)
        {
            return;
        }
    }

    public void Setup(float activetime, float cooltime)
    {
        CoolTime = cooltime;
        ActiveTime = activetime;
        Active_FeverTime = true;
    }

    //지속시간
    bool SetActive()
    {
        ActiveTime -= Time.deltaTime;

        if (ActiveTime <= 0)
        {
            Destroy(this.gameObject);
            return false;
        }
        //지속 시간 UI 추가
        return true;
    }


    private void OnDestroy()
    {
        Active_FeverTime = false;
    }
}
