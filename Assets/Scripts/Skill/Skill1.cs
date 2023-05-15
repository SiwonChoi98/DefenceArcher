using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : MonoBehaviour
{
    


    public int damage = 20;//skill1 의 데미지


    public BoxCollider skillArea;
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(skill1Effect());

        
    }


    IEnumerator skill1Effect()
    {
        yield return new WaitForSeconds(0.3f); // 번개화살 남아있는 시간
        skillArea.enabled = false;



    }


}

