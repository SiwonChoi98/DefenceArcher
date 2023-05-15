using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C}; //몬스터의 타입
    public Type enemyType;
    public GameManager manager; //게임매니저를 담을 변수
    

    public int damage;
    public float targetRadius = 1f; //공격 지름
    public float targetRange = 1f;  //공격 범위
    public int maxHealth; // 몬스터의 최대체력
    public int curHealth; // 몬스터의 현재체력
    public Transform target; // 몬스터가 지정한 타겟
    public BoxCollider meleeArea; // 몬스터 무기 범위
    public bool isChase; //타겟으로 이동하는 bool값
    public bool isAttack; //공격처리하는 bool값
    public bool isTower = false; // 타워에 데미지 한번만 입히기 위한 bool값
    bool isDamaged = false; //몬스터 공격받는 유무
 
    Rigidbody rigid;
    NavMeshAgent nav; //NavMeshAgent 쓰기 위해서는 using UnityEngine.AI 패키지 필요*
    Animator anim; //애니메이션
    
    public AudioSource deathSound;
    public AudioSource hitSound;
    public GameObject damageText; //데미지 텍스트
    public Transform damageTextPos; //데미지 텍스트 포지션

    public GameObject iceParticle;

    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        Invoke("ChaseStart", 0.1f); //0.1초 대기 했다가 ChaseStart 함수를 실행
    }

    void ChaseStart()
    {
        isChase = true;
           
    }

    void FreezeRotation()
    {
        if (isChase || isAttack) //쫓거나 공격시 물리 판정 받지않음
        {
            rigid.velocity = Vector3.zero; //이동멈춤
            rigid.angularVelocity = Vector3.zero; //회전하는 이동멈춤
        }
        
    }

    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position); //타겟 포지션으로 이동해라
            nav.isStopped = !isChase; //멈췄을때 이동하지 않는다.
        }
        OnDestroy();
    }


    void TargetingCastle()
    {

        RaycastHit[] rayHits = //레이케스트 = 레이저를 쏴서 충돌감지 등 활용
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Wall"));
        //레이어마스크는 콜라이더에만 적용가능하다. 그래서 콜라이더에 layer가 있어야 rayHits가 닿는다.
        if (rayHits.Length > 0 && !isAttack) //레이저가 쏘는 거리가 0보다 크면 실행해줘라 && attack중이 아닐 때 
        {
          
            StartCoroutine(Attack());
        }


    }

    IEnumerator Attack() // 공격 시 
    {
        
        isChase = false; //이동시간 끝
        isAttack = true; //공격시간 시작
        anim.SetBool("IsAttack", true);
        
        yield return new WaitForSeconds(0.9f);
        meleeArea.enabled = true;
        isTower = true;
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
        isTower = false;
        yield return new WaitForSeconds(1f);
        isChase = true; //공격을 다한 후 이동시간 시작
        isAttack = false; //공격을 다한 후 공격시간 끝
        anim.SetBool("IsAttack", false);
    }
    void FixedUpdate()
    {
        RaycastHit[] rayHit = //레이케스트 = 레이저를 쏴서 충돌감지 등 활용
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHit.Length > 0 && !isAttack) //레이저가 쏘는 거리가 0보다 크면 실행해줘라 && attack중이 아닐 때 
        {
            target = GameObject.Find("Player").transform;
            StartCoroutine(Attack());
        }
        else
        {
            TargetingCastle();
        }
       
        FreezeRotation();
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            Weapon weapon = other.GetComponent<Weapon>();
            curHealth -= weapon.damage;
            StartCoroutine(OnDamage());
        }
        else if (other.tag == "Arrow")
        {
            
            Arrow arrow = other.GetComponent<Arrow>();
            curHealth -= arrow.damage;
            hitSound.Play();
            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = arrow.damage; //데미지텍스트를 생성한 데미지는 arrow.damage이다.
           

            if (isDamaged == false) // 데미지 애니메이션이 끝난 후에 실행
            {
                isDamaged = true;
                StartCoroutine(OnDamage());
            }
        }
        else if(other.tag == "TurretArrow")
        {
            TurretArrow turretarrow = other.GetComponent<TurretArrow>();
            curHealth -= turretarrow.damage;
            hitSound.Play();
            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = turretarrow.damage; //데미지텍스트를 생성한 데미지는 arrow.damage이다.

            if (isDamaged == false) // 데미지 애니메이션이 끝난 후에 실행
            {
                isDamaged = true;
                StartCoroutine(OnDamage());
            }
        }

        else if (other.tag == "Skill1")
        {
            Skill1 skill1 = other.GetComponent<Skill1>();
            curHealth -= skill1.damage;
            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = skill1.damage; //데미지텍스트를 생성한 데미지는 skill2.damage이다.
            if (!isDamaged) // 데미지 애니메이션이 끝난 후에 실행
            {
                isDamaged = true;
                StartCoroutine(OnDamage()); //일반공격과 똑같은 피격판정이므로, OnDamage로 보낸다.
            }
        }

        else if (other.tag == "Skill2")
        {
            Skill2 skill2 = other.GetComponent<Skill2>();
            curHealth -= skill2.damage;

            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = skill2.damage; //데미지텍스트를 생성한 데미지는 skill2.damage이다.

            StartCoroutine(Skill2Damage());


        }

        else if (other.tag == "Skill3")
        {
            Skill3 skill3 = other.GetComponent<Skill3>();
            curHealth -= skill3.damage;
            Vector3 reactVec = -(transform.forward);

            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = skill3.damage; //데미지텍스트를 생성한 데미지는 skill2.damage이다.

            StartCoroutine(Skill3Damage(reactVec));

        }

        else if (other.tag == "Skill4")
        {
            Skill4 skill4 = other.GetComponent<Skill4>();
            curHealth -= skill4.damage;

            //데미지 텍스트 
            GameObject damageTxt = Instantiate(damageText); //데미지 생성
            damageTxt.transform.position = damageTextPos.position; //데미지텍스트 포지션 맞추고
            damageTxt.GetComponent<DamageText>().damage = skill4.damage; //데미지텍스트를 생성한 데미지는 skill2.damage이다.


            StartCoroutine(Skill4Damage());
        }
        
        else
        {
            return;
        }
    
}
    IEnumerator OnDamage()
    {
        
        if (curHealth > 0) //몬스터의 체력이 0보다 클경우
        {
            if (isDamaged) { 
                isChase = false; //이동시간 멈춤
                nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
                anim.SetTrigger("DoDamage"); //맞는 모션을 보여준다.

                yield return new WaitForSeconds(0.8f);
                isDamaged = false;
            }
            isChase = true; //이동시간 시작
            nav.enabled = true; //다시 다겟으로 이동하는 범위 시작
        }
        else if (curHealth <= 0) //몬스터의 체력이 0보다 작을 경우
        {

            gameObject.layer = 11; //physics에서 지정한 죽었을 때 다른 물체와 겹치게 하지 않게
            isChase = false; //이동 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            meleeArea.enabled = false; //공격범위도 비활성화
            deathSound.Play();
            anim.SetTrigger("DoDeath"); //죽는 모션을 보여준다.

            if (manager.stage <= 5)
            {
                
                if (enemyType == Type.B)
                {
                    manager.money += 500;
                }
                else
                    manager.money += 300;
            }
            else if (manager.stage > 5 && manager.stage <= 10)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 700;
                }
                else
                    manager.money += 500;
            }
            else if (manager.stage > 10 && manager.stage <= 15)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1000;
                }
                else
                    manager.money += 700;
            }
            else if (manager.stage > 15 && manager.stage <= 20)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1500;
                }
                else
                    manager.money += 1000;
            }

            Destroy(gameObject, 2); //게임 오브젝트를 2초뒤 없애준다.
        }
       

    } //일반화살을 맞았을 경우
    IEnumerator Skill2Damage() // 얼음화살을 맞았을 경우
    {
        if (curHealth > 0) //몬스터의 체력이 0보다 클경우
        {

            isChase = false; //이동시간 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            anim.speed = 0f; //일시적으로 애니메이션 속도를 0으로해 멈춘다.

            yield return new WaitForSeconds(1f); // 1초동안 고정

            anim.speed = 1.0f;
            anim.SetTrigger("DoDamage"); //맞는 모션을 보여준다.
            iceParticle.SetActive(true);
            yield return new WaitForSeconds(0.8f);

            isChase = true;
            nav.enabled = true;
            //여기서 잠시동안 느려진다. 

            nav.speed = 0.7f;
            anim.SetBool("IsWalk", true);
            
            yield return new WaitForSeconds(5f);
            iceParticle.SetActive(false);
            nav.speed = 1f;
            anim.SetBool("IsWalk", false);
        }
        else if (curHealth <= 0) //몬스터의 체력이 0보다 작을 경우
        {
           
            gameObject.layer = 11; //physics에서 지정한 죽었을 때 다른 물체와 겹치게 하지 않게
            isChase = false; //이동 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            meleeArea.enabled = false; //공격범위도 비활성화
            anim.SetTrigger("DoDeath"); //죽는 모션을 보여준다.
            
            if (manager.stage <= 5)
            {

                if (enemyType == Type.B)
                {
                    manager.money += 500;
                }
                else
                    manager.money += 300;
            }
            else if (manager.stage > 5 && manager.stage <= 10)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 700;
                }
                else
                    manager.money += 500;
            }
            else if (manager.stage > 10 && manager.stage <= 15)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1000;
                }
                else
                    manager.money += 700;
            }
            else if (manager.stage > 15 && manager.stage <= 20)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1500;
                }
                else
                    manager.money += 1000;
            }
            Destroy(gameObject, 2); //게임 오브젝트를 2초뒤 없애준다.
        }


    }

    IEnumerator Skill3Damage(Vector3 reactVec) // 파워샷을 맞았을 경우
    {
        if (curHealth > 0) //몬스터의 체력이 0보다 클경우
        {

            isChase = false; //이동시간 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            anim.SetTrigger("DoDamage"); //날아가는 모션을 보여준다.

            //reactVec += Vector3.up;
            rigid.AddForce(reactVec * 10, ForceMode.Impulse);
            yield return new WaitForSeconds(0.8f);

            isChase = true;
            nav.enabled = true;
        }
        else if (curHealth <= 0) //몬스터의 체력이 0보다 작을 경우
        {
          
            gameObject.layer = 11; //physics에서 지정한 죽었을 때 다른 물체와 겹치게 하지 않게
            isChase = false; //이동 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            meleeArea.enabled = false; //공격범위도 비활성화
            anim.SetTrigger("DoDeath"); //죽는 모션을 보여준다.

            if (manager.stage <= 5)
            {

                if (enemyType == Type.B)
                {
                    manager.money += 500;
                }
                else
                    manager.money += 300;
            }
            else if (manager.stage > 5 && manager.stage <= 10)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 700;
                }
                else
                    manager.money += 500;
            }
            else if (manager.stage > 10 && manager.stage <= 15)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1000;
                }
                else
                    manager.money += 700;
            }
            else if (manager.stage > 15 && manager.stage <= 20)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1500;
                }
                else
                    manager.money += 1000;
            }
            Destroy(gameObject, 2); //게임 오브젝트를 2초뒤 없애준다.
        }
    }

    IEnumerator Skill4Damage() // 드래곤샷 맞았을 경우
    {
        if (curHealth > 0) //몬스터의 체력이 0보다 클경우
        {

            isChase = false; //이동시간 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            anim.SetTrigger("DoDamage"); //날아가는 모션을 보여준다.

            yield return new WaitForSeconds(0.8f);
            isChase = true;
            nav.enabled = true;

        }
        else if (curHealth <= 0) //몬스터의 체력이 0보다 작을 경우
        {
       
            gameObject.layer = 11; //physics에서 지정한 죽었을 때 다른 물체와 겹치게 하지 않게
            isChase = false; //이동 멈춤
            nav.enabled = false; //타겟으로 이동하는 범위도 멈춤
            meleeArea.enabled = false; //공격범위도 비활성화
            anim.SetTrigger("DoDeath"); //죽는 모션을 보여준다.

            if (manager.stage <= 5)
            {

                if (enemyType == Type.B)
                {
                    manager.money += 500;
                }
                else
                    manager.money += 300;
            }
            else if (manager.stage > 5 && manager.stage <= 10)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 700;
                }
                else
                    manager.money += 500;
            }
            else if (manager.stage > 10 && manager.stage <= 15)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1000;
                }
                else
                    manager.money += 700;
            }
            else if (manager.stage > 15 && manager.stage <= 20)
            {
                if (enemyType == Type.B)
                {
                    manager.money += 1500;
                }
                else
                    manager.money += 1000;
            }
            Destroy(gameObject, 2); //게임 오브젝트를 2초뒤 없애준다.
        }


    }
    void OnDestroy()
    {
        if(manager.isBattle == false) { 
            if(gameObject.tag == "Enemy")
            {
                
                Destroy(gameObject);
            }
        }
    }


}
