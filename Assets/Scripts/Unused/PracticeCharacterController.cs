using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PracticeCharacterController : MonoBehaviour
{

    public GameObject fireBallPfb;
    public GameObject poisonBallPfb;

    public p1Health healthBar;
    public p1Magic magicBar;

    Rigidbody rb;

    Animator plAnimator;

    

    int maxHealth = 150;
    int currentHealth;
    int maxMagic = 100;
    int currentMagic;

    int fireBallCost = 15;
    int fireBallDamage = 40;

    int poisonBallCost = 10;
    int poisonDPF = 1;

    bool blockingB;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("p1Health").GetComponent<p1Health>();
        magicBar = GameObject.FindGameObjectWithTag("p1Magic").GetComponent<p1Magic>();

        healthBar.SetMaxHealth(maxHealth);
        magicBar.SetMaxMagic(maxMagic);

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
        currentMagic = maxMagic;
        magicBar.SetMagic(maxMagic);

        rb = GetComponent<Rigidbody>();
        plAnimator = GetComponent<Animator>();
        blockingB = false;

        InvokeRepeating("gradualMagic", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(blockingB == false && Input.GetKeyDown(KeyCode.A) && Time.timeScale == 1)
        {
            plAnimator.SetTrigger("Punch");
        }

        if (blockingB == false && Input.GetKeyDown(KeyCode.S) && Time.timeScale == 1)
        {
            plAnimator.SetTrigger("Kick");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && blockingB == false && Time.timeScale == 1)
        {
            plAnimator.SetBool("Blocking", true);
            blockingB = true;
        }
        
        if(Input.GetKeyDown(KeyCode.UpArrow) && blockingB == true && Time.timeScale == 1)
        {
            plAnimator.SetBool("Blocking", false);
            blockingB = false;
        }

        if(Input.GetKeyDown(KeyCode.R) && currentMagic >= fireBallCost && Time.timeScale == 1)
        {
            Vector3 fballPos = transform.position;
            Vector3 offset = new Vector3(1.0f, 1.0f, 0f);

            updateMagic(-fireBallCost);
            var fireball = GameObject.Instantiate(fireBallPfb);
            fireball.transform.position = fballPos + offset;
            fireball.transform.rotation = transform.rotation;
            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * 5;
        }

        if (Input.GetKeyDown(KeyCode.P) && currentMagic >= poisonBallCost && Time.timeScale == 1)
        {
            Vector3 pballPos = transform.position;
            Vector3 offset = new Vector3(1.5f, 1.0f, 0f);

            updateMagic(-poisonBallCost);
            var poisonball = GameObject.Instantiate(poisonBallPfb);
            poisonball.transform.position = pballPos + offset;
            poisonball.transform.rotation = transform.rotation;
            poisonball.GetComponent<Rigidbody>().velocity = poisonball.transform.forward * 4;
        }
    }

    private void FixedUpdate()
    {
        if (blockingB == false)
        {
            var xIn = Input.GetAxis("Horizontal");
            rb.velocity = new Vector3(xIn * 5, 0, 0);
        }
    }



    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Fireball")
        {
            takeDamage(fireBallDamage);
            Destroy(collision.gameObject);
        }
    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    void updateMagic(int magicDif)
    {
        currentMagic += magicDif;
        magicBar.SetMagic(currentMagic);
    }

    void gradualMagic()
    {
        if (currentMagic < maxMagic)
        {
            currentMagic += 1;
            magicBar.SetMagic(currentMagic);
        }
    }
}
