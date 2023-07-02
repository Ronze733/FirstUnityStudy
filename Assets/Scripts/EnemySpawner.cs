using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float[] arrPosX = { -2.2f, -0.75f, 0.75f, 2.2f };

    [SerializeField]
    private float spawnTimer;
    [SerializeField]
    private float firstSpawnTime;

    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private GameObject boss;

    [SerializeField]
    private GameObject background1;
    [SerializeField]
    private GameObject background2;


    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine"); // multiThread 활용을 위해
    }

    public void stopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(firstSpawnTime);

        float moveSpeed = 5f;
        int minEnemyIndex = 0;
        int maxEnemyIndex = 1;
        int i = 0;
        int bossCount = 0;

        while (true)
        { 
            foreach(float posX in arrPosX)
            {
                if(minEnemyIndex >= 6)
                    minEnemyIndex = 6;

                if(maxEnemyIndex >= 7)
                    maxEnemyIndex = 7;

                int index = Random.Range(minEnemyIndex, maxEnemyIndex);
                spawnEnemy(posX, index, moveSpeed);
            }

            i++;


            if(i % 5 == 0)
            {
                maxEnemyIndex++;
            }

            if(i % 10 == 0)
            {
                minEnemyIndex++;
                maxEnemyIndex--;
            }

            if(maxEnemyIndex >= enemies.Length && bossCount < 1) {
                SpawnBoss();
                bossCount++;
                i = 0;
                minEnemyIndex = 0;
                maxEnemyIndex = 1;
                moveSpeed = 5f;
            }

            if(i % 6 == 0)
            {
                moveSpeed += 2;
                if (moveSpeed >= 13)
                {
                    moveSpeed = 13;
                }

                Background b1 = background1.GetComponent<Background>();
                Background b2 = background2.GetComponent<Background>();

                b1.setMoveSpeed(moveSpeed);
                b2.setMoveSpeed(moveSpeed);
            }

            yield return new WaitForSeconds(spawnTimer);
        }
    }

    void spawnEnemy(float posX, int i, float moveSpeed)
    {
        if(Random.Range(0, 10) == 0)
        {
            i += 1;
        }

        if(i >= enemies.Length)
            i = enemies.Length - 1;

        Vector3 spawnPos = new Vector3(posX, transform.position.y, 0f);
        GameObject enemyObject = Instantiate(enemies[i], spawnPos, Quaternion.identity);

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.setMoveSpeed(moveSpeed);
    }

    void SpawnBoss()
    {
        Vector3 spawnPos = new Vector3(0, transform.position.y, 0f);
        Instantiate(boss, spawnPos, Quaternion.identity);
    }

}
