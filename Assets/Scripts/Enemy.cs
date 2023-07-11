using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public AIPath aiPath;

    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
            if(aiPath.desiredVelocity.x >= 0.01f){
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if(aiPath.desiredVelocity.x <= 0.01f){
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if(currentHealth <= 0){
            Die();
        }
    }

    void Die(){
        Debug.Log("Enemy died.");

        animator.SetTrigger("Death");

        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        this.enabled = false;
        StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy(){
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

}
