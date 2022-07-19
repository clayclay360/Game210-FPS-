using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float stoppingDis;
    public float minAcc, maxAcc;
    public float minRotX, maxRotX;

    public float bulletPower;
    public float reloadTime;
    public int lives;

    public Vector3 targetOffset;
    public GameObject target, bulletPrefab, bulletSpawnPos, Gun, Coin;

    public bool canShoot, isShooting, isAlive;

    private NavMeshAgent agent;
    private Animator animator;

    private PlayerController playerController;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        playerController = FindObjectOfType<PlayerController>();
        gameController = FindObjectOfType<GameController>();

        target = playerController.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Movement();
            Shoot();
        }
    }

    private void Movement()
    {
        if (Vector3.Distance(target.transform.position, transform.position) > 5)
        {
            agent.destination = target.transform.position;
            animator.SetBool("Walk", true);
            agent.acceleration = minAcc;
            canShoot = false;
        }
        else
        {
            animator.SetBool("Walk", false);
            agent.acceleration = maxAcc;
            canShoot = true;
        }

        agent.stoppingDistance = stoppingDis;
        agent.speed = speed;

        transform.LookAt(target.transform.position, Vector3.up);
        float rotX = transform.rotation.x;
        rotX = Mathf.Clamp(rotX, minRotX, maxRotX);
        transform.rotation = Quaternion.Euler(rotX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Shoot()
    {
        if (canShoot && !isShooting)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir += targetOffset;


            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPos.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(dir * bulletPower);
            Destroy(bullet, 2);

            isShooting = true;
            StartCoroutine(Reload());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            lives--;

            if (lives <= 0 && isAlive)
            {
                animator.enabled = false;
                isAlive = false;
                Gun.SetActive(false);
                gameController.enemyCount--;

                Instantiate(Coin, new Vector3(transform.position.x,transform.position.y + 1
                    ,transform.position.z), Quaternion.identity);
            }
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isShooting = false;
    }
}
