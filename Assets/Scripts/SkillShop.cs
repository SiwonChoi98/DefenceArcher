using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShop : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerController enterplayer;
    public GameManager manager;
    public int[] itemPrice; //�����۰��ݹ迭

    public Skill1 skill1;
    public Skill2 skill2;
    public Skill3 skill3;
    public Skill4 skill4;
    // Start is called before the first frame update
    public void Enter(PlayerController player) //����
    {
        enterplayer = player;
        uiGroup.anchoredPosition = new Vector3(798, -527, 0); //ui�� ���߾ӿ� ���Ը����.

    }
    public void Exit() //����
    {
        uiGroup.anchoredPosition = Vector3.down * 1800;
    }

    public void Buy(int index) //�����۱���
    {
        int price = itemPrice[index];
        if (price > manager.money) // ������ ���ݺ��� �÷��̾� ���� �� ���� �� ����
        {
            return;

        }
        //����Ʈ�� ��
        if (index == 0)
        {
            manager.money -= price;
            skill1.damage += 10;
        }
        //���̽� ���ο� 
        if (index == 1)
        {
            if (manager.stage >= 6)
            {
                manager.money -= price;
                skill2.damage += 10;
            }
            else
            {
                return;
            }
        }
        //�Ŀ� ��
        if (index == 2)
        {
            if (manager.stage >= 11)
            {
                manager.money -= price;
                skill3.damage += 10;
            }
            else
            {
                return;
            }
        }
        //�巡����
        if (index == 3)
        {
            if(manager.stage >= 16)
            {
                manager.money -= price;
                skill4.damage += 30;
            }
            else
            {
                return;
            }
        }



    }
}
