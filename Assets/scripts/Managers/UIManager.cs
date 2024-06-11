using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject instructionPanel;

    public TextMeshProUGUI scoreTxt_1;
    public TextMeshProUGUI scoreTxt_2;
    public GameObject combo;
    public TextMeshProUGUI comboTxt_1;
    public TextMeshProUGUI comboTxt_2;
    public GameObject UpperPanel;
    public GameObject lowerPanel;
    public float instructionPanelActivationTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);

        }
        else
        {
            Instance = this;
        }
    }
    public void ActivatPanel(bool activate)
    {
        ScoreManager.instance.ScoreReset();
        instructionPanel.SetActive(activate);
        StartCoroutine(DeactivatePanel());

    }
    public void UpperPanelActive(bool activate)
    {
        UpperPanel.gameObject.SetActive(true);
        LowerPanelActivate(true);


    }
    public void LowerPanelActivate(bool activate)
    {
        lowerPanel.gameObject.SetActive(true);
    }
    IEnumerator DeactivatePanel()
    {
        yield return new WaitForSeconds(instructionPanelActivationTime);
        instructionPanel.SetActive(false);
        UpperPanelActive(true);
        SpawnManager.instance.StartSpawningObjects(true);
    }

    public void ScoreUpdater(int score)
    {
        scoreTxt_2.text = score.ToString();
    }

    public void ResetComboScoreUpdater()
    {
        combo.gameObject.SetActive(false);
    }

    public void ComboScoreUpdater(int score)
    {
        comboTxt_1.text = score.ToString();
        comboTxt_2.text = score.ToString();
        combo.gameObject.SetActive(true);
    }
}
