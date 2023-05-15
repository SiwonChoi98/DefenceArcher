using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset; //새로 추가
    //private Vector3 lookOffset = new Vector3(0, 5.0f, -4.0f);
    public Texture2D mouseCursorIMG;

    //카메라 흔들림 연출

    Vector3 cameraPos;
    public Camera bossFrontCamera;
    [Range(0.01f, 0.1f)] float shakeRange = 0.1f;
    [Range(0.1f, 1f)] float duration = 1.5f;

    void Start()
    {
        Cursor.SetCursor(mouseCursorIMG, Vector2.zero, CursorMode.ForceSoftware);  
    }
    void LateUpdate()
    {//만약 target이 제대로 설정이 안되었다면, unitychan을 찾아서 넣어준다.
        if (target == null)
        {
            GameObject go = GameObject.Find("Mesh Object");
            target = go.transform;
        }
        // 대상으로부터 offset 만큼 떨어져 있도록 설정
        transform.position = target.position + Offset;

        // 대상을 보도록 설정


        //transform.LookAt(target);

    }
    public void Shake()
    {
        cameraPos = bossFrontCamera.transform.position;
        InvokeRepeating("ShakeStart", 0f, 0.005f);
        Invoke("ShakeEnd", duration);
    }

    public void ShakeStart()
    {
        
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = bossFrontCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        bossFrontCamera.transform.position = cameraPos;
    }
    public void ShakeEnd()
    {
        CancelInvoke("ShakeStart");
        bossFrontCamera.transform.position = cameraPos;
    }
}


