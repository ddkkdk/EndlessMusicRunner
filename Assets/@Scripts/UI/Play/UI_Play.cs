using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Play : MonoBehaviour
{
    public static UI_Play Instance;

    [Header("키 설명")]
    public GameObject instructionPanel;

    [Header("콤보 오브젝트")]
    public GameObject combo;

    [Header("콤보 및 스코어 텍스트")]
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI comboTxt;

    float DelayTime = 7;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivatPanel(bool activate)
    {
        ScoreManager.instance.ScoreReset();
        instructionPanel.SetActive(activate);
        StartCoroutine(DeactivatePanel());
    }

    IEnumerator DeactivatePanel()
    {
        yield return new WaitForSeconds(DelayTime);
        instructionPanel.SetActive(false);
        SpawnManager.instance.StartSpawningObjects(true);
    }

    //스코어 셋팅    
    public void SetScore(int score)
    {
        scoreTxt.text = score.ToString();
    }

    //콤보 리셋
    public void Reset_Combo()
    {
        combo.SetActive(false);
    }

    //콤보 셋팅
    public void Set_Combo(int score)
    {
        comboTxt.text = score.ToString();
        combo.SetActive(true);
    }
    public async void Btn_Pause()
    {
        var name = "UI_Pause";
        await name.CreateOBJ<UI_Pause>();
    }
}