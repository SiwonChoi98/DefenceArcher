using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : MonoBehaviour
{

    public int damage;//skill2 의 데미지

    public BoxCollider skillArea;
    private void OnTriggerEnter(Collider other)
    {
            
         StartCoroutine(skill2Effect());
        
           


    }


    IEnumerator skill2Effect()
    {
        yield return new WaitForSeconds(1f); // 스킬 지속시간
        skillArea.enabled = false;
        yield return new WaitForSeconds(1.5f); // 스킬 초기화시간
        skillArea.enabled = true;

    }
}
