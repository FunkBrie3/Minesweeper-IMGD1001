using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    float winTimer = 0;
    float rx, ry, rz;
    void Awake() {
        rx = Random.Range(-2.5f, 2.5f);
        ry = Random.Range(-2.5f, 2.5f);
    }
    void Update() {
        winTimer += Time.deltaTime;
        transform.Translate(new Vector3(rx * Time.deltaTime, ry * Time.deltaTime, 0));
        if(winTimer >= 2) Destroy(this.gameObject);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Abs((winTimer / 2) - 1));
    }
}
