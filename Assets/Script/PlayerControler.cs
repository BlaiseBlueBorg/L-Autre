using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform rotateRoot;
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform respawn;

    [SerializeField] private Transform targetCamera;
    [SerializeField] private float force = 3f;
    [SerializeField] private float jumpForce = 300f;
    [SerializeField] private float dashForce = 50f;
    [SerializeField] private bool isOnFloor = true;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool freezeMovement = false;
    private float dashingTime = 0.25f; 

    private float speedFactor = 1f;
    [SerializeField] private float speedFactorWalk = 1f;
    [SerializeField] private float speedFactorRun = 4f;
    [SerializeField] private float speedFactorCrouch = 0.5f;
    private Vector3 direction; 
    [SerializeField] private float rotateMultiplier = 10f;

    Keyboard keyboard = Keyboard.current;

    private Vector3 dashDirection = Vector3.zero;

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = false;
            isDashing = false;
            Debug.Log("Is Not On Floor");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            isOnFloor = true;
            isJumping = false;
            Debug.Log("Is On Floor");
        }

        if (collision.gameObject.CompareTag("DeathPlane"))
        {
            transform.position = respawn.position; 
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Congratulations!!!!!!!!");
        }
    }

    
    private void FixedUpdate()
    {
        if (dashDirection != Vector3.zero)
        {
            rb.AddForce(dashDirection * dashForce);
            StartCoroutine(CountDown());
            Debug.Log("Dashing");
            isJumping = false;
            
            dashDirection = Vector3.zero;
        }
    }

    void Update()
    {
        direction = Vector3.zero; 

        Mouvement();
        
        Rotation();
        
        Sprint();
        
        Crouch();
        
        float y = rb.velocity.y;
        if (!isDashing)
        {
            transform.Translate(-direction.normalized * speedFactor * Time.deltaTime * force);
        }
        
        Jump();
    }

    public void Mouvement()
    {
        if (keyboard != null)
        {
            if (freezeMovement)
            {
                return; 
            }

            if(keyboard.dKey.isPressed && isDashing)
            {
                dashDirection = camera.transform.right;
            }
            else if(keyboard.dKey.isPressed)
            {
                direction += camera.transform.right;
            }

            if(keyboard.aKey.isPressed && isDashing)
            {
                dashDirection = -camera.transform.right;
            }
            else if(keyboard.aKey.isPressed)
            {
                direction += -camera.transform.right;
            }
        
            if(keyboard.wKey.isPressed && isDashing)
            {
                dashDirection = camera.transform.forward;
            }
            else if(keyboard.wKey.isPressed)
            {
                direction += camera.transform.forward;
            }
        
            if(keyboard.sKey.isPressed && isDashing)
            {
                dashDirection = -camera.transform.forward;
            }
            else if(keyboard.sKey.isPressed)
            {
                direction += -camera.transform.forward;
            }
        
            direction.y = 0;
        }
        else
        {
            keyboard = Keyboard.current;
        }
    }

    public void Rotation()
    {
        if (direction == Vector3.zero)
                return;
            
        var lookRotation = Quaternion.LookRotation(direction);
        var rotateSlerp = Quaternion.Slerp(rotateRoot.rotation, lookRotation, Mathf.Clamp01(rotateMultiplier * Time.deltaTime));
        rotateRoot.rotation = rotateSlerp;
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isJumping)
        {
            speedFactor = speedFactorRun;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedFactor = speedFactorWalk;
        }

        if (Input.GetKeyDown(KeyCode.E) && isDashing)
        {
            speedFactor = speedFactorRun; 
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            speedFactor = speedFactorWalk;
        }
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);

            speedFactor = speedFactorCrouch;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);

            speedFactor = speedFactorWalk;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnFloor)
        {
            rb.AddForce(0, jumpForce, 0);
            isJumping = true; 
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && isJumping && !isDashing)
        {
            rb.AddForce(0, jumpForce * 2, 0);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            isDashing = true;
            freezeMovement = true;
            StartCoroutine(CountDown());
        }
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(1);
        freezeMovement = false;

        yield return new WaitForSecondsRealtime(2);
        isDashing = false;
    }
}
