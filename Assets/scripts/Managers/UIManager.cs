using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject instructionPanel;

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
    IEnumerator DeactivatePanel() 
    {
        yield return new WaitForSeconds(5f);
        instructionPanel.SetActive(false);
        SpawnManager.instance.StartSpawningObjects(true);
    
    }
}
