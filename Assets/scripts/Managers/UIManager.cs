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
    public TextMeshProUGUI scoreTxt;
    public GameObject combo;
    public TextMeshProUGUI comboTxt;
    public GameObject UpperPanel;
    public GameObject lowerPanel;
    public float upperPosition;
    public Image fillAmount;

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
        //Invoke("LowerPanelActivate", 0.2f);
        LowerPanelActivate(true);

    }
    public void LowerPanelActivate(bool activate) 
    {
        lowerPanel.gameObject.SetActive(true);
    }
    IEnumerator DeactivatePanel() 
    {
        yield return new WaitForSeconds(5f);
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
            comboTxt.gameObject.SetActive(true);
        
        }
     
    }


    public void SetHealthBar(float currentHealth, float maxHealth) 
    {
        currentHealth = HealthBar.Instance.currentHealth;
        maxHealth= HealthBar.Instance.maxHealth;
        fillAmount.fillAmount = currentHealth / maxHealth;

    
    }
    
}
