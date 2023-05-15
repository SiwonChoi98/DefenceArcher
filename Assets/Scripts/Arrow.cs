using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int damage;
    public int maxDamage;
    public GameObject arrow;
   

    private void OnTriggerEnter(Collider other)
    {
        
        //Rigidbody arrowRigid = arrow.GetComponent<Rigidbody>();
        //arrowRigid.velocity = new Vector3(0, 0, 0); // 나중에 완성도 높일 때 쓰일거같다.
        if(other.tag == "Wall" || other.tag == "Enemy" || other.tag == "Floor" || other.tag == "Tower" || other.tag == "Obstacle")
            Destroy(arrow);
        
        if(other.tag == "BossDoor")
        {
            Destroy(arrow);
        }
        if (other.tag == "BossDoor2")
        {
            Destroy(arrow);
        }
        if (other.tag == "BossDoor3")
        {
            Destroy(arrow);
        }

    }


}
