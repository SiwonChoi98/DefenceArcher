using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSecond : MonoBehaviour
{

    //#region Variable
    [Header("dsadsa")]
    public PlayerController player; //플레이어를 담을 변수
    public int damage1 = 10;
    public int damage2 = 15;
    public int damage3 = 50;

    float targetRadius = 4.5f; //공격 지름 ??
    float targetRange = 1f;  //공격 범위 ??
    public int maxHealth; // 보스의 최대체력
    public int curHealth; // 보스의 현재체력

    //#endregion
    public Transform target; // 보스가 지정한 타겟
    public BoxCollider damageArea; //보스의 피격 범위
    public BoxCollider biteArea; // 보스의 물기 공격 범위
    public BoxCollider horeArea; // 보스의 발톱 공격 범위
    public BoxCollider jumpAttackArea; //보스의 충격파 범위
    public bool isChase; //타겟으로 이동하는 bool값
    public bool isAttack; //공격처리하는 bool값
    public bool isJumpAttack = false; // 점프 공격을 처리하는 bool값
                          //public bool isBattle; // 배틀 시작을 알려주는 bool값

    Rigidbody rigid;
    NavMeshAgent nav; //NavMeshAgent 쓰기 위해서는 using UnityEngine.AI 패키지 필요*
    Animator anim; //애니메이션


    public AudioSource deathSound;
    public AudioSource attackSound;
    public AudioSource skillhitSound;
    public AudioSource hitSound;
    public AudioSource screamSound;
    public GameObject damageText; //데미지 텍스트
    public Transform damageTextPos; //데미지 텍스트 포지션

    //public GameObject iceParticle;
   

    bool isHalf = false; // 체력이 반 남았다는 걸 알려주는 bool값
    bool isScream = false; // 폭주 상태로 넘어가기전 scream하고 넘어가기 위한 bool값
    bool isOpening = false; //처음 입장했을 때 bool값
    bool isDamage = false;


    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isChase = true;
        nav.enabled = false;
    }


    void Update()
    {
        if (player.GreenisbossDoor)
        {
            if (!isOpening && player.bossFrontCamera.enabled == true)
            {
                anim.SetTrigger("doScream");
                screamSound.Play();
                isOpening = true;
            }
            else if (isOpening && player.bossFrontCamera.enabled == false)
            {
                nav.enabled = true;
                target = player.transform;
            }

            if (nav.enabled && !isAttack && !isHalf) //일반 상태
            {
                anim.SetBool("IsWalk", true);
                nav.SetDestination(target.position); //타겟 포지션으로 이동해라
            }
            else if (nav.enabled && !isAttack && isHalf && isScream) //폭주 상태
            {
                anim.SetBool("IsRun", true);
                nav.speed = 15f;
                nav.angularSpeed = 340f;
                nav.SetDestination(target.position); //타겟 포지션으로 이동해라
            }
            if (nav.enabled && isJumpAttack && isHalf)
            {

                nav.SetDestination(target.position);

            }
            if (nav.enabled && horeArea.enabled && !isDamage)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward * 3, 0.01f);
            }
            if (curHealth <= (maxHealth / 2) && !isScream && !isHalf) // 절반보다 피가 줄었을 경우
            {
                isHalf = true;
                anim.SetBool("IsWalk", false);
                StartCoroutine(DragonScream());

            }

            TargetingPlayer();
        }
        
    }

    IEnumerator DragonScream() //울부짖으면서 폭주 상태로 돌입한다.
    {

        nav.enabled = false;
        anim.SetTrigger("doScream");
        screamSound.Play();
        yield return new WaitForSeconds(3.4f);

        nav.enabled = true;
        isScream = true;
    }
    void FixedUpdate()
    {

        FreezeRotation();
    }

    void FreezeRotation()
    {
        if (isChase || isAttack) //쫓거나 공격시 물리 판정 받지않음
        {
            rigid.velocity = Vector3.zero; //이동멈춤
            rigid.angularVelocity = Vector3.zero; //회전하는 이동멈춤
        }

    }
    void TargetingPlayer()
    {

        RaycastHit[] rayHits = //레이케스트 = 레이저를 쏴서 충돌감지 등 활용
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        //레이어마스크는 콜라이더에만 적용가능하다. 그래서 콜라이더에 layer가 있어야 rayHits가 닿는다.
        if (rayHits.Length > 0 && !isAttack && !isHalf && !isScream &&nav.enabled) //레이저가 쏘는 거리가 0보다 크면 실행해줘라 && attack중이 아닐 때
        {
            StartCoroutine(Attack());
        }
        else if (rayHits.Length > 0 && !isAttack && isHalf && isScream && nav.enabled && curHealth > 0)
        {
            StartCoroutine(RageAttack());
        }


    }

    IEnumerator Attack() // 공격 시 
    {
        nav.enabled = false;
        isChase = false; //이동시간 끝
        isAttack = true; //공격시간 시작

        yield return new WaitForSeconds(0.1f);



        int ranAction = Random.Range(0, 4);

        switch (ranAction) // 확률적으로 다른 패턴을 사용한다.
        {
            case 0:
            case 1:
                //물기 패턴
                StartCoroutine(biteAttack());
                break;

            case 2:
            case 3:
                //박치기 공격 패턴
                StartCoroutine(hornAttack());
                break;


        }

    }

    IEnumerator RageAttack() // 공격 시 
    {
        nav.enabled = false;
        isChase = false; //이동시간 끝
        isAttack = true; //공격시간 시작

        yield return new WaitForSeconds(0.1f);


        int ranAction = Random.Range(0, 7);

        switch (ranAction) // 확률적으로 다른 패턴을 사용한다.
        {
            case 0:
            case 1:
                //물기 패턴
                StartCoroutine(biteAttack());
                break;
            case 2:
            case 3:
                //박치기 공격 패턴
                StartCoroutine(hornAttack());
                break;
            case 4:
            case 5:
            case 6:
                //점프 어택 패턴
                StartCoroutine(jumpAttack());
                break;

        }
    }

    IEnumerator biteAttack()
    {

        transform.LookAt(target.position);
        
        anim.SetTrigger("doBite");
        attackSound.Play();
        yield return new WaitForSeconds(0.1f);
        biteArea.enabled = true;

        yield return new WaitForSeconds(1.1f);
        biteArea.enabled = false;

        yield return new WaitForSeconds(0.5f);

        isChase = true;
        isAttack = false;
        nav.enabled = true;
    }




    IEnumerator hornAttack()
    {


        transform.LookAt(target.position);
        anim.SetTrigger("doHorn");

        yield return new WaitForSeconds(0.3f);
        damageArea.enabled = false;
        horeArea.enabled = true;
        attackSound.Play();

        yield return new WaitForSeconds(1.2f); 

        horeArea.enabled = false;
        damageArea.enabled = true;

        yield return new WaitForSeconds(0.5f);

        isChase = true;
        isAttack = false;
        nav.enabled = true;
    }

    IEnumerator jumpAttack()
    {


        anim.SetTrigger("doJumpAttack");
        screamSound.Play();

        damageArea.enabled = false;

        yield return new WaitForSeconds(1.2f);

        nav.enabled = true; //
        isJumpAttack = true; // 점프 어택 실행

        yield return new WaitForSeconds(0.9f);

        jumpAttackArea.enabled = true;
        nav.enabled = false; //

        yield return new WaitForSeconds(0.7f);


        jumpAttackArea.enabled = false;
        damageArea.enabled = true;
        isJumpAttack = false;

        yield return new WaitForSeconds(1.2f);
        isChase = true;
        isAttack = false;
        nav.enabled = true;



    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Arrow")
        {

            Arrow arrow = other.GetComponent<Arrow>();
            curHealth -= arrow.damage;
            hitSound.Play();
            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = arrow.damage; //데미지텍스트를 생성한 데미지는 arrow.damage이다.
            if (curHealth <= 0) //몬스터의 체력이 0보다 작을 경우
            {

                isChase = false; //이동 멈춤
                nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
                deathSound.Play();
                anim.SetTrigger("doDeath"); //죽는 모션을 보여준다.
                gameObject.layer = 11;
                Destroy(gameObject, 2); //게임 오브젝트를 2초뒤 없애준다.
            }

        }

        else if (other.tag == "Skill1")
        {
            Skill1 skill1 = other.GetComponent<Skill1>();
            curHealth -= skill1.damage;
            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = skill1.damage; //데미지텍스트를 생성한 데미지는 skill1.damage이다.

            StartCoroutine(Skill1Damage()); //일반공격과 똑같은 피격판정이므로, OnDamage로 보낸다.

        }

        else if (other.tag == "Skill2")
        {
            Skill2 skill2 = other.GetComponent<Skill2>();
            curHealth -= skill2.damage;

            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = skill2.damage; //데미지텍스트를 생성한 데미지는 skill2.damage이다.

            StartCoroutine(Skill1Damage());
        }


    }

    IEnumerator Skill1Damage()
    {
        if (curHealth > 0 && !isDamage) //몬스터의 체력이 0보다 클 경우
        {
            isDamage = true;
            isChase = false; //이동시간 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            anim.SetTrigger("doDamage"); //맞는 모션을 보여준다.
            skillhitSound.Play();
            yield return new WaitForSeconds(2f);


            isChase = true; //이동시간 시작
            nav.enabled = true; //다시 타겟으로 이동하는 범위 시작
            isDamage = false;
        }
        else if (curHealth <= 0) //몬스터의 체력이 0보다 작을 경우
        {

            isChase = false; //이동 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            deathSound.Play();
            anim.SetTrigger("doDeath"); //죽는 모션을 보여준다.
            gameObject.layer = 11;
            Destroy(gameObject, 2); //게임 오브젝트를 2초뒤 없애준다.
        }

    }


    


    
}
