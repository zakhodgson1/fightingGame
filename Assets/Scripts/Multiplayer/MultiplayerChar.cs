using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerChar : MonoBehaviour
{
    public float maxHealth;
    public float maxMagic;
    public float superCost;
    public float ultraCost;
    public float punchDamage;
    public float kickDamage;
    public float speed;
    public float jumpPower;
    public float gravity;

    public Rigidbody rb;
    public Animator pAnimator;

    public GameObject super;
    public GameObject ultra;

    public Collider[] meleeHitboxes;

    public p1Health healthBar;
    public p1Magic magicBar;

    public float currHealth;
    public float currMagic;

    public bool blockB;
    public bool jumpBool;
    public bool leftMost;

    public bool playerNumDecided;
    public bool isP1;

    public float fireBallDamage = 40;
    public float poisonDPF = 1;
    public float snowballDamage = 10;
    public float snowBallSlow = 0.5f;
    public float iceBlockDamage = 20;

    float mapWidth = 7;
    float floor = 0.6f;
    float cieling = 5;

    public bool charged;

    // Start is called before the first frame update
    void Start()
    {

        blockB = false;
        jumpBool = false;

        playerNumDecided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isP1)
        {
            

        } else if(!isP1)
        {

        }
    }

    public void FixedUpdate()
    {

        var players = GameObject.FindGameObjectsWithTag("Player");

        foreach(var player in players)
        {
            if(player.transform == transform)
            {
                continue;
            } else
            {
                GameObject enemy = player;
                if (enemy.transform.position.x < transform.position.x)
                {
                    leftMost = false;
                    Quaternion target = Quaternion.Euler(0, -90, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
                }
                else if (enemy.transform.position.x > transform.position.x)
                {
                    leftMost = true;
                    Quaternion target = Quaternion.Euler(0, 90, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5.0f);
                }
            }
        }
   

        if(isP1)
        {
            Vector3 moveVector = rb.velocity;

            if (blockB == false && Input.GetKeyDown(KeyCode.E) && Time.timeScale == 1)
            {
                pAnimator.SetTrigger("Punch");
                StartCoroutine(launchMeleeAttack(meleeHitboxes[0], punchDamage));
                if (charged == true)
                {
                    charged = false;
                }
            }

            if (blockB == false && Input.GetKeyDown(KeyCode.R) && Time.timeScale == 1)
            {
                pAnimator.SetTrigger("Kick");
                StartCoroutine(launchMeleeAttack(meleeHitboxes[0], kickDamage));
                if (charged == true)
                {
                    charged = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.S) && blockB == false && Time.timeScale == 1)
            {
                pAnimator.SetBool("Blocking", true);
                blockB = true;
            }

            if (Input.GetKeyDown(KeyCode.W) && blockB == true && Time.timeScale == 1)
            {
                pAnimator.SetBool("Blocking", false);
                blockB = false;
            }


            if (blockB == false && Input.GetKeyDown(KeyCode.Q) && Time.timeScale == 1 && jumpBool == false)
            {
                moveVector.y += jumpPower;
                jumpBool = true;
            }
            else if (blockB == false && Time.timeScale == 1)
            {
                var xIn = Input.GetAxis("HorizontalKeys");
                moveVector.x = xIn * speed;
                moveVector.y -= gravity * Time.deltaTime;
            }
            else if (Time.timeScale == 1)
            {
                moveVector.x = 0;
                moveVector.y = -10;
            }
            else
            {
                moveVector = new Vector3(0.0f, 0.0f, 0.0f);
            }
            rb.velocity = moveVector;

        } else if(!isP1)
        {
            Vector3 moveVector = rb.velocity;

            if (blockB == false && Input.GetKeyDown(KeyCode.N) && Time.timeScale == 1)
            {
                pAnimator.SetTrigger("Punch");
                StartCoroutine(launchMeleeAttack(meleeHitboxes[0], punchDamage));
                if (charged == true)
                {
                    charged = false;
                }
            }

            if (blockB == false && Input.GetKeyDown(KeyCode.M) && Time.timeScale == 1)
            {
                pAnimator.SetTrigger("Kick");
                StartCoroutine(launchMeleeAttack(meleeHitboxes[0], kickDamage));
                if (charged == true)
                {
                    charged = false;
                }
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

            if (blockB == false && Input.GetKeyDown(KeyCode.Space) && Time.timeScale == 1 && jumpBool == false)
            {
                moveVector.y += jumpPower;
                jumpBool = true;
            }
            else if (blockB == false && Time.timeScale == 1)
            {
                var xIn = Input.GetAxis("Horizontal");
                moveVector.x = xIn * speed;
                moveVector.y -= gravity * Time.deltaTime;
            }
            else if (Time.timeScale == 1)
            {
                moveVector.x = 0;
                moveVector.y = -10;
            }
            else
            {
                moveVector = new Vector3(0.0f, 0.0f, 0.0f);
            }
            rb.velocity = moveVector;

        }
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

        if(collision.gameObject.tag == "iceBlock")
        {
            takeDamage(iceBlockDamage);
        }

        if (collision.gameObject.tag == "Snowball")
        {
            takeDamage(snowballDamage);
            Destroy(collision.gameObject);
            StartCoroutine(SlowDown(0.5f));
        }

        if(collision.gameObject.tag == "stunShot")
        {
            Destroy(collision.gameObject);
            StartCoroutine(SlowDown(0));
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
            c.GetComponent<MultiplayerChar>().takeDamage(damage);
        }
    }

    public IEnumerator SlowDown(float slowDown)
    {
        float origSpeed = speed;
        if (slowDown == 0)
        {
            speed = 0;
            yield return new WaitForSeconds(2.0f);
            speed = origSpeed;

        }
        else
        {
            speed *= slowDown;
            yield return new WaitForSeconds(1.5f);
            speed = origSpeed;
        }

    }

    public void takeDamage(float damage)
    {
        if (blockB == true)
        {
            damage = damage * 0.1f;
        }
        currHealth -= damage;
        healthBar.SetHealth(currHealth);
    }

    public void pushToWall(int wall)
    {
        if (wall == 1)
        {
            transform.position = new Vector3(-mapWidth, transform.position.y, transform.position.z);
        }
        else if (wall == 2)
        {
            transform.position = new Vector3(mapWidth, transform.position.y, transform.position.z);
        }
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
