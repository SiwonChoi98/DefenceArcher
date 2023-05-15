using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public float moveSpeed; //텍스트 이동속도
    public float alphaSpeed; //투명도 변환속도
    public float destroyTime; //삭제시간
    TextMeshPro text;
    Color alpha;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime); //destroyTime 을 2초로 설정해줌 

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed) ; //color.a 값에 따라 투명도가 변한다 0에가까울수록 투명 //Mathf.Lerp(a,b,t) t값에 의해서 a와 b사이의 값을 반환한다. ex) a=0, b=10, t=0.1일때 반환값 0.1
        text.color = alpha; //text.color 값을 alpha 값으로 초기화 함
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
