using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStart : MonoBehaviour
{
    public RectTransform helpUi;
    public AudioSource startSound;
    public AudioSource helpSound;
    public AudioSource battleSound;

    void Awake()
    {
        battleSound.Play();
    }
    public void Startgame()
    {
        startSound.Play();
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    public void Help()
    {
        helpSound.Play();
        helpUi.anchoredPosition = new Vector3(0, 0, 0); //ui를 정중앙에 오게만든다.
    }
    public void HelpExit() //퇴장
    {
        helpSound.Play();
        helpUi.anchoredPosition = Vector3.down * 700;
    }
}
