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

    private string mText = "선택받은 용사여...눈을 뜨거라!";
    private string mText2 = "으으.. 여기가 어디지..난 방금까지 유튜브 보고 있었는데..? \n이 옷과 활은 또 뭐야?! ";
    private string mText3 = "자네는 나의 부름을 받고 온 용사다. 나를 좀 도와줘야겠어..! \n저 몰려오는 적들로부터 내 보물들을 지켜주게나!";
    private string mText4 = "네..? 제가 왜요..? 전 그럴 힘도 없고 평범한 대학생일뿐인데...";
    private string mText5 = "그건 걱정말게! 자네가 적들을 물리쳐나간다면 점점 강해질테니..! \n내가 돌아올 때까지만 버텨주시게!";
    private string mText6 = "알겠습니다..! 빨리 돌아오셔야 해요!!";
    private string mText7 = "물론이지!! 20스테이지만 버텨주시게^^";

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
    IEnumerator typing() //1스테이지 텍스트 연출
    {
       
        yield return new WaitForSeconds(1f);
        mTextObject.SetActive(true);
        for (int i = 0; i <= mText.Length; i++)
        {
            typingSound.Play();
            tx.text = mText.Substring(0,i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);
            
        }
        yield return new WaitForSeconds(2f);
        mTextObject.SetActive(false);
        playerImage.SetActive(true);
        m2TextObject.SetActive(true);
        for (int i = 0; i <= mText2.Length; i++)
        {
            typingSound.Play();
            tx2.text = mText2.Substring(0, i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m2TextObject.SetActive(false);
        m3TextObject.SetActive(true);
        for (int i = 0; i <= mText3.Length; i++)
        {
            typingSound.Play();
            tx3.text = mText3.Substring(0, i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m3TextObject.SetActive(false);
        m4TextObject.SetActive(true);
        for (int i = 0; i <= mText4.Length; i++)
        {
            typingSound.Play();
            tx4.text = mText4.Substring(0, i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m4TextObject.SetActive(false);
        m5TextObject.SetActive(true);
        for (int i = 0; i <= mText5.Length; i++)
        {
            typingSound.Play();
            tx5.text = mText5.Substring(0, i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);
        }
        yield return new WaitForSeconds(1f);
        m5TextObject.SetActive(false);
        m6TextObject.SetActive(true);
        for (int i = 0; i <= mText6.Length; i++)
        {
            typingSound.Play();
            tx6.text = mText6.Substring(0, i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);

        }
        yield return new WaitForSeconds(1f);
        m6TextObject.SetActive(false);
        m7TextObject.SetActive(true);
        for (int i = 0; i <= mText7.Length; i++)
        {
            typingSound.Play();
            tx7.text = mText7.Substring(0, i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.17f);
        }
    }
    
}
