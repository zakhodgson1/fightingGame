using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloudScipt : MonoBehaviour
{
    float delay = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
