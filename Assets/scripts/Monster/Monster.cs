using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public int damageAmount;
    public GameObject damageFx;


    private void Update()
    {
        if (this.gameObject.transform.position.y >= 3.47 && this.gameObject.transform.position.x<=-56)
        {

            UpperHitCollisionDetection.Instance.comboScore = 0;
            HitCollisionDetection.Instance.comboScore = 0;
            UIManager.Instance.ComboScoreUpdater(0);


        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contactPoint = other.contacts[0];
        Vector2 hitPoint = contactPoint.point;

        if (other.gameObject.tag == "Player")
        {          
            other.gameObject.GetComponent<PlayerController>().Damage(damageAmount);
            GameObject opsFx = Instantiate(damageFx, hitPoint, Quaternion.identity);

            UpperHitCollisionDetection.Instance.comboScore = 0;
            HitCollisionDetection.Instance.comboScore = 0;
            UIManager.Instance.ComboScoreUpdater(0);

            HitMoveAnimation(opsFx,hitPoint);
            Destroy(opsFx,0.2f);

        }

    }
    public void HitMoveAnimation(GameObject hitFx,Vector2 hitPoint)
    {
        hitFx.transform.DOMoveY(hitPoint.y + 5, 0.5f).SetEase(Ease.OutBounce);
        
        
    }




}
