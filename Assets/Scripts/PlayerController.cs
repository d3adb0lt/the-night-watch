using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem dodgeEffect;

    /*General Prop Vars*/
    public float speed = 80;
    public float heighLimit = 3.6f;
    public float jumpVal = 10;

    /*Gravity Manipulation Vars*/
    public float fallMultiplier = 6f;
    public float lowJumpMultiplier = 3f;

    /*Dash Vars*/
    public float dashDistance = 30f;
    public float sidestepForce = 30f;
    public float sidestepDuration = 0.5f;
    public float sidestepCooldown = 0.5f;

    /* Internal Vars*/
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private Vector2 moveInput;
    private bool canSidestep = true;
    private bool isInvulnerable = false;

    void Start()
    {
        if (!dodgeEffect)
            dodgeEffect.Stop();
        
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // falling down, negative y-component
        if (rb.linearVelocity.y < 0)
        {
            // falling down -> apply stronger gravity for less of a floating effect
            // me not know why gravity not strong like it do be in life, cringe
            rb.linearVelocity += (fallMultiplier - 1) * Physics.gravity.y * Time.fixedDeltaTime * Vector3.up;
        }
        // currently jumping
        else if (rb.linearVelocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
        {
            // early jump release -> short hop
            rb.linearVelocity += (lowJumpMultiplier - 1) * Physics.gravity.y * Time.fixedDeltaTime * Vector3.up;
        }
        // update player transform and state
        Vector3 movement = new(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
        movementX = moveInput.x;
        movementY = movementValue.Get<Vector2>().y;
    }

    // ^ Jump up (press Spacebar)
    void OnJump(InputValue spacebar)
    {
        if (spacebar.isPressed && transform.position.y <= heighLimit)
        {
            rb.AddForce(Vector3.up * jumpVal, ForceMode.Impulse);
        }
    }

    // <-- Sidestep left (press Q)
    void OnSidestepLeft()
    {
        if (canSidestep)
            // run the sidestep routine with the knowledge of player wanting to dash right
            StartCoroutine(PerformSidestep(transform.right));
    }

    // --> Sidestep right (press E)
    void OnSidestepRight()
    {
        if (canSidestep)
            // run the sidestep routine with the knowledge of player wanting to dash left
            StartCoroutine(PerformSidestep(-transform.right));
    }

    private IEnumerator PerformSidestep(Vector3 direction)
    {
        canSidestep = false;
        isInvulnerable = true;

        if (dodgeEffect != null)
            dodgeEffect.Play();

        rb.AddForce(direction.normalized * sidestepForce, ForceMode.Impulse);

        // wait when in the midst of dash to allow for invincibility for a certain number of frames
        yield return new WaitForSeconds(sidestepDuration);
        isInvulnerable = false;

        if (dodgeEffect != null)
            dodgeEffect.Stop();

        // wait to allow for a cooldown of the dodge, this way they cannot spam it
        yield return new WaitForSeconds(sidestepCooldown);
        canSidestep = true;
    }

    // for later use, bc currently the "invincibility" is entirely limited to a boolean,
    // no tangible effect atm
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
}