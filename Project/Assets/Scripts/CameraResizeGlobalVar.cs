using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizeGlobalVar : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Camera>().orthographicSize = GlobalVars.cameraSize;
    }
}
