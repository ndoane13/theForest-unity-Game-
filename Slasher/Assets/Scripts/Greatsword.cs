using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greatsword : MonoBehaviour
{
    int health = 10;
    float speed = 1.5f;
    bool isHit = false, isAttacking = false, isDead = false;

    public Transform target;
    private Animator anim;
    private Rigidbody2D rb;
    public GameObject attackHitBox;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackHitBox.SetActive(false);
    }

    private void FixedUpdate()
    {
        float relativePos = transform.position.x - target.position.x;
        if (Mathf.Abs(relativePos) >= 1f && !isAttacking && !isHit && !isDead)
        {
            anim.Play("walk");
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
        else if (Mathf.Abs(relativePos) <= 1f && !isHit && !isDead && !isAttacking)
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
        anim.Play("attack1");
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.35f);
        attackHitBox.SetActive(false);
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "AttackPos/hitbox" && !isHit)
        {
            Debug.Log("enemy hit");
            StartCoroutine(TakeDamage(2));
        }
    }

    IEnumerator TakeDamage(int damage)
    {
        isHit = true;
        anim.Play("hit");
        Debug.Log("hp left: " + health);
        health -= damage;
        yield return new WaitForSeconds(.25f);
        isHit = false;
        Debug.Log("hp left: " + health);
        if (health <= 0)
        {
            anim.Play("dead");
            yield return new WaitForSeconds(.5f);
            Destroy(gameObject);
        }
    }
}
