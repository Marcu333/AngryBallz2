using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionChild : MonoBehaviour
{
    Transform host;

    // Start is called before the first frame update
    void Start()
    {
        host = transform.parent;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = host.position;
    }
}
