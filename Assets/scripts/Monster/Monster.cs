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
    public static void Create(string folderName,string name,Transform CreatePos,int hp,int speed, UniqMonster Uniq_MonsterType)
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
        player.Damage(damageAmount);
        GameObject opsFx = Instantiate(damageFx, transform.position, Quaternion.identity);
        UIManager.Instance.ResetComboScoreUpdater();
        HitMoveAnimation(opsFx, transform.position);
        Destroy(opsFx, 0.2f);
    }

    public void SetHit(bool perfect)
    {
        _Attack = true;
        HitCollisionDetection.Instance.SetHit(this.gameObject, perfect);
    }


    public void HitMoveAnimation(GameObject hitFx, Vector2 hitPoint)
    {
        hitFx.transform.DOMoveY(hitPoint.y + 5, 0.5f).SetEase(Ease.OutBounce);
    }
}

public enum UniqMonster
{
    Normal,SendBack,
}
public enum Monster_Type
{
    Normal,Boss
}