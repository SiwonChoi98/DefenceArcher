using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shop : MonoBehaviour //���� ���� -0502�߰�-
{
    public GameManager manager;
    public RectTransform uiGroup;
    PlayerController enterplayer;
    public TowerHealth tower;
    public Weapon weapon;
    public Arrow arrow; //ȭ�� ���ݷ����� ����
    public TurretArrow turretArrow; //�ͷ� ȭ�� ���ݷ����� ����
    public TurretArrow turretArrowMax;

    //public GameObject[] itemObj; //�����۹迭
    public int[] itemPrice; //�����۰��ݹ迭
    
    //��ƼŬ
    public GameObject hillParticle; //ȸ���� ������ ��ƼŬ
    public GameObject upParticle; //���� ���� ��ƼŬ
    public GameObject powerShieldParticle; // ������ ��ƼŬ

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

    public AudioSource buySound; //������ ���� �Ҹ�
    
   
 
    public void Enter(PlayerController player) //����
    {
        enterplayer = player;
        uiGroup.anchoredPosition = new Vector3(798,-527,0); //ui�� ���߾ӿ� ���Ը����.
        
    }


    public void Exit() //����
    {
        uiGroup.anchoredPosition = Vector3.down * 1800;
    }

    public void Buy(int index) //�����۱���
    {
        int price = itemPrice[index];
        if(price > manager.money) // ������ ���ݺ��� �÷��̾� ���� �� ���� �� ����
        {
            return;

        }
        //������ 1��(����)
        if (index == 0)  
        {
            if (enterplayer.maxHealth >= 300) // �ִ�� �ø� �� �ִ� ü���� 300����
            {
                return;
            }
            //��ƼŬ
            hillParticle.SetActive(true);
            hillParticle.GetComponent<ParticleSystem>().Play();
           
            enterplayer.curHealth += 10; //����ü�� 10 ����
            enterplayer.maxHealth += 10; //�ִ�ü�� 10 ����
            buySound.Play();
            manager.money -= price; //player ������ itemprice �ݾ� ��������
            
            
            if(itemPrice[0] == 1500) //itemPrice[0] ������ 1500���̸� 500���߰� ���ְ� �ؽ�Ʈ 2000���� ������
            {
                itemPrice[0] += 500;
                ItemATxt.text = "2,000";
            }
            else if (itemPrice[0] == 2000) //�������� ������ �� itemPrice[0] ������ 2000���̰� �ؽ�Ʈ�� 2000������ ������ ���¿��� ������ itemPrice[0] 500�� �þ�� �ؽ�Ʈ 2500�� �ؿ� ����
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
        //������ 2��(ȭ�� ���� ����) 
        if (index == 1)
        {   //ȭ�� ���� �����ϱ� ���� bool ��
            if (upArrow == true) // upArrow �� false �̱� ������ ó������ �߻����� �ʴ´�. //2��° ���� �� �߻�
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
        //������ 3��(�÷��̾� Ȱ ���ݷ� 5 ����)
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

            if (itemPrice[2] == 3000) //itemPrice[0] ������ 1500���̸� 500���߰� ���ְ� �ؽ�Ʈ 2000���� ������
            {
                itemPrice[2] += 1000;
                ItemCTxt.text = "4,000";
            }
            else if (itemPrice[2] == 4000) //�������� ������ �� itemPrice[0] ������ 2000���̰� �ؽ�Ʈ�� 2000������ ������ ���¿��� ������ itemPrice[0] 500�� �þ�� �ؽ�Ʈ 2500�� �ؿ� ����
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
        //������ 4��(������ ����)
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
        //������ 5�� (�� ü�� 100����) 
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
        //������ 6�� (��ü�� 100ȸ��)
        if (index == 5)
        {
            if (tower.towerCurHealth == tower.towerMaxHealth) //Ÿ�� ü���� max�� ���ȳ����� �����
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
        
        //������ 8�� (������ ȭ�� ���ݷ� 5 ����) 
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
