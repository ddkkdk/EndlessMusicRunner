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

    }

    public virtual void SetHP(int damageAmount)
    {
        currentHealth += damageAmount;
        SetAddHp(damageAmount);
        SetMinusHp(damageAmount);
        SetHealthBar();
    }

    //HP획득 처리
    void SetAddHp(int damageAmount)
    {
        if (damageAmount <= 0)
        {
            return;
        }

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //HP깎임 처리
    void SetMinusHp(int damageAmount)
    {
        if (damageAmount > 0)
        {
            return;
        }

        //쉴드 작동 확인
        var checkshield = ShieldBuster.CheckShield();
        if (checkshield)
        {
            currentHealth += -damageAmount;
            return;
        }

        //콤보 리셋 및 MISS추가
        ScoreManager.instance.SetScoreState(ScoreManager.E_ScoreState.Miss);
        ScoreManager.instance.SetBestCombo_Reset();

        //공격 사운드 및 애니메이션 처리
        PlayerSystem.SetPlayerAni(PlayerSystem.E_AniType.Hit);
        AudioManager.instance.PlayerHItSound();

        //사망처리
        if (currentHealth <= 0)
        {
            AudioManager.instance.StopMusic();
            SpawnManager.instance.StopAllCoroutines();
            UI_GameOver.Create();
        }
    }

    public bool SetMonsterHp(int damageAmount)
    {
        currentHealth += damageAmount;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

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
