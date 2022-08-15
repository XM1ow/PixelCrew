using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Rigidbody2D RB;
    [SerializeField] Animator Anim;

    [Header("Position Info")]
    public LayerMask ground;
    public LayerMask platform;
    public Transform groundCheck;
    [SerializeField] private bool onGround;

    [Header("Move")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float Hdirection;
    [SerializeField] private float Vdirection;
    [SerializeField] private Vector2 MoveVector;
    private bool faceRight = true;
    private bool faceNow;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int jumpCount;
    [SerializeField] private int maxJumpCount = 2;
    public float fallMultiplier = 3f;//Scale of gravity when falling

    [Header("Platform")]
    [SerializeField] private float fallTime;
    private int normalLayer = 11;
    private int fallLayer = 12;

    [Header("Stealth")]
    [SerializeField] private float stealthSpeed = 2f;
    [SerializeField] private bool toStealth = false; // ��������ת���������Ƿ����Ǳ��״̬
    [SerializeField] private bool canStealth; // �ж��Ƿ���Ǳ��������
    public LayerMask StealthArea;
    public Transform StealthCheck;

    [Header("Dash")]
    [SerializeField] private bool canDash = true; // �Ƿ���Գ��
    [SerializeField] private bool isDashing; // ���ڳ����
    [SerializeField] private float dashingPower = 20f; //��̵�����
    [SerializeField] private float dashingTime = 0.1f; //��̵�ʱ��
    [SerializeField] private float dashCooldown = 2f; //��̵���ȴʱ��
    [SerializeField] private TrailRenderer dashTrail;
    private float DashDir;

    // Setting InputMap to Gameplay Map
    // to make sure that the methods below are conducted;
    void OnEnable()
    {
        playerInput.onMove += Move;
        playerInput.onStopMove += StopMove;
        playerInput.onJump += Jump;
        playerInput.onStealth += Stealth;
        playerInput.onDash += toDash;
    }
    void OnDisable()
    {
        playerInput.onMove -= Move;
        playerInput.onStopMove -= StopMove;
        playerInput.onJump -= Jump;
        playerInput.onStealth -= Stealth;
        playerInput.onDash -= toDash;
    }
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        playerInput.EnableGameplayInput();
    }

    void Update()
    {
        // Position Maintainance
        onGround = Physics2D.OverlapCircle(groundCheck.position, .05f, ground) || Physics2D.OverlapCircle(groundCheck.position, .05f, platform);
        canStealth = Physics2D.OverlapCircle(StealthCheck.position, .05f, StealthArea);
        // Animation
        Anim.SetFloat("SpeedY", RB.velocity.y);
        Anim.SetBool("onGround", onGround);

        // Setting states
        if (onGround)
        {
            // Animation
            if (Mathf.Abs(RB.velocity.y) < 0.1f)
                Anim.SetBool("Falling", false);
            // Setting value
            jumpCount = maxJumpCount;
            
        }

        if (!onGround)
        {   
            if(RB.velocity.y < 0)
            {
                // Animation
                Anim.SetBool("Falling", true);
            }
            // �����ƽ̨�����£���Ծ��-1
            if (jumpCount == maxJumpCount)
                jumpCount -= 1;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        // Continue Moving
        RB.velocity = new Vector2(Hdirection * Speed, RB.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Portal")
        {
            coll.gameObject.transform.GetComponent<Portal>().changeScene();
        }
    }

    void Move(Vector2 dir)
    {
        Hdirection = dir.x;
        Vdirection = dir.y;
        // Animation
        Anim.SetFloat("Speed", Mathf.Abs(Hdirection)); // Remember for walking
        Anim.SetBool("Movement", Hdirection != 0f);

        if(RB.velocity.y < 0)
            // Multiplying Gravity when falling
            RB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        faceNow = (Hdirection > 0.1f);
        if (faceNow != faceRight) { Flip(); }
    }
    void Flip()
    {
        // Record NowFacing
        faceRight = !faceRight;
        // Core Function
        transform.Rotate(0f, 180f, 0f);
    }

    void StopMove()
    {
        Anim.SetFloat("Speed", 0f);
        Anim.SetBool("Movement", false);
        Hdirection = 0f;
        Vdirection = 0f;
    }

    void Jump()
    {
        if(Vdirection < 0f)
        {
            StartCoroutine(fallPlatform());
        }
        else {
            if (onGround)
            {
                // Animation
                Anim.SetTrigger("Jump");
                // Ordinary Jump
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.velocity += Vector2.up * jumpForce;
                jumpCount--;
            } else if (jumpCount > 0)
            {
                // Animation
                Anim.SetTrigger("DoubleJump");
                RB.velocity = new Vector2(RB.velocity.x, 0);
                RB.velocity += Vector2.up * jumpForce;
                jumpCount--;
            }
        }
    }
    IEnumerator fallPlatform()
    {
        gameObject.layer = fallLayer;
        yield return new WaitForSecondsRealtime(fallTime);
        gameObject.layer = normalLayer;
    }

    void toDash()
    {
        if (canDash)
            StartCoroutine(Dash());
    }
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = RB.gravityScale;
        RB.gravityScale = 0;

        // Dash
        DashDir = faceRight ? 1 : -1;
        RB.velocity = new Vector2(DashDir * dashingPower, 0f);
        // dashTrail.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        RB.gravityScale = originalGravity;
        // dashTrail.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }


    void Stealth()
    {
        if (!onGround || !canStealth)
            return;
        toStealth = !toStealth; // �㰴�л�
        // TODO
    }
}
