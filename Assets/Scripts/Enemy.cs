using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator animator;        
        
    public Score score;
    public AIPath aiPath;
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform enemyGPX;    

    public int maxHealth = 100;
    int currentHealth;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rb;

    public Attack npcAttack;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        npcAttack = this.GetComponent<Attack>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();        
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        if(this.tag == "Bandit")
            InvokeRepeating("callAttackNPC",1f, 1f);
        if(this.tag == "Mage")
            InvokeRepeating("callAttackNPC",1f, 3f);
        if(this.tag == "Brute")
            InvokeRepeating("callAttackNPC",1f, 5f);
    }

    void callAttackNPC(){
        npcAttack.AttackNPC();
        animator.SetTrigger("Attack");
    }

    void OnPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWaypoint = 0;
        }
    }

    void UpdatePath(){
        if(seeker.IsDone()){
            seeker.StartPath(rb.position, target.position, OnPathComplete);
            
        }
        animator.SetInteger("AnimState", 2);

    }
    void FixedUpdate()
    {
        if(path == null)
            return;
        
        if(currentWaypoint >= path.vectorPath.Count){
            reachedEndOfPath = true;
            return;
        } else{
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);        

        if(distance < nextWaypointDistance){
            currentWaypoint++;
        }

        if(force.x >= 0.01f){
                enemyGPX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if(force.x <= 0.01f){
                enemyGPX.localScale = new Vector3(1f, 1f, 1f);
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
        
        if(this.tag == "Bandit"){
            score.addScore(5);
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EnemyAI>().enabled = false;        
        }

        if(this.tag == "Mage")
            score.addScore(10);

        if(this.tag == "Brute")
            score.addScore(50);

        this.enabled = false;
        StartCoroutine(DestroyEnemy());
    }

    IEnumerator DestroyEnemy(){
        if(this.tag == "Bandit")
            yield return new WaitForSeconds(1.5f);
        else if(this.tag == "Mage")
            yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }

}
