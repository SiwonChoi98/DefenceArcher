using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMinimap : MonoBehaviour
{   //미니맵 기능 중 확대, 축소 기능을 사용하려고 만든 스크립트
    [SerializeField] private Camera minimapCamera; //서브카메라(미니맵카메라) 생성해서 넣어줌
    [SerializeField] private float zoomMin = 1; //카메라의 orthographicSize 최소 크기
    [SerializeField] private float zoomMax = 30; //카메라의 orthographicSize 최대 크기
    [SerializeField] private float zoomOneStep = 1; //1회 줌 할 때 증가/감소되는 수치

    void Awake()
    {
        
    }

    // Update is called once per frame
    public void ZoomIn()
    {
        minimapCamera.orthographicSize = Mathf.Max(minimapCamera.orthographicSize - zoomOneStep, zoomMin);
    }
    public void ZoomOut()
    {
        minimapCamera.orthographicSize = Mathf.Min(minimapCamera.orthographicSize + zoomOneStep, zoomMax);
    }
}
