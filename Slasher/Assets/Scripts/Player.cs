using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float health;
    float startHealth = 5;
    public static bool isHit = false, isDead = false;
    public static bool isAttacking = false;

    Animator anim;
    public Transform groundCheck;
    public GameObject attackHitBox;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = startHealth;
        attackHitBox.SetActive(false);
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking && !isDead && !PauseMenu.isPaused)
        {
            isAttacking = true;

            if (!Controller.isGrounded)
            {
                FindObjectOfType<AudioManager>().Play("Sword2");
                FindObjectOfType<AudioManager>().Play("PlayerGrunt");
                anim.Play("jump_attack");
                StartCoroutine(DoAttack(.25f));
            }
            else
            {
                anim.Play("attack1");
                FindObjectOfType<AudioManager>().Play("PlayerGrunt");
                FindObjectOfType<AudioManager>().Play("Sword1");
                StartCoroutine(DoAttack(.25f));
            }
        }
    }

    IEnumerator DoAttack(float delay)
    {
        yield return new WaitForSeconds(.1f);
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(delay);
        attackHitBox.SetActive(false);
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "attackHitBox" && !isHit && !isDead)
        {
            Debug.Log("player hit");
            StartCoroutine(TakeDamage(1));
        }
    }

    IEnumerator TakeDamage(int damage)
    {
        isHit = true;
        anim.Play("player_hurt");
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        health -= damage;
        yield return new WaitForSeconds(.25f);
        isHit = false;
        if(health <= 0)
        {
            isDead = true;
            anim.Play("player_death");
            yield return new WaitForSeconds(.25f);
            anim.Play("player_dead");
            gameObject.GetComponent<Controller>().enabled = false;

        }
    }

    public void Heal()
    {
        health = startHealth;
    }
}
