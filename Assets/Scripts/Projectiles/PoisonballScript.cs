using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonballScript : MonoBehaviour
{
    public ParticleSystem poisonCloud;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground")
        {
            var cloud = Instantiate(poisonCloud);
            cloud.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
