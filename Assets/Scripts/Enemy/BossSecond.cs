using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossSecond : MonoBehaviour
{

    //#region Variable
    [Header("dsadsa")]
    public PlayerController player; //�÷��̾ ���� ����
    public int damage1 = 10;
    public int damage2 = 15;
    public int damage3 = 50;

    float targetRadius = 4.5f; //���� ���� ??
    float targetRange = 1f;  //���� ���� ??
    public int maxHealth; // ������ �ִ�ü��
    public int curHealth; // ������ ����ü��

    //#endregion
    public Transform target; // ������ ������ Ÿ��
    public BoxCollider damageArea; //������ �ǰ� ����
    public BoxCollider biteArea; // ������ ���� ���� ����
    public BoxCollider horeArea; // ������ ���� ���� ����
    public BoxCollider jumpAttackArea; //������ ����� ����
    public bool isChase; //Ÿ������ �̵��ϴ� bool��
    public bool isAttack; //����ó���ϴ� bool��
    public bool isJumpAttack = false; // ���� ������ ó���ϴ� bool��
                          //public bool isBattle; // ��Ʋ ������ �˷��ִ� bool��

    Rigidbody rigid;
    NavMeshAgent nav; //NavMeshAgent ���� ���ؼ��� using UnityEngine.AI ��Ű�� �ʿ�*
    Animator anim; //�ִϸ��̼�


    public AudioSource deathSound;
    public AudioSource attackSound;
    public AudioSource skillhitSound;
    public AudioSource hitSound;
    public AudioSource screamSound;
    public GameObject damageText; //������ �ؽ�Ʈ
    public Transform damageTextPos; //������ �ؽ�Ʈ ������

    //public GameObject iceParticle;
   

    bool isHalf = false; // ü���� �� ���Ҵٴ� �� �˷��ִ� bool��
    bool isScream = false; // ���� ���·� �Ѿ���� scream�ϰ� �Ѿ�� ���� bool��
    bool isOpening = false; //ó�� �������� �� bool��
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

            if (nav.enabled && !isAttack && !isHalf) //�Ϲ� ����
            {
                anim.SetBool("IsWalk", true);
                nav.SetDestination(target.position); //Ÿ�� ���������� �̵��ض�
            }
            else if (nav.enabled && !isAttack && isHalf && isScream) //���� ����
            {
                anim.SetBool("IsRun", true);
                nav.speed = 15f;
                nav.angularSpeed = 340f;
                nav.SetDestination(target.position); //Ÿ�� ���������� �̵��ض�
            }
            if (nav.enabled && isJumpAttack && isHalf)
            {

                nav.SetDestination(target.position);

            }
            if (nav.enabled && horeArea.enabled && !isDamage)
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward * 3, 0.01f);
            }
            if (curHealth <= (maxHealth / 2) && !isScream && !isHalf) // ���ݺ��� �ǰ� �پ��� ���
            {
                isHalf = true;
                anim.SetBool("IsWalk", false);
                StartCoroutine(DragonScream());

            }

            TargetingPlayer();
        }
        
    }

    IEnumerator DragonScream() //���¢���鼭 ���� ���·� �����Ѵ�.
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
        if (isChase || isAttack) //�Ѱų� ���ݽ� ���� ���� ��������
        {
            rigid.velocity = Vector3.zero; //�̵�����
            rigid.angularVelocity = Vector3.zero; //ȸ���ϴ� �̵�����
        }

    }
    void TargetingPlayer()
    {

        RaycastHit[] rayHits = //�����ɽ�Ʈ = �������� ���� �浹���� �� Ȱ��
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));
        //���̾��ũ�� �ݶ��̴����� ���밡���ϴ�. �׷��� �ݶ��̴��� layer�� �־�� rayHits�� ��´�.
        if (rayHits.Length > 0 && !isAttack && !isHalf && !isScream &&nav.enabled) //�������� ��� �Ÿ��� 0���� ũ�� ��������� && attack���� �ƴ� ��
        {
            StartCoroutine(Attack());
        }
        else if (rayHits.Length > 0 && !isAttack && isHalf && isScream && nav.enabled && curHealth > 0)
        {
            StartCoroutine(RageAttack());
        }


    }

    IEnumerator Attack() // ���� �� 
    {
        nav.enabled = false;
        isChase = false; //�̵��ð� ��
        isAttack = true; //���ݽð� ����

        yield return new WaitForSeconds(0.1f);



        int ranAction = Random.Range(0, 4);

        switch (ranAction) // Ȯ�������� �ٸ� ������ ����Ѵ�.
        {
            case 0:
            case 1:
                //���� ����
                StartCoroutine(biteAttack());
                break;

            case 2:
            case 3:
                //��ġ�� ���� ����
                StartCoroutine(hornAttack());
                break;


        }

    }

    IEnumerator RageAttack() // ���� �� 
    {
        nav.enabled = false;
        isChase = false; //�̵��ð� ��
        isAttack = true; //���ݽð� ����

        yield return new WaitForSeconds(0.1f);


        int ranAction = Random.Range(0, 7);

        switch (ranAction) // Ȯ�������� �ٸ� ������ ����Ѵ�.
        {
            case 0:
            case 1:
                //���� ����
                StartCoroutine(biteAttack());
                break;
            case 2:
            case 3:
                //��ġ�� ���� ����
                StartCoroutine(hornAttack());
                break;
            case 4:
            case 5:
            case 6:
                //���� ���� ����
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
        isJumpAttack = true; // ���� ���� ����

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
            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = arrow.damage; //�������ؽ�Ʈ�� ������ �������� arrow.damage�̴�.
            if (curHealth <= 0) //������ ü���� 0���� ���� ���
            {

                isChase = false; //�̵� ����
                nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
                deathSound.Play();
                anim.SetTrigger("doDeath"); //�״� ����� �����ش�.
                gameObject.layer = 11;
                Destroy(gameObject, 2); //���� ������Ʈ�� 2�ʵ� �����ش�.
            }

        }

        else if (other.tag == "Skill1")
        {
            Skill1 skill1 = other.GetComponent<Skill1>();
            curHealth -= skill1.damage;
            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill1.damage; //�������ؽ�Ʈ�� ������ �������� skill1.damage�̴�.

            StartCoroutine(Skill1Damage()); //�Ϲݰ��ݰ� �Ȱ��� �ǰ������̹Ƿ�, OnDamage�� ������.

        }

        else if (other.tag == "Skill2")
        {
            Skill2 skill2 = other.GetComponent<Skill2>();
            curHealth -= skill2.damage;

            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill2.damage; //�������ؽ�Ʈ�� ������ �������� skill2.damage�̴�.

            StartCoroutine(Skill1Damage());
        }


    }

    IEnumerator Skill1Damage()
    {
        if (curHealth > 0 && !isDamage) //������ ü���� 0���� Ŭ ���
        {
            isDamage = true;
            isChase = false; //�̵��ð� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            anim.SetTrigger("doDamage"); //�´� ����� �����ش�.
            skillhitSound.Play();
            yield return new WaitForSeconds(2f);


            isChase = true; //�̵��ð� ����
            nav.enabled = true; //�ٽ� Ÿ������ �̵��ϴ� ���� ����
            isDamage = false;
        }
        else if (curHealth <= 0) //������ ü���� 0���� ���� ���
        {

            isChase = false; //�̵� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            deathSound.Play();
            anim.SetTrigger("doDeath"); //�״� ����� �����ش�.
            gameObject.layer = 11;
            Destroy(gameObject, 2); //���� ������Ʈ�� 2�ʵ� �����ش�.
        }

    }


    


    
}
