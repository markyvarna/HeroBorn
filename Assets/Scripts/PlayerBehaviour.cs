using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //MARK: Multipliers
    //moveSpeed - speed forward & backwards, rotateSpeed - speed rotate left or right
    public float moveSpeed = 10f;
    public float rotateSpeed = 75f;
    public float jumpVelocity = 5f;
    public float distanceToGround = 0.1f;
    public LayerMask groundLayer;

    //MARK: Player Inputs
    //vInput - vertical axis input, hInput - horizontal axis input
    private float vInput;
    private float hInput;

    private Rigidbody _rb;
    private CapsuleCollider _col;

    //MARK: Bullet
    //this will hold the Bullet Prefab
    public GameObject bullet;
    public float bulletSpeed = 100f;

    private GameBehaviour _gameManager;

    // MARK: Firing Events (FE)
    //FE: Declare new delegate type that returns void and takes no parameters
    public delegate void JumpingEvent();
    /*FE: create an event of JumpingEvent type, named playerJump, that can be
    treated as a method that matches the delegates void return and no param signature*/
    public event JumpingEvent playerJump;

    private void Start()
    {
        //set the players Rigidbody to the _rb variable
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();

        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();

    }

    void Update()
    {
        //MARK: Movement

        /*detect up,down,W,or S Keys are pressed, then multiply value by moveSpeed.
        Up & W Keys return 1 -> move player forward
        Down & S Keys return -1 -> move player backwards*/
        vInput = Input.GetAxis("Vertical") * moveSpeed;

        /*detects Left,Right,A,D Keys are pressed, multiplies value by rotateSpeed
        Right & D Keys return 1 -> rotates player to right
        Left & A Keys return -1 -> rotates player left*/
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;


        /*moves player transform component
        Time.deltaTime - direction & speed move forward or backwards along z axis
        at the speed we supplied to it.*/
        //this.transform.Translate(Vector3.forward * vInput * Time.deltaTime);

        /*Rotate Method - rotates player transform to the Vector we pass it*/
        //this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            //FE: calls playerJump after force is applied in Update()
            playerJump();
        }

    }

    //any physics or rigidbody code always goes in fixed update
    private void FixedUpdate()
    {
        //created new variable to store Left & Right rotation
        Vector3 rotation = Vector3.up * hInput * Time.fixedDeltaTime;
        //Quanternion takes in a Vector3 value & returns a rotaion value in Euler angles
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        _rb.MovePosition(this.transform.position + this.transform.forward
            * vInput * Time.fixedDeltaTime);
        /*angleRot already has horizontal inputs from keyboard, so all thats left
        to do is multiply player rigidbody rotation by deltaRotation*/
        _rb.MoveRotation(_rb.rotation * deltaRotation);

        //Checks to see if mouse button pressed returns true.
        // 0 -> Left Mouse Button
        // 1 -> Right Mouse Button
        // 2 -> Middle or Scroll Button
        if (Input.GetMouseButtonDown(0))
        {
            //create new bullet game object everytime the mouse is pressed
            GameObject newBullet = Instantiate(bullet, this.transform.position,
                this.transform.rotation) as GameObject;
            Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
            bulletRB.velocity = this.transform.forward * bulletSpeed;
        }


    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y,
            _col.bounds.center.z);
        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom,
            distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Capsule")
        {
            _gameManager.HP -= 1;
        }

    }
}
