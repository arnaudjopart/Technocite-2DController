using System;
using UnityEngine;



public class TheBestCharacterController : MonoBehaviour
{

    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_rigidbody2D;
    [SerializeField] private float m_speed=5;
    [SerializeField] private Transform m_overlapBotton_01Transform;
    //[SerializeField] private Transform m_overlapBotton_02Transform;
    [SerializeField] private float m_groundCheckRadius;
    [SerializeField] private LayerMask m_groundLayerMask;
    [SerializeField] private bool m_isGrounded;
    private bool m_wasAlreadyGroundedDuringPreviousFrame;
    private float m_inputX;
    [SerializeField] private float m_jumpImpulse =10;
    [SerializeField] private float m_lowJumpFactor = 3;

    [SerializeField] private bool m_isAlreadyDoubleJumping;
    [Header("-- Abilities --")]
    [SerializeField] private int m_playerCurrentAbilities=6;
    [SerializeField] private int m_doubleJumpAbilityValue = 2;
    [SerializeField] private int m_dashAbilityValue = 4;
    [SerializeField] private int m_groundAttackValue = 8;
    private float m_jumpBuffer;
    [SerializeField] private float m_jumpBufferLimit =20;
    [SerializeField] private float m_gravityFall=5;

    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_jumpBuffer += Time.deltaTime;
        m_isGrounded = Physics2D.OverlapCircle(m_overlapBotton_01Transform.position, m_groundCheckRadius, m_groundLayerMask);
        
        if(m_isGrounded && m_wasAlreadyGroundedDuringPreviousFrame == false)
        {
            m_isAlreadyDoubleJumping = false;
            if (m_jumpBuffer < m_jumpBufferLimit)
            {
                ResetVerticalVelocity();
                Jump();
            }
            else  m_animator.SetTrigger("OnGroundTrigger");
        }

        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            m_jumpBuffer = 0;
            if (m_isGrounded) 
            {
                Jump();
            } 
            else
            {
                if (CanDoDoubleJump())
                {
                    m_isAlreadyDoubleJumping = true;
                    ResetVerticalVelocity();
                    Jump();
                }
                
            }
        }

        if (m_rigidbody2D.velocity.y > 0 && Input.GetKey(KeyCode.Space)==false)
        {
            var velocity = m_rigidbody2D.velocity;
            velocity += Vector2.up * (Physics2D.gravity.y * m_lowJumpFactor * Time.deltaTime);
            m_rigidbody2D.velocity = velocity;
        }

        if (m_rigidbody2D.velocity.y < 0 && m_isGrounded == false) 
        { 
            m_rigidbody2D.velocity+= Vector2.up * (Physics2D.gravity.y * m_gravityFall * Time.deltaTime);
        }

        m_inputX = Input.GetAxis("Horizontal");
        bool isMoving = Mathf.Abs(m_inputX) > 0.1f;
        m_spriteRenderer.flipX = m_inputX < 0;

        m_animator.SetBool("RunningBool", isMoving);
        m_animator.SetFloat("VerticalVelocity", m_rigidbody2D.velocity.y);

        m_wasAlreadyGroundedDuringPreviousFrame = m_isGrounded;
    }

    private void Jump()
    {
        m_rigidbody2D.AddForce(Vector2.up * m_jumpImpulse, ForceMode2D.Impulse);
    }

    private void ResetVerticalVelocity()
    {
        var currentVelocity = m_rigidbody2D.velocity;
        currentVelocity.y = 0;
        m_rigidbody2D.velocity = currentVelocity;
    }

    private bool CanDoDoubleJump()
    {
        //return m_isAlreadyDoubleJumping == false;
        return (m_playerCurrentAbilities & m_doubleJumpAbilityValue) > 0 && m_isAlreadyDoubleJumping == false;
    }

    private void FixedUpdate()
    {
        m_rigidbody2D.velocity = new Vector2(m_inputX * m_speed, m_rigidbody2D.velocity.y);
    }
}
