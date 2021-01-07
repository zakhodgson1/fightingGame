using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyroMP : MultiplayerChar
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pAnimator = GetComponent<Animator>();

        punchDamage = 15;
        kickDamage = 20;
        superCost = 20;
        ultraCost = 20;

        speed = 4;
        jumpPower = 8;
        gravity = 3;
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

        if (isP1)
        {
            if (Input.GetKeyDown(KeyCode.F) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 fballPos = transform.position;
                Vector3 offset;
                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var fireball = GameObject.Instantiate(super);
                    fireball.transform.position = fballPos + offset;
                    fireball.transform.rotation = transform.rotation;
                    fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 3;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var fireball = GameObject.Instantiate(super);
                    fireball.transform.position = fballPos + offset;
                    fireball.transform.rotation = transform.rotation;
                    fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 3;
                }
            }

            if (Input.GetKeyDown(KeyCode.G) && currMagic >= ultraCost && Time.timeScale == 1 && currHealth < 130)
            {
                healthBar.SetHealth(currHealth + 20);
                updateMagic(-ultraCost);
            }

        }
        else if (!isP1)
        {
            if (Input.GetKeyDown(KeyCode.K) && currMagic >= superCost && Time.timeScale == 1)
            {
                Vector3 fballPos = transform.position;
                Vector3 offset;
                if (leftMost == true)
                {
                    offset = new Vector3(1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var fireball = GameObject.Instantiate(super);
                    fireball.transform.position = fballPos + offset;
                    fireball.transform.rotation = transform.rotation;
                    fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 3;
                }
                else
                {
                    offset = new Vector3(-1.5f, 1.0f, 0f);
                    updateMagic(-superCost);
                    var fireball = GameObject.Instantiate(super);
                    fireball.transform.position = fballPos + offset;
                    fireball.transform.rotation = transform.rotation;
                    fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 3;
                }
            }

            if (Input.GetKeyDown(KeyCode.L) && currMagic >= ultraCost && Time.timeScale == 1 && currHealth < 130)
            {
                healthBar.SetHealth(currHealth + 20);
                updateMagic(-ultraCost);
            }
        }

    }
}
