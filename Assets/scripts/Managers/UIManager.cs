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
    public TextMeshProUGUI scoreTxt;
    public GameObject combo;
    public TextMeshProUGUI comboTxt;
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
        scoreTxt.text=score.ToString();

        if (score >= 5) 
        {
            combo.gameObject.SetActive(true);
            comboTxt.text=score.ToString(); 
          
        
        }
     
    }


  
    
}
