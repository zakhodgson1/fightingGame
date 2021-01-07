using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huntressPlayerControl : Player
{
    bool charged = false;
    int chargeMultiplier = 3;
    float baseCSD = 20;
    float bonusCSD = 0;
    int bonusDPF = 1;
    float maxBonusDamage = 80;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pAnimator = GetComponent<Animator>();

        healthBar = GameObject.FindGameObjectWithTag("p1Health").GetComponent<p1Health>();
        magicBar = GameObject.FindGameObjectWithTag("p1Magic").GetComponent<p1Magic>();

        blockB = false;
        jumpBool = false;

        maxHealth = 150;
        maxMagic = 100;

        superCost = 10;
        ultraCost = 25;

        punchDamage = 10;
        kickDamage = 15;
        speed = 5;

        jumpPower = 10;
        gravity = 2;

        healthBar.SetMaxHealth(maxHealth);
        magicBar.SetMaxMagic(maxMagic);

        currHealth = maxHealth;
        healthBar.SetHealth(currHealth);
        currMagic = maxMagic;
        magicBar.SetMagic(currMagic);


        InvokeRepeating("gradualMagic", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (blockB == false && Input.GetKeyDown(KeyCode.Q) && Time.timeScale == 1)
        {
            pAnimator.SetTrigger("Punch");
            if(charged == false)
            {
                StartCoroutine(launchMeleeAttack(meleeHitboxes[0], punchDamage));
            } else
            {
                StartCoroutine(launchMeleeAttack(meleeHitboxes[0], punchDamage * chargeMultiplier));
                charged = false;
            }
            //launchMeleeAttack(meleeHitboxes[0], 5);
        }

        if (blockB == false && Input.GetKeyDown(KeyCode.E) && Time.timeScale == 1)
        {
            pAnimator.SetTrigger("Kick");
            if (charged == false)
            {
                StartCoroutine(launchMeleeAttack(meleeHitboxes[1], kickDamage));
            } else
            {
                StartCoroutine(launchMeleeAttack(meleeHitboxes[1], kickDamage * chargeMultiplier));
                charged = false;
            }
            
            //launchMeleeAttack(meleeHitboxes[1], 10);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && blockB == false && Time.timeScale == 1)
        {
            pAnimator.SetBool("Blocking", true);
            blockB = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && blockB == true && Time.timeScale == 1)
        {
            pAnimator.SetBool("Blocking", false);
            blockB = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && currMagic >= superCost && Time.timeScale == 1)
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

        if (Input.GetKeyDown(KeyCode.T) && currMagic >= ultraCost && Time.timeScale == 1)
        {
            charged = true;
            updateMagic(-ultraCost);
        }
    }
}
