using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossThird : MonoBehaviour
{

    
    public PlayerController player; //���ӸŴ����� ���� ����
   
    public int damage1 = 10;
    public int damage2 = 15;
    float targetRadius = 4f; //���� ���� ??
    float targetRange = 1.5f;  //���� ���� ??
    public int maxHealth; // ������ �ִ�ü��
    public int curHealth; // ������ ����ü��
    public Transform target; // ������ ������ Ÿ��
    public BoxCollider damageArea; //������ �ǰ� ����
    public BoxCollider biteArea; // ������ ���� ���� ����
    public BoxCollider clawArea; // ������ ���� ���� ����
    public BoxCollider bressArea; //������ �극�� ���� ����
  
    public ParticleSystem[] particle; // ������ �극�� ��ƼŬ
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
    public AudioSource flameSound;
    public GameObject damageText; //������ �ؽ�Ʈ
    public Transform damageTextPos; //������ �ؽ�Ʈ ������

    
    bool isHalf = false; // ü���� �� ���Ҵٴ� �� �˷��ִ� bool��
    bool isScream = false; // ���� ���·� �Ѿ���� scream�ϰ� �Ѿ�� ���� bool��
    bool isOpening = false; // ó�� �������� �� bool��
    bool isDamage = false;
    bool isFlame = false;
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

        if (player.PurpleisbossDoor)
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
        if (rayHits.Length > 0 && !isAttack && !isHalf && !isScream &&nav.enabled ) //�������� ��� �Ÿ��� 0���� ũ�� ��������� && attack���� �ƴ� ��
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


        int ranAction = Random.Range(0,7);

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
                //�극�� ���� ����
                StartCoroutine(flameAttack());
                break;

        }
    }

    IEnumerator BressAttack()
    {
        nav.enabled = false;
        isChase = false; //�̵��ð� ��
        isAttack = true; //���ݽð� ����

        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(9, 10);

        switch (ranAction) // Ȯ�������� �ٸ� ������ ����Ѵ�.
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
                //���� ����
                nav.enabled = true;
                isChase = true; //�̵��ð� ��
                isAttack = false; //���ݽð� ����
                break;
            case 9:
                //�극�� ���� ����
                StartCoroutine(flameAttack());
                break;
        }
    }

    IEnumerator biteAttack() // bite ���� �ݶ��̴��� �������� �����ϴ� ������ �ʹ� �پ��� ������ �ִ�. ���� biteAttack�� �������� Ʈ���ŵǵ� �������� ���� �ʰ� �ؾ��Ѵ�.
    {

        transform.LookAt(target.position);
        anim.SetTrigger("doBite");
        attackSound.Play();
        yield return new WaitForSeconds(0.2f);
        biteArea.enabled = true;

        yield return new WaitForSeconds(0.8f);
        biteArea.enabled = false;

        yield return new WaitForSeconds(0.5f);

        isChase = true;
        isAttack = false;
        nav.enabled = true;
    }




    IEnumerator clawAttack()
    {

        transform.LookAt(target.position);
        anim.SetTrigger("doClaw");
        damageArea.enabled = false; // �������� �ݶ��̴��� ��ø���� ������ ���� ���� ����
        yield return new WaitForSeconds(0.4f);
        clawArea.enabled = true;
        attackSound.Play();

        yield return new WaitForSeconds(0.6f);
        clawArea.enabled = false;
        damageArea.enabled = true; // ���� ������ �������� �ٽ� �ǰ� ������ Ȱ��ȭ��Ų��.
        yield return new WaitForSeconds(0.5f);

        isChase = true;
        isAttack = false;
        nav.enabled = true;
    }


    IEnumerator flameAttack()
    {
        isFlame = true;
        transform.LookAt(target.position);

        anim.SetTrigger("doFlame");

        flameSound.Play();

        yield return new WaitForSeconds(0.5f);

        bressArea.enabled = true;
        for (int i = 0; i <= 1; i++)
        {
            particle[i].GetComponent<ParticleSystem>().Play();
        }


        yield return new WaitForSeconds(1.3f);

        bressArea.enabled = false;

        for (int i = 0; i <= 1; i++)
        {
            particle[i].GetComponent<ParticleSystem>().Stop();
        }

        yield return new WaitForSeconds(1f);

        isFlame = false;
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
            damageTxt.GetComponent<DamageText>().damage = skill2.damage; //�������ؽ�Ʈ�� ������ �������� skill1.damage�̴�.

            StartCoroutine(Skill1Damage()); //�Ϲݰ��ݰ� �Ȱ��� �ǰ������̹Ƿ�, OnDamage�� ������.

        }

        else if (other.tag == "Skill3")
        {
            Skill3 skill3 = other.GetComponent<Skill3>();
            curHealth -= skill3.damage;
            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill3.damage; //�������ؽ�Ʈ�� ������ �������� skill1.damage�̴�.

            StartCoroutine(Skill1Damage()); //�Ϲݰ��ݰ� �Ȱ��� �ǰ������̹Ƿ�, OnDamage�� ������.

        }


        IEnumerator Skill1Damage()
        {
            if (curHealth > 0 && !isDamage && !isFlame) //������ ü���� 0���� Ŭ ���
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
