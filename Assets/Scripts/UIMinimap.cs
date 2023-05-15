using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMinimap : MonoBehaviour
{   //�̴ϸ� ��� �� Ȯ��, ��� ����� ����Ϸ��� ���� ��ũ��Ʈ
    [SerializeField] private Camera minimapCamera; //����ī�޶�(�̴ϸ�ī�޶�) �����ؼ� �־���
    [SerializeField] private float zoomMin = 1; //ī�޶��� orthographicSize �ּ� ũ��
    [SerializeField] private float zoomMax = 30; //ī�޶��� orthographicSize �ִ� ũ��
    [SerializeField] private float zoomOneStep = 1; //1ȸ �� �� �� ����/���ҵǴ� ��ġ

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
