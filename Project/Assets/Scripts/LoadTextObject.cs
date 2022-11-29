using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadTextObject : MonoBehaviour
{
    [SerializeField]
    private string name;

    int length;
    void Awake()
    {
        string fileread = Application.streamingAssetsPath + "/" + name + ".txt";
        List<string> fileLines = File.ReadAllLines(fileread).ToList();
        length = fileLines.Count;

        int i = 0;
        foreach(string s in fileLines)
        {
            GameObject obj = new GameObject(s);
            obj.transform.parent = this.gameObject.transform;
            obj.transform.localPosition = new Vector3(0, -i);
            obj.AddComponent<MeshRenderer>();
            obj.AddComponent<TextMesh>();
            TextMesh textMesh = obj.GetComponent<TextMesh>();
            textMesh.text = s;
            textMesh.characterSize = 0.1f;
            textMesh.fontSize = 50;
            //textMesh.anchor = TextAnchor.UpperCenter;
            i++;
        }
    }

    [SerializeField] private float scrollSpeed = 4;
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (transform.position.y < -4.5f + length)
                transform.Translate(new Vector3(0, scrollSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            if(transform.position.y > 4.5f)
                transform.Translate(new Vector3(0, -scrollSpeed * Time.deltaTime, 0));
        }
    }
}
