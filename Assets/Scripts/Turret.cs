using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform GunBody = null; //��ü�� ������ �κ�
    [SerializeField] float turret_range = 0f; //�ͷ��� �����Ÿ�
    [SerializeField] LayerMask turret_LayerMask = 0; //Ư������ũ�� �����Ҽ� �ְ�
    [SerializeField] float spinSpeed = 0f; //���� Ž������ �� �󸶳� ������ ȸ���ؼ� �ܴ��� 
    [SerializeField] float fireRate = 0f; //����ӵ�
    float m_currentFireRate; //���������� �� �ӵ�
    Transform Target = null; //������ ���


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
        Collider[] cols = Physics.OverlapSphere(transform.position, turret_range, turret_LayerMask); //OverlapSphere : ��ü �ֺ� �ݶ��̴��� ����
        Transform t_shortestTarget = null; //�ͷ��� ���� ����� �� ã��

        if(cols.Length > 0)
        {
            float t_shortestDistance = Mathf.Infinity; //Infinity : ���� ���Ŀ����.
            foreach (Collider t_colTarget in cols) //�ֺ��ݶ��̴��� t_colTarget���� �Ѱ���
            {
                float t_disfance = Vector3.SqrMagnitude(transform.position - t_colTarget.transform.position); //SqrMagnitude : ������ȯ
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
        InvokeRepeating("SearchEnemy", 0f, 0.5f); // 0.5�� ���� �ݺ�
    }

    // Update is called once per frame
    void Update()
    {   //Ÿ���� ������ �ǹٵ��� ȸ���� ��� �����ش�. �װ� �ƴϸ� Ÿ������ ȸ�� �����ְ� �߻� ��Ų��.
        if (Target == null) 
            anim.SetBool("IsShoot", false);
        else
        {
            Quaternion t_lookRotation = Quaternion.LookRotation(Target.position - transform.position); //LookRotation : Ư����ǥ�� �ٶ󺸰� ����� ȸ������ ����
            Vector3 t_euler = Quaternion.RotateTowards(GunBody.rotation, t_lookRotation, spinSpeed * Time.deltaTime).eulerAngles; //RotateTowards : a�������� b�������� c�� ���ǵ�� ȸ��

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
                    Debug.Log("�߻� !!!");




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
