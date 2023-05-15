using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{
    public float moveSpeed; //�ؽ�Ʈ �̵��ӵ�
    public float alphaSpeed; //���� ��ȯ�ӵ�
    public float destroyTime; //�����ð�
    TextMeshPro text;
    Color alpha;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;
        Invoke("DestroyObject", destroyTime); //destroyTime �� 2�ʷ� �������� 

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed) ; //color.a ���� ���� ������ ���Ѵ� 0���������� ���� //Mathf.Lerp(a,b,t) t���� ���ؼ� a�� b������ ���� ��ȯ�Ѵ�. ex) a=0, b=10, t=0.1�϶� ��ȯ�� 0.1
        text.color = alpha; //text.color ���� alpha ������ �ʱ�ȭ ��
    }
    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
