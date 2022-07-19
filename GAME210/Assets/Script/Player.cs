using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed, playerJumpMagnitude;

    private float horizontalInpputAxis, verticalInputAxis;
    private int jumpCount;
    private bool isGrounded = true;

    // Update is called once per frame
    void Update()
    {
        //Movement
        horizontalInpputAxis = Input.GetAxis("Horizontal");
        verticalInputAxis = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontalInpputAxis, 0.0f, verticalInputAxis).normalized * Time.deltaTime * playerSpeed);

        //Rotation
        if (Input.GetMouseButton(1))
        {
            float rotY = Input.GetAxis("Mouse X");
            transform.eulerAngles += Vector3.up * rotY * Time.deltaTime * 1000f;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded || Input.GetKeyDown(KeyCode.Space) && jumpCount == 1 ) 
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * playerJumpMagnitude);
            isGrounded = false;
            jumpCount++;
        }

        //Shoot
        if(Input.GetKey(KeyCode.A) /*&& GameManager.canPlayer.shoot && !GameManager.isPlayer.shooting*/)
        {
            Debug.Log("fire");
            GetComponent<Animator>().SetTrigger("Shoot");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }
}
