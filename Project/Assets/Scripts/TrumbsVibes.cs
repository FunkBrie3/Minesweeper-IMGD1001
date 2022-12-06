using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrumbsVibes : MonoBehaviour
{
    [SerializeField] private bool isTheme;

    void Update()
    {
        if(isTheme) {
            switch(GlobalVars.themeSet) {
                case GlobalVars.ThemeSet.Clay:
                    gameObject.transform.position = new Vector3(
                        transform.position.x, 0.75f, transform.position.z
                    );
                    return;
                case GlobalVars.ThemeSet.Ores:
                    gameObject.transform.position = new Vector3(
                        transform.position.x, 3.75f, transform.position.z
                    );
                    return;
                case GlobalVars.ThemeSet.Logs:
                    gameObject.transform.position = new Vector3(
                        transform.position.x, 2.25f, transform.position.z
                    );
                    return;
            }
        } else {
            switch(GlobalVars.width) {
                case 16:
                    gameObject.transform.position = new Vector3(
                        transform.position.x, 0.75f, transform.position.z
                    );
                    return;
                case 8:
                    gameObject.transform.position = new Vector3(
                        transform.position.x, 3.75f, transform.position.z
                    );
                    return;
                case 12:
                    gameObject.transform.position = new Vector3(
                        transform.position.x, 2.25f, transform.position.z
                    );
                    return;
            }
        }
    }
}
