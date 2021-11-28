using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public Vector3 offset;
void Update ()
    {
        gameObject.transform.position = GameObject.Find("Player").transform.position + offset;
    }
}
