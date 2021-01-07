using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressMP : MultiplayerChar
{
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pAnimator = GetComponent<Animator>();

        charged = false;

        punchDamage = 8;
        kickDamage = 15;
        superCost = 10;
        ultraCost = 25;

        speed = 6;
        jumpPower = 12;
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

        if(charged == true)
        {
            kickDamage = 30;
            punchDamage = 20;
        } else if(charged == false)
        {
            kickDamage = 15;
            punchDamage = 8;
        }

        if (isP1)
        {
            if (Input.GetKeyDown(KeyCode.F) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 sLaserPos = transform.position;
                Vector3 offset;
                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var sLaser = GameObject.Instantiate(super);
                    sLaser.transform.position = sLaserPos + offset;
                    sLaser.transform.rotation = transform.rotation;
                    sLaser.GetComponent<Rigidbody>().velocity = sLaser.transform.forward * 6;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var sLaser = GameObject.Instantiate(super);
                    sLaser.transform.position = sLaserPos + offset;
                    sLaser.transform.rotation = transform.rotation;
                    sLaser.GetComponent<Rigidbody>().velocity = sLaser.transform.forward * 6;
                }
            }

            if (Input.GetKeyDown(KeyCode.G) && currMagic >= ultraCost && Time.timeScale == 1)
            {
                charged = true;
                updateMagic(-ultraCost);
            }

        }
        else if (!isP1)
        {
            if (Input.GetKeyDown(KeyCode.K) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 sLaserPos = transform.position;
                Vector3 offset;
                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var sLaser = GameObject.Instantiate(super);
                    sLaser.transform.position = sLaserPos + offset;
                    sLaser.transform.rotation = transform.rotation;
                    sLaser.GetComponent<Rigidbody>().velocity = sLaser.transform.forward * 6;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var sLaser = GameObject.Instantiate(super);
                    sLaser.transform.position = sLaserPos + offset;
                    sLaser.transform.rotation = transform.rotation;
                    sLaser.GetComponent<Rigidbody>().velocity = sLaser.transform.forward * 6;
                }
            }

            if (Input.GetKeyDown(KeyCode.L) && currMagic >= ultraCost && Time.timeScale == 1)
            {
                charged = true;
                updateMagic(-ultraCost);
            }
        }

    }
}
