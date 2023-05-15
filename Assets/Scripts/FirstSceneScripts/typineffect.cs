using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class typineffect : MonoBehaviour
{
    public GameManager manager;
    public Text tx;
    public Text tx2;
    public Text tx3;
    public Text tx4;
    public Text tx5;
    public Text tx6;
    public Text tx7;

    public GameObject mTextObject;
    public GameObject m2TextObject;
    public GameObject m3TextObject;
    public GameObject m4TextObject;
    public GameObject m5TextObject;
    public GameObject m6TextObject;
    public GameObject m7TextObject;



    public GameObject playerImage;

    public AudioSource typingSound;

    private string mText = "���ù��� ��翩...���� �߰Ŷ�!";
    private string mText2 = "����.. ���Ⱑ �����..�� ��ݱ��� ��Ʃ�� ���� �־��µ�..? \n�� �ʰ� Ȱ�� �� ����?! ";
    private string mText3 = "�ڳ״� ���� �θ��� �ް� �� ����. ���� �� ������߰ھ�..! \n�� �������� ����κ��� �� �������� �����ְԳ�!";
    private string mText4 = "��..? ���� �ֿ�..? �� �׷� ���� ���� ����� ���л��ϻ��ε�...";
    private string mText5 = "�װ� ��������! �ڳװ� ������ �����ĳ����ٸ� ���� �������״�..! \n���� ���ƿ� �������� �����ֽð�!";
    private string mText6 = "�˰ڽ��ϴ�..! ���� ���ƿ��ž� �ؿ�!!";
    private string mText7 = "��������!! 20���������� �����ֽð�^^";

    // Start is called before the first frame update
    void Start()
    {
        if(manager.stage <= 20)
        {
            
            StartCoroutine(typing());
        }
        

    }
   void update()
    {
     
    }
    IEnumerator typing() //1�������� �ؽ�Ʈ ����
    {
       
        yield return new WaitForSeconds(1f);
        mTextObject.SetActive(true);
        for (int i = 0; i <= mText.Length; i++)
        {
            typingSound.Play();
            tx.text = mText.Substring(0,i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);
            
        }
        yield return new WaitForSeconds(2f);
        mTextObject.SetActive(false);
        playerImage.SetActive(true);
        m2TextObject.SetActive(true);
        for (int i = 0; i <= mText2.Length; i++)
        {
            typingSound.Play();
            tx2.text = mText2.Substring(0, i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m2TextObject.SetActive(false);
        m3TextObject.SetActive(true);
        for (int i = 0; i <= mText3.Length; i++)
        {
            typingSound.Play();
            tx3.text = mText3.Substring(0, i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m3TextObject.SetActive(false);
        m4TextObject.SetActive(true);
        for (int i = 0; i <= mText4.Length; i++)
        {
            typingSound.Play();
            tx4.text = mText4.Substring(0, i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m4TextObject.SetActive(false);
        m5TextObject.SetActive(true);
        for (int i = 0; i <= mText5.Length; i++)
        {
            typingSound.Play();
            tx5.text = mText5.Substring(0, i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m5TextObject.SetActive(false);
        m6TextObject.SetActive(true);
        for (int i = 0; i <= mText6.Length; i++)
        {
            typingSound.Play();
            tx6.text = mText6.Substring(0, i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);

        }
        yield return new WaitForSeconds(1f);
        m6TextObject.SetActive(false);
        m7TextObject.SetActive(true);
        for (int i = 0; i <= mText7.Length; i++)
        {
            typingSound.Play();
            tx7.text = mText7.Substring(0, i); //0��°���� ~°����
            yield return new WaitForSeconds(0.17f);
        }
    }
    
}
