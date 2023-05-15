using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //ī�޶����
    public CameraController shakeCamera;
    public Camera bossCamera; //���������� ī�޶�
    public Camera bossFrontCamera; //���� ���� �� �ٷ� �տ��� �����ִ� ī�޶�
    public TimeLineController timeLineController; //���� ����
    public Camera mainCamera; //���� ī�޶�
    public Camera frostCamera; //�������� �¾����� ī�޶�
    bool isCamera = false;

    public GameManager manager;
    public AudioSource shotSound; //�Ϲݰ��� ����
    public AudioSource dieSound; //�׾����� ����
    public AudioSource hitSound; //�������� �¾����� ����
    public Shop shop;

    Rigidbody rigid;
    public float speed; //�÷��̾� �̵��ӵ�
    public float curHealth; //�÷��̾� ���� ü��
    public float maxHealth; //�÷��̾� �ִ� ü��
   
    public int shield; //�÷��̾� ����
    public int maxShield; //�÷��̾� �ִ� ����

    float hAxis;
    float vAxis;

    bool attackMode;
    bool attackDown;
    bool wDown;
    bool fDown;
    bool sDown;
    bool aDown;
    bool isBorder; // ��� bool��
    bool isDodge;
    bool isDamage; //�ǰ� bool��
    bool dodge;
    bool isFireReady = true;
    bool isSlow;
    //��ų����
    [Header("skill Fire")]
    public Image abiltyImage1;
    public float cooldown1 = 10;
    public bool isCooldown = false;
    public KeyCode ability1;

    [Header("skill Ice")]
    public Image abiltyImage2;
    public float cooldown2 = 12;
    public bool isCooldown2 = false;
    public KeyCode ability2;

    [Header("skill Light")]
    public Image abiltyImage3;
    public float cooldown3 = 10;
    public bool isCooldown3 = false;
    public KeyCode ability3;

    [Header("skill Air")]
    public Image abiltyImage4;
    public float cooldown4 = 30;
    public bool isCooldown4 = false;
    public KeyCode ability4;


    [Header("skill")]
    bool casting1 = false; // ��ų 1���� ���������� ��ų�� ����� �� �ְ� // *�ܵ����� ���콺 ������ Ű�θ� ������ ���ϰ�*
    bool casting2 = false;  // ��ų 2���� ���������� ��ų�� ����� �� �ְ�
    bool casting3 = false;  // ��ų 3���� ���������� ��ų�� ����� �� �ְ�
    bool casting4 = false;  // ��ų 4���� ���������� ��ų�� ����� �� �ְ�
    bool inCasting = false; // ��ų�� ���������� �˸��� bool��
    [Header("SkillEffect")]
    public Transform skillPlay;// ó�� �߻�Ǵ� ��ġ
    public Rigidbody skill3Rigid; // �з����� ���� rigidbody
    public GameObject skill3; // ���� ������Ʈ ��ġ�� �ʱ�ȭ ��Ű������ ������Ʈ
    public BoxCollider skill3Area; // Ÿ�� ���� ����
    public Rigidbody skill4Rigid; // �з����� ���� rigidbody
    public GameObject skill4; // ���� ������Ʈ ��ġ�� �ʱ�ȭ ��Ű������ ������Ʈ
    public BoxCollider skill4Area; // Ÿ�� ���� ����
    

    GameObject nearObject;
    public GameObject[] TargetMarker;

    Vector3 moveVec;
    Vector3 dodgeVec;

    Animator anim;

    public Weapon equipWeapon;
    float fireDelay;
    public GameObject[] Prefabs;
    public GameObject[] PrefabsCast;

    private AudioSource soundComponent; //Play audio from Prefabs
    private AudioClip clip;
    private AudioSource soundComponentCast; //Play audio from PrefabsCast

    public GameObject player;

    //Blue Boss Zone
    public GameObject[] iceZoneFire;
    public GameObject[] iceZoneLight;
    
    public GameObject BluebossDoor;
    public GameObject BluebossDoorCol;
    public bool BlueisbossDoor = false;
    Vector3 targetBlueBossDoor = new Vector3(0, 0, 69.8f);
    Vector3 targetBlueBossDoor2 = new Vector3(10, 0, 69.8f);
    //Green Boss Zone
    public GameObject[] GreenZoneFire;
    public GameObject[] GreenZoneLight;

    public GameObject GreenbossDoor;
    public GameObject GreenbossDoorCol;
    public bool GreenisbossDoor = false;
    Vector3 targetGreenBossDoor = new Vector3(65.1f, 0, 68f);
    Vector3 targetGreenBossDoor2 = new Vector3(75f, 0, 68f);
    //Purple Boss Zone
    public GameObject[] PurpleZoneFire;
    public GameObject[] PurpleZoneLight;

    public GameObject PurplebossDoor;
    public GameObject PurplebossDoorCol;
    public bool PurpleisbossDoor = false;
    Vector3 targetPurpleBossDoor = new Vector3(-70.56f, 0, 65.9f);
    Vector3 targetPurpleBossDoor2 = new Vector3(-63.6f, 0, 65.9f);
    public AudioSource bossDoorSound;


    void Awake()
    {
        
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        abiltyImage1.fillAmount = 0;
        abiltyImage2.fillAmount = 0;
        abiltyImage3.fillAmount = 0;
        abiltyImage4.fillAmount = 0;
    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Dodge();
        //������Ȳ
        if (attackMode) // ���콺 �������� ������ �� 
        {
            if (attackDown == false) // ������ ��Ȳ�̸� Ȱ ���
            {
                attackDown = true;
                anim.SetBool("IsAim", true); // ���� �ڼ� ��� �̵� bool�� ���� ����?
            }
            else
            {
                attackDown = false; // �ݴ� ��Ȳ�̸� Ȱ�� ������
                anim.SetBool("IsAim", false);
            }
        }
        Shoot(); //���콺 ����
        Attack(); //��������
        
        if (Input.GetKeyDown("1")  && isCooldown == false) //Ű���� 1��
        {
            casting1 = true;
            StartCoroutine(aim1());            
        }

        if (Input.GetKeyDown("2") && manager.stage >= 6 && isCooldown2 == false) //Ű���� 2��
        {
            casting2 = true;
            StartCoroutine(aim2());
        }

        if (Input.GetKeyDown("3") && manager.stage >= 11 && isCooldown3 == false) //Ű���� 3��
        {
            casting3 = true;
            StartCoroutine(aim3());
        }

        if (Input.GetKeyDown("4") && manager.stage >= 16 && isCooldown4 == false) //Ű���� 4��
        {
            casting4 = true;
            StartCoroutine(aim4());
        }

        Skill1(); //1�� ��ų
        Skill2(); //2�� ��ų
        Skill3(); //3�� ��ų
        Skill4(); //4�� ��ų

        //��ų ��Ÿ�� ����
        Ability1(); 
        Ability2();
        Ability3();
        Ability4();
        //UI����
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return; //���콺�� ui���� ���� ���� �Ʒ� �ڵ带 �������� �ʵ��� ����
        //Blue Boss Zone Door
        if (BlueisbossDoor == true)
        {
            if(manager.stage == 6) {
                BluebossDoor.transform.position = Vector3.MoveTowards(BluebossDoor.transform.position, targetBlueBossDoor2, 0.2f);
            }
            else
            {
                BluebossDoor.transform.position = Vector3.MoveTowards(BluebossDoor.transform.position, targetBlueBossDoor, 0.2f);
            }
        }
        //Green Boss Zone Door
        if (GreenisbossDoor == true)
        {
            if(manager.stage == 11)
            {
                GreenbossDoor.transform.position = Vector3.MoveTowards(GreenbossDoor.transform.position, targetGreenBossDoor2, 0.2f);
            }
            else 
            { 
                GreenbossDoor.transform.position = Vector3.MoveTowards(GreenbossDoor.transform.position, targetGreenBossDoor, 0.2f);
            }
        }
        //Purple Boss Zone Door
        if (PurpleisbossDoor == true)
        {
            if (manager.stage == 16)
            {
                PurplebossDoor.transform.position = Vector3.MoveTowards(PurplebossDoor.transform.position, targetPurpleBossDoor2, 0.2f);
            }
            else
            {
                PurplebossDoor.transform.position = Vector3.MoveTowards(PurplebossDoor.transform.position, targetPurpleBossDoor, 0.2f);
            }
        }
    }
    void FixedUpdate()
    {
        FreezeRotation();        
    }

    IEnumerator aim1() // 1�� ��ų�� Ÿ�� ��Ŀ[0]�� aim ǥ��
    {
        TargetMarker[0].SetActive(true);
        while (true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                TargetMarker[0].transform.position = hit.point;
              
            }
            if (casting1 == false || isDodge) // dash������ casting �ø�� false�� �����
            {
                TargetMarker[0].SetActive(false);
                break;
            }
            if (Input.GetKeyDown("2")) // 2�� ��ų�� ���� ���
            {
                TargetMarker[0].SetActive(false);
                casting2 = true;
                StartCoroutine(aim2());
                casting1 = false;
                break;
            }

            else if (Input.GetKeyDown("3")) // 3�� ��ų�� ���� ���
            {
                TargetMarker[0].SetActive(false);
                casting3 = true;
                StartCoroutine(aim3());
                casting1 = false;
                break;
            }

            else if (Input.GetKeyDown("4")) // 4�� ��ų�� ���� ���
            {
                TargetMarker[0].SetActive(false);
                casting4 = true;
                StartCoroutine(aim4());
                casting1 = false;
                break;
            }
            else
            {
                yield return null;
            }

        }

    }

    void Skill1() // ���� ȭ�� ĳ���� �κ�
    {

        if (fDown && casting1 && !isCamera) //���콺 ������ Ű�� �����鼭 ĳ����1�� true�� ���� ���ǰ�
        {
            anim.SetTrigger("DoSkill1");
            StartCoroutine(Skill_1());
            casting1 = false;
            inCasting = true;
        }
    }

    IEnumerator Skill_1() // ���� ȭ�� ��ų
    {
        if (PrefabsCast[0].GetComponent<AudioSource>()) // ��� ���� ��� �κ�
        {
            soundComponentCast = PrefabsCast[0].GetComponent<AudioSource>();
            clip = soundComponentCast.clip;
            soundComponentCast.PlayOneShot(clip);
        }
        for (int i = 0; i <= 1; i++)
        {
            PrefabsCast[i].GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(0.8f);

        inCasting = false;

        Prefabs[0].transform.position = TargetMarker[0].transform.position;
        Prefabs[0].GetComponent<ParticleSystem>().Play();

        if (Prefabs[0].GetComponent<AudioSource>()) // ȭ���� ������ ���� ��� �κ�
        {
            soundComponent = Prefabs[0].GetComponent<AudioSource>();
            clip = soundComponent.clip;
            soundComponent.PlayOneShot(clip);
        }

        yield return new WaitForSeconds(0.2f);


    }

    IEnumerator aim2() // 2�� ��ų�� Ÿ�� ��Ŀ[1]�� aim ǥ��
    {
        TargetMarker[1].SetActive(true);
        while (true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                TargetMarker[1].transform.position = hit.point;
            }
            if (casting2 == false || isDodge) // dash������ casting �ø�� false�� �����
            {
                TargetMarker[1].SetActive(false);
                break;
            }
            if (Input.GetKeyDown("1")) // 1�� ��ų�� ���� ���
            {
                TargetMarker[1].SetActive(false);
                casting1 = true;
                StartCoroutine(aim1());
                casting2 = false;
                break;
            }

            else if (Input.GetKeyDown("3")) // 3�� ��ų�� ���� ���
            {
                TargetMarker[1].SetActive(false);
                casting3 = true;
                StartCoroutine(aim3());
                casting2 = false;
                break;
            }

            else if (Input.GetKeyDown("4")) // 4�� ��ų�� ���� ���
            {
                TargetMarker[1].SetActive(false);
                casting4 = true;
                StartCoroutine(aim4());
                casting2 = false;
                break;
            }
            else
            {
                yield return null;
            }

        }

    }

    void Skill2() //���� ȭ�� ĳ���� �κ�
        {
            if (fDown && casting2 && !isCamera) //���콺 ������ Ű�� �����鼭 ĳ����2�� true�� ���� ���ǰ�
            {
                anim.SetTrigger("DoSkill2");
                StartCoroutine(Skill_2());
                casting2 = false;
                inCasting = true;
            }
        }

    IEnumerator Skill_2() // ���� ȭ�� ��ų
    {

        PrefabsCast[2].GetComponent<ParticleSystem>().Play();
        if (PrefabsCast[2].GetComponent<AudioSource>())
        {
            soundComponentCast = PrefabsCast[2].GetComponent<AudioSource>();
            clip = soundComponentCast.clip;
            soundComponentCast.PlayOneShot(clip);
        }
        PrefabsCast[3].GetComponent<ParticleSystem>().Play();

        yield return new WaitForSeconds(0.8f);

        inCasting = false;

        Prefabs[1].transform.position = TargetMarker[1].transform.position;


        Prefabs[1].GetComponent<ParticleSystem>().Play();

        if (Prefabs[1].GetComponent<AudioSource>()) // ȭ���� ������ ���� ��� �κ�
        {
            soundComponent = Prefabs[1].GetComponent<AudioSource>();
            clip = soundComponent.clip;
            soundComponent.PlayOneShot(clip);
        }

        yield return new WaitForSeconds(2f);

        Prefabs[1].transform.position = new Vector3(-50, -50, -50);



    }

    IEnumerator aim3() // 3�� ��ų�� Ÿ�� ��Ŀ[2]�� aim ǥ��
    {
        TargetMarker[2].SetActive(true);
        while (true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                TargetMarker[2].transform.rotation = Quaternion.LookRotation(hit.point - TargetMarker[2].transform.position);
                
            }

            

            if (casting3 == false || isDodge) // dash������ casting �ø�� false�� �����
            {
                TargetMarker[2].SetActive(false);
                break;
            }
            if (Input.GetKeyDown("1")) // 1�� ��ų�� ���� ���
            {
                TargetMarker[2].SetActive(false);
                casting1 = true;
                StartCoroutine(aim1());
                casting3 = false;
                break;
            }

            else if (Input.GetKeyDown("2")) // 2�� ��ų�� ���� ���
            {
                TargetMarker[2].SetActive(false);
                casting2 = true;
                StartCoroutine(aim2());
                casting3 = false;
                break;
            }

            else if (Input.GetKeyDown("4")) // 4�� ��ų�� ���� ���
            {
                TargetMarker[2].SetActive(false);
                casting4 = true;
                StartCoroutine(aim4());
                casting3 = false;
                break;
            }
            else
            {
                yield return null;
            }

        }

    }

    void Skill3() //�Ŀ��� ĳ���� �κ�
    {
        if (fDown && casting3 && !isCamera) //���콺 ������ Ű�� �����鼭 ĳ����2�� true�� ���� ���ǰ�
        {
            anim.SetTrigger("DoSkill3");
            StartCoroutine(Skill_3());
            casting3 = false;
            inCasting = true;
        }
    }

    IEnumerator Skill_3() // �Ŀ��� ��ų �κ�
    {

        PrefabsCast[4].GetComponent<ParticleSystem>().Play();
        soundComponentCast = PrefabsCast[4].GetComponent<AudioSource>();
        clip = soundComponentCast.clip;
        soundComponentCast.PlayOneShot(clip);


        Prefabs[2].GetComponent<ParticleSystem>().Play();
        StartCoroutine(skill3Effect()); // ���⼭ Skiil3

        yield return new WaitForSeconds(0.85f);

        inCasting = false;
    }

    IEnumerator skill3Effect()
    {
        yield return new WaitForSeconds(0.2f);
        skill3Area.enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
        {
            Vector3 nextVec = rayHit.point - transform.position;
            nextVec.y = 0;
            nextVec = nextVec.normalized;
            skill3Rigid.AddForce(nextVec * 25, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.5f);
        skill3Area.enabled = false;
        skill3Rigid.velocity = Vector3.zero;
        skill3.transform.position = skillPlay.position;

    }

    IEnumerator aim4() // 4�� ��ų�� Ÿ�� ��Ŀ[3]�� aim ǥ��
    {
        TargetMarker[3].SetActive(true);
        while (true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
            {
                TargetMarker[3].transform.rotation = Quaternion.LookRotation(hit.point - TargetMarker[3].transform.position);

            }
            if (casting4 == false || isDodge) // dash������ casting �ø�� false�� �����
            {
                TargetMarker[3].SetActive(false);
                break;
            }
            if (Input.GetKeyDown("1")) // 1�� ��ų�� ���� ���
            {
                TargetMarker[3].SetActive(false);
                casting1 = true;
                StartCoroutine(aim1());
                casting4 = false;
                break;
            }

            else if (Input.GetKeyDown("2")) // 2�� ��ų�� ���� ���
            {
                TargetMarker[3].SetActive(false);
                casting2 = true;
                StartCoroutine(aim2());
                casting4 = false;
                break;
            }

            else if (Input.GetKeyDown("3")) // 3�� ��ų�� ���� ���
            {
                TargetMarker[3].SetActive(false);
                casting3 = true;
                StartCoroutine(aim3());
                casting4 = false;
                break;
            }
            else
            {
                yield return null;
            }

        }

    }

    void Skill4() // �� ȭ�� ĳ���� �κ�
    {

        if (fDown && casting4 && !isCamera) //���콺 ������ Ű�� �����鼭 ĳ����1�� true�� ���� ���ǰ�
        {
            anim.SetTrigger("DoSkill4");
            StartCoroutine(Skill_4());
            casting4 = false;
            inCasting = true;
        }
    }

    IEnumerator Skill_4() // �� ȭ�� ��ų
    {
        if (PrefabsCast[5].GetComponent<AudioSource>())
        {
            soundComponentCast = PrefabsCast[5].GetComponent<AudioSource>();
            clip = soundComponentCast.clip;
            soundComponentCast.PlayOneShot(clip);
        }
        PrefabsCast[5].GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.15f);
        if (Prefabs[3].GetComponent<FrontAttack>() != null)
        {
            foreach (var component in Prefabs[3].GetComponentsInChildren<FrontAttack>())
            {
                component.playMeshEffect = true;
            }
            StartCoroutine(skill4Effect()); // ���⼭ Skiil4

            yield return new WaitForSeconds(0.95f);

        }


        inCasting = false;

    }
    IEnumerator skill4Effect()
    {
        yield return new WaitForSeconds(0.1f);
        skill4Area.enabled = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
        {
            Vector3 nextVec = rayHit.point - transform.position;
            nextVec.y = 0;
            nextVec = nextVec.normalized;
            skill4Rigid.AddForce(nextVec * 27, ForceMode.Impulse);
        }
        yield return new WaitForSeconds(0.8f);
        skill4Area.enabled = false;
        skill4Rigid.velocity = Vector3.zero;
        skill4.transform.position = skillPlay.position;

    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); //ad
        vAxis = Input.GetAxisRaw("Vertical"); //ws
        wDown = Input.GetButton("Walk"); //shift
        dodge = Input.GetButtonDown("Jump"); // space
        fDown = Input.GetButton("Fire1"); //���콺 ����
        sDown = Input.GetButtonDown("Fire2"); //���콺 ������
        attackMode = Input.GetButtonDown("Fire2");// ���콺 ������
        aDown = Input.GetButtonDown("Atk"); //��������
        

    }

    void Move()
    {
        if (isCamera == true)
        {
            moveVec = new Vector3(0, 0, 0).normalized;
        }
        else
        {
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        }
        if (isDodge)
            moveVec = dodgeVec;

        if (!isBorder && !isDamage && !inCasting && !attackDown) //���� �ƴϰ� �ǰ� ������ �ʾ��� �� �����̰� + ��ų ���� �߿��� �� �����̰�
        {
            if (isSlow)
            {
                transform.position += moveVec * speed * 0.9f * Time.deltaTime;
            }
            else
            {
                transform.position += moveVec * speed * 1.2f * Time.deltaTime;
            }
            anim.SetBool("IsRun", moveVec != Vector3.zero);
            anim.SetBool("IsWalk", wDown);
        }
       

    }
   
    void Turn()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (moveVec != Vector3.zero && !isDamage && !inCasting && !attackDown) //������ �������� �¾��� ���� ��ų ���� �߿��� ȸ�� �Ұ�
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVec), 20 * Time.deltaTime);

        if (fDown && !attackDown && (casting1 || casting2 || casting3 || casting4))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                Vector3 nextVec = rayHit.point - transform.position;
                nextVec.y = 0;
                transform.LookAt(transform.position + nextVec);
            }


        }
    }

    void Shoot()
    {
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;


        if (attackDown && !inCasting && !isCamera)
        {
            anim.SetFloat("PosX", hAxis);
            anim.SetFloat("PosY", vAxis);
            moveVec = new Vector3(hAxis, 0, vAxis).normalized;
            transform.position += moveVec * speed * 0.7f * Time.deltaTime;
            StartCoroutine(ShootEvent());
        }
        if (attackDown && fDown && !isDodge && isFireReady && !casting1 && !casting2 && !casting3 && !casting4 && !isCamera)
        {
            if (shop.upArrow == true && shop.upArrow2 == false)
            {
                equipWeapon.UseBow2();
            }
            else if (shop.upArrow2 == true)
            {
                equipWeapon.UseBow3();
            }
            else
            {
                equipWeapon.UseBow();
            }
            shotSound.Play();
            anim.SetTrigger("DoShoot");
            fireDelay = 0;


        }
    }
    IEnumerator ShootEvent()
    {

        while (true)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (!inCasting) { 
                if (Physics.Raycast(ray, out rayHit, Mathf.Infinity, LayerMask.GetMask("Floor")))
                {
                    Vector3 nextVec = rayHit.point - transform.position;
                    nextVec.y = 0;
                    transform.LookAt(transform.position + nextVec);

                }
              }
            if (attackDown == false)
            {
                break;
            }
            else
            {
                yield return null;
            }

        }

    }

    void Attack() // ��������
        {
            fireDelay += Time.deltaTime;
            isFireReady = equipWeapon.rate < fireDelay;

            if (aDown && !isDodge && isFireReady)
            {
                equipWeapon.UseMelee();
                anim.SetTrigger("DoAttack");
                fireDelay = 0;
            }
        }
    void Dodge()
        {
            if (dodge && moveVec != Vector3.zero && !isDamage && !attackDown && !isCamera)
            {

                dodgeVec = moveVec;
                speed *= 2;
                anim.SetTrigger("DoDodge");
                isDodge = true;

                Invoke("DodgeOut", 0.4f);
            }
        }

    void DodgeOut()
        {
            speed *= 0.5f;
            isDodge = false;
        }

    void FreezeRotation()
    {
        rigid.angularVelocity = Vector3.zero;
    }




    void OnTriggerEnter(Collider other) // ���� ���⿡ �¾��� ��
    {
        if (other.tag == "BossAttack1" && !isDamage) // ���� ����
        {
            Boss bossAttack = other.GetComponentInParent<Boss>();
            curHealth -= bossAttack.damage1;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }

        }
        else if (other.tag == "BossAttack2" && !isDamage) // ���� ����
        {
            Boss bossAttack = other.GetComponentInParent<Boss>();
            curHealth -= bossAttack.damage2;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }

        }
        else if (other.tag == "BossAttack3" && !isDamage)
        {
            BossSword bossAttack = other.GetComponentInParent<BossSword>();
            curHealth -= bossAttack.damage;
            hitSound.Play();
            StartCoroutine(OnDamage());
            StartCoroutine(FrostCamera());
            StartCoroutine(SlowDebuff());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }

        }

        if (other.tag == "BossSecondAttack1" && !isDamage) // ���� ����
        {
            BossSecond bossAttack = other.GetComponentInParent<BossSecond>();
            curHealth -= bossAttack.damage1;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }

        }
        else if (other.tag == "BossSecondAttack2" && !isDamage) // horn ����
        {
            BossSecond bossAttack = other.GetComponentInParent<BossSecond>();
            curHealth -= bossAttack.damage2;

            hitSound.Play();
            StartCoroutine(OnDamage());

            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }

        }
        else if (other.tag == "BossSecondAttack3" && !isDamage)
        {
            BossSecond bossAttack = other.GetComponentInParent<BossSecond>();
            curHealth -= bossAttack.damage3;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }

        }

        if (other.tag == "BossThirdAttack1" && !isDamage)
        {
            BossThird bossAttack = other.GetComponentInParent<BossThird>();
            curHealth -= bossAttack.damage1;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }
        }
        else if (other.tag == "BossThirdAttack2" && !isDamage)
        {
            BossThird bossAttack = other.GetComponentInParent<BossThird>();
            curHealth -= bossAttack.damage2;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }
        }
        else if (other.tag == "BossThirdAttack3" && !isDamage)
        {
            BossFlame bossAttack = other.GetComponent<BossFlame>();
            curHealth -= bossAttack.damage;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }
        }
        if (other.tag == "EnemyWeapon" && !isDamage)
        {
            Enemy enemyWeapon = other.GetComponentInParent<Enemy>();
            curHealth -= enemyWeapon.damage - shield;
            hitSound.Play();
            StartCoroutine(OnDamage());
            if (curHealth <= 0) //���� ���⿡ �¾Ҵµ� �÷��̾� ü���� 0���Ϸ� �������� 
            {
                curHealth = 0;
                moveVec *= 0;
                anim.SetTrigger("DoDie");
                manager.battleStageSound.Pause();
                manager.backGroundSound.Pause();
                StartCoroutine(waitFortime());
            }
        }
        if (nearObject == null)
        {
            if (other.tag == "Shop")
            {
                Shop shop = other.GetComponent<Shop>();
                shop.Enter(this); //�ڱ��ڽſ� ������ ���� this Ű���� ���
            }
            else if(other.tag == "SkillShop")
            {
                SkillShop skillshop = other.GetComponent<SkillShop>();
                skillshop.Enter(this); //�ڱ��ڽſ� ������ ���� this Ű���� ���
            }
        }
                //ice ������ ���������ڵ�
        if (other.tag == "FireTower1")
            {
            iceZoneFire[0].SetActive(true);
            iceZoneFire[1].SetActive(true);
            iceZoneLight[0].SetActive(true);
            iceZoneLight[1].SetActive(true);
            }
        if (other.tag == "FireTower2")
        {
            iceZoneFire[2].SetActive(true);
            iceZoneFire[3].SetActive(true);
            iceZoneLight[2].SetActive(true);
            iceZoneLight[3].SetActive(true);
        }
        if (other.tag == "FireTower3")
        {
            iceZoneFire[4].SetActive(true);
            iceZoneFire[5].SetActive(true);
            iceZoneLight[4].SetActive(true);
            iceZoneLight[5].SetActive(true);
        }
        if (other.tag == "FireTower4")
        {
            iceZoneFire[6].SetActive(true);
            iceZoneFire[7].SetActive(true);
            iceZoneLight[6].SetActive(true);
            iceZoneLight[7].SetActive(true);
        }
        if (other.tag == "FireTower5")
        {
            iceZoneFire[8].SetActive(true);
            iceZoneFire[9].SetActive(true);
            iceZoneLight[8].SetActive(true);
            iceZoneLight[9].SetActive(true);
        }
        if (other.tag == "BossDoor")
        {
            BlueisbossDoor = true;
            bossDoorSound.Play();
            if (BlueisbossDoor == true)
            {
                BluebossDoorCol.SetActive(false);
            }
            isCamera = true;
            StartCoroutine(bossDirectorCamera());
        }
        if (other.tag == "Green Fire 1")
        {
            GreenZoneFire[0].SetActive(true);
            GreenZoneFire[1].SetActive(true);
            GreenZoneLight[0].SetActive(true);
            GreenZoneLight[1].SetActive(true);
        }
        if (other.tag == "Green Fire 2")
        {
            GreenZoneFire[2].SetActive(true);
            GreenZoneFire[3].SetActive(true);
            GreenZoneLight[2].SetActive(true);
            GreenZoneLight[3].SetActive(true);
        }
        if (other.tag == "Green Fire 3")
        {
            GreenZoneFire[4].SetActive(true);
            GreenZoneFire[5].SetActive(true);
            GreenZoneLight[4].SetActive(true);
            GreenZoneLight[5].SetActive(true);
        }
        if (other.tag == "Green Fire 4")
        {
            GreenZoneFire[6].SetActive(true);
            GreenZoneFire[7].SetActive(true);
            GreenZoneLight[6].SetActive(true);
            GreenZoneLight[7].SetActive(true);
        }
        if (other.tag == "Green Fire 5")
        {
            GreenZoneFire[8].SetActive(true);
            GreenZoneFire[9].SetActive(true);
            GreenZoneLight[8].SetActive(true);
            GreenZoneLight[9].SetActive(true);
        }
        if (other.tag == "BossDoor2")
        {
            GreenisbossDoor = true;
            bossDoorSound.Play();
            if (GreenisbossDoor == true)
            {
                GreenbossDoorCol.SetActive(false);
            }
            isCamera = true;
            StartCoroutine(bossDirectorCamera());
        }
        if (other.tag == "Purple Fire 1")
        {
            PurpleZoneFire[0].SetActive(true);
            PurpleZoneFire[1].SetActive(true);
            PurpleZoneLight[0].SetActive(true);
            PurpleZoneLight[1].SetActive(true);
        }
        if (other.tag == "Purple Fire 2")
        {
            PurpleZoneFire[2].SetActive(true);
            PurpleZoneFire[3].SetActive(true);
            PurpleZoneLight[2].SetActive(true);
            PurpleZoneLight[3].SetActive(true);
        }
        if (other.tag == "Purple Fire 3")
        {
            PurpleZoneFire[4].SetActive(true);
            PurpleZoneFire[5].SetActive(true);
            PurpleZoneLight[4].SetActive(true);
            PurpleZoneLight[5].SetActive(true);
        }
        if (other.tag == "Purple Fire 4")
        {
            PurpleZoneFire[6].SetActive(true);
            PurpleZoneFire[7].SetActive(true);
            PurpleZoneLight[6].SetActive(true);
            PurpleZoneLight[7].SetActive(true);
        }
        if (other.tag == "Purple Fire 5")
        {
            PurpleZoneFire[8].SetActive(true);
            PurpleZoneFire[9].SetActive(true);
            PurpleZoneLight[8].SetActive(true);
            PurpleZoneLight[9].SetActive(true);
        }
        if (other.tag == "BossDoor3")
        {
            PurpleisbossDoor = true;
            bossDoorSound.Play();
            if (PurpleisbossDoor == true)
            {
                PurplebossDoorCol.SetActive(false);
            }
            isCamera = true;
            StartCoroutine(bossDirectorCamera());
        }
        if (other.tag == "Warring")
        { 
            
            Vector3 reactVec = -(transform.forward);
            rigid.AddForce(reactVec * 50, ForceMode.Impulse); 
         }
        if(other.tag == "GreenZonePortal")
        {
            player.transform.position = new Vector3(0.56f, 0, -14.7f);
        }
        if (other.tag == "BlueZonePortal")
        {
            player.transform.position = new Vector3(0.56f, 0, -14.7f);
        }
        if (other.tag == "PurpleZonePortal")
        {
            player.transform.position = new Vector3(0.56f, 0, -14.7f);
        }

    }
    IEnumerator FrostCamera() //�������� ���� �¾��� �� ī�޶�
    {
        mainCamera.enabled = false;
        frostCamera.enabled = true;
        yield return new WaitForSeconds(4f);
        frostCamera.enabled = false;
        mainCamera.enabled = true;
    }
    IEnumerator bossDirectorCamera() //���� ����ī�޶�
    {
        mainCamera.enabled = false;
        bossCamera.enabled = true;
        timeLineController.BossZone();
        yield return new WaitForSeconds(4);
        bossCamera.enabled = false;
        bossFrontCamera.enabled = true;
        yield return new WaitForSeconds(2);
        shakeCamera.Shake();
        yield return new WaitForSeconds(2.5f);
        bossFrontCamera.enabled = false;
        mainCamera.enabled = true;
        isCamera = false;


    }
    IEnumerator SlowDebuff()
    {
        isSlow = true;

        yield return new WaitForSeconds(4f);

        isSlow = false;
        Debug.Log("slowEnd");

    }
    //��������
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Shop" || other.tag == "SkillShop")
        {
            nearObject = other.gameObject;
        }    
    }
    //��������
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Shop" )
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            nearObject = null;
        }
        else if (other.tag == "SkillShop")
        {
            SkillShop skillshop = nearObject.GetComponent<SkillShop>();
            skillshop.Exit();
            nearObject = null;
        }
    }
    IEnumerator OnDamage()
    {
        isDamage = true;
        anim.SetTrigger("DoDamage");

        yield return new WaitForSeconds(1f);

       
        isDamage = false;
    }
    IEnumerator waitFortime() //�״¸�� ���Ŀ� 1.3�ʵڿ� �й��ǳ� ��Ÿ�����ϰ� Ÿ�ӽ����� 0���� �����. �۽�Ʈ ������ ���ӽ��� ������ �ٽ� 1�̵ȴ�.
    {
        yield return new WaitForSeconds(1.2f);
        manager.DiePanel.SetActive(true);
        dieSound.Play();
        Time.timeScale = 0; //���ӽ��ǵ� 0�̶�� �� �� �Ͻ�������  = 1 �̵Ǹ� �ٽ� ���� ���ǵ�� �� (�簳)
        
    }

    //��ų ��Ÿ�� ����
    void Ability1()
    {
        if (Input.GetKey(ability1) && isCooldown == false)
        {
            isCooldown = true;
            abiltyImage1.fillAmount = 1; //fillAmount �� 1 �̸� �� ���̰� �Ǵ� ���
        }
        if (isCooldown)
        {

            abiltyImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (abiltyImage1.fillAmount <= 0)
            {
                abiltyImage1.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            isCooldown2 = true;
            abiltyImage2.fillAmount = 1;
        }
        if (isCooldown2)
        {
            abiltyImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (abiltyImage2.fillAmount <= 0)
            {
                abiltyImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            isCooldown3 = true;
            abiltyImage3.fillAmount = 1;
        }
        if (isCooldown3)
        {
            abiltyImage3.fillAmount -= 1 / cooldown3 * Time.deltaTime;
            if (abiltyImage3.fillAmount <= 0)
            {
                abiltyImage3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
    void Ability4()
    {
        if (Input.GetKey(ability4) && isCooldown4 == false)
        {
            isCooldown4 = true;
            abiltyImage4.fillAmount = 1;
        }
        if (isCooldown4)
        {
            abiltyImage4.fillAmount -= 1 / cooldown4 * Time.deltaTime;
            if (abiltyImage4.fillAmount <= 0)
            {
                abiltyImage4.fillAmount = 0;
                isCooldown4 = false;
            }
        }
    }
}
