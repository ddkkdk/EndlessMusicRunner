using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    const string Name = "GameOver/@UI_GameOver";
    public static void Create()
    {
        var obj = Resources.Load<GameObject>(Name);
        Instantiate<GameObject>(obj);
    }

    [SerializeField] TextMeshProUGUI[] T_TextList;
    private void Start()
    {
        SetName();
        SetScore();
        SetAccuracy();
        SetBestCombo();
        SetScoreState();
        SetRating();
    }

    void SetName()
    {
        T_TextList[0].text = "엘라스타즈";
    }

    void SetScore()
    {
        T_TextList[1].text = ScoreManager.instance.GetCurrentScore().ToString();
        T_TextList[2].text = ScoreManager.instance.GetBestScore().ToString();
        ScoreManager.instance.SetBestScore();
    }

    void SetAccuracy()
    {
        var maxcount = ScoreManager.instance.GetMaxState();
        var count = ScoreManager.instance.GetAccuracy();

        var accuracy = (float)maxcount / (float)count;

        T_TextList[3].text = "정확도 : " + accuracy.ToString();
    }

    void SetBestCombo()
    {
        T_TextList[4].text = "최고 콤보수\n" + ScoreManager.instance.GetBestCombo().ToString();
    }

    void SetScoreState()
    {
        T_TextList[5].text = "PERFECT : " + ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Perfect).ToString();
        T_TextList[6].text = "GREAT : " + ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Great).ToString();
        T_TextList[7].text = "EARLY : " + ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Early).ToString();
        T_TextList[8].text = "LATE : " + ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Late).ToString();
        T_TextList[9].text = "PASS : " + ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Pass).ToString();
        T_TextList[10].text = "MISS : " + ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Miss).ToString();
    }

    void SetRating()
    {
        T_TextList[11].text = "S";
    }

    public void Btn_Exit()
    {
        SecenManager.LoadScene("UIScene");
    }

    public void Btn_RePlay()
    {
        SecenManager.LoadScene("MainGameScene");
    }
}