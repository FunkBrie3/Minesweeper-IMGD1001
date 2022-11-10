using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGColorSet : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Camera>().backgroundColor = GlobalVars.bgcolor;
    }
}
