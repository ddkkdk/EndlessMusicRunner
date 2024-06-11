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

    public UniqMonster uniqMonster;

    bool EndTimes;
    void Update()
    {
        // 오브젝트를 왼쪽으로 이동
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (SendBack || uniqMonster == UniqMonster.SendBack)
        {
            // EndTimes가 true이면 속도를 20으로 설정하고 종료
            if (EndTimes)
            {
                speed = 20;
                return;
            }

            // 플레이어 위치를 가져와서 오프셋을 적용
            var pos = GameManager.instance.player.transform.position;
            pos.y = this.transform.position.y;
            pos.x += OffSetX;

            // 현재 위치가 pos에 도달했는지 확인
            float distance = Vector3.Distance(transform.position, pos);

            if (distance < 0.5f)
            {
                speed = 0;
                DelayTime -= Time.deltaTime;

                // DelayTime이 0보다 크거나 같으면 리턴
                if (DelayTime >= 0)
                {
                    return;
                }

                // DelayTime이 0보다 작아지면 EndTimes를 true로 설정
                EndTimes = true;
            }
        }

        // DestoryX 값이 0이면 -60, 아니면 DestoryX 사용
        var values = DestoryX == 0 ? -60 : DestoryX;

        // 오브젝트가 특정 위치를 벗어나면 파괴
        if (transform.position.x < values)
        {
            Destroy(this.gameObject);
        }
    }
}
