using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enemyHealth healthBar;
    public enemyMagic magicBar;

    public Rigidbody rb;
    public Animator eAnimator;
    public NavMeshAgent theMasterBrain;

    public Collider[] meleeHitboxes;

    public float maxHealth;
    public float currHealth;
    public float maxMagic;
    public float currMagic;

    public bool blockB;
    public bool jumpBool;
    public bool leftMost;

    public int gravity;

    int fireBallDamage = 40;
    int snowBallDamage = 10;
    float snowSlowDown = 0.5f;
    float stunShotSlow = 0.0f;
    int poisonDPF = 1;
    int iceBlockDamage = 25;

    public int mapWidth = 7;

    float floor = 0.6f;
    float cieling = 5;

    float speed = 2.5f;



    // Start is called before the first frame update
    void Start()
    {
        leftMost = false;
        theMasterBrain.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void FixedUpdate()
    {
        Debug.Log(rb.velocity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.position.x < transform.position.x)
        {
            leftMost = false;
            Quaternion target = Quaternion.Euler(0, -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        }
        else if (player.transform.position.x > transform.position.x)
        {
            leftMost = true;
            Quaternion target = Quaternion.Euler(0, 90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
        }

        Vector3 moveVector = rb.velocity;

        if(blockB == false && Time.timeScale == 1)
        {
            moveVector.y = -1;
        }

        if(blockB == true)
        {
            theMasterBrain.isStopped = true;
        } else if(blockB == false && theMasterBrain.isStopped == true)
        {
            theMasterBrain.isStopped = false;
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
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
            takeDamage(snowBallDamage);
            StartCoroutine(SlowDown(snowSlowDown));
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "stunShot")
        {
            StartCoroutine(SlowDown(stunShotSlow));
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "iceBlock")
        {
            takeDamage(iceBlockDamage);
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

    public void takeDamage(float damage)
    {
        if(blockB == true)
        {
            damage = damage * 0.1f;
        }
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }

    void updateMagic(float magicDif)
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


    public IEnumerator SlowDown(float slowDown)
    {
        if(slowDown == 0)
        {
            if(leftMost == true)
            {
                Vector3 leftWallPos = new Vector3(-mapWidth, transform.position.y, transform.position.z);
                theMasterBrain.SetDestination(leftWallPos);
                theMasterBrain.speed *= slowDown;
                yield return new WaitForSeconds(2.0f);
                theMasterBrain.speed = speed;
            } else
            {
                Vector3 rightWallPos = new Vector3(mapWidth, transform.position.y, transform.position.z);
                theMasterBrain.SetDestination(rightWallPos);
                theMasterBrain.speed *= slowDown;
                yield return new WaitForSeconds(2.0f);
                theMasterBrain.speed = speed;
            }
  
        } else
        {
            theMasterBrain.speed *= slowDown;
            yield return new WaitForSeconds(1.5f);
            theMasterBrain.speed = speed;
        }
        
    }

    public IEnumerator launchMeleeAttack(Collider col, float damage)
    {
        var cols = Physics.OverlapBox(col.bounds.center, col.bounds.extents, col.transform.rotation, LayerMask.GetMask("Player"));
        Debug.Log("Hello");
        yield return new WaitForSeconds(0.5f);
        foreach (Collider c in cols)
        {
            Debug.Log(c.name);
            /*
            if(c.transform.parent.parent == transform)
            {
                continue;
            }
            */
            c.GetComponent<Player>().takeDamage(damage);
            break;
        }
    }
}
