using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EnemyAI
{

    Vector3 myPos;
    Transform player;

    float maxRange = 4.0f;
    float meleeRange = 1.5f;

    int punchDamage = 10;
    int kickDamage = 15;

    bool meleeAttacked;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eAnimator = GetComponent<Animator>();
        theMasterBrain = GetComponent<NavMeshAgent>();

        healthBar = GameObject.FindGameObjectWithTag("enemyHealth").GetComponent<enemyHealth>();
        magicBar = GameObject.FindGameObjectWithTag("enemyMagic").GetComponent<enemyMagic>();

        blockB = false;
        meleeAttacked = false;

        gravity = 15;

        maxHealth = 150;
        maxMagic = 100;

        healthBar.SetMaxHealth(maxHealth);
        magicBar.SetMaxMagic(maxMagic);

        currHealth = maxHealth;
        healthBar.SetHealth(currHealth);
        currMagic = maxMagic;
        magicBar.SetMagic(maxMagic);

        InvokeRepeating("gradualMagic", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] projectiles = findGameObjectsWithLayer(11);

        if(projectiles == null)
        {
            eAnimator.SetBool("Blocking", false);
            blockB = false;
        }

        
        float blockRand = Random.Range(0.0f, 1.0f);
        float rand = Random.Range(0.0f, 1.0f);
        myPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        float distanceBetween = Vector3.Distance(myPos, player.position);

        if(distanceBetween > maxRange)
        {
            theMasterBrain.destination = player.position;
            if (projectiles != null)
            {

                if (blockRand < 0.5f)
                {
                    eAnimator.SetBool("Blocking", true);
                    blockB = true;
                }
            }
        } else if(distanceBetween < maxRange && distanceBetween > meleeRange)
        {
            if(meleeAttacked == true)
            {
                meleeAttacked = false;
            }
            if(blockB == true && projectiles == null)
            {
                eAnimator.SetBool("Blocking", false);
                blockB = false;
            }
            if (projectiles != null)
            {

                if (blockRand < 0.5f)
                {
                    eAnimator.SetBool("Blocking", true);
                    blockB = true;
                }
            }
        }
        
        else if(distanceBetween < meleeRange)
        {
            if(rand < 0.5f)
            {
                if(meleeAttacked == false)
                {
                    eAnimator.SetTrigger("Punch");
                    StartCoroutine(launchMeleeAttack(meleeHitboxes[0], punchDamage));
                    meleeAttacked = true;
                } else
                {
                    eAnimator.SetBool("Blocking", true);
                    blockB = true;
                }
                
            } else
            {
                if(meleeAttacked == false)
                {
                    eAnimator.SetTrigger("Kick");
                    StartCoroutine(launchMeleeAttack(meleeHitboxes[1], kickDamage));
                    meleeAttacked = true;
                }
                else
                {
                    eAnimator.SetBool("Blocking", true);
                    blockB = true;
                }
            }
                
            
        }
    }

    public void pushToWall(int wall)
    {
     
        if (wall == 1)
        {
            transform.position = new Vector3(-mapWidth, transform.position.y, transform.position.z);
        } else if(wall == 2)
        {
            transform.position = new Vector3(mapWidth, transform.position.y, transform.position.z);
        }
    }

    GameObject[] findGameObjectsWithLayer(int layer) {

        GameObject[] gameos = GameObject.FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();

        for (var i = 0; i< gameos.Length; i++) {
         if (gameos[i].layer == layer) {
             goList.Add(gameos[i]);
         }
        }
    if (goList.Count == 0)
    {
    return null;
    }
    return goList.ToArray();
    }

    

}
