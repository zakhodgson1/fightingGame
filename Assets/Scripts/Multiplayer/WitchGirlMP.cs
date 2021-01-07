using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchGirlMP : MultiplayerChar
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pAnimator = GetComponent<Animator>();

        punchDamage = 10;
        kickDamage = 15;
        superCost = 15;
        ultraCost = 15;

        speed = 2.5f;
        jumpPower = 7;
        gravity = 1;
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
                leftMost = true;
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
                leftMost = false;
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
                Vector3 pballPos = transform.position;
                Vector3 offset;
                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var poisonball = GameObject.Instantiate(super);
                    poisonball.transform.position = pballPos + offset;
                    poisonball.transform.rotation = transform.rotation;
                    poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var poisonball = GameObject.Instantiate(super);
                    poisonball.transform.position = pballPos + offset;
                    poisonball.transform.rotation = transform.rotation;
                    poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
                }
            }

            if (Input.GetKeyDown(KeyCode.G) && currMagic >= ultraCost && Time.timeScale == 1)
            {
                Vector3 myPos = transform.position;
                
                var players = GameObject.FindGameObjectsWithTag("Player");
                GameObject enemy;

                foreach(var player in players)
                {
                    if(player.transform == transform)
                    {
                        continue;
                    } else
                    {
                        enemy = player;
                        if (myPos.x < enemy.transform.position.x)
                        {
                            updateMagic(-ultraCost);
                            enemy.GetComponent<MultiplayerChar>().pushToWall(2);
                        }
                        else
                        {
                            updateMagic(-ultraCost);
                            enemy.GetComponent<MultiplayerChar>().pushToWall(1);
                        }
                    }
                }
            }
        } else if(!isP1)
        {
            if (Input.GetKeyDown(KeyCode.K) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 pballPos = transform.position;
                Vector3 offset;
                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var poisonball = GameObject.Instantiate(super);
                    poisonball.transform.position = pballPos + offset;
                    poisonball.transform.rotation = transform.rotation;
                    poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var poisonball = GameObject.Instantiate(super);
                    poisonball.transform.position = pballPos + offset;
                    poisonball.transform.rotation = transform.rotation;
                    poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
                }
            }

            if (Input.GetKeyDown(KeyCode.L) && currMagic >= ultraCost && Time.timeScale == 1)
            {
                Vector3 myPos = transform.position;
                var players = GameObject.FindGameObjectsWithTag("Player");
                GameObject enemy;

                foreach (var player in players)
                {
                    if (player.transform == transform)
                    {
                        continue;
                    }
                    else
                    {
                        enemy = player;
                        if (myPos.x < enemy.transform.position.x)
                        {
                            updateMagic(-ultraCost);
                            enemy.GetComponent<MultiplayerChar>().pushToWall(2);
                        }
                        else
                        {
                            updateMagic(-ultraCost);
                            enemy.GetComponent<MultiplayerChar>().pushToWall(1);
                        }
                    }
                }
            }
        }
        
    }
}
