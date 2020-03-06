using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    int health = 4;
    bool isHit = false, isAttacking = false, isDead = false;
    float speed = 1f;
    public float updateRate = 2f, nextWaypointDistance = 3f;
    public GameObject attackHitBox;
    public GameObject hpPotion;
    Transform target;
    EnemySpawn es;

    private Animator anim;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        es = GameObject.Find("GM").GetComponent<EnemySpawn>();
        target = GameObject.Find("player").transform;
        attackHitBox.SetActive(false);
    }

    void FixedUpdate()
    {
        float relativePos = transform.position.x - target.position.x;
        if (Mathf.Abs(relativePos) >= 1f && !isAttacking && !isHit && !isDead)
        {           
            anim.Play("skeleton_walk");
            if (relativePos < 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (relativePos > 0)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector3(-1, 1, 1);
            }            
        }
        else if(Mathf.Abs(relativePos) <= 1f && !isHit && !isDead && !isAttacking)
        {
            if (!isHit && !isDead && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        anim.Play("skeleton_attack");
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }

    IEnumerator TakeDamage(int damage)
    {
        isHit = true;
        anim.Play("skeleton_hit");
        Debug.Log("hp left: " + health);
        health -= damage;
        yield return new WaitForSeconds(.25f);
        isHit = false;
        Debug.Log("hp left: " + health);
        if (health <= 0)
        {
            isDead = true;
            anim.Play("skeleton_dead");
            FindObjectOfType<AudioManager>().Play("SkeleDeath");
            yield return new WaitForSeconds(.5f);
            es.SkeleDeath();
            if (Random.Range(0, 12) < 2)
            {
                Vector3 hpPosition = new Vector3(0, .35f, 0);
                Instantiate(hpPotion, gameObject.transform.position + hpPosition, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackPos/hitbox")
        {
            Debug.Log("enemy hit");
            FindObjectOfType<AudioManager>().Play("SkeleImpact");
            StartCoroutine(TakeDamage(2));
        }
    }
}

