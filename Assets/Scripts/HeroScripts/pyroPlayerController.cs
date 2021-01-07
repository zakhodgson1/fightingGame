using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pyroPlayerController : Player
{

    // Start is called before the first frame update
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

        superCost = 15;
        ultraCost = 30;

        punchDamage = 5;
        kickDamage = 10;
        speed = 3;

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
            StartCoroutine(launchMeleeAttack(meleeHitboxes[0], punchDamage));
            //launchMeleeAttack(meleeHitboxes[0], 5);
        }

        if (blockB == false && Input.GetKeyDown(KeyCode.E) && Time.timeScale == 1)
        {
            pAnimator.SetTrigger("Kick");
            StartCoroutine(launchMeleeAttack(meleeHitboxes[0], kickDamage));
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
            Vector3 fballPos = transform.position;
            Vector3 offset;
            if(leftMost == true)
            {
                offset = new Vector3(1.5f, 1.0f, 0f);
                updateMagic(-superCost);
                var fireball = GameObject.Instantiate(super);
                fireball.transform.position = fballPos + offset;
                fireball.transform.rotation = transform.rotation;
                fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 3;
            } else
            {
                offset = new Vector3(-1.5f, 1.0f, 0f);
                updateMagic(-superCost);
                var fireball = GameObject.Instantiate(super);
                fireball.transform.position = fballPos + offset;
                fireball.transform.rotation = transform.rotation;
                fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 3;
            }

        }

        if (Input.GetKeyDown(KeyCode.T) && currMagic >= ultraCost && Time.timeScale == 1 && currHealth < 130)
        {

            healthBar.SetHealth(currHealth + 20);
            updateMagic(-ultraCost);
        }
    }


}
