using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //스킬 쿨타임 위해서 넣음
public class Abilities : MonoBehaviour
{
    public PlayerController player;

    [Header("skill Fire")]
    public Image abiltyImage1;
    public float cooldown1 = 5;
    public bool isCooldown = false;
    public KeyCode ability1;

    [Header("skill Ice")]
    public Image abiltyImage2;
    public float cooldown2 = 5;
    public bool isCooldown2 = false;
    public KeyCode ability2;

    [Header("skill Light")]
    public Image abiltyImage3;
    public float cooldown3 = 7;
    public bool isCooldown3 = false;
    public KeyCode ability3;

    [Header("skill Air")]
    public Image abiltyImage4;
    public float cooldown4 = 10;
    public bool isCooldown4 = false;
    public KeyCode ability4;
    // Start is called before the first frame update
    void Start()
    {
        abiltyImage1.fillAmount = 0;
        abiltyImage2.fillAmount = 0;
        abiltyImage3.fillAmount = 0;
        abiltyImage4.fillAmount = 0;
    }


    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
        Ability4();
    }
    void Ability1()
    {
        if (Input.GetKey(ability1) && isCooldown == false)
        {
            isCooldown = true;
            abiltyImage1.fillAmount = 1; //fillAmount 가 1 이면 다 보이게 되는 방식
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
