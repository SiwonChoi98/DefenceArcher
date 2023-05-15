using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlame : MonoBehaviour
{
    public int damage = 35;

    public BoxCollider bressArea;

   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            bressArea.enabled = false;
        }
        

    }

}
