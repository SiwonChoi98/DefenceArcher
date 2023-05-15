using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSword : MonoBehaviour
{

    public int damage = 10;


    private void OnTriggerEnter(Collider other)
    {

        Rigidbody arrowRigid = this.GetComponent<Rigidbody>();
       
        if (other.tag == "Wall" || other.tag == "Player" || other.tag == "Floor")
            arrowRigid.velocity = new Vector3(0, 0, 0); // �ٴڿ� ���� ������ ������ �������� �ֵ��� �������.
            Destroy(gameObject,2f);



    }




}
