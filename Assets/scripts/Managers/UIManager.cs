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
    public GameObject attackPoints;
    public TextMeshProUGUI scoreTxt_1;
    public TextMeshProUGUI scoreTxt_2;
    public GameObject combo;
    public TextMeshProUGUI comboTxt_1;
    public TextMeshProUGUI comboTxt_2;
    public GameObject UpperPanel;
    public GameObject lowerPanel;
    public float upperPosition;
    public Image fillAmount;
    public float instructionPanelActivationTime;

    private void Start()
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
        scoreTxt_1.text=score.ToString();
        scoreTxt_2.text = score.ToString();

        if (score >= 5) 
        {
            combo.gameObject.SetActive(true);
            comboTxt_1.text=score.ToString();
            comboTxt_2.text = score.ToString();


        }
     
    }


  
    
}
