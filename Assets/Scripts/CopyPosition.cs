using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{  //[�̴ϸ�] Ÿ���� ���� �ٴϴ� ī�޶� ��ũ��Ʈ
    [SerializeField] private bool x, y, z; // �� ���� true�� target�� ��ǥ, false�� ���� ��ǥ �״�� ���
    [SerializeField] private Transform target; //�i�ư����� ��� Transform
   

    // Update is called once per frame
    void Update()
    {
        //�i�ư����� ����� ������ ����
        if (!target) return;

        transform.position = new Vector3((x ? target.position.x : transform.position.x), (y ? target.position.y : transform.position.y), (z ? target.position.z : transform.position.z));
    }
}
