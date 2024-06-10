using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;
    [SerializeField] private bool m_isGrounded;
    [SerializeField] private Transform m_colliderCheckTransform;
    [SerializeField] private float m_lowJumpFactor;
    [SerializeField] private float m_radius = .01f;
    [SerializeField] private LayerMask m_layerMask;
    [SerializeField] private int m_abilityIndex = 4;
    [SerializeField] private float m_gravityFallModifier=3;
    [SerializeField] private float m_jumpHeightMax=2;
    [SerializeField] private float m_speed;

    // Start is called before the first frame update
    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_colliderCheckTransform.position, m_radius, m_layerMask);

        var inputX = Input.GetAxis("Horizontal");
        m_rigidbody2D.velocity = new Vector2(inputX*m_speed,m_rigidbody2D.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded && (m_abilityIndex & 1<<2) >0)
        {
            var velocity = m_rigidbody2D.velocity;
            velocity.y = Mathf.Sqrt(m_jumpHeightMax * -2*Physics2D.gravity.y);
            m_rigidbody2D.velocity = velocity;
            m_isGrounded = false;
        }
        
        if (m_rigidbody2D.velocity.y>0 && !Input.GetKey(KeyCode.Space))
        {
            var velocity = m_rigidbody2D.velocity;
            velocity += Vector2.up * (Physics2D.gravity.y * m_lowJumpFactor * Time.deltaTime);
            m_rigidbody2D.velocity = velocity;
        }

        if (m_rigidbody2D.velocity.y<0 &&m_isGrounded==false )
        {
            var velocity = m_rigidbody2D.velocity;
            velocity += Vector2.up * (Physics2D.gravity.y *m_gravityFallModifier* Time.deltaTime);
            m_rigidbody2D.velocity = velocity;
        }
    }
}
