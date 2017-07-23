using UnityEngine;
using UnityEngine.Networking;

public class EnemyManager : NetworkBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public override void OnStartServer()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


 /*   void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }
    */
/*
    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
    */

    void Spawn()
    {/*
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }*/

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        GameObject enemies = (GameObject) Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        NetworkServer.Spawn(enemies);
    }
}
