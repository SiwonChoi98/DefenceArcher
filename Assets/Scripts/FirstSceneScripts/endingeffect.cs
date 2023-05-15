using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class endingeffect : MonoBehaviour
{
    public GameManager manager;
    public Text tx;
    
    
    
    public GameObject mTextObject;
    

   

    public AudioSource typingSound;
    private string mText = "Defence Archer Clear";
    
   

    // Start is called before the first frame update
    void Start()
    {

            StartCoroutine(typing());
    }
   void update()
    {
     
    }
    IEnumerator typing() //1스테이지 텍스트 연출
    {
        //yield return new WaitForSeconds(2f);
        for(int i = 0; i <= mText.Length; i++)
        {
            typingSound.Play();
            tx.text = mText.Substring(0,i); //0번째부터 ~째까지
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1.5f);
        
        
       
    }
    
}
