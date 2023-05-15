using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public EndingTimeLine endingline;
    public TimeLineController timeLineController;
    public Arrow arrow;
    public Shop shop;
    public PlayerController player;
    public TowerHealth tower;
    public TurretArrow t_arrow;
    public TurretArrow t_arrowMax;
    //������ ���������;� ��
    public Boss blueBoss; 
    public BossSecond greenBoss;
    public BossThird purpleBoss;
    //5���� ü�� ��
    public RectTransform blueBossHealthGroup;
    public RectTransform blueBossHealthBar;
    public Text blueBossHealthText;
    //10���� ü�� ��
    public RectTransform greenBossHealthGroup;
    public RectTransform greenBossHealthBar;
    public Text greenBossHealthText;
    //15���� ü�� ��
    public RectTransform purpleBossHealthGroup;
    public RectTransform purpleBossHealthBar;
    public Text purpleBossHealthText;

    //�÷��̾� ü�� ��
    public RectTransform PlayerHealthGroup;
    public RectTransform PlayerHealthBar;
    public Text PlayerHealthText;
    //��Ƽ�� �ð� ��
    public RectTransform BattleTimeGroup;
    public RectTransform BattleTimeBar;
    //Ŭ���� �ؽ�Ʈ
    public GameObject clearTxt;
    
    //���� â
    public Text powerText; //���ݷ�(��)
    public Text turretText; //�ͷ� ����
    
    //�� ü��
    public RectTransform towerHealthGroup;
    public RectTransform towerHealthBar;
    public Text towerHealthText;

    //���� ����
    public Transform[] enemyZones; //���Ͱ� ������ 3�� ��
    public GameObject[] enemies; //���� A,B
    //���� ����
    public Transform BluebossZones; //5������ ������ ��
    public Transform GreenbossZones; //10������ ������ ��
    public Transform PurplebossZones; //15������ ������ ��

    public GameObject BlueBossWall; //5������ �����ִ� ��
    public GameObject GreenBossWall; //10������ �����ִ� ��
    public GameObject PurpleBossWall; //15������ �����ִ� ��

    public GameObject blueBossHealth; //5 ���� ü�� ȭ�鿡 �����ִ� ����
    public GameObject greenBossHealth; //10 ���� ü�� ȭ�鿡 �����ִ� ����
    public GameObject purpleBossHealth; //15 ���� ü�� ȭ�鿡 �����ִ� ����

    public GameObject greenZonePortal;
    public GameObject blueZonePortal;
    public GameObject purpleZonePortal;


    public int money; //�÷��̾� ��
    public int maxMoney; //�÷��̾� �ִ� ��
    //�������� ����
    public int stage;
    public bool isBattle;
    public bool isBoss1;
    public bool isBoss2;
    public bool isBoss3;
    //�� �÷��� �ð�
    public float sumPlayTime;
    public Text sumPlayTimeTxt;
    //��Ƽ�� �ð�
    public float battlePlayTime;     
    public Text battlePlayTimeTxt;

    public Text stageTxt;
    public Text moneyTxt;
    public GameObject bossStageTxt;

    
    public GameObject startZone;
    public GameObject shopZone;
    public GameObject noneZone;
    //��ƼŬ
    public GameObject particle; //�÷��̾� �̵� �� ������ ��ƼŬ
    public GameObject particleMonster; // ���Ͱ� �����ɶ� ������ ��ƼŬ
    public GameObject bossMapParticle; //������ �̵� �� ������ ��ƼŬ
    public GameObject greenMapParticle;
    public GameObject purpleMapParticle; 
    
    //����޴�
    public GameObject menuPanel;
    //�����޴�
    public GameObject DiePanel;
    //���丮������
    public GameObject StoryPanel;
    public GameObject EndingPanel;
    //����
    public AudioSource clearSound; //Ŭ���� ����
    public AudioSource exitSound; //������ ����
    public AudioSource backGroundSound; //�Ϲݽ������� ����
    public AudioSource battleStageSound; //��Ʋ�������� ����
    public AudioSource bossStage5Sound; //������������5 ����
    public AudioSource bossStage15Sound; //������������15 ����
    public AudioSource bossStage10Sound; //������������10 ����
    //����̹���
    public GameObject lockImage2;
    public GameObject lockImage3;
    public GameObject lockImage4;
    
    void Awake()
    {
        arrow.damage = 10; //���� ����۵Ǹ� �⺻ ������ �ʱ�ȭ
        t_arrow.damage = 10; //���� ����۵Ǹ� �ͷ� ������ �ʱ�ȭ
        t_arrowMax.damage = 10;
        backGroundSound.Play();

        if (stage == 1) { 
            StoryPanel.SetActive(true); //���丮 ���� ����
            endingline.OpenEnding();
            StartCoroutine(Paneldestroy()); //�ð� ���� �����ش�.
        }
        //enemyList = new List<int>(); //����Ʈ�� �ʱ�ȭ �����ش�. -> ������������ ��Ÿ�� ���͵��� �����͸� �����ؾ��� InBattle ����
    }
    
    IEnumerator Paneldestroy()
    {
        yield return new WaitForSeconds(63f);
        StoryPanel.SetActive(false);
    }
    void Start()
    {
        battlePlayTimeTxt.text = battlePlayTime.ToString();
        

    }
    
    public void StageStart() //�������� ����
    {
        if (stage == 5) //5�������� ����
        {
            blueBossHealth.SetActive(true);
            bossStage5Sound.Play();
            bossMapParticle.GetComponent<ParticleSystem>().Play();
            player.transform.position = new Vector3(0, 0, 42);
            
        }else if (stage == 10) //10�������� ����
        {
            greenBossHealth.SetActive(true);
            bossStage10Sound.Play();
            greenMapParticle.GetComponent<ParticleSystem>().Play();
            player.transform.position = new Vector3(46, 0, 19);
        }else if (stage == 15) //15�������� ����
        {
            purpleBossHealth.SetActive(true);
            bossStage15Sound.Play();
            purpleMapParticle.GetComponent<ParticleSystem>().Play();
            player.transform.position = new Vector3(-50, 0, 17);
        }
        else
        {
            player.transform.position = new Vector3(0.56f,0,-14.7f); //���������� ���� �� �� ���� �ڸ����� ���۵ǰ�
            battleStageSound.Play();
        }

        startZone.SetActive(false); //��ŸƮ���� ��Ȱ��ȭ ��Ų��.
        shopZone.SetActive(false);
        noneZone.SetActive(false);

        foreach (Transform zone in enemyZones) //���������� ���۵Ǹ� enemyZone�� Ȱ��ȭ ��Ų��.
            zone.gameObject.SetActive(true);
        isBattle = true;
       
        StartCoroutine(InBattle());
        backGroundSound.Pause();
        
    }
    public void StageEnd() //�������� ����
    {
        
        
        stage++; //���������� ���� �Ǹ� stage�� 1 �÷��ش�.
        particle.GetComponent<ParticleSystem>().Play();
        if(stage == 6)
        {
            blueZonePortal.SetActive(true);
            blueBossHealth.SetActive(false);
            timeLineController.OpenSkill1();
            BlueBossWall.SetActive(false);
        }else if(stage == 11)
        {
            greenZonePortal.SetActive(true);
            greenBossHealth.SetActive(false);
            timeLineController.OpenSkill2();
            GreenBossWall.SetActive(false);
        }else if(stage == 16)
        {
            purpleZonePortal.SetActive(true);
            purpleBossHealth.SetActive(false);
            timeLineController.OpenSkill3();
            PurpleBossWall.SetActive(false);
        }
        else
        {
            player.transform.position = new Vector3(0.56f, 0.06f, -14.7f); //���������� ���� �� �� ���� �ڸ��� �ǵ��ƿ���
        }
       
            
       

        //���� �������� �˸�
        if(stage == 5 || stage == 10 || stage == 15)
        {
            bossStageTxt.SetActive(true);
        }
        else
        {
            bossStageTxt.SetActive(false);
        }
        
        
        
        //stage 20 �� ������ ��Ʋ�� �̻���
        if (stage <= 20)
        {
            startZone.SetActive(true);
        }
        if (stage == 21)
        {
            EndingPanel.SetActive(true); //���丮 �ǳ�(����)
            endingline.OpenEnding();
        }
        shopZone.SetActive(true);
        noneZone.SetActive(true);

        foreach (Transform zone in enemyZones) //���������� ������ enemyZone�� ��Ȱ��ȭ ��Ų��.
            zone.gameObject.SetActive(false);
       

        StartCoroutine(Clear());
        player.curHealth = player.maxHealth; //���������� ���� �Ǹ� ü���� �ִ�ü������ �ٽ� ������ش�.
        battleStageSound.Stop();
        bossStage5Sound.Pause();
        bossStage10Sound.Pause();
        bossStage15Sound.Pause();
        backGroundSound.Play();

        money += 500 * stage;
        //�������� ����� ����̹��� Ǯ����
       
        if(stage >= 6)
        {
            lockImage2.SetActive(false);
        }
        if (stage >= 11)
        {
            lockImage3.SetActive(false);
        }
        if (stage >= 16)
        {
            lockImage4.SetActive(false);
        }
      
    }
    IEnumerator Clear() //������������ �Ѿ�� �Ǹ� clear ���� ������ �Ҹ� ����
    {
        clearTxt.SetActive(true);
        clearSound.Play();
        
        yield return new WaitForSeconds(3);
        clearTxt.SetActive(false);
    }
    IEnumerator InBattle() //(��������) �������� ���� 
    {
        yield return new WaitForSeconds(1f); //���������� ���۵ǰ� ���Ͱ� ��������� �ð�
                                             
       
        while (isBattle) // ���� ����
        {

            if (stage <= 4)
            {
                int ranZone = Random.Range(0, 3); //���� ���� �� 1,2,3�� ����
                int ran = Mathf.FloorToInt(Random.Range(0f, 1f)); // ���� 1��, 2�� ����

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //���� ���� ������ �ٸ��� ������ �Ϸ��� ��ġ ����

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //��Ÿ�� ����      ��Ÿ�������� ��ġ
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---Ÿ������---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //���͸� ������ȭ�� ���ױ� ������ target�� scene�� �ö�� ������Ʈ�� ������ ���ϴϱ� ��ũ��Ʈ�� ���ش�.
                enemy.target = tower.transform;
                enemy.manager = this;

                //enemyList.RemoveAt(0); //���� �Ŀ��� ���� �����ʹ� ���� //������ �����ָ� ���� ��ӳ����� �������� �ȳ���
                yield return new WaitForSeconds(4f - 0.2f * stage); //���Ͱ� �����Ǵ� ������ ���ݽð�

            }
            else if(stage == 5)
            {
                if (blueBoss.curHealth <= 0)
                {
                    yield return new WaitForSeconds(5f);

                    isBattle = false;

                }
                else if (blueBoss.curHealth > 0 && battlePlayTime <= 0)
                {
                    player.curHealth = 0;
                }

                yield return null;
            }
            else if (5 < stage && stage <= 9)
            {
                int ranZone = Random.Range(0, 3); //���� ���� �� 1,2,3�� ����
                int ran = Mathf.FloorToInt(Random.Range(0.5f, 1.5f)); // ���� 1��, 2�� ����

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //���� ���� ������ �ٸ��� ������ �Ϸ��� ��ġ ����

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //��Ÿ�� ����      ��Ÿ�������� ��ġ
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---Ÿ������---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //���͸� ������ȭ�� ���ױ� ������ target�� scene�� �ö�� ������Ʈ�� ������ ���ϴϱ� ��ũ��Ʈ�� ���ش�.
                enemy.target = tower.transform;
                enemy.damage += 5;
                enemy.maxHealth += 50;
                enemy.curHealth += 50;

                enemy.manager = this;

                //enemyList.RemoveAt(0); //���� �Ŀ��� ���� �����ʹ� ���� //������ �����ָ� ���� ��ӳ����� �������� �ȳ���
                yield return new WaitForSeconds(2.5f - 0.2f * (stage - 5)); //���Ͱ� �����Ǵ� ������ ���ݽð�

            }
            //���� �� : stage 10
            else if (stage == 10)
            {
                if (greenBoss.curHealth <= 0)
                {
                    yield return new WaitForSeconds(5f);
                    
                    isBattle = false;

                }
                else if (greenBoss.curHealth > 0 && battlePlayTime <= 0)
                {
                    player.curHealth = 0;
                }
                yield return null;
            }
            else if (10 < stage && stage <= 14)
            {
                int ranZone = Random.Range(0, 3); //���� ���� �� 1,2,3�� ����
                int ran = Mathf.FloorToInt(Random.Range(0.7f, 1.7f)); // ���� 1��, 2�� ����

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //���� ���� ������ �ٸ��� ������ �Ϸ��� ��ġ ����

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //��Ÿ�� ����      ��Ÿ�������� ��ġ
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---Ÿ������---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //���͸� ������ȭ�� ���ױ� ������ target�� scene�� �ö�� ������Ʈ�� ������ ���ϴϱ� ��ũ��Ʈ�� ���ش�.
                enemy.target = tower.transform;
                enemy.damage += 10;
                enemy.maxHealth += 150;
                enemy.curHealth += 150;

                enemy.manager = this;

                //enemyList.RemoveAt(0); //���� �Ŀ��� ���� �����ʹ� ���� //������ �����ָ� ���� ��ӳ����� �������� �ȳ���
                yield return new WaitForSeconds(1.8f - 0.2f * (stage - 10)); //���Ͱ� �����Ǵ� ������ ���ݽð�

            }
            else if (stage == 15)
            {

                if (purpleBoss.curHealth <= 0)
                {
                    yield return new WaitForSeconds(5f);
                    
                    isBattle = false;

                }
                else if (purpleBoss.curHealth > 0 && battlePlayTime <= 0)
                {
                    player.curHealth = 0;
                }

                yield return null;

            }
            else if (15 < stage && stage <= 20)
            {
                int ranZone = Random.Range(0, 3); //���� ���� �� 1,2,3�� ����
                int ran = Mathf.FloorToInt(Random.Range(0.9f, 1.9f)); // ���� 1��, 2�� ����

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //���� ���� ������ �ٸ��� ������ �Ϸ��� ��ġ ����

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //��Ÿ�� ����      ��Ÿ�������� ��ġ
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---Ÿ������---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //���͸� ������ȭ�� ���ױ� ������ target�� scene�� �ö�� ������Ʈ�� ������ ���ϴϱ� ��ũ��Ʈ�� ���ش�.
                enemy.target = tower.transform;
                enemy.damage += 15;
                enemy.maxHealth += 200;
                enemy.curHealth += 200;
                instantEnemy.GetComponent<NavMeshAgent>().speed *= 1.4f;

                enemy.manager = this;
                //enemyList.RemoveAt(0); //���� �Ŀ��� ���� �����ʹ� ���� //������ �����ָ� ���� ��ӳ����� �������� �ȳ���
                yield return new WaitForSeconds(1.3f - 0.2f * (stage - 15)); //���Ͱ� �����Ǵ� ������ ���ݽð�

            }
           
            }
        StageEnd();


    }
    void Update()
    {
       
       sumPlayTime += Time.deltaTime;

        if (isBattle) //��Ƽ�½ð�
        {
            if (battlePlayTime > 0)
            {  //��Ƽ�½ð��� 0���� ũ�� ��� �پ���
                battlePlayTime -= Time.deltaTime;
            }
            else if (battlePlayTime <= 0) //��Ƽ�½ð��� 0�� ���ų� ������ �ð��� �ٽ� 60�ʷ� ���µ�
            {
                battlePlayTime = 60f;
                isBattle = false;
            }
            battlePlayTimeTxt.text = Mathf.Round(battlePlayTime).ToString(); //�Ѱ��ӽð�����̶� �ٸ���� Round : �ε��Ҽ��� ���� �ݿø� ��Ų��.

        }
        else if(!isBattle)
        {
            if (stage == 5 || stage == 10 || stage == 15)
            {
                battlePlayTime = 100f;
                battlePlayTimeTxt.text = Mathf.Round(battlePlayTime).ToString();
            }
            else if (stage == 6 || stage == 11 || stage == 16)
            {
                battlePlayTime = 60f;
                battlePlayTimeTxt.text = Mathf.Round(battlePlayTime).ToString();
            }
            //������ �������� �÷��̾� health�� 0���� ����� ������ �����Ų��.
        }
        

        //0509 ����޴� �߰�
        if (Input.GetButtonDown("Cancel")) //esc�� �������� 
        {
            if (menuPanel.activeSelf) //�޴��ǳ��� ����������
            {
                menuPanel.SetActive(false); //����
            }
            else
                menuPanel.SetActive(true); //�ƴϸ� �Ѷ�
                
        }
    }
    
    void LateUpdate()
    {
        // -------------------------------------------UI--------------------------------------------
        if(stage == 20)
        {
            stageTxt.text = "LAST STAGE";
        }
        else
        {
            stageTxt.text = "STAGE " + stage;
        }
        

        //�� ���� �ð� ���
        int hour = (int)(sumPlayTime / 3600);
        int min = (int)((sumPlayTime - hour * 3600) / 60);
        int second = (int)(sumPlayTime % 60);

        sumPlayTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second); //�� ���ӽð�
        //�÷��̾� �� ui
        moneyTxt.text = string.Format("{0:0}", this.money);
        //����ü�� ui
        if (blueBoss.curHealth <= 0)
        {
            blueBoss.curHealth = 0;
        }
        
         blueBossHealthBar.localScale = new Vector3((float)blueBoss.curHealth / blueBoss.maxHealth, 1, 1);
         blueBossHealthText.text = blueBoss.curHealth + " / " + blueBoss.maxHealth;
        
        if (greenBoss.curHealth <= 0)
        {
            greenBoss.curHealth = 0;

        }
        greenBossHealthBar.localScale = new Vector3((float)greenBoss.curHealth / greenBoss.maxHealth, 1, 1);
        greenBossHealthText.text = greenBoss.curHealth + " / " + greenBoss.maxHealth;
        if (purpleBoss.curHealth <= 0)
        {
            purpleBoss.curHealth = 0;
        }
        purpleBossHealthBar.localScale = new Vector3((float)purpleBoss.curHealth / purpleBoss.maxHealth, 1, 1);
        purpleBossHealthText.text = purpleBoss.curHealth + " / " + purpleBoss.maxHealth;



        //ü��
        if (player.curHealth >= 0)
        {
            PlayerHealthBar.localScale = new Vector3((float)player.curHealth / player.maxHealth, 1, 1);
            PlayerHealthText.text = player.curHealth + " / " + player.maxHealth;
            
        }
        //����â
        powerText.text = arrow.damage + " / " + arrow.maxDamage;
        turretText.text = shop.turretCount + " / " + shop.maxTurretCount;
        //��ü��
        if(tower.towerCurHealth >= 0) { 
        towerHealthBar.localScale = new Vector3(tower.towerCurHealth / tower.towerMaxHealth,1,1);
        towerHealthText.text = tower.towerCurHealth + " / " + tower.towerMaxHealth;
        }

        if (stage == 5 || stage == 10 || stage == 15)
        {
            BattleTimeBar.localScale = new Vector3((float)battlePlayTime / 100, 1, 1); //��ƲŸ��
        }
        else
        {
            BattleTimeBar.localScale = new Vector3((float)battlePlayTime / 60, 1, 1); //��ƲŸ��
        }
      
       
    }
    
    //����޴� 
    //�Լ��ϱ�� �����ִ� �Լ��� ����
    //�����ϱ�
    public void GameExit()
    {
        exitSound.Play();
        Application.Quit(); //�����Ϳ����� ���� �ȵǰ� �����ؼ� ������ ���Ӹ���� �׶��� ������.
    }
}
