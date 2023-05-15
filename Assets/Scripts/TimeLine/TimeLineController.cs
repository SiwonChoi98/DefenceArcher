using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimeLineController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public PlayableDirector playableDirector2;
    public PlayableDirector playableDirector3;
    public PlayableDirector playableDirectorBoss;
    public GameManager manager;
    
    public void OpenSkill1()
    {
        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();
    }
    public void OpenSkill2()
    {
        playableDirector2.gameObject.SetActive(true);
        playableDirector2.Play();
    }
    public void OpenSkill3()
    {
        playableDirector3.gameObject.SetActive(true);
        playableDirector3.Play();
    }
    public void BossZone()
    {
        playableDirectorBoss.gameObject.SetActive(true);
        playableDirectorBoss.Play();
    }

}
