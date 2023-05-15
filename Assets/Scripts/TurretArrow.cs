using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretArrow : MonoBehaviour
{
    public int damage;
    public int maxDamage;

    private void OnTriggerEnter(Collider other)
    {

        //Rigidbody arrowRigid = arrow.GetComponent<Rigidbody>();
        //arrowRigid.velocity = new Vector3(0, 0, 0); // 나중에 완성도 높일 때 쓰일거같다.
        if (other.tag != "Player")
            Destroy(gameObject);


    }
}
