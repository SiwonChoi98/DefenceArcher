using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour // �׳� ���� ������
{
    
    public PlayerController player; //�÷��̾ ���� ����
    public Transform[] swordZone;
    public int damage1 = 10;
    public int damage2 = 15;
    public GameObject IceSword;
    float targetRadius = 4.5f; //���� ���� ??
    float targetRange = 2f;  //���� ���� ??
    public int maxHealth; // ������ �ִ�ü��
    public int curHealth; // ������ ����ü��
    public Transform target; // ������ ������ Ÿ��
    public BoxCollider damageArea; //������ �ǰ� ����
    public BoxCollider biteArea; // ������ ���� ���� ����
    public BoxCollider clawArea; // ������ ���� ���� ����

    public bool isChase; //Ÿ������ �̵��ϴ� bool��
    public bool isAttack; //����ó���ϴ� bool��
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

    
    bool isHalf = false; // ü���� �� ���Ҵٴ� �� �˷��ִ� bool��
    bool isScream = false; // ���� ���·� �Ѿ���� scream�ϰ� �Ѿ�� ���� bool��
    bool isOpening = false; // ó�� �������� �� bool��
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
        if (player.BlueisbossDoor)
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
            if (curHealth <= (maxHealth / 2) && !isScream && !isHalf) // ���ݺ��� �ǰ� �پ��� ���
            {
                isHalf = true;
                anim.SetBool("IsWalk", false);
                StartCoroutine(DragonScream());

            }
            TargetingPlayer();
        }
       
    }

    IEnumerator DragonScream()
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
        if (rayHits.Length > 0 && !isAttack && !isHalf &&!isScream &&nav.enabled) //�������� ��� �Ÿ��� 0���� ũ�� ��������� && attack���� �ƴ� ��
        {
            StartCoroutine(Attack());
        }
        else if (rayHits.Length > 0 && !isAttack && isHalf &&isScream && nav.enabled && curHealth > 0)
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
                //���� ���� ����
                StartCoroutine(clawAttack());
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
                //���� ���� ����
                StartCoroutine(clawAttack());
                break;
            case 4:
            case 5:
            case 6:
                //������ ��ȯ ����
                StartCoroutine(swordAttack());
                break;

        }
    }

    IEnumerator biteAttack() // bite ���� �ݶ��̴��� �������� �����ϴ� ������ �ʹ� �پ��� ������ �ִ�. ���� biteAttack�� �������� Ʈ���ŵǵ� �������� ���� �ʰ� �ؾ��Ѵ�.
    {

        transform.LookAt(target.position);
        anim.SetTrigger("doBite");
        attackSound.Play();
        yield return new WaitForSeconds(0.1f);
        biteArea.enabled = true;

        yield return new WaitForSeconds(1.1f);
        biteArea.enabled = false;

        yield return new WaitForSeconds(0.1f);

        isChase = true;
        isAttack = false;
        nav.enabled = true;
    }




    IEnumerator clawAttack()
    {

        transform.LookAt(target.position);
        anim.SetTrigger("doClaw");
        damageArea.enabled = false; // �������� �ݶ��̴��� ��ø���� ������ ���� ���� ����
        yield return new WaitForSeconds(0.8f);
        clawArea.enabled = true;
        attackSound.Play();

        yield return new WaitForSeconds(1.4f);
        clawArea.enabled = false;
        damageArea.enabled = true; // ���� ������ �������� �ٽ� �ǰ� ������ Ȱ��ȭ��Ų��.
        yield return new WaitForSeconds(0.8f);

        isChase = true;
        isAttack = false;
        nav.enabled = true;
    }

    
    IEnumerator swordAttack()
    {
        transform.LookAt(target.position);
        anim.SetTrigger("doSword");
        screamSound.Play();

        yield return new WaitForSeconds(1f);
        
        for (int i = 0; i < 5; i++)
        {
            swordZone[i].rotation = Quaternion.LookRotation(target.position - swordZone[i].transform.position);
            GameObject instantSword = Instantiate(IceSword, swordZone[i].position, swordZone[i].rotation);
            Rigidbody swordRigid = instantSword.GetComponent<Rigidbody>();
            swordRigid.velocity = swordZone[i].forward * 25;
            yield return new WaitForSeconds(0.5f);
        }

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


    

}

