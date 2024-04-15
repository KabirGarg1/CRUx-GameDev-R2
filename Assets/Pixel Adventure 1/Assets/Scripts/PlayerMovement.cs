using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;



    [SerializeField] private LayerMask jumpableGround;

    private float dirX;
    private float timer = 0f;



    private enum MovementState { idle, running, jumping, falling };
    private MovementState state = MovementState.idle;

    [SerializeField] private AudioSource jumpSoundEffect;



    private void animationUpdate()
    {
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .01f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.01f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private bool gg()
    {

        if (!Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0.001f, Vector2.down, .1f, jumpableGround))
        {
            timer += Time.deltaTime;
            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                timer = 2f;
            }
            if (rb.velocity.y < 0 && timer < 2f)
            {
                return true;
            }
            return false;
        }
        else { timer = 0f; }

        return false;
    }
    private bool IsGrounded()
    {
        if (gg()) { return true; }
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

    }
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
        gg();
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, 7f);
        }

        animationUpdate();
    }
}