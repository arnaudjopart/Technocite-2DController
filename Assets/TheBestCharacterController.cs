using System;
using Unity.Mathematics;
using UnityEngine;


namespace Arnaudtest
{
    public class TheBestCharacterController : MonoBehaviour
    {

        [Header("-- Ground Checker --")]
        [SerializeField] private Transform m_overlapBotton_01Transform;
        [SerializeField] private float m_groundCheckRadius;
        [SerializeField] private LayerMask m_groundLayerMask;
        [SerializeField] private bool m_isGrounded;

        [Header("-- Movement Data --")]
        [SerializeField] private float m_speed = 5;
        [SerializeField] private float m_jumpImpulse = 10;
        [SerializeField] private float m_lowJumpFactor = 3;
        [SerializeField] private float m_gravityFall = 5;
        [SerializeField] private bool m_isAlreadyDoubleJumping;

        [Header("-- Abilities --")]
        [SerializeField] private int m_playerCurrentAbilities = 6;
        [SerializeField] private Ability m_doubleJump;

        [Header("-- Buffers --")]
        [Range(.0f, .5f)]
        [SerializeField] private float m_jumpBufferLimit = .2f;
        [Range(.0f, .5f)]
        [SerializeField] private float m_coyoteBufferLimit = .2f;

        [Header("-- Animators --")]
        [SerializeField] private RuntimeAnimatorController m_swordAnimatorController;

        private bool m_wasGroundedDuringPreviousFrame;
        private float m_inputX;
        private float m_jumpBuffer;
        private float m_coyoteJumpBuffer;
        private Animator m_animator;
        private Rigidbody2D m_rigidbody2D;



        // Start is called before the first frame update
        void Awake()
        {
            var value = UnityEngine.Random.value;
            m_animator = GetComponent<Animator>();
            m_rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            m_jumpBuffer += Time.deltaTime;
            m_coyoteJumpBuffer += Time.deltaTime;

            m_isGrounded = Physics2D.OverlapCircle(m_overlapBotton_01Transform.position, m_groundCheckRadius, m_groundLayerMask);

            if (m_isGrounded && m_wasGroundedDuringPreviousFrame == false)
            {
                m_isAlreadyDoubleJumping = false;
                if (m_jumpBuffer < m_jumpBufferLimit)
                {
                    ResetVerticalVelocity();
                    Jump();
                }
            }

            if (m_isGrounded == false && m_wasGroundedDuringPreviousFrame == true)
            {
                m_coyoteJumpBuffer = 0;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_jumpBuffer = 0;
                if (m_isGrounded || m_coyoteJumpBuffer < m_coyoteBufferLimit)
                {
                    print("Jump");
                    ResetVerticalVelocity();
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

            if (m_rigidbody2D.velocity.y > 0 && Input.GetKey(KeyCode.Space) == false)
            {
                var velocity = m_rigidbody2D.velocity;
                velocity += Vector2.up * (Physics2D.gravity.y * m_lowJumpFactor * Time.deltaTime);
                m_rigidbody2D.velocity = velocity;
            }

            if (m_rigidbody2D.velocity.y < 0 && m_isGrounded == false)
            {
                m_rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * m_gravityFall * Time.deltaTime);
            }

            m_inputX = Input.GetAxis("Horizontal");
            bool isMoving = Mathf.Abs(m_inputX) > 0.1f;

            if (m_inputX < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (m_inputX > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            m_animator.SetBool("RunningBool", isMoving);
            m_animator.SetFloat("VerticalVelocity", m_rigidbody2D.velocity.y);
            m_animator.SetBool("IsGroundedBool", m_isGrounded);

            m_wasGroundedDuringPreviousFrame = m_isGrounded;
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
            return true;// (m_playerCurrentAbilities & m_doubleJump.m_value) > 0 && m_isAlreadyDoubleJumping == false;
        }

        private void FixedUpdate()
        {
            m_rigidbody2D.velocity = new Vector2(m_inputX * m_speed, m_rigidbody2D.velocity.y);
        }


        public void GetSword()
        {
            m_animator.runtimeAnimatorController = m_swordAnimatorController;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ICollectable>(out var collectable)) { collectable.Collect(this); }
        }

        internal void UnlockDoubleJump()
        {
            //m_playerCurrentAbilities += m_doubleJump.m_value;
        }
    }

}
