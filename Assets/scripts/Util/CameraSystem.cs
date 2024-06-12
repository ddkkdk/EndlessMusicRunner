using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public static CameraSystem cameraSystem;
    [SerializeField] Camera camera_main;

    [SerializeField] Vector3 orginPos;
    [SerializeField] float orginSize;

    [SerializeField] Vector3 ZoomInPos;
    [SerializeField] float ZoomInSize;

    [SerializeField] float Up_Down_Szie;

    float targetSize;
    Vector3 target;

    private void Awake()
    {
        cameraSystem = this;
        targetSize = orginSize;
        target = this.transform.position;
    }

    private void Update()
    {
        SetZoom();
        SetMove();
    }

    void SetZoom()
    {
        var cursize = camera_main.orthographicSize;

        if (cursize >= targetSize)
        {
            camera_main.orthographicSize = targetSize;
            return;
        }

        camera_main.orthographicSize += Up_Down_Szie;
    }

    void SetMove()
    {
        var pos = transform.position;
        var updownsize = target.x > pos.x ? Up_Down_Szie : -Up_Down_Szie;

        var check = updownsize > 0 ? pos.x >= target.x : pos.x <= target.x;

        if (check)
        {
            return;
        }

        pos.x += updownsize;
        transform.position = pos;
    }

    public void SetZoomIn()
    {
        targetSize = ZoomInSize;
        target = ZoomInPos;
    }

    public void ReSetZoom()
    {
        targetSize = orginSize;
        target = orginPos;
    }
}
