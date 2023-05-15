using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform GunBody = null; //물체가 나가는 부분
    [SerializeField] float turret_range = 0f; //터렛의 사정거리
    [SerializeField] LayerMask turret_LayerMask = 0; //특정마스크만 공격할수 있게
    [SerializeField] float spinSpeed = 0f; //적을 탐색했을 때 얼마나 빠르게 회전해서 겨눌지 
    [SerializeField] float fireRate = 0f; //연산속도
    float m_currentFireRate; //실제연산의 쓸 속도
    Transform Target = null; //공격할 대상


    Animator anim;
    public Transform ArrowPos;
    public GameObject Arrow;

    public GameObject ArrowMax;
    public TurretArrow turretArrowdamage;

    public AudioSource shotSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void SearchEnemy()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, turret_range, turret_LayerMask); //OverlapSphere : 객체 주변 콜라이더를 검출
        Transform t_shortestTarget = null; //터렛과 가장 가까운 거 찾기

        if(cols.Length > 0)
        {
            float t_shortestDistance = Mathf.Infinity; //Infinity : 무한 계속커진다.
            foreach (Collider t_colTarget in cols) //주변콜라이더를 t_colTarget으로 넘겨줌
            {
                float t_disfance = Vector3.SqrMagnitude(transform.position - t_colTarget.transform.position); //SqrMagnitude : 제곱반환
                if (t_shortestDistance > t_disfance)
                {
                    t_shortestDistance = t_disfance;
                    t_shortestTarget = t_colTarget.transform;
                }
            }
        }

        Target = t_shortestTarget;
    } 
    void Start()
    {
        m_currentFireRate = fireRate;
        InvokeRepeating("SearchEnemy", 0f, 0.5f); // 0.5초 마다 반복
    }

    // Update is called once per frame
    void Update()
    {   //타겟이 없으면 건바디의 회전을 계속 시켜준다. 그게 아니면 타겟으로 회전 시켜주고 발사 시킨다.
        if (Target == null) 
            anim.SetBool("IsShoot", false);
        else
        {
            Quaternion t_lookRotation = Quaternion.LookRotation(Target.position - transform.position); //LookRotation : 특정좌표를 바라보게 만드는 회전값을 리턴
            Vector3 t_euler = Quaternion.RotateTowards(GunBody.rotation, t_lookRotation, spinSpeed * Time.deltaTime).eulerAngles; //RotateTowards : a지점부터 b지점까지 c의 스피드로 회전

            GunBody.rotation = Quaternion.Euler(0, t_euler.y, 0);

            Quaternion t_fireRotation = Quaternion.Euler(0, t_lookRotation.eulerAngles.y, 0);



            if (Quaternion.Angle(GunBody.rotation, t_fireRotation) < 5f)
            {
                m_currentFireRate -= Time.deltaTime;
                if(m_currentFireRate <= 0)
                {
                    m_currentFireRate = fireRate;
                    anim.SetBool("IsShoot", true);
                    ArrowPos.rotation = Quaternion.LookRotation(Target.position - ArrowPos.transform.position);
                    
                    StartCoroutine("Shot");
                    shotSound.Play();
                    Debug.Log("발사 !!!");




                }

            }
        }
    }
    IEnumerator Shot() 
    {
        if (turretArrowdamage.damage >= 30)
        {
            GameObject intantArrow2 = Instantiate(ArrowMax, ArrowPos.position, ArrowPos.rotation);
            Rigidbody arrowRigid2 = intantArrow2.GetComponent<Rigidbody>();
            arrowRigid2.velocity = ArrowPos.forward * 30;
            yield return null;
        }
        else
        {
            GameObject intantArrow = Instantiate(Arrow, ArrowPos.position, ArrowPos.rotation);
            Rigidbody arrowRigid = intantArrow.GetComponent<Rigidbody>();
            arrowRigid.velocity = ArrowPos.forward * 30;
            yield return null;
        }
        
        
        
    }
}
