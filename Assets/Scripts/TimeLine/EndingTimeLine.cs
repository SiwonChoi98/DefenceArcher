using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class EndingTimeLine : MonoBehaviour
{
    public PlayableDirector playableDirector;
    // Start is called before the first frame update

   

    public void OpenEnding()
    {
        playableDirector.gameObject.SetActive(true);
        playableDirector.Play();
    }
}
