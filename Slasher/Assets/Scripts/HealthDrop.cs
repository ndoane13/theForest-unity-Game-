using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    Player player;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "player")
        {
            player = GameObject.Find("player").GetComponent<Player>();
            player.Heal();
            FindObjectOfType<AudioManager>().Play("HealthPickUp");
            Destroy(gameObject);
        }
    }
}
