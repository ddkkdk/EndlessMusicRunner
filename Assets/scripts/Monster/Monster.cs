using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : Entity
{
    public int damageAmount;
    public GameObject damageFx;
    [SerializeField] bool Change;
    [SerializeField] SkeletonDataAsset[] Sk;
    [SerializeField] SkeletonAnimation My;
    bool _Attack;
    public static void Create(string folderName, string name, Transform CreatePos, int hp, int speed, UniqMonster Uniq_MonsterType)
    {
        string path = $"{folderName}/{name}";
        var load = Resources.Load<GameObject>(path);
        var monster = Instantiate<GameObject>(load);
        var monsterValue = monster.GetComponent<Monster>();
        monsterValue.maxHealth = hp;
        var monsterValue2 = monster.GetComponent<MoveLeft>();
        monsterValue2.speed = speed;
        monsterValue2.uniqMonster = Uniq_MonsterType;
        monster.transform.position = CreatePos.position;

    }

    public System.Action Ac_Hit;
    public System.Action Ac_Die;
    private void Start()
    {
        if (Change)
        {
            My.skeletonDataAsset = Sk[UI_Lobby.Type == false ? 0 : 1];
            My.Initialize(true);
        }
    }

    private void Update()
    {
        SetAttack();
    }

    void SetAttack()
    {
        if (_Attack)
        {
            return;
        }

        var player = GameManager.instance.player;

        var targetpos = player.transform.position;

        if (targetpos.x < transform.position.x)
        {
            return;
        }

        _Attack = true;

        //몬스터와 플레이어의 Y포지션으로 위치잡아서 플레이어게 데미지전달
        float playerYPosSpare = 0.5f;
        var monsterPositionY = transform.position.y;
        var monsterType = GetComponent<MoveLeft>().uniqMonster;
        //샌드백 일경우 플레이어를 지나감
        //일반 몬스터는 통용적으로 가능함
        bool isMonsterAttackingPlayer =
            (targetpos.y + playerYPosSpare >= monsterPositionY && targetpos.y - playerYPosSpare <= monsterPositionY);
        bool isSendBack = monsterType == UniqMonster.SendBack;
        if (isSendBack || isMonsterAttackingPlayer)
        {
            player.SetHP(damageAmount *-1);
            GameObject opsFx = Instantiate(damageFx, transform.position, Quaternion.identity);
            HitMoveAnimation(opsFx, transform.position);
            Destroy(opsFx, 0.2f);
        }
        // 뮤즈대쉬 처럼 몬스터가 플레이어 그냥 지나칠때 콤보 초기화
        else if (targetpos.x >= transform.position.x)
        {
            ScoreManager.instance.SetBestCombo_Reset();
        }
    }

    public void SetHit(ScoreManager.E_ScoreState perfect)
    {
        Ac_Hit?.Invoke();
        SetMonsterHp(-1);
        _Attack = true;
        HitCollisionDetection.Instance.SetHit(this.gameObject, perfect);
        SetDie(currentHealth <= 0);
    }

    public void SetDie(bool checkdie)
    {
        if (!checkdie)
        {
            return;
        }
        Ac_Die?.Invoke();

        SetNoneAttack();
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<MoveLeft>().speed = 0;
        float position = transform.position.y;

        if (position > -8)
        {
            //Debug.Log("Hit Upper");
            GetComponent<Rigidbody2D>().AddForce(-transform.up * 50, ForceMode2D.Impulse);
        }

        Destroy(this.gameObject, 1f);
    }

    public void SetNoneAttack()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public void HitMoveAnimation(GameObject hitFx, Vector2 hitPoint)
    {
        hitFx.transform.DOMoveY(hitPoint.y + 5, 0.5f).SetEase(Ease.OutBounce);
    }
}

public enum UniqMonster
{
    Normal, SendBack,
}
public enum Monster_Type
{
    Normal, Boss
}