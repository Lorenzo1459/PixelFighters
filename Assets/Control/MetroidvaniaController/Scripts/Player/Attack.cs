using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Attack : MonoBehaviour
{
	public string rpgClass;
	public float dmgValue = 4;
	public GameObject throwableObject;
	public Transform attackCheck;
	private Rigidbody2D m_Rigidbody2D;
	public Animator animator;
	public bool canAttack = true;
	public bool isTimeToCheck = false;

	public GameObject cam;
	public PhotonView view;
	public Transform attackPoint;
	public float attackRange = 0.5f;
	public LayerMask enemyLayers;
	public int attackDamage = 40;
	public float attackRate = 2f;
	float nextAttackTime = 0f;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Start is called before the first frame update
	void Start()
    {
        view = GetComponent<PhotonView>();
    }

	void AttackMelee(){
		//animator.SetTrigger
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("We hit" + enemy.name);
			enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
		}
	}	

	public void AttackNPC(){
		//animator.SetTrigger
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

		foreach(Collider2D enemy in hitEnemies){
			Debug.Log("We hit" + enemy.name);
			enemy.GetComponent<CharacterController2D>().TakeDamage(attackDamage, transform.position);
		}
	}

	void OnDrawGizmosSelected()
	{
		if(attackPoint == null)
			return;
		
		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}

    // Update is called once per frame
    void Update()
    {
		if(view.IsMine){
			if(Time.time >= nextAttackTime){
				if (Input.GetKeyDown(KeyCode.Z) && canAttack)
				{
					AttackMelee();					
					nextAttackTime = (Time.time + 1f) / attackRate;
					canAttack = false;			
					animator.SetBool("IsAttacking", true);						
					StartCoroutine(AttackCooldownMelee());
				}

				if (Input.GetKeyDown(KeyCode.V))
				{
					GameObject throwableWeapon = Instantiate(throwableObject, transform.position + new Vector3(transform.localScale.x * 0.5f,-0.2f), Quaternion.identity) as GameObject; 
					Vector2 direction = new Vector2(transform.localScale.x, 0);
					throwableWeapon.GetComponent<ThrowableWeapon>().direction = direction; 
					throwableWeapon.name = "ThrowableWeapon";
				}
			}
		}
	}

	IEnumerator AttackCooldownMage()
	{
		yield return new WaitForSeconds(0f);
		animator.SetBool("IsAttacking", false);
		canAttack = true;
	}
	IEnumerator AttackCooldownMelee()
	{
		yield return new WaitForSeconds(0f);
		animator.SetBool("IsAttacking", false);
		
		canAttack = true;
	}

	public void DoDashDamage()
	{
		dmgValue = Mathf.Abs(dmgValue);
		Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(attackCheck.position, 0.9f);
		for (int i = 0; i < collidersEnemies.Length; i++)
		{
			if (collidersEnemies[i].gameObject.tag == "Enemy")
			{
				if (collidersEnemies[i].transform.position.x - transform.position.x < 0)
				{
					dmgValue = -dmgValue;
				}
				collidersEnemies[i].gameObject.SendMessage("TakeDamage", dmgValue);
				cam.GetComponent<CameraFollow>().ShakeCamera();
			}
		}
	}
}
