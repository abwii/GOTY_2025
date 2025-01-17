using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCoordinator : MonoBehaviour
{
    public float timeDelay = 4;
    public float timeDelayMin = 0.5f;
    public GameObject enemyPreFab;
    public GameObject[] spawnPoints = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void SpawnEnemy()
    {
        float randX = Random.Range(0f, 1f);
        float randZ = Random.Range(0f, 1f);
        float randPosX = this.spawnPoints[1].transform.position.x - this.spawnPoints[0].transform.position.x;
        float randPosZ = this.spawnPoints[2].transform.position.z - this.spawnPoints[0].transform.position.z
        ;
        randPosX = randPosX * randX;
        randPosZ = randPosZ * randZ;
        Vector3 randPos = new(randPosX, 0, randPosZ);

        randPos = this.spawnPoints[0].transform.position + randPos;

        // Vector3 randPos = this.spawnPoints[1].transform.position - this.spawnPoints[0].transform.position;
        // randPos = randPos * rand;
        // randPos = this.spawnPoints[0].transform.position + randPos;
        Instantiate(this.enemyPreFab, randPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnRoutine(){
        while(true){
            SpawnEnemy();
            yield return new WaitForSeconds(this.timeDelay);
            if (this.timeDelay > this.timeDelayMin) this.timeDelay -= 0.1f;
        }
        // yield return new WaitForSeconds(this.timeDelay);
        // SpawnEnemy();
        // SpawnRoutine();
    }
}
