using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variables")]
    public int lives;
    public float recoveringSpeed;
    public bool isAlive = true;
    public bool isHealing = false;

    public GameObject cameraRig;
    
    [Header("Gun Variables")]
    public float firePower;
    public Rigidbody bulletPrefab;
    public Transform bulletSpawnTransformRight, bulletSpawnTransformLeft;

    [Header("Force Variables")]
    public float forcePower;

    public static GameObject player;

    public bool ForceChargeUp { get{ return forceChargeUp; } set { forceChargeUp = value; } }

    private int weapon;
    private float speed;
    private float moveHorizontal;
    private float moveVertical;
    private bool handDirection;
    private bool isShielding;
    private bool forceChargeUp;

    private Animator animator;
    private CameraController cameraController;
    private UIController uiController;

    private void Awake()
    {
        player = gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraController = FindObjectOfType<CameraController>();
        uiController = FindObjectOfType<UIController>();
        weapon = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            GetInput();
        }

        if (isHealing)
        {
            cameraController._vignette.intensity.value =
            Mathf.Lerp(cameraController._vignette.intensity.value, 0, recoveringSpeed);
        }
    }

    private void GetInput()
    {
        if (!GameManager.isGamePaused)
        {
            //Movement
            #region Motion
            Vector3 forwardMovement = Vector3.zero;
            Vector3 rightMovement = Vector3.zero;

            GameManager.isPlayer.walking = false;
            if (GameManager.canPlayer.walk)
            {
                moveHorizontal = Input.GetAxisRaw("Horizontal");
                moveVertical = Input.GetAxisRaw("Vertical");

                if (moveHorizontal != 0 || moveVertical != 0)
                {
                    GameManager.isPlayer.walking = true;

                    //sprint                    
                    speed = GameManager.playerWalkSpeed;

                    if (Input.GetKey(KeyCode.LeftShift) && !GameManager.isPlayer.jumping)
                    {
                        speed = GameManager.playerSprintSpeed;
                    }

                    //move forward and backwards
                    if (moveVertical != 0f)
                    {
                        forwardMovement = new Vector3(cameraRig.transform.forward.x, 0, cameraRig.transform.forward.z);
                    }
                    forwardMovement.Normalize();
                    transform.position += forwardMovement * Time.deltaTime * speed * moveVertical;

                    //move left and right
                    if (moveHorizontal != 0f)
                    {
                        rightMovement = new Vector3(cameraRig.transform.right.x, 0, cameraRig.transform.right.z);
                    }
                    rightMovement.Normalize();
                    transform.position += rightMovement * Time.deltaTime * speed * moveHorizontal;

                }
            }

            //jump
            if (GameManager.canPlayer.jump && Input.GetKeyDown(KeyCode.Space) && !GameManager.isPlayer.jumping)
            {
                GameManager.isPlayer.jumping = true;

                Vector3 pushDir = new Vector3(0, 1, 0);
                GetComponent<Rigidbody>().AddForce(pushDir * GameManager.playerJumpMagnitude);
            }

            //shoot and punch
            if (Input.GetButton("Fire1") && GameManager.canPlayer.attack && !GameManager.isPlayer.attacking)
            {
                GameManager.canPlayer.attack = false;
                handDirection = !handDirection;

                GetComponent<Animator>().SetBool("Hand", handDirection);
                GetComponent<Animator>().SetTrigger("Attack");
            }

            //shield
            if(weapon == 3 && !GameManager.isPlayer.attacking && Input.GetButton("Fire2"))
            {
                isShielding = true;
            }
            else
            {
                isShielding = false;
            }

            GetComponent<Animator>().SetBool("Shield", isShielding);

            //force push
            if(weapon == 4 && forceChargeUp)
            {
                if (Input.GetMouseButton(0))
                {
                    forceChargeUp = true;
                }
                else
                {
                    forceChargeUp = false;
                }
            }

            animator.SetBool("Hold", forceChargeUp);

            #endregion

            //Animator
            #region Animation
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weapon = 1; //fist
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weapon = 2; //pistol
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                weapon = 3; //sword and sheild
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                weapon = 4; //force push
            }

            animator.SetInteger("Weapon", weapon);
            #endregion
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(GameManager.isPlayer.jumping)
        {
            StartCoroutine(CanJumpAgain());
        }
    }

    public void TakeDamage()
    {
        cameraController._vignette.intensity.value += 0.07f;
        lives--;

        if (lives <= 0)
        {
            isAlive = false;
            animator.SetBool("Death", true);
            cameraController.enabled = false;
            uiController.restartButton.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SpawnBullet(int index)
    {
        Transform t = null;

        switch (index)
        {
            case 0:
                t = bulletSpawnTransformLeft;
                break;
            case 1:
                t = bulletSpawnTransformRight;
                break;
        }

        Rigidbody bullet = Instantiate(bulletPrefab, t.position, transform.rotation);
        bullet.AddForce(t.up * firePower);
        Destroy(bullet.gameObject, 2);
    }

    private IEnumerator CanJumpAgain()
    {
        GameManager.canPlayer.jump = false;
        yield return new WaitForSeconds(0.075f);
        GameManager.isPlayer.jumping = false;
        GameManager.canPlayer.jump = true;
    }
}
