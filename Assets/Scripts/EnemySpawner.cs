using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    public Transform[] spawnPoints;
    public GameObject enemy;
 
    public float startSpawnTime = 0f;
    public float spawnTime = 5f;

    bool esquerda = false; 
 
    // Use this for initialization
    void Start () {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating ("Spawn", startSpawnTime, spawnTime);
    }
 
    // Update is called once per frame
    void Update () {
     
    }
 
    void Spawn () {
        // Find a random index between zero and one less than the number of spawn points.
        if(esquerda == false){
            // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            Instantiate(this.enemy,this.spawnPoints[0].position, this.spawnPoints[0].rotation);
            esquerda = true;
        }else if(esquerda == true){
            Instantiate(this.enemy,this.spawnPoints[1].position, this.spawnPoints[1].rotation);
            esquerda = false;
        }   
 
    }
}
