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
    public AudioSource dieSound; //죽었을때 사운드

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon")
        {
            Enemy enemyWeapon = other.GetComponentInParent<Enemy>();
            towerCurHealth -= enemyWeapon.damage;
            if (towerCurHealth <= 0) //몬스터 무기에 맞았는데 타워 체력이 0이하로 내려가면 
            {
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                towerCurHealth = 0;
                StartCoroutine(waitFortime());
            }
            
        }


    }
    IEnumerator waitFortime() //죽는모션 이후에 1.3초뒤에 패배판넬 나타나게하고 타임스케일 0으로 만든다. 퍼스트 씬에서 게임시작 누를시 다시 1이된다.
    {
        yield return new WaitForSeconds(1.2f);
        manager.DiePanel.SetActive(true);
        dieSound.Play();
        Time.timeScale = 0; //게임스피드 0이라는 뜻 즉 일시정지됨  = 1 이되면 다시 원래 스피드로 됨 (재개)

    }


}
