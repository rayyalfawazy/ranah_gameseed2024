using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashLoad : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(CompleteSplash());
    }

    private IEnumerator CompleteSplash()
    {
        yield return new WaitForSeconds(15.5f);
        SceneManager.LoadScene("Main Menu");
    }
}
