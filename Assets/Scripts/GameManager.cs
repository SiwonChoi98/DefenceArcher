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
    //보스들 변수가져와야 함
    public Boss blueBoss; 
    public BossSecond greenBoss;
    public BossThird purpleBoss;
    //5보스 체력 바
    public RectTransform blueBossHealthGroup;
    public RectTransform blueBossHealthBar;
    public Text blueBossHealthText;
    //10보스 체력 바
    public RectTransform greenBossHealthGroup;
    public RectTransform greenBossHealthBar;
    public Text greenBossHealthText;
    //15보스 체력 바
    public RectTransform purpleBossHealthGroup;
    public RectTransform purpleBossHealthBar;
    public Text purpleBossHealthText;

    //플레이어 체력 바
    public RectTransform PlayerHealthGroup;
    public RectTransform PlayerHealthBar;
    public Text PlayerHealthText;
    //버티는 시간 바
    public RectTransform BattleTimeGroup;
    public RectTransform BattleTimeBar;
    //클리어 텍스트
    public GameObject clearTxt;
    
    //스텟 창
    public Text powerText; //공격력(힘)
    public Text turretText; //터렛 숫자
    
    //성 체력
    public RectTransform towerHealthGroup;
    public RectTransform towerHealthBar;
    public Text towerHealthText;

    //몬스터 관련
    public Transform[] enemyZones; //몬스터가 나오는 3개 존
    public GameObject[] enemies; //몬스터 A,B
    //보스 관련
    public Transform BluebossZones; //5보스가 나오는 존
    public Transform GreenbossZones; //10보스가 나오는 존
    public Transform PurplebossZones; //15보스가 나오는 존

    public GameObject BlueBossWall; //5보스존 막혀있는 문
    public GameObject GreenBossWall; //10보스존 막혀있는 문
    public GameObject PurpleBossWall; //15보스존 막혀있는 문

    public GameObject blueBossHealth; //5 보스 체력 화면에 보여주는 변수
    public GameObject greenBossHealth; //10 보스 체력 화면에 보여주는 변수
    public GameObject purpleBossHealth; //15 보스 체력 화면에 보여주는 변수

    public GameObject greenZonePortal;
    public GameObject blueZonePortal;
    public GameObject purpleZonePortal;


    public int money; //플레이어 돈
    public int maxMoney; //플레이어 최대 돈
    //스테이지 관리
    public int stage;
    public bool isBattle;
    public bool isBoss1;
    public bool isBoss2;
    public bool isBoss3;
    //총 플레이 시간
    public float sumPlayTime;
    public Text sumPlayTimeTxt;
    //버티는 시간
    public float battlePlayTime;     
    public Text battlePlayTimeTxt;

    public Text stageTxt;
    public Text moneyTxt;
    public GameObject bossStageTxt;

    
    public GameObject startZone;
    public GameObject shopZone;
    public GameObject noneZone;
    //파티클
    public GameObject particle; //플레이어 이동 시 나오는 파티클
    public GameObject particleMonster; // 몬스터가 생성될때 나오는 파티클
    public GameObject bossMapParticle; //보스맵 이동 시 나오는 파티클
    public GameObject greenMapParticle;
    public GameObject purpleMapParticle; 
    
    //서브메뉴
    public GameObject menuPanel;
    //죽음메뉴
    public GameObject DiePanel;
    //스토리나오게
    public GameObject StoryPanel;
    public GameObject EndingPanel;
    //사운드
    public AudioSource clearSound; //클리어 사운드
    public AudioSource exitSound; //나가는 사운드
    public AudioSource backGroundSound; //일반스테이지 사운드
    public AudioSource battleStageSound; //배틀스테이지 사운드
    public AudioSource bossStage5Sound; //보스스테이지5 사운드
    public AudioSource bossStage15Sound; //보스스테이지15 사운드
    public AudioSource bossStage10Sound; //보스스테이지10 사운드
    //잠금이미지
    public GameObject lockImage2;
    public GameObject lockImage3;
    public GameObject lockImage4;
    
    void Awake()
    {
        arrow.damage = 10; //게임 재시작되면 기본 데미지 초기화
        t_arrow.damage = 10; //게임 재시작되면 터렛 데미지 초기화
        t_arrowMax.damage = 10;
        backGroundSound.Play();

        if (stage == 1) { 
            StoryPanel.SetActive(true); //스토리 글자 시작
            endingline.OpenEnding();
            StartCoroutine(Paneldestroy()); //시간 이후 없애준다.
        }
        //enemyList = new List<int>(); //리스트를 초기화 시켜준다. -> 각스테이지의 나타날 몬스터들의 데이터를 저장해야함 InBattle 에서
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
    
    public void StageStart() //스테이지 관리
    {
        if (stage == 5) //5스테이지 보스
        {
            blueBossHealth.SetActive(true);
            bossStage5Sound.Play();
            bossMapParticle.GetComponent<ParticleSystem>().Play();
            player.transform.position = new Vector3(0, 0, 42);
            
        }else if (stage == 10) //10스테이지 보스
        {
            greenBossHealth.SetActive(true);
            bossStage10Sound.Play();
            greenMapParticle.GetComponent<ParticleSystem>().Play();
            player.transform.position = new Vector3(46, 0, 19);
        }else if (stage == 15) //15스테이지 보스
        {
            purpleBossHealth.SetActive(true);
            bossStage15Sound.Play();
            purpleMapParticle.GetComponent<ParticleSystem>().Play();
            player.transform.position = new Vector3(-50, 0, 17);
        }
        else
        {
            player.transform.position = new Vector3(0.56f,0,-14.7f); //스테이지가 시작 된 후 원래 자리부터 시작되게
            battleStageSound.Play();
        }

        startZone.SetActive(false); //스타트존을 비활성화 시킨다.
        shopZone.SetActive(false);
        noneZone.SetActive(false);

        foreach (Transform zone in enemyZones) //스테이지가 시작되면 enemyZone을 활성화 시킨다.
            zone.gameObject.SetActive(true);
        isBattle = true;
       
        StartCoroutine(InBattle());
        backGroundSound.Pause();
        
    }
    public void StageEnd() //스테이지 관리
    {
        
        
        stage++; //스테이지가 종료 되면 stage를 1 올려준다.
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
            player.transform.position = new Vector3(0.56f, 0.06f, -14.7f); //스테이지가 종료 된 후 원래 자리로 되돌아오게
        }
       
            
       

        //보스 스테이지 알림
        if(stage == 5 || stage == 10 || stage == 15)
        {
            bossStageTxt.SetActive(true);
        }
        else
        {
            bossStageTxt.SetActive(false);
        }
        
        
        
        //stage 20 이 끝나면 배틀존 미생성
        if (stage <= 20)
        {
            startZone.SetActive(true);
        }
        if (stage == 21)
        {
            EndingPanel.SetActive(true); //스토리 판넬(엔딩)
            endingline.OpenEnding();
        }
        shopZone.SetActive(true);
        noneZone.SetActive(true);

        foreach (Transform zone in enemyZones) //스테이지가 끝나면 enemyZone을 비활성화 시킨다.
            zone.gameObject.SetActive(false);
       

        StartCoroutine(Clear());
        player.curHealth = player.maxHealth; //스테이지가 종료 되면 체력을 최대체력으로 다시 만들어준다.
        battleStageSound.Stop();
        bossStage5Sound.Pause();
        bossStage10Sound.Pause();
        bossStage15Sound.Pause();
        backGroundSound.Play();

        money += 500 * stage;
        //스테이지 종료시 잠금이미지 풀게함
       
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
    IEnumerator Clear() //다음스테이지 넘어가게 되면 clear 글자 나오고 소리 나게
    {
        clearTxt.SetActive(true);
        clearSound.Play();
        
        yield return new WaitForSeconds(3);
        clearTxt.SetActive(false);
    }
    IEnumerator InBattle() //(스테이지) 전투상태 구현 
    {
        yield return new WaitForSeconds(1f); //스테이지가 시작되고 몬스터가 나오기까지 시간
                                             
       
        while (isBattle) // 몬스터 생성
        {

            if (stage <= 4)
            {
                int ranZone = Random.Range(0, 3); //몬스터 생성 존 1,2,3번 랜덤
                int ran = Mathf.FloorToInt(Random.Range(0f, 1f)); // 몬스터 1번, 2번 랜덤

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //몬스터 생성 존에서 다르게 나오게 하려고 위치 지정

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //나타날 몬스터      나타날몬스터의 위치
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---타겟지정---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //몬스터를 프리펩화를 시켰기 때문에 target을 scene에 올라온 오브젝트에 접근을 못하니까 스크립트로 해준다.
                enemy.target = tower.transform;
                enemy.manager = this;

                //enemyList.RemoveAt(0); //생성 후에는 사용된 데이터는 삭제 //삭제를 안해주면 몬스터 계속나오고 스테이지 안끝남
                yield return new WaitForSeconds(4f - 0.2f * stage); //몬스터가 생성되는 리스폰 간격시간

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
                int ranZone = Random.Range(0, 3); //몬스터 생성 존 1,2,3번 랜덤
                int ran = Mathf.FloorToInt(Random.Range(0.5f, 1.5f)); // 몬스터 1번, 2번 랜덤

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //몬스터 생성 존에서 다르게 나오게 하려고 위치 지정

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //나타날 몬스터      나타날몬스터의 위치
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---타겟지정---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //몬스터를 프리펩화를 시켰기 때문에 target을 scene에 올라온 오브젝트에 접근을 못하니까 스크립트로 해준다.
                enemy.target = tower.transform;
                enemy.damage += 5;
                enemy.maxHealth += 50;
                enemy.curHealth += 50;

                enemy.manager = this;

                //enemyList.RemoveAt(0); //생성 후에는 사용된 데이터는 삭제 //삭제를 안해주면 몬스터 계속나오고 스테이지 안끝남
                yield return new WaitForSeconds(2.5f - 0.2f * (stage - 5)); //몬스터가 생성되는 리스폰 간격시간

            }
            //보스 맵 : stage 10
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
                int ranZone = Random.Range(0, 3); //몬스터 생성 존 1,2,3번 랜덤
                int ran = Mathf.FloorToInt(Random.Range(0.7f, 1.7f)); // 몬스터 1번, 2번 랜덤

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //몬스터 생성 존에서 다르게 나오게 하려고 위치 지정

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //나타날 몬스터      나타날몬스터의 위치
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---타겟지정---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //몬스터를 프리펩화를 시켰기 때문에 target을 scene에 올라온 오브젝트에 접근을 못하니까 스크립트로 해준다.
                enemy.target = tower.transform;
                enemy.damage += 10;
                enemy.maxHealth += 150;
                enemy.curHealth += 150;

                enemy.manager = this;

                //enemyList.RemoveAt(0); //생성 후에는 사용된 데이터는 삭제 //삭제를 안해주면 몬스터 계속나오고 스테이지 안끝남
                yield return new WaitForSeconds(1.8f - 0.2f * (stage - 10)); //몬스터가 생성되는 리스폰 간격시간

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
                int ranZone = Random.Range(0, 3); //몬스터 생성 존 1,2,3번 랜덤
                int ran = Mathf.FloorToInt(Random.Range(0.9f, 1.9f)); // 몬스터 1번, 2번 랜덤

                int x = Random.Range(-1, 2);
                int z = Random.Range(-1, 2);
                Vector3 ranVec = new Vector3(x, 0, z); //몬스터 생성 존에서 다르게 나오게 하려고 위치 지정

                GameObject instantEnemy = Instantiate(enemies[ran], enemyZones[ranZone].position + ranVec, enemyZones[ranZone].rotation);
                //나타날 몬스터      나타날몬스터의 위치
                particleMonster.GetComponent<ParticleSystem>().Play();

                //---타겟지정---                                     
                Enemy enemy = instantEnemy.GetComponent<Enemy>(); //몬스터를 프리펩화를 시켰기 때문에 target을 scene에 올라온 오브젝트에 접근을 못하니까 스크립트로 해준다.
                enemy.target = tower.transform;
                enemy.damage += 15;
                enemy.maxHealth += 200;
                enemy.curHealth += 200;
                instantEnemy.GetComponent<NavMeshAgent>().speed *= 1.4f;

                enemy.manager = this;
                //enemyList.RemoveAt(0); //생성 후에는 사용된 데이터는 삭제 //삭제를 안해주면 몬스터 계속나오고 스테이지 안끝남
                yield return new WaitForSeconds(1.3f - 0.2f * (stage - 15)); //몬스터가 생성되는 리스폰 간격시간

            }
           
            }
        StageEnd();


    }
    void Update()
    {
       
       sumPlayTime += Time.deltaTime;

        if (isBattle) //버티는시간
        {
            if (battlePlayTime > 0)
            {  //버티는시간이 0보다 크면 계속 줄어들게
                battlePlayTime -= Time.deltaTime;
            }
            else if (battlePlayTime <= 0) //버티는시간이 0과 같거나 작으면 시간이 다시 60초로 리셋됨
            {
                battlePlayTime = 60f;
                isBattle = false;
            }
            battlePlayTimeTxt.text = Mathf.Round(battlePlayTime).ToString(); //총게임시간계산이랑 다른방식 Round : 부동소수점 값을 반올림 시킨다.

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
            //보스를 못잡으면 플레이어 health를 0으로 만들어 게임을 종료시킨다.
        }
        

        //0509 서브메뉴 추가
        if (Input.GetButtonDown("Cancel")) //esc를 눌렀을때 
        {
            if (menuPanel.activeSelf) //메뉴판낼이 켜져있으면
            {
                menuPanel.SetActive(false); //끄고
            }
            else
                menuPanel.SetActive(true); //아니면 켜라
                
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
        

        //총 게임 시간 계산
        int hour = (int)(sumPlayTime / 3600);
        int min = (int)((sumPlayTime - hour * 3600) / 60);
        int second = (int)(sumPlayTime % 60);

        sumPlayTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second); //총 게임시간
        //플레이어 돈 ui
        moneyTxt.text = string.Format("{0:0}", this.money);
        //보스체력 ui
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



        //체력
        if (player.curHealth >= 0)
        {
            PlayerHealthBar.localScale = new Vector3((float)player.curHealth / player.maxHealth, 1, 1);
            PlayerHealthText.text = player.curHealth + " / " + player.maxHealth;
            
        }
        //스탯창
        powerText.text = arrow.damage + " / " + arrow.maxDamage;
        turretText.text = shop.turretCount + " / " + shop.maxTurretCount;
        //성체력
        if(tower.towerCurHealth >= 0) { 
        towerHealthBar.localScale = new Vector3(tower.towerCurHealth / tower.towerMaxHealth,1,1);
        towerHealthText.text = tower.towerCurHealth + " / " + tower.towerMaxHealth;
        }

        if (stage == 5 || stage == 10 || stage == 15)
        {
            BattleTimeBar.localScale = new Vector3((float)battlePlayTime / 100, 1, 1); //배틀타임
        }
        else
        {
            BattleTimeBar.localScale = new Vector3((float)battlePlayTime / 60, 1, 1); //배틀타임
        }
      
       
    }
    
    //서브메뉴 
    //게속하기는 원래있는 함수로 실행
    //종료하기
    public void GameExit()
    {
        exitSound.Play();
        Application.Quit(); //에디터에서는 실행 안되고 빌드해서 꺼내서 게임만들면 그때는 꺼진다.
    }
}
