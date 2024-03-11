using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputSystem playerInput;
    InputAction move;

    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody rb;

    private Vector3 playerVector;
    [SerializeField] private float playerForce;
    [SerializeField] private float directionForce;
    public bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new InputSystem();
    }

    private void Start()
    {
        isGrounded = true;
    }

    private void OnEnable()
    {
        playerInput.Move.Jump.performed += OnJump;
        playerInput.Move.Left.performed += OnLeftKeyPressed;
        playerInput.Move.Right.performed += OnRightKeyPressed;
        move = playerInput.Move.Movement;
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Move.Jump.performed -= OnJump;
        playerInput.Move.Left.performed -= OnLeftKeyPressed;
        playerInput.Move.Right.performed -= OnRightKeyPressed;
        move = playerInput.Move.Movement;
        playerInput.Disable();
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnLeftKeyPressed(InputAction.CallbackContext ctx)
    {
        rb.AddForce(-directionForce, 0, 0);
    }

    void OnRightKeyPressed(InputAction.CallbackContext ctx)
    {
        rb.AddForce(directionForce, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*playerVector += move.ReadValue<Vector2>().x * transform.right * speed;
        playerVector += move.ReadValue<Vector2>().y * transform.forward * speed;*/

        playerVector = Vector3.zero;
        rb.AddForce(playerVector, ForceMode.Impulse);
        rb.AddForce(0, 0, playerForce);
        Vector3 velocity = rb.velocity;
        velocity.y = 0;
        if(velocity.sqrMagnitude > playerForce * playerForce)
        {
            rb.velocity = velocity.normalized * playerForce + Vector3.up * rb.velocity.y;
        }
    }
}
