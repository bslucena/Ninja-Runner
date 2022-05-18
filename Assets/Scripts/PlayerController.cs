using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // movement variables
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;

    //boundries variables
    public float xRange = 12.0f;
    public float zRange = 4.0f;

    // jump variables
    private bool isGrounded;
    public float jumpForce = 700.0f;
    public float gravityModifier = 1.5f;

    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerRb = GetComponent<Rigidbody>();
        MovePlayer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void MovePlayer()
    {

        CheckBoundries();

        // get user input to move the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        //playerRb.AddForce(Vector3.forward * speed * horizontalInput);
        //playerRb.AddForce(Vector3.right * speed * verticalInput);

        // jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce *gravityModifier, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void CheckBoundries()
    {
        // Boundries
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        }

        if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
    }
}
