using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wGirlPlayerControl : Player
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

        superCost = 20;
        ultraCost = 15;

        punchDamage = 5;
        kickDamage = 10;
        speed = 2.5f;

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
            } else
            {
                offset = new Vector3(-1.5f, 1.0f, 0f);
                updateMagic(-superCost);
                var poisonball = GameObject.Instantiate(super);
                poisonball.transform.position = pballPos + offset;
                poisonball.transform.rotation = transform.rotation;
                poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.T) && currMagic >= ultraCost && Time.timeScale == 1)
        {
            Vector3 myPos = transform.position;
            var player = GameObject.FindGameObjectWithTag("Enemy").transform;

            if(myPos.x < player.position.x)
            {
                updateMagic(-ultraCost);
                player.GetComponent<EnemyScript>().pushToWall(2);
            } else
            {
                updateMagic(-ultraCost);
                player.GetComponent<EnemyScript>().pushToWall(1);
            }
            /*
            Vector3 pballPos = transform.position;
            Vector3 offset = new Vector3(1.5f, 1.0f, 0f);

            updateMagic(-ultraCost);
            var poisonball = GameObject.Instantiate(ultra);
            poisonball.transform.position = pballPos + offset;
            poisonball.transform.rotation = transform.rotation;
            poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
            */
        }
    }
}
