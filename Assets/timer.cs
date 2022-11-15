using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class timer : MonoBehaviour
{
    private TextMeshPro textmeshPro;
    private DateTime initialTime;
    // Start is called before the first frame update
    void Start()
    {
        textmeshPro = GetComponent<TextMeshPro>();
        initialTime = System.DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        textmeshPro.SetText((System.DateTime.Now - initialTime).ToString().Substring(3, 8));
        print(textmeshPro.text);
    }
}
