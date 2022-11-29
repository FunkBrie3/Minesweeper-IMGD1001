using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{
    float explodeTimer = 0;
    int i = 0;
    float speed = 0.05f;
    void Update() {
        explodeTimer += Time.deltaTime;
        while(explodeTimer / speed >= 1) {
            explodeTimer -= speed;
            i++;
            if(i >= 16) Destroy(this.gameObject);
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Sprite/explode/explosion_{i}");
        }
    }
}
