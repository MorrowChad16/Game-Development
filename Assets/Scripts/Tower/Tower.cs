using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private Projectile projectile;
    private Enemy targetEnemy = null;
    private float attackCounter;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackCounter -= Time.deltaTime;
        if (!targetEnemy || targetEnemy.IsDead)
        {
            Enemy tempEnemy = GetNearestEnemy();
            if (tempEnemy && Vector2.Distance(transform.localPosition, tempEnemy.transform.localPosition) <= attackRadius)
            {
                targetEnemy = tempEnemy;
            }
        }
        else {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            }
            else {
                isAttacking = false;
            }

            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }
        }
    }

    private void FixedUpdate() //common Unity practice for moving objects with physics, do not put it in Update()!!!
    {
        if (isAttacking) {
            Attack();
        }
    }

    public void Attack() {
        isAttacking = false;
        Projectile attackProjectile = Instantiate(projectile) as Projectile;
        attackProjectile.transform.position = transform.localPosition;
        if (attackProjectile.ProjectileType == projectileType.arrow)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Arrow);
        }

        if (attackProjectile.ProjectileType == projectileType.fireball)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Fireball);
        }

        if (attackProjectile.ProjectileType == projectileType.rock) {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Rock);
        }

        if (!targetEnemy)
        {
            Destroy(attackProjectile.gameObject);
        }
        else
        {
            StartCoroutine(MoveProjectile(attackProjectile));
        }
    }

    IEnumerator MoveProjectile(Projectile projectile) {
        while (getTargetDistance(targetEnemy) > 0.2f && projectile && targetEnemy) {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.position, targetEnemy.transform.position, 5f * Time.deltaTime);
            yield return null;
        }

        if (projectile || !targetEnemy)
        {
            Destroy(projectile.gameObject);
            yield return null;
        }
    }

    private float getTargetDistance(Enemy targetEnemy) {
        if (!targetEnemy) {
            targetEnemy = GetNearestEnemy();
            if (!targetEnemy)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition));
    }

    private List<Enemy> GetEnemiesInRange() {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach(Enemy enemy in GameManager.Instance.EnemyList) {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius) {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach (Enemy enemy in GetEnemiesInRange()) {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance) {
                nearestEnemy = enemy;
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
            }
        }
        return nearestEnemy;
    }
}
