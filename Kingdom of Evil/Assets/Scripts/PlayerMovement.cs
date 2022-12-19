using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float fightRange = 0.5f;
    public LayerMask enemyLayers;
    public Transform fightPoint;
    Rigidbody2D myRigidbody;
    Vector2 moveInput;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())//using for stop fight anim while pause menu
        {
           if(!isAlive)
           {
              return;
           }

           Run();
           FlipSprite();
           ClimbLadder();
           Die();
    
           if (Input.GetKeyDown(KeyCode.Mouse0))
           {
             Fight();
           } 
        }
    }

    void Fight()
    {
        myAnimator.SetTrigger("Fighting");//play an attack animation
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fightPoint.position, fightRange, enemyLayers);//detect enemies in range of attack

        foreach(Collider2D enemy in hitEnemies)//damage them
        {
          enemy.GetComponent<EnemyMovement>().TakeDamage(20);
        } 
        
    }
    void OnDrawGizmosSelected()
    {
        if(fightPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(fightPoint.position, fightRange);
    }

  
    void OnMove(InputValue value)
    {
        if(!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>(); 
    }

    void OnJump(InputValue value)
    {   
        if(!isAlive)
        {
            return;
        }

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if(value.isPressed )
        {
            //do staff
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);    
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;// method that knows if we are moving
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);// make if u running true if not false
    }

    void FlipSprite()
    {
       bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
       
       if(playerHasHorizontalSpeed)
       {
          transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
       }
    }

    void ClimbLadder()
    {   
      if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
      {
         myRigidbody.gravityScale = gravityScaleAtStart;
         myAnimator.SetBool("isClimbing", false);
         return;
      }
      Vector2 climbVelocity = new Vector2( myRigidbody.velocity.x, moveInput.y * climbSpeed);
      myRigidbody.velocity = climbVelocity;
      myRigidbody.gravityScale = 0f;
      bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
      myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
        
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
 
}
