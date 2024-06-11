using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] T_TextList;
    [SerializeField] GameObject[] Raring;

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

        T_TextList[3].text = accuracy.ToString();
    }

    void SetBestCombo()
    {
        T_TextList[4].text = ScoreManager.instance.GetBestCombo().ToString();
    }

    void SetScoreState()
    {
        T_TextList[5].text = ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Perfect).ToString();
        T_TextList[6].text = ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Great).ToString();
        T_TextList[7].text = ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Early).ToString();
        T_TextList[8].text = ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Late).ToString();
        T_TextList[9].text = ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Pass).ToString();
        T_TextList[10].text = ScoreManager.instance.GetScoreState_Count(ScoreManager.E_ScoreState.Miss).ToString();
    }

    void SetRating()
    {
        Raring[0].SetActive(true);
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