using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    public bool isControllable = true;

    private float horizontalSpeed;
    private float verticalSpeed;
    private float speed;
    private float gravity;
    private float jumpSpeed;

    private Vector3 moveDirection;
    private CharacterController _characterController;
    // Use this for initialization
    void Start ()
    {
        horizontalSpeed = 5;
        verticalSpeed = 6;
        speed = 6;
        jumpSpeed = 8f;
        gravity = 20f;
        moveDirection = Vector3.zero;
        _characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isControllable)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (_characterController.isGrounded)
            {
                moveDirection = new Vector3(h, 0, v);
                moveDirection = transform.GetChild(0).TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }


            moveDirection.y -= gravity * Time.deltaTime;

            _characterController.Move(moveDirection * Time.deltaTime);
        }
    }
}
