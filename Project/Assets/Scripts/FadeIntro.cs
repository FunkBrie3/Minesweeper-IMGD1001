using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIntro : MonoBehaviour
{
    private float fadeTime = 1, holdTime = 2;
    [SerializeField] private Sprite sprite2;
    bool secondSprite = false;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.timeSinceLevelLoad;
        float time2 = (2 * fadeTime) + holdTime;
        if (time2 < time && !secondSprite)
        {
            GetComponent<SpriteRenderer>().sprite = sprite2;
            secondSprite = true;
        }
        if (0 < time && time <= fadeTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time / fadeTime);
        } else if (fadeTime < time && time <= fadeTime + holdTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        } else if (fadeTime + holdTime < time && time <= (2 * fadeTime) + holdTime)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Abs(1 - ((time - (fadeTime + holdTime)) / fadeTime)));
        } else if (time2 < time && time <= fadeTime + time2)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (time - time2) / fadeTime);
        }
        else if (fadeTime + time2 < time && time <= fadeTime + holdTime + time2)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else if (fadeTime + holdTime + time2 < time && time <= (2 * fadeTime) + holdTime + time2)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Abs(1 - (((time - time2) - (fadeTime + holdTime)) / fadeTime)));
        }
        else if (2 * time2 < time && secondSprite)
        {
            SceneManager.LoadScene("menu");
        }
    }
}
