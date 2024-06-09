using UnityEngine;

public class LongNote : MonoBehaviour
{
    [SerializeField] Transform Tr;
    [SerializeField] GameObject G_Effect;
    [SerializeField] GameObject G_End;
    [SerializeField] Transform start_1;
    [SerializeField] Transform start_2;
    [SerializeField] GameObject lowecollision;

    GameObject Effect;

    float Dealy = 0f;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player")
        {
            return;
        }
       

        if (PlayerController.CheckHold)
        {
            Dealy -= Time.deltaTime;
            if (Dealy <= 0)
            {
                AudioManager.instance.PlaySound();
                Dealy = 0.1f;
            }

            if (Effect == null)
            {
                //var createpos = GameManager.instance.skeleton.transform.position;
                var createpos = GameManager.instance.lowerAttackPoint.transform.position;
               
               // createpos.x += 1;
                //createpos.y = 0;
                Effect = Instantiate(G_Effect, createpos, default, null);
            }

            var scale = Tr.localScale;
            scale.x -= 0.14f;

            Tr.localScale = scale;

            if (Tr.localScale.x <= 0)
            {
                Destroy(this.gameObject);
                Destroy(Effect);
                var createpos = GameManager.instance.skeleton.transform.position;
                createpos.x += 1;
                createpos.y = 0;
                var end = Instantiate(G_End, createpos, default, null);
                Destroy(end, 1f);
            }
            return;
        }
        if (Effect)
        {
            Destroy(Effect);
            
            
        }

        var box = Tr.GetComponent<BoxCollider2D>();
        box.enabled = false;
    }
}