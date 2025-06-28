using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region MovementVariables

    [SerializeField] float moveSpeed = 4;
    [SerializeField] float rotateSpeed = 15;
    [SerializeField] int gravity = 25;

    float velocityY;
    CharacterController characterController;
    Vector2 moveInput;
    Vector3 direction;
    Transform cam;
    Animator anim;

    #endregion

    [SerializeField] AnimationCurve dodgeCurve;

    bool isDodging;
    float dodgeTimer, dodge_coolDown;

    AudioSource audioSource;
    [SerializeField] AudioClip dodgeAudio;

    void Start()
    {
        cam = Camera.main.transform;
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Keyframe dodge_lastFrame = dodgeCurve[dodgeCurve.length - 1];
        dodgeTimer = dodge_lastFrame.time;
    }

    void Update()
    {
        RecordControls();

        if (!isDodging)
            PlayerMovement();

        PlayerRotation();

        if (dodge_coolDown > 0)
            dodge_coolDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (dodge_coolDown > 0) return;

            if (direction.magnitude != 0)
            {
                StartCoroutine(Dodge()); // Only if the character is moving, dodging is allowed.
            }
        }
    }

    IEnumerator Dodge()
    {
        anim.SetTrigger("Dodge");
        isDodging = true;

        float timer = 0;
        audioSource.PlayOneShot(dodgeAudio);

        bool heightCompressed = false;
        dodge_coolDown = dodgeTimer + 0.25f;

        while (timer < dodgeTimer)
        {
            if (!heightCompressed && timer > dodgeTimer / 3)
            {
                characterController.center = new Vector3(0, 0.5f, 0);
                characterController.height = 1;
                heightCompressed = true;
            }

            float speed = dodgeCurve.Evaluate(timer);
            Vector3 dir = (transform.forward * speed) + (Vector3.up * velocityY); // Adding direction and gravity.
            characterController.Move(dir * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        characterController.center = new Vector3(0, 1.1f, 0);
        characterController.height = 2;
        isDodging = false;
    }

    void RecordControls()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        direction = (right * moveInput.x + forward * moveInput.y).normalized;
        anim.SetFloat("Movement", direction.magnitude, 0.1f, Time.deltaTime);
    }

    void PlayerMovement()
    {
        velocityY -= Time.deltaTime * gravity;
        velocityY = Mathf.Clamp(velocityY, -10, 10);

        Vector3 fallVelocity = Vector3.up * velocityY;
        Vector3 velocity = (direction * moveSpeed) + fallVelocity;

        characterController.Move(velocity * Time.deltaTime);
    }

    void PlayerRotation()
    {
        if (direction.magnitude == 0) return;

        float rs = isDodging ? 3 : rotateSpeed;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rs * Time.deltaTime);
    }
}
