using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowBallScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 7.0f || transform.position.x < -7.0f)
        {
            Destroy(gameObject);
        }
    }
}
