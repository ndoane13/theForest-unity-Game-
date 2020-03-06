using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    float random;
    Transform spawn;
    public int skeleCount = 0;
    public Transform spawnLeft;
    public Transform spawnRight;
    public GameObject skeleton;
    //public GameObject greater;
    
    public void SpawnSkeleton(){
        random = Random.value;
        if(random == 1f){
            spawn = spawnLeft;
        }
        else
        {
            spawn = spawnRight;
        }
        if (skeleCount < 4)
        {
            skeleCount++;
            Instantiate(skeleton, spawn.position, Quaternion.identity);
        }
    }

    public void SkeleDeath()
    {
        skeleCount--;
    }

/*    public void SpawnGreater(){
        random = Random.value;
        if(Mathf.Approximately(random, 0f)){
            spawn = spawnLeft;
        }
        else
        {
            spawn = spawnRight;
        }
        Instantiate(greater, spawn.position, Quaternion.identity);
    }*/

}