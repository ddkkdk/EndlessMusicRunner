using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_SendBack : MonoBehaviour
{
    [SerializeField] MoveLeft M_MoveLeft;
    [SerializeField] Monster monster;

    bool EndTimes;
    float DelayTime = 3;

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
        if (!EndTimes)
        {
            return;
        }

        DelayTime -= Time.deltaTime;

        if (DelayTime > 0)
        {
            return;
        }

        monster.SetNoneAttack();
        SetZoomOut();
        M_MoveLeft.speed = 20;
        return;
    }

    void SetZoomIn()
    {
        EndTimes = true;
        M_MoveLeft.speed = 0;
        GameManager.instance.player.SetDirectMoveIdx(PlayerSystem.E_AttackPoint.Middle);
        CameraSystem.cameraSystem.SetZoomIn();
    }

    void SetZoomOut()
    {
        CameraSystem.cameraSystem.ReSetZoom();
    }
}