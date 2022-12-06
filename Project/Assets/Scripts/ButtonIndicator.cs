using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonIndicator : MonoBehaviour
{
    [SerializeField] private Sprite text;
    [SerializeField] private Sprite textL;
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null && hit.collider.gameObject.name.Equals(this.gameObject.name))
        {
            GetComponent<SpriteRenderer>().sprite = textL;
        } else {
            GetComponent<SpriteRenderer>().sprite = text;
        }
    }
}
