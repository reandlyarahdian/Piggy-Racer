using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;


    [Header("Dash Variables")]
    public float dashingVelocity = 14f;
    public float dashingTime = 0.3f;
    public float dashingCooldown = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private bool canRun = true;

    [Header("References")]
    Animator myAnimator;
    Vector2 moveInput;
    Rigidbody2D myrigidbody;
    //CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float gravityAtStart;
    float health;

    [HideInInspector]
    public int Points;

    //[Header("CameraShake")]
    //public CameraShake cameraShake;
    //public float mag = 0.3f;
    //public float tims = 0.4f;

    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        //myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = myrigidbody.gravityScale;
        health = 3;
    }

    public void RSpeed(float speed)
    {
        runSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        JumpCheck();

        float h = Input.GetAxisRaw("Horizontal") < 0 ? 0 : Input.GetAxisRaw("Horizontal");

        var move = new Vector2(h, 0);

        OnMove(move);

        var jump = Input.GetButtonDown("Jump");

        if (jump)
        {
            OnJump(jump);
        }

        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0f);
            }
            StartCoroutine(StopDashing());

        }

        if (isDashing)
        {
            myrigidbody.velocity = dashingDir.normalized * dashingVelocity;
            //StartCoroutine(cameraShake.Shake(tims, mag));
            return;
        }
    }

    void OnMove(Vector2 value)
    {
        moveInput = value;
    }

    void OnJump(bool value)
    {
        if (value)
        {
            myrigidbody.velocity += new Vector2(1f, jumpSpeed);
            myAnimator.SetBool("Jump", value);
        }
    }

    public void Run()
    {
        if (canRun)
        {
            Vector2 PlayerVelocity = new Vector2(moveInput.x * runSpeed, myrigidbody.velocity.y);
            myrigidbody.velocity = PlayerVelocity;

            bool isMyManJumping = Mathf.Abs(myrigidbody.velocity.y) > Mathf.Epsilon;

            bool isMyManRunning = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;
            //myAnimator.SetBool("IsRunning", isMyManRunning);
        }
    }

    void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;

        //if (playerHorizontalSpeed)
        //{
        //    transform.localScale = new Vector2(Mathf.Sign(myrigidbody.velocity.x), 1f);
        //}
    }

    void JumpCheck()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Jump", false);
        }
        else if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("Jump", true);
        }
    }

    public void OnHit()
    {
        myAnimator.SetBool("Jump", false);
        myAnimator.SetTrigger("Hit");
        AudioManager.Instance.PlaySFX("SFX");
        health -= 1;
        if(health <= 0)
        {
            GameManager.instance.EndGame();
        }
        StartCoroutine(StopRunnig());
    }

    public void Point()
    {
        Points += 1;
    }

    private IEnumerator StopRunnig()
    {
        canRun = false;
        myrigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(1.5f);
        canRun = true;
        myAnimator.SetTrigger("Run");
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
