using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public int damageAmount;
    public GameObject damageFx;
    [SerializeField] bool Change;
    [SerializeField] SkeletonDataAsset[] Sk;
    [SerializeField] SkeletonAnimation My;
    bool _Attack;

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
        player.Damage(damageAmount);
        GameObject opsFx = Instantiate(damageFx, transform.position, Quaternion.identity);
        HitMoveAnimation(opsFx, transform.position);
        Destroy(opsFx, 0.2f);
    }

    public void SetHit(bool perfect)
    {
        Ac_Hit?.Invoke();
        MonsterDamage(1);
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
