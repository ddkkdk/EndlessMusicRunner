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
        ScoreManager.instance.SetScoreState(ScoreManager.E_ScoreState.Miss);
        ScoreManager.instance.SetBestCombo_Reset();
        currentHealth -= damageAmount;
        SetHealthBar();
        PlayerSystem.SetPlayerAni(PlayerSystem.E_AniType.Hit);
        AudioManager.instance.PlayerHItSound();
        if (currentHealth <= 0)
        {
            AudioManager.instance.StopMusic();
            SpawnManager.instance.StopAllCoroutines();
            UI_GameOver.Create();
        }
    }

    public bool MonsterDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        return currentHealth <= 0;
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
