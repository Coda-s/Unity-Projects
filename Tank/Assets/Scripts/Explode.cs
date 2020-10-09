using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private float timeVal = 0.167f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeVal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
