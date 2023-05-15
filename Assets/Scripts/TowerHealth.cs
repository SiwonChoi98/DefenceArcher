using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public float towerCurHealth;
    public float towerMaxHealth;

    public float Shield;
    public float maxsShield;

    public GameManager manager;
    public AudioSource dieSound; //�׾����� ����

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon")
        {
            Enemy enemyWeapon = other.GetComponentInParent<Enemy>();
            towerCurHealth -= enemyWeapon.damage;
            if (towerCurHealth <= 0) //���� ���⿡ �¾Ҵµ� Ÿ�� ü���� 0���Ϸ� �������� 
            {
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                towerCurHealth = 0;
                StartCoroutine(waitFortime());
            }
            
        }


    }
    IEnumerator waitFortime() //�״¸�� ���Ŀ� 1.3�ʵڿ� �й��ǳ� ��Ÿ�����ϰ� Ÿ�ӽ����� 0���� �����. �۽�Ʈ ������ ���ӽ��� ������ �ٽ� 1�̵ȴ�.
    {
        yield return new WaitForSeconds(1.2f);
        manager.DiePanel.SetActive(true);
        dieSound.Play();
        Time.timeScale = 0; //���ӽ��ǵ� 0�̶�� �� �� �Ͻ�������  = 1 �̵Ǹ� �ٽ� ���� ���ǵ�� �� (�簳)

    }


}
