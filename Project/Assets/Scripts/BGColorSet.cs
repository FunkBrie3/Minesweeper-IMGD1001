using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGColorSet : MonoBehaviour
{
    void Awake()
    {
        GameObject o = new GameObject("skybox");
        o.transform.localScale = Vector3.one * 2;
        o.transform.position = new Vector3(0, 0, 100);
        o.transform.parent = gameObject.transform;

        o.AddComponent<SpriteRenderer>();
        o.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/endVoid");
    }
}
