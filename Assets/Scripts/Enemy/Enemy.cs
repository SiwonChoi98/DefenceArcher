using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C}; //������ Ÿ��
    public Type enemyType;
    public GameManager manager; //���ӸŴ����� ���� ����
    

    public int damage;
    public float targetRadius = 1f; //���� ����
    public float targetRange = 1f;  //���� ����
    public int maxHealth; // ������ �ִ�ü��
    public int curHealth; // ������ ����ü��
    public Transform target; // ���Ͱ� ������ Ÿ��
    public BoxCollider meleeArea; // ���� ���� ����
    public bool isChase; //Ÿ������ �̵��ϴ� bool��
    public bool isAttack; //����ó���ϴ� bool��
    public bool isTower = false; // Ÿ���� ������ �ѹ��� ������ ���� bool��
    bool isDamaged = false; //���� ���ݹ޴� ����
 
    Rigidbody rigid;
    NavMeshAgent nav; //NavMeshAgent ���� ���ؼ��� using UnityEngine.AI ��Ű�� �ʿ�*
    Animator anim; //�ִϸ��̼�
    
    public AudioSource deathSound;
    public AudioSource hitSound;
    public GameObject damageText; //������ �ؽ�Ʈ
    public Transform damageTextPos; //������ �ؽ�Ʈ ������

    public GameObject iceParticle;

    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        Invoke("ChaseStart", 0.1f); //0.1�� ��� �ߴٰ� ChaseStart �Լ��� ����
    }

    void ChaseStart()
    {
        isChase = true;
           
    }

    void FreezeRotation()
    {
        if (isChase || isAttack) //�Ѱų� ���ݽ� ���� ���� ��������
        {
            rigid.velocity = Vector3.zero; //�̵�����
            rigid.angularVelocity = Vector3.zero; //ȸ���ϴ� �̵�����
        }
        
    }

    void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position); //Ÿ�� ���������� �̵��ض�
            nav.isStopped = !isChase; //�������� �̵����� �ʴ´�.
        }
        OnDestroy();
    }


    void TargetingCastle()
    {

        RaycastHit[] rayHits = //�����ɽ�Ʈ = �������� ���� �浹���� �� Ȱ��
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Wall"));
        //���̾��ũ�� �ݶ��̴����� ���밡���ϴ�. �׷��� �ݶ��̴��� layer�� �־�� rayHits�� ��´�.
        if (rayHits.Length > 0 && !isAttack) //�������� ��� �Ÿ��� 0���� ũ�� ��������� && attack���� �ƴ� �� 
        {
          
            StartCoroutine(Attack());
        }


    }

    IEnumerator Attack() // ���� �� 
    {
        
        isChase = false; //�̵��ð� ��
        isAttack = true; //���ݽð� ����
        anim.SetBool("IsAttack", true);
        
        yield return new WaitForSeconds(0.9f);
        meleeArea.enabled = true;
        isTower = true;
        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
        isTower = false;
        yield return new WaitForSeconds(1f);
        isChase = true; //������ ���� �� �̵��ð� ����
        isAttack = false; //������ ���� �� ���ݽð� ��
        anim.SetBool("IsAttack", false);
    }
    void FixedUpdate()
    {
        RaycastHit[] rayHit = //�����ɽ�Ʈ = �������� ���� �浹���� �� Ȱ��
            Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

        if (rayHit.Length > 0 && !isAttack) //�������� ��� �Ÿ��� 0���� ũ�� ��������� && attack���� �ƴ� �� 
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
            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = arrow.damage; //�������ؽ�Ʈ�� ������ �������� arrow.damage�̴�.
           

            if (isDamaged == false) // ������ �ִϸ��̼��� ���� �Ŀ� ����
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
            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = turretarrow.damage; //�������ؽ�Ʈ�� ������ �������� arrow.damage�̴�.

            if (isDamaged == false) // ������ �ִϸ��̼��� ���� �Ŀ� ����
            {
                isDamaged = true;
                StartCoroutine(OnDamage());
            }
        }

        else if (other.tag == "Skill1")
        {
            Skill1 skill1 = other.GetComponent<Skill1>();
            curHealth -= skill1.damage;
            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill1.damage; //�������ؽ�Ʈ�� ������ �������� skill2.damage�̴�.
            if (!isDamaged) // ������ �ִϸ��̼��� ���� �Ŀ� ����
            {
                isDamaged = true;
                StartCoroutine(OnDamage()); //�Ϲݰ��ݰ� �Ȱ��� �ǰ������̹Ƿ�, OnDamage�� ������.
            }
        }

        else if (other.tag == "Skill2")
        {
            Skill2 skill2 = other.GetComponent<Skill2>();
            curHealth -= skill2.damage;

            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill2.damage; //�������ؽ�Ʈ�� ������ �������� skill2.damage�̴�.

            StartCoroutine(Skill2Damage());


        }

        else if (other.tag == "Skill3")
        {
            Skill3 skill3 = other.GetComponent<Skill3>();
            curHealth -= skill3.damage;
            Vector3 reactVec = -(transform.forward);

            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill3.damage; //�������ؽ�Ʈ�� ������ �������� skill2.damage�̴�.

            StartCoroutine(Skill3Damage(reactVec));

        }

        else if (other.tag == "Skill4")
        {
            Skill4 skill4 = other.GetComponent<Skill4>();
            curHealth -= skill4.damage;

            //������ �ؽ�Ʈ 
            GameObject damageTxt = Instantiate(damageText); //������ ����
            damageTxt.transform.position = damageTextPos.position; //�������ؽ�Ʈ ������ ���߰�
            damageTxt.GetComponent<DamageText>().damage = skill4.damage; //�������ؽ�Ʈ�� ������ �������� skill2.damage�̴�.


            StartCoroutine(Skill4Damage());
        }
        
        else
        {
            return;
        }
    
}
    IEnumerator OnDamage()
    {
        
        if (curHealth > 0) //������ ü���� 0���� Ŭ���
        {
            if (isDamaged) { 
                isChase = false; //�̵��ð� ����
                nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
                anim.SetTrigger("DoDamage"); //�´� ����� �����ش�.

                yield return new WaitForSeconds(0.8f);
                isDamaged = false;
            }
            isChase = true; //�̵��ð� ����
            nav.enabled = true; //�ٽ� �ٰ����� �̵��ϴ� ���� ����
        }
        else if (curHealth <= 0) //������ ü���� 0���� ���� ���
        {

            gameObject.layer = 11; //physics���� ������ �׾��� �� �ٸ� ��ü�� ��ġ�� ���� �ʰ�
            isChase = false; //�̵� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            meleeArea.enabled = false; //���ݹ����� ��Ȱ��ȭ
            deathSound.Play();
            anim.SetTrigger("DoDeath"); //�״� ����� �����ش�.

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

            Destroy(gameObject, 2); //���� ������Ʈ�� 2�ʵ� �����ش�.
        }
       

    } //�Ϲ�ȭ���� �¾��� ���
    IEnumerator Skill2Damage() // ����ȭ���� �¾��� ���
    {
        if (curHealth > 0) //������ ü���� 0���� Ŭ���
        {

            isChase = false; //�̵��ð� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            anim.speed = 0f; //�Ͻ������� �ִϸ��̼� �ӵ��� 0������ �����.

            yield return new WaitForSeconds(1f); // 1�ʵ��� ����

            anim.speed = 1.0f;
            anim.SetTrigger("DoDamage"); //�´� ����� �����ش�.
            iceParticle.SetActive(true);
            yield return new WaitForSeconds(0.8f);

            isChase = true;
            nav.enabled = true;
            //���⼭ ��õ��� ��������. 

            nav.speed = 0.7f;
            anim.SetBool("IsWalk", true);
            
            yield return new WaitForSeconds(5f);
            iceParticle.SetActive(false);
            nav.speed = 1f;
            anim.SetBool("IsWalk", false);
        }
        else if (curHealth <= 0) //������ ü���� 0���� ���� ���
        {
           
            gameObject.layer = 11; //physics���� ������ �׾��� �� �ٸ� ��ü�� ��ġ�� ���� �ʰ�
            isChase = false; //�̵� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            meleeArea.enabled = false; //���ݹ����� ��Ȱ��ȭ
            anim.SetTrigger("DoDeath"); //�״� ����� �����ش�.
            
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
            Destroy(gameObject, 2); //���� ������Ʈ�� 2�ʵ� �����ش�.
        }


    }

    IEnumerator Skill3Damage(Vector3 reactVec) // �Ŀ����� �¾��� ���
    {
        if (curHealth > 0) //������ ü���� 0���� Ŭ���
        {

            isChase = false; //�̵��ð� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            anim.SetTrigger("DoDamage"); //���ư��� ����� �����ش�.

            //reactVec += Vector3.up;
            rigid.AddForce(reactVec * 10, ForceMode.Impulse);
            yield return new WaitForSeconds(0.8f);

            isChase = true;
            nav.enabled = true;
        }
        else if (curHealth <= 0) //������ ü���� 0���� ���� ���
        {
          
            gameObject.layer = 11; //physics���� ������ �׾��� �� �ٸ� ��ü�� ��ġ�� ���� �ʰ�
            isChase = false; //�̵� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            meleeArea.enabled = false; //���ݹ����� ��Ȱ��ȭ
            anim.SetTrigger("DoDeath"); //�״� ����� �����ش�.

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
            Destroy(gameObject, 2); //���� ������Ʈ�� 2�ʵ� �����ش�.
        }
    }

    IEnumerator Skill4Damage() // �巡�Ｆ �¾��� ���
    {
        if (curHealth > 0) //������ ü���� 0���� Ŭ���
        {

            isChase = false; //�̵��ð� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            anim.SetTrigger("DoDamage"); //���ư��� ����� �����ش�.

            yield return new WaitForSeconds(0.8f);
            isChase = true;
            nav.enabled = true;

        }
        else if (curHealth <= 0) //������ ü���� 0���� ���� ���
        {
       
            gameObject.layer = 11; //physics���� ������ �׾��� �� �ٸ� ��ü�� ��ġ�� ���� �ʰ�
            isChase = false; //�̵� ����
            nav.enabled = false; //Ÿ������ �̵��ϴ� ������ ����
            meleeArea.enabled = false; //���ݹ����� ��Ȱ��ȭ
            anim.SetTrigger("DoDeath"); //�״� ����� �����ش�.

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
            Destroy(gameObject, 2); //���� ������Ʈ�� 2�ʵ� �����ش�.
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
