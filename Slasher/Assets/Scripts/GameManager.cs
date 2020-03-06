using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float spawnTime, countDown = 5f;
    bool isSpawningSkele = false;
    EnemySpawn enemySpawn;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isSpawningSkele){
            spawnTime = Random.Range(1f, 4f);
            StartCoroutine(SpawnSkele(spawnTime));
        }

        if (Player.isDead)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                countDown = 5f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }

/*        if(greaterCount < 2 && isSpawningGreater == false){
            spawnTime = Random.Range(3f, 6f);
            StartCoroutine(SpawnGreat(spawnTime));
        }*/
    }

    IEnumerator SpawnSkele(float time){
        isSpawningSkele = true;
        yield return new WaitForSeconds(time);
        enemySpawn.SpawnSkeleton();
        isSpawningSkele = false;
    }



/*    IEnumerator SpawnGreat(float time){
        isSpawningGreater = true;
        yield return new WaitForSeconds(time);
        enemySpawn.SpawnGreater();
        greaterCount++;
        isSpawningGreater = false;
    }*/
}
