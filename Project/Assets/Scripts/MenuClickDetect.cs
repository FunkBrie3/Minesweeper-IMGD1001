using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClickDetect : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                switch (hit.collider.gameObject.name)
                {
                    case "buttonPlay":
                        SceneManager.LoadScene("game");
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonCredits":
                        SceneManager.LoadScene("credits");
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonHistory":
                        SceneManager.LoadScene("version");
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonCustomize":
                        SceneManager.LoadScene("customize");
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonQuit":
                        Application.Quit();
                        return;
                }
            }

        }
    }
}
