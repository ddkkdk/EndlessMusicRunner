using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_SendBack : MonoBehaviour
{
    [SerializeField] MoveLeft M_MoveLeft;
    [SerializeField] Monster monster;

    bool EndTimes;
    float DelayTime = 3;
    public float OffSetX;

    private void Start()
    {
        monster.Ac_Hit += SetZoomIn;
        monster.Ac_Die += SetZoomOut;
    }

    // Update is called once per frame
    void Update()
    {
        SetMove();
    }

    void SetMove()
    {
        // EndTimes가 true이면 속도를 20으로 설정하고 종료
        if (EndTimes)
        {
            monster.SetNoneAttack();
            SetZoomOut();
            M_MoveLeft.speed = 20;
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
            M_MoveLeft.speed = 0;
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

    void SetZoomIn()
    {
        GameManager.instance.player.SetDirectMoveIdx(PlayerSystem.E_AttackPoint.Middle);
        CameraSystem.cameraSystem.SetZoomIn();
    }

    void SetZoomOut()
    {
        GameManager.instance.player.SetDirectMoveIdx(PlayerSystem.E_AttackPoint.Down);
        CameraSystem.cameraSystem.ReSetZoom();
    }
}