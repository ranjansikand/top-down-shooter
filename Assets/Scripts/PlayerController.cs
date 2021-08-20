using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)] float maxAcceleration = 20f;

    Vector3 velocity, desiredVelocity;

    Vector3 mousePos;

    private Rigidbody rb;
    private Camera cam;

    public delegate void UpdateGameState();
    public static UpdateGameState playerDeath;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void FixedUpdate () 
    {
        velocity = rb.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = 
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);;

        rb.velocity = velocity;

    }
    
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");

        desiredVelocity = new Vector3(playerInput.x, 
            0f, playerInput.y) * maxSpeed;

        // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = cam.WorldToScreenPoint(transform.position);

        Vector3 lookDir = mousePos - playerPos;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
    }

    void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.tag == "Enemy") {
            playerDeath();

            Destroy(gameObject);
        }
    }
}
