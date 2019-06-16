using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int target = 0; //initial checkpoint destination
    [SerializeField]
    private Transform exitPoint; //point on our 2D map for our exit point
    [SerializeField]
    private Transform[] waypoints; //contains all of our checkpoints for monster movement
    [SerializeField]
    private float navigationUpdate; //compares to our clock time for different CPU's
    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private int rewardAmount;
    bool isDead = false;
    private Collider2D enemyCollider;
    private Animator anim;

    private Transform enemy; //our enemy
    private float navigationTime = 0; //

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        GameManager.Instance.RegisterEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints != null && !isDead) {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate) {
                if (target < waypoints.Length)
                {
                    enemy.position = Vector2.MoveTowards(enemy.position, waypoints[target].position, navigationTime);
                }
                else {
                    enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
                }
                navigationTime = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Checkpoint")
        {
            target += 1;
        }
        else if (collision.tag == "Finish")
        {
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnRegisterEnemy(this);
            GameManager.Instance.IsWaveOver();
        }
        else if (collision.tag == "projectile") {
            Projectile tempProj = collision.gameObject.GetComponent<Projectile>(); //allows you to access the internal variables of the projectile
            EnemyHit(tempProj.AttackStrength); //calls function to reduce enemy health
            Destroy(collision.gameObject); //destroys projectile when it hits the enemy
        }
    }

    public void EnemyHit(int hitpoints) {
        if ((healthPoints - hitpoints) > 0)
        {
            healthPoints -= hitpoints;
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Hit);
            anim.Play("Hurt");
        }
        else {
            Death();
        }
    }

    public void Death() {
        isDead = true;
        enemyCollider.enabled = false;
        GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Death);
        anim.SetTrigger("didDie");
        GameManager.Instance.TotalKilled += 1;
        GameManager.Instance.addMoney(rewardAmount);
        GameManager.Instance.IsWaveOver();
    }

    public bool IsDead {
        get {
            return isDead;
        }
    }
}
