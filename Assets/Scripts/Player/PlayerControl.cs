using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Camera")] public Camera cam;

    [Header("CharacterRigidBody")] public Rigidbody2D rb;

    public float mMoveSpeed = 16.0f;

    private Vector2 _direction;
    private Vector2 _movement;
    private float _leaveGround = 0;
    // private HealthController _healthController;

    [Header("Turret")]
    public Transform turret;
    public AudioSource soundGun;
    public ParticleSystem particleGun;
    public Transform lineHintRender;

    [Header("Jelly")]
    // public JellyEffect jelly;

    [Header("Particle")]
    public ParticleSystem rayParticle;
    public ParticleSystem shootParticle;
    public GameObject hurtParticlePrefab;

    public TimeMachine timeMachine;
    public GameObject inControl;


    void Start()
    {
        // particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeInverse.globalTimeDirection != GetComponent<TimeInverse>().timeDirection)
        {
            inControl.SetActive(false); 
            return;
        }
        inControl.SetActive(true);

        // Get Mouse Direction
        Vector2 mousePosOnScreen = Input.mousePosition;
        Vector2 screenPos = cam.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = cam.ScreenToWorldPoint(mousePosOnScreen);
        _direction = mousePosition - rb.position;

        /*
        var hit = Physics2D.GetRayIntersection(new Ray(turret.position, _direction), 100f, ~0);
        lineHintRender.localScale = new Vector2(hit ? hit.distance : 100f, 0.05f);
        lineHintRender.localPosition = new Vector2(0.5f * lineHintRender.localScale.x + 0.5f, 0f);
        */
        


        _movement = Vector3.zero;
        _movement.x = Input.GetAxisRaw("Horizontal");
        // _movement.z = Input.GetAxisRaw("Vertical");
        if (_movement.magnitude > 0)
            _movement = _movement.normalized * mMoveSpeed;
        _movement.y = rb.velocity.y;
        rb.velocity = rb.velocity * 0.95f + _movement * 0.05f;

        if (Input.GetButtonDown("Fire1"))
        {
            soundGun.Play();
            if (GetComponent<TimeInverse>().timeDirection == 1)
                particleGun.Play();
        }


        if (Input.GetKey("space"))
        {
            if (Time.time - _leaveGround <= 0.2f)
            {
                Vector3 v = rb.velocity;
                v.y = 0.01f;
                rb.velocity = v;
                rb.AddForce(Vector2.up * 6 * rb.mass, ForceMode2D.Impulse);
                _leaveGround = 0;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contactCount <= 0) return;
        for (int i = 0; i < collision.contactCount; i++)
            if (collision.GetContact(i).rigidbody && !collision.GetContact(i).collider.isTrigger &&
                collision.GetContact(i).normal.y > 1e-4)
            {
                Debug.Log(collision.GetContact(i).normal);
                _leaveGround = Time.time;
                break;
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionStay2D(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
    }

    private void FixedUpdate()
    {
        if (TimeInverse.globalTimeDirection != GetComponent<TimeInverse>().timeDirection)
            return;

        // Set Player Direction 
        Transform trans = GetComponent<Transform>();
        Vector3 scale = trans.localScale;
        if (rb.velocity.x != 0)
        {
            scale.x = rb.velocity.x >= 0 ? 1 : -1;
            trans.localScale = scale;
        }
        turret.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(_direction.y, _direction.x) * 180 / Mathf.PI + (scale.x > 0 ? 0 : 180));

        // rb.MovePosition(rb.position + _movement * (Time.fixedDeltaTime));
    }

    public void InvertTime()
    {
        foreach (var t in gameObject.GetComponentsInChildren<TimeInverse>())
        {
            t.timeDirection ^= 1;
            t.NeedRefresh();
        }
    }
}
