using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizeClickDetect : MonoBehaviour
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
                    case "buttonOres":
                        GlobalVars.themeSet = GlobalVars.ThemeSet.Ores;
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonClay":
                        GlobalVars.themeSet = GlobalVars.ThemeSet.Clay;
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonLogs":
                        GlobalVars.themeSet = GlobalVars.ThemeSet.Logs;
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonSmall":
                        GlobalVars.width = 8;
                        GlobalVars.height = 8;
                        GlobalVars.mineCount = 10;
                        GlobalVars.cameraSize = 5;
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonMedium":
                        GlobalVars.width = 12;
                        GlobalVars.height = 12;
                        GlobalVars.mineCount = 22;
                        GlobalVars.cameraSize = 7.5f;
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                    case "buttonLarge":
                        GlobalVars.width = 16;
                        GlobalVars.height = 16;
                        GlobalVars.mineCount = 40;
                        GlobalVars.cameraSize = 10;
                        AudioManager.Play(AudioManager.AudioType.Click);
                        return;
                }
            }

        }
    }
}
