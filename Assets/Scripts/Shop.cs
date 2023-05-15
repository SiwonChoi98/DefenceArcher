using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour //상점 관련 -0502추가-
{
    public GameManager manager;
    public RectTransform uiGroup;
    PlayerController enterplayer;
    public TowerHealth tower;
    public Weapon weapon;
    public Arrow arrow; //화살 공격력증가 관련
    public TurretArrow turretArrow; //터렛 화살 공격력증가 관련
    public TurretArrow turretArrowMax;

    //public GameObject[] itemObj; //아이템배열
    public int[] itemPrice; //아이템가격배열
    
    //파티클
    public GameObject hillParticle; //회복시 나오는 파티클
    public GameObject upParticle; //방어력 증가 파티클
    public GameObject powerShieldParticle; // 절대방어 파티클

    [Header ("Turret 1")]
    public GameObject turret1;
    public GameObject turret2;
    public GameObject turret3;
    public GameObject turret4;
    public GameObject turret5;
    public GameObject turret6;
    [Header("Turret 2")]
    public GameObject turret7;
    public GameObject turret8;
    public GameObject turret9;
    public GameObject turret10;
    public GameObject turret11;
    public GameObject turret12;
    bool isTurret = false;

    public bool upArrow = false;
    public bool upArrow2 = false;
    public int arrowCount ;
    [SerializeField] public int turretCount = 0;
    [SerializeField] public int maxTurretCount = 12;


    //public Transform turretPos;

    public Text ItemATxt;
    public Text ItemBTxt;
    public Text ItemCTxt;
    public Text ItemDTxt;
    public Text ItemHTxt;

    public AudioSource buySound; //아이템 구매 소리
    
   
 
    public void Enter(PlayerController player) //입장
    {
        enterplayer = player;
        uiGroup.anchoredPosition = new Vector3(798,-527,0); //ui를 정중앙에 오게만든다.
        
    }


    public void Exit() //퇴장
    {
        uiGroup.anchoredPosition = Vector3.down * 1800;
    }

    public void Buy(int index) //아이템구입
    {
        int price = itemPrice[index];
        if(price > manager.money) // 아이템 가격보다 플레이어 돈이 더 없을 때 리턴
        {
            return;

        }
        //아이템 1번(포션)
        if (index == 0)  
        {
            if (enterplayer.maxHealth >= 300) // 최대로 올릴 수 있는 체력은 300까지
            {
                return;
            }
            //파티클
            hillParticle.SetActive(true);
            hillParticle.GetComponent<ParticleSystem>().Play();
           
            enterplayer.curHealth += 10; //현재체력 10 증가
            enterplayer.maxHealth += 10; //최대체력 10 증가
            buySound.Play();
            manager.money -= price; //player 돈에서 itemprice 금액 빠져나감
            
            
            if(itemPrice[0] == 1500) //itemPrice[0] 가격이 1500원이면 500원추가 해주고 텍스트 2000으로 고쳐짐
            {
                itemPrice[0] += 500;
                ItemATxt.text = "2,000";
            }
            else if (itemPrice[0] == 2000) //다음번에 눌렀을 때 itemPrice[0] 가격은 2000원이고 텍스트도 2000원으로 고쳐진 상태에서 누르면 itemPrice[0] 500원 늘어나고 텍스트 2500됨 밑에 동일
            {
                itemPrice[0] += 500;
                ItemATxt.text = "2,500";
            }
            else if (itemPrice[0] == 2500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "3,000";
            }
            else if (itemPrice[0] == 3000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "3,500";
            }
            else if (itemPrice[0] == 3500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "4,000";
            }
            else if (itemPrice[0] == 4000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "4,500";
            }
            else if (itemPrice[0] == 4500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "5,000";
            }
            else if (itemPrice[0] == 5000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "5,500";
            }
            else if (itemPrice[0] == 5500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "6,000";
            }
            else if (itemPrice[0] == 6000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "6,500";
            }
            else if (itemPrice[0] == 6500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "7,000";
            }
            else if (itemPrice[0] == 7000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "7,500";
            }
            else if (itemPrice[0] == 7500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "8,000";
            }
            else if (itemPrice[0] == 8000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "8,500";
            }
            else if (itemPrice[0] == 8500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "9,000";
            }
            else if (itemPrice[0] == 9000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "9,500";
            }
            else if (itemPrice[0] == 9500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "1,0000";
            }
            else if (itemPrice[0] == 10000)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "10,500";
            }
            else if (itemPrice[0] == 10500)
            {
                itemPrice[0] += 500;
                ItemATxt.text = "11,000";
            }
            else if (itemPrice[0] == 11000)
            {
                ItemATxt.text = "MAX";
            }


        }
        //아이템 2번(화살 개수 증가) 
        if (index == 1)
        {   //화살 갯수 구분하기 위한 bool 값
            if (upArrow == true) // upArrow 는 false 이기 때문에 처음에는 발생하지 않는다. //2번째 구매 시 발생
            {
                upArrow2 = true;
            }
            upArrow = true;

            
            if (arrowCount < 2)
            {

                buySound.Play();
                manager.money -= price;
            }
            arrowCount += 1;
            if (arrowCount > 1)
            {
                ItemBTxt.text = "MAX";
                return;
            }
            
            upParticle.SetActive(true);
            upParticle.GetComponent<ParticleSystem>().Play();

            
            
            
            

        }
        //아이템 3번(플레이어 활 공격력 5 증가)
        if (index == 2) 
        {
            if(arrow.damage >= 45)
            {
                ItemCTxt.text = "MAX";
            }
            if(arrow.damage >= 50)
            {
                
                return;
            }
            

            arrow.damage += 5;
            buySound.Play();
            upParticle.SetActive(true);
            upParticle.GetComponent<ParticleSystem>().Play();
            manager.money -= price;

            if (itemPrice[2] == 3000) //itemPrice[0] 가격이 1500원이면 500원추가 해주고 텍스트 2000으로 고쳐짐
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "4,000";
            }
            else if (itemPrice[2] == 4000) //다음번에 눌렀을 때 itemPrice[0] 가격은 2000원이고 텍스트도 2000원으로 고쳐진 상태에서 누르면 itemPrice[0] 500원 늘어나고 텍스트 2500됨 밑에 동일
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "5,000";
            }
            else if (itemPrice[2] == 5000)
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "6,000";
            }
            else if (itemPrice[2] == 6000)
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "7,000";
            }
            else if (itemPrice[2] == 7000)
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "8,000";
            }
            else if (itemPrice[2] == 8000)
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "9,000";
            }
            else if (itemPrice[2] == 9000)
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "10,000";
            }
            else if (itemPrice[2] == 10000)
            {
                ItemCTxt.text = "MAX";
            }
        }
        //아이템 4번(지원군 생성)
        if (index == 3) 
        {
            if(turretCount == 0)
            { 
                turret1.SetActive(true);
            }
            else if (turretCount == 1)
            {
                turret2.SetActive(true);
                
            }
            else if (turretCount == 2)
            {
                turret3.SetActive(true);
                
            }
            else if (turretCount == 3)
            {
                turret4.SetActive(true);
                
            }
            else if (turretCount == 4)
            {
                turret5.SetActive(true);

            }
            else if (turretCount == 5)
            {
                turret6.SetActive(true);
            }
            else if (turretCount == 6)
            {
                turret7.SetActive(true);
            }
            else if (turretCount == 7)
            {
                turret8.SetActive(true);
            }
            else if (turretCount == 8)
            {
                turret9.SetActive(true);
            }
            else if (turretCount == 9)
            {
                turret10.SetActive(true);
            }
            else if (turretCount == 10)
            {
                turret11.SetActive(true);
            }
            else if (turretCount == 11)
            {
                turret12.SetActive(true);
                ItemDTxt.text = "MAX";
            }
            else if (turretCount == 12)
            {
                return;
            }
            buySound.Play();
            turretCount += 1;       
            

            //GameObject turret1 = Instantiate(turret, turretPos.position, turretPos.rotation);
            manager.money -= price;
        }
        //아이템 5번 (성 체력 100증가) 
        if (index == 4) 
        {
            if(tower.towerMaxHealth >= 2000)
            {
                return;
            }
            tower.towerCurHealth += 100;
            tower.towerMaxHealth += 100;
            buySound.Play();
            manager.money -= price;
            
            
        }
        //아이템 6번 (성체력 100회복)
        if (index == 5)
        {
            if (tower.towerCurHealth == tower.towerMaxHealth) //타워 체력이 max면 돈안나가고 못사게
            {
                return;
            }
            else if (tower.towerCurHealth <= tower.towerMaxHealth)
            {
                tower.towerCurHealth += 100;
                if (tower.towerCurHealth >= tower.towerMaxHealth)
                {
                    tower.towerCurHealth = tower.towerMaxHealth;
                }
                buySound.Play();
                manager.money -= price;
            }
           
            
           
            
            





        }
        
        //아이템 8번 (지원군 화살 공격력 5 증가) 
        if (index == 6)
        {
            
            if(turretArrow.damage >= 29)
            {
                ItemHTxt.text = "MAX";
                return;
            }
            if (turretArrowMax.damage >= 29)
            {
                ItemHTxt.text = "MAX";
                return;
            }

            turretArrow.damage += 5;
            turretArrowMax.damage += 5;
            buySound.Play();
            manager.money -= price;
           

        }

    }
    void Update()
    {
       
        

    }

}
