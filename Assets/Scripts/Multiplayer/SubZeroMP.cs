using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubZeroMP : MultiplayerChar
{
    public Transform[] spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pAnimator = GetComponent<Animator>();

        spawnPoints = GameObject.FindGameObjectWithTag("blockSpawner").GetComponent<blockSpawnerScript>().spawnPoints;

        punchDamage = 12;
        kickDamage = 18;
        superCost = 10;
        ultraCost = 20;

        speed = 4;
        jumpPower = 10;
        gravity = 2;
    }

    // Update is called once per frame
    void Update()
    {
        while (playerNumDecided == false)
        {
            if (transform.position.x < 0)
            {
                isP1 = true;
                playerNumDecided = true;
                healthBar = GameObject.FindGameObjectWithTag("p1Health").GetComponent<p1Health>();
                magicBar = GameObject.FindGameObjectWithTag("p1Magic").GetComponent<p1Magic>();
                maxHealth = 150;
                maxMagic = 100;
                healthBar.SetMaxHealth(maxHealth);
                magicBar.SetMaxMagic(maxMagic);
                currHealth = maxHealth;
                healthBar.SetHealth(currHealth);
                currMagic = maxMagic;
                magicBar.SetMagic(currMagic);
                InvokeRepeating("gradualMagic", 0.0f, 1.0f);
            }
            else if (transform.position.x > 0)
            {
                isP1 = false;
                playerNumDecided = true;
                healthBar = GameObject.FindGameObjectWithTag("p2Health").GetComponent<p1Health>();
                magicBar = GameObject.FindGameObjectWithTag("p2Magic").GetComponent<p1Magic>();
                maxHealth = 150;
                maxMagic = 100;
                healthBar.SetMaxHealth(maxHealth);
                magicBar.SetMaxMagic(maxMagic);
                currHealth = maxHealth;
                healthBar.SetHealth(currHealth);
                currMagic = maxMagic;
                magicBar.SetMagic(currMagic);
                InvokeRepeating("gradualMagic", 0.0f, 1.0f);
            }
        }

        if(isP1)
        {
            if (Input.GetKeyDown(KeyCode.F) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 sballPos = transform.position;
                Vector3 offset;

                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var snowball = GameObject.Instantiate(super);
                    snowball.transform.position = sballPos + offset;
                    snowball.transform.rotation = transform.rotation;
                    snowball.GetComponent<Rigidbody>().velocity = snowball.transform.forward * 5;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var snowball = GameObject.Instantiate(super);
                    snowball.transform.position = sballPos + offset;
                    snowball.transform.rotation = transform.rotation;
                    snowball.GetComponent<Rigidbody>().velocity = snowball.transform.forward * 5;
                }
            }
                if (Input.GetKeyDown(KeyCode.G) && currMagic >= ultraCost && Time.timeScale == 1)
                {
                    Debug.Log("Hello");
                    if (leftMost == true)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (spawnPoints[i].position.x > transform.position.x)
                            {
                                var iceBlock = GameObject.Instantiate(ultra);
                                iceBlock.transform.position = spawnPoints[i].position;
                                Vector3 angle = new Vector3(1, 0.0f, 0.0f);
                                iceBlock.GetComponent<Rigidbody>().velocity = angle;
                            }
                        }
                        updateMagic(-ultraCost);

                    }
                    else
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (spawnPoints[i].position.x < transform.position.x)
                            {
                                var iceBlock = GameObject.Instantiate(ultra);
                                iceBlock.transform.position = spawnPoints[i].position;
                                Vector3 angle = new Vector3(-1, 0.0f, 0.0f);
                                iceBlock.GetComponent<Rigidbody>().velocity = angle;
                            }
                        }
                        updateMagic(-ultraCost);
                    }
                }
        } else if(!isP1)
        {
            if (Input.GetKeyDown(KeyCode.K) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 sballPos = transform.position;
                Vector3 offset;

                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var snowball = GameObject.Instantiate(super);
                    snowball.transform.position = sballPos + offset;
                    snowball.transform.rotation = transform.rotation;
                    snowball.GetComponent<Rigidbody>().velocity = snowball.transform.forward * 5;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var snowball = GameObject.Instantiate(super);
                    snowball.transform.position = sballPos + offset;
                    snowball.transform.rotation = transform.rotation;
                    snowball.GetComponent<Rigidbody>().velocity = snowball.transform.forward * 5;
                }
            }
            if (Input.GetKeyDown(KeyCode.L) && currMagic >= ultraCost && Time.timeScale == 1)
            {
                if (leftMost == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (spawnPoints[i].position.x > transform.position.x)
                        {
                            var iceBlock = GameObject.Instantiate(ultra);
                            iceBlock.transform.position = spawnPoints[i].position;
                            Vector3 angle = new Vector3(1, 0.0f, 0.0f);
                            iceBlock.GetComponent<Rigidbody>().velocity = angle;
                        }
                    }
                    updateMagic(-ultraCost);

                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (spawnPoints[i].position.x < transform.position.x)
                        {
                            var iceBlock = GameObject.Instantiate(ultra);
                            iceBlock.transform.position = spawnPoints[i].position;
                            Vector3 angle = new Vector3(-1, 0.0f, 0.0f);
                            iceBlock.GetComponent<Rigidbody>().velocity = angle;
                        }
                    }
                    updateMagic(-ultraCost);
                }
            }
        }


    }
}
