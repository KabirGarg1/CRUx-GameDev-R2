using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*7f,rb.velocity.y);
            
        if (Input.GetButton("Jump"))
        { 
          rb.velocity = new Vector2(rb.velocity.x,7);
        }
        if (dirX > 0f){
            anim.SetBool("Running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f){
            anim.SetBool("Running", true);
            sprite.flipX = true;
        }
        else{
            anim.SetBool("Running", false);
        }
    }
}
