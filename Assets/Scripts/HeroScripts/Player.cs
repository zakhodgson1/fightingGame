using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float punchDamage;
    public float kickDamage;

    public GameObject super;
    public GameObject ultra;

    public int superCost;
    public int ultraCost;

    public float maxHealth;
    public float maxMagic;
    public float currHealth;
    public float currMagic;

    public bool blockB;
    public bool jumpBool;
    public bool leftMost;

    public int jumpPower;
    public int gravity;

    public p1Health healthBar;
    public p1Magic magicBar;

    public Rigidbody rb;
    public Animator pAnimator;

    public Collider[] meleeHitboxes;

    public float fireBallDamage = 40;
    public float poisonDPF = 1;
    public float snowballDamage = 10;
    public float snowBallSlow = 0.5f;

    int mapWidth = 7;
    float floor = 0.6f;
    float cieling = 5;

    // Start is called before the first frame update
    void Start()
    {
        leftMost = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {


        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if(enemy.transform.position.x < transform.position.x)
        {
            leftMost = false;
            Quaternion target = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        } else if(enemy.transform.position.x > transform.position.x)
        {
            leftMost = true;
            Quaternion target = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        }


        Vector3 moveVector = rb.velocity;

       
       if (blockB == false && Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1 && jumpBool == false)
       {
            moveVector.y += jumpPower;
            jumpBool = true;
       } else if (blockB == false && Time.timeScale == 1)
       {
            var xIn = Input.GetAxis("Horizontal");
            moveVector.x = xIn * speed;
            moveVector.y -= gravity * Time.deltaTime;
       }
       else if(Time.timeScale == 1)
       {
            moveVector.x = 0;
            moveVector.y = -10;
       } else
        {
            moveVector = new Vector3(0.0f, 0.0f, 0.0f);
        }
        rb.velocity = moveVector;
 
    }

    public void LateUpdate()
    {
        Vector3 playerPos = transform.position;

        playerPos.x = Mathf.Clamp(transform.position.x, -mapWidth, mapWidth);
        playerPos.y = Mathf.Clamp(transform.position.y, floor, cieling);
        transform.position = playerPos;
    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground" && jumpBool == true)
        {
            jumpBool = false;
        }

        if (collision.gameObject.tag == "Fireball")
        {
            takeDamage(fireBallDamage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Snowball")
        {
            takeDamage(snowballDamage);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Poison")
        {
            takeDamage(poisonDPF);
        }
    }

    public IEnumerator launchMeleeAttack(Collider col, float damage)
    {
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Player"));
        yield return new WaitForSeconds(0.5f);
        foreach (Collider c in cols)
        {
            /*
            if(c.transform.parent.parent == transform)
            {
                continue;
            }
            */
            c.GetComponent<EnemyScript>().takeDamage(damage);
        }
    }

    public void takeDamage(float damage)
    {
        if(blockB == true)
        {
            damage = damage * 0.1f;
        }
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }

    public void updateMagic(float magicDif)
    {
        currMagic += magicDif;
        magicBar.SetMagic(currMagic);
    }

    public void gradualMagic()
    {
        if (currMagic < maxMagic)
        {
            currMagic += 1;
            magicBar.SetMagic(currMagic);
        }
    }
}
