using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{


    public float maxHealth;
    public float currentHealth;
    public Image fillAmount;
    public bool isOnGround = true;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
    protected virtual void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;

    }



    public virtual void Damage(int damageAmount)
    {

        currentHealth -= damageAmount;
        SetHealthBar();
        if (currentHealth <= 0)
            SceneManager.LoadScene("UIScene");
        FlashFx.Instance.callFlash();
        AudioManager.instance.PlayerHItSound();

    }

    public void SetHealthBar()
    {
        if (fillAmount == null)
        {
            return;
        }
        fillAmount.fillAmount = currentHealth / maxHealth;


    }
}
