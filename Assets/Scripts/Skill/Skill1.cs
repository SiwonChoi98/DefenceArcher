using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    


    public int damage = 20;//skill1 �� ������


    public BoxCollider skillArea;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(skill1Effect());

        
    }


    IEnumerator skill1Effect()
    {
        yield return new WaitForSeconds(0.3f); // ����ȭ�� �����ִ� �ð�
        skillArea.enabled = false;



    }


}

