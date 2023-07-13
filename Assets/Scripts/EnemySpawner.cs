using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public Transform[] spawnPoints;
    public GameObject[] enemy;
 
    public int difficultyTimer = 1;
    public float startSpawnTime = 0f;
    public float spawnTime = 5f;

    bool alternateMeleeSpawn = false; //Used for alternating spawn location of enemies
    bool alternateMageSpawn = false;
    bool alternateBruteSpawn = false;
 
    // Use this for initialization
    void Start () {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("SpawnBandit", startSpawnTime, spawnTime);
        InvokeRepeating ("SpawnMage", startSpawnTime + 10f, spawnTime + 5f);
        InvokeRepeating ("SpawnBrute", startSpawnTime + 20f, spawnTime + 10f);
        InvokeRepeating ("difficultyUp", startSpawnTime + 30f, spawnTime + 30f);
    }
 
    // Update is called once per frame
    void Update () {
     
    }

    void difficultyUp(int difficulty){
        switch(difficulty){
            case 1:
                SpawnBandit();
                break;
            case 2:
                SpawnMage();
                break;
            case 3:
                SpawnBandit();
                SpawnMage();
                break;
            case 4:
                SpawnBrute();
                break;
            case 5:
                SpawnBandit();
                SpawnBrute();
                break;
            case 6:
                SpawnMage();
                SpawnBrute();
                break;
            case 7:
                SpawnBandit();
                SpawnMage();
                SpawnBrute();
                break;
        }
        difficulty++;
    }
 
    void SpawnBandit () {
        // Find a random index between zero and one less than the number of spawn points.
        if(alternateMeleeSpawn == false){
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(this.enemy[0],this.spawnPoints[0].position, this.spawnPoints[0].rotation);
            alternateMeleeSpawn = true;
        }else if(alternateMeleeSpawn == true){
            Instantiate(this.enemy[0],this.spawnPoints[1].position, this.spawnPoints[1].rotation);
            alternateMeleeSpawn = false;
        }           
    }

    void SpawnMage () {
        // Find a random index between zero and one less than the number of spawn points.
        if(alternateMageSpawn == false){
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(this.enemy[1],this.spawnPoints[2].position, this.spawnPoints[2].rotation);
            alternateMageSpawn = true;
        }else if(alternateMageSpawn == true){
            Instantiate(this.enemy[1],this.spawnPoints[3].position, this.spawnPoints[3].rotation);
            alternateMageSpawn = false;
        }   
    }

    void SpawnBrute () {
        // Find a random index between zero and one less than the number of spawn points.
        if(alternateBruteSpawn == false){
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(this.enemy[2],this.spawnPoints[0].position, this.spawnPoints[0].rotation);
            alternateBruteSpawn = true;
        }else if(alternateBruteSpawn == true){
            Instantiate(this.enemy[2],this.spawnPoints[1].position, this.spawnPoints[1].rotation);
            alternateBruteSpawn = false;
        }   
    }
}
