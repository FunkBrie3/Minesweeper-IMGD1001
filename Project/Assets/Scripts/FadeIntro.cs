using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIntro : MonoBehaviour
{
    private float fadeTime = 1, holdTime = 2;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.timeSinceLevelLoad;
        if (0 < time && time <= fadeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / fadeTime);
        } else if (fadeTime < time && time <= fadeTime + holdTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        } else if (fadeTime + holdTime < time && time <= (2 * fadeTime) + holdTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Abs(1 - ((time - (fadeTime + holdTime)) / fadeTime)));
        } else if ((2 * fadeTime) + holdTime < time)
        {
            SceneManager.LoadScene("menu");
        }
    }
}
