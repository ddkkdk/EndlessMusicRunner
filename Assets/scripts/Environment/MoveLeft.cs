using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    public int monsterNumber;
    [SerializeField] float DestoryX;

    void Update()
    {
        // 오브젝트를 왼쪽으로 이동
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // DestoryX 값이 0이면 -60, 아니면 DestoryX 사용
        var values = DestoryX == 0 ? -60 : DestoryX;

        // 오브젝트가 특정 위치를 벗어나면 파괴
        if (transform.position.x < values)
        {
            Destroy(this.gameObject);
        }
    }
}
