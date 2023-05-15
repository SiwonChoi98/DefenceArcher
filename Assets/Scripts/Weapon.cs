using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public int damage; //�������� ���� ������
    public float rate; //�������� ���� �ӵ�
    public BoxCollider meleeArea; //�������� ���� ����
    

    public Transform ArrowPos;
    public GameObject Arrow;

    public Transform ArrowPos2;
    public GameObject Arrow2;

    public Transform ArrowPos3;
    public GameObject Arrow3;
    public void UseBow()
    {
        StartCoroutine("Shot");
    }
    public void UseBow2()
    {
        StartCoroutine("Shot2");
    }
    public void UseBow3()
    {
        StartCoroutine("Shot3");
    }

    /*public void UseSkill()
    {
        StartCoroutine("Skill");
    }
    */
    public void UseMelee()
    {
        StopCoroutine("Melee");
        StartCoroutine("Melee");
    }


    IEnumerator Shot()
    {
        GameObject intantArrow = Instantiate(Arrow, ArrowPos.position, ArrowPos.rotation);
        Rigidbody arrowRigid = intantArrow.GetComponent<Rigidbody>();
        arrowRigid.velocity = ArrowPos.forward * 18;

        

        yield return null;
    }
    IEnumerator Shot2() //���׷��̵� ȭ��
    {
        GameObject intantArrow = Instantiate(Arrow, ArrowPos.position, ArrowPos.rotation);
        Rigidbody arrowRigid = intantArrow.GetComponent<Rigidbody>();
        arrowRigid.velocity = ArrowPos.forward * 18;
        GameObject intantArrow2 = Instantiate(Arrow2, ArrowPos2.position, ArrowPos2.rotation);
        Rigidbody arrowRigid2 = intantArrow2.GetComponent<Rigidbody>();
        arrowRigid2.velocity = ArrowPos2.forward * 18;



        yield return null;
    }
    IEnumerator Shot3() //���׷��̵�2 ȭ��
    {
        GameObject intantArrow = Instantiate(Arrow, ArrowPos.position, ArrowPos.rotation);
        Rigidbody arrowRigid = intantArrow.GetComponent<Rigidbody>();
        arrowRigid.velocity = ArrowPos.forward * 18;
        GameObject intantArrow2 = Instantiate(Arrow2, ArrowPos2.position, ArrowPos2.rotation);
        Rigidbody arrowRigid2 = intantArrow2.GetComponent<Rigidbody>();
        arrowRigid2.velocity = ArrowPos2.forward * 18;
        GameObject intantArrow3 = Instantiate(Arrow3, ArrowPos3.position, ArrowPos3.rotation);
        Rigidbody arrowRigid3 = intantArrow3.GetComponent<Rigidbody>();
        arrowRigid3.velocity = ArrowPos3.forward * 18;



        yield return null;
    }


   


    IEnumerator Melee()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);

    }
}
