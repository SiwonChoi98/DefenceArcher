using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShop : MonoBehaviour
{
    public RectTransform uiGroup;
    PlayerController enterplayer;
    public GameManager manager;
    public int[] itemPrice; //아이템가격배열

    public Skill1 skill1;
    public Skill2 skill2;
    public Skill3 skill3;
    public Skill4 skill4;
    // Start is called before the first frame update
    public void Enter(PlayerController player) //입장
    {
        enterplayer = player;
        uiGroup.anchoredPosition = new Vector3(798, -527, 0); //ui를 정중앙에 오게만든다.

    }
    public void Exit() //퇴장
    {
        uiGroup.anchoredPosition = Vector3.down * 1800;
    }

    public void Buy(int index) //아이템구입
    {
        int price = itemPrice[index];
        if (price > manager.money) // 아이템 가격보다 플레이어 돈이 더 없을 때 리턴
        {
            return;

        }
        //라이트닝 샷
        if (index == 0)
        {
            manager.money -= price;
            skill1.damage += 10;
        }
        //아이스 에로우 
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
        //파워 샷
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
        //드래곤즈
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
