using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSkill : MonoBehaviour
{
    
    public RectTransform skill1Help;
    public RectTransform skill2Help;
    public RectTransform skill3Help;
    public RectTransform skill4Help;
    // Start is called before the first frame update
    public void Skill1() //1����
    {
        skill1Help.anchoredPosition = new Vector3(1098, 433, 0);
       
    }
    public void Skill2() //2����
    {
        skill2Help.anchoredPosition = new Vector3(1098, 433, 0);

    }
    public void Skill3() //3����
    {
        skill3Help.anchoredPosition = new Vector3(1098, 433, 0);

    }
    public void Skill4() //4����
    {
        skill4Help.anchoredPosition = new Vector3(1098, 433, 0);

    }


    // Update is called once per frame
    public void Exit() //����
    {
        skill1Help.anchoredPosition = Vector3.down * 1300;
        skill2Help.anchoredPosition = Vector3.down * 1300;
        skill3Help.anchoredPosition = Vector3.down * 1300;
        skill4Help.anchoredPosition = Vector3.down * 1300;
    }
}
