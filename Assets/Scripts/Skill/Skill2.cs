using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{

    public int damage;//skill2 �� ������

    public BoxCollider skillArea;
    private void OnTriggerEnter(Collider other)
    {
            
         StartCoroutine(skill2Effect());
        
           


    }


    IEnumerator skill2Effect()
    {
        yield return new WaitForSeconds(1f); // ��ų ���ӽð�
        skillArea.enabled = false;
        yield return new WaitForSeconds(1.5f); // ��ų �ʱ�ȭ�ð�
        skillArea.enabled = true;

    }
}
