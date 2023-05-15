using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DieScene : MonoBehaviour
{
    public AudioSource clickSound;
    // Start is called before the first frame update
    public void FirstScene()
    {
        clickSound.Play();
        SceneManager.LoadScene("FirstScene");
    }
}
