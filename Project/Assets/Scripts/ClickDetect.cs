using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickDetect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit != null && hit.collider != null)
            {
                switch (hit.collider.gameObject.name)
                {
                    case "buttonPlay":
                        SceneManager.LoadScene("game");
                        return;
                    case "buttonCredits":
                        SceneManager.LoadScene("game");
                        return;
                    case "buttonHistory":
                        SceneManager.LoadScene("game");
                        return;
                    case "buttonQuit":
                        Application.Quit();
                        return;
                }
            }

        }
    }
}
