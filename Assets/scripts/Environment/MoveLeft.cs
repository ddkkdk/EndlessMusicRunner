using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    public int monsterNumber;
    public bool SendBack;
    public float OffSetX;
    float DelayTime = 3;
    [SerializeField] float DestoryX;

    bool EndTimes;
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (SendBack)
        {
            var pos = GameManager.instance.player.transform.position;
            pos.y = this.transform.position.y;
            if (EndTimes)
            {
                pos.x += -100;
                transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
                return;
            }

            pos.x += OffSetX;
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, pos) < 0.1f)
            {
                DelayTime -= Time.deltaTime;

                if (DelayTime >= 0)
                {
                    return;
                }

                EndTimes = true;
            }
        }

        var values = DestoryX == 0 ? -60 : DestoryX;

        if (transform.position.x < values)
        {
            Destroy(this.gameObject);
        }

    }
}
