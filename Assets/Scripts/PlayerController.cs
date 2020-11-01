using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Ludiq.Peek;

public class PlayerController : MonoBehaviour
{
    //variaveis publicas
    public float speed = 5;
    public float jumpForce = 5;
    public float acceleration = 0.04f;
    public float decceleration = 0.01f;
    public bool handsBusy;
    //variaveis privadas
    private bool wantToJump;
    private bool isMoving;
    private bool hasSquashed;
    private bool facingLeft;
    private bool facingRight;
    private float x = 0;
    private float y = 0;
    private float yVelocity;
    private float jumpCooldown;
    private bool jumpBlock;
    private GameObject holdingObject;
    //referencias publicas
    public Animator playerAnim;
    public GameObject playerRenderer;
    public GameObject pivot;
    public GameObject holdPosition;
    public GameObject maos;
    //referencias privadas
    Animator renderAnimator;
    Rigidbody2D rb;
    PlayerCollision playerCollision;

    private void Awake()
    {
        renderAnimator = playerRenderer.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<PlayerCollision>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            transform.position = new Vector3(0, -2, 0);
        }
        Pulo();
        ObjectHandling();
        if (isMoving) playerAnim.SetBool("isMoving", true); else playerAnim.SetBool("isMoving", false);
        if (playerCollision.onGround || playerCollision.onGroundCoyote) playerAnim.SetBool("isGrounded", true); else playerAnim.SetBool("isGrounded", false);
    }

    private void FixedUpdate()
    {
        Movimentação();
        SquashStretch();
        //RenderRotation();
    }

    public void HoldObject(GameObject obj)
    {
        if (holdingObject == null)
        {
            holdingObject = obj;
            handsBusy = true;
            if (holdingObject.GetComponent<Rigidbody2D>())
            {
                holdingObject.GetComponent<BoxCollider2D>().enabled = false;
                holdingObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
    }

    public void DropObject()
    {
        if(holdingObject != null)
        {
            Invoke("DROPAAA", 0.2f);
            if (facingRight)
            {
                holdingObject.transform.DOMove(new Vector2(transform.position.x + 1, holdingObject.transform.position.y), 0.2f).SetEase(Ease.Linear);
                //holdingObject.transform.position = new Vector2(transform.position.x + 2, holdingObject.transform.position.y);
            }
            else
            {
                holdingObject.transform.DOMove(new Vector2(transform.position.x - 1, holdingObject.transform.position.y), 0.2f).SetEase(Ease.Linear);
                //holdingObject.transform.position = new Vector2(transform.position.x - 2, holdingObject.transform.position.y);
            }
        }
    }

    void DROPAAA()
    {
        if (holdingObject.GetComponent<Rigidbody2D>())
        {
            holdingObject.GetComponent<BoxCollider2D>().enabled = true;
            holdingObject.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        holdingObject = null;
        handsBusy = false;
    }

    void ObjectHandling()
    {
        if (handsBusy)
        {
            maos.SetActive(true);
        }
        else
        {
            maos.SetActive(false);
        }

        if(holdingObject != null)
        {
            holdingObject.transform.position = holdPosition.transform.position;
        }
    }

    private void Pulo()
    {
        if (Input.GetButtonDown("Jump"))
        {
            CancelInvoke("ResetWantToJump");
            Invoke("ResetWantToJump", 0.07f);
            wantToJump = true;
        }

        if ((playerCollision.onGround || playerCollision.onGroundCoyote) && jumpCooldown <= 0 && wantToJump == true)
        {
            Jump();
            jumpCooldown = 0.5f;
        }

        if (jumpCooldown > 0)
        {
            jumpCooldown = jumpCooldown - Time.deltaTime;
        }
    }

    private void Movimentação()
    {
        if (rb.velocity.x <= 0 && isMoving)
        {
            Debug.Log("Esq");
            pivot.transform.localScale = new Vector3(-1, 1, 1);
            facingLeft = true;
            facingRight = false;
        }
        else if (rb.velocity.x >= 0 && isMoving)
        {
            Debug.Log("Dir");
            pivot.transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
            facingLeft = false;
        }





        //if (!playerCollision.onGround) acceleration = Mathf.SmoothDamp(acceleration, 0, ref yVelocity, 0.1f);
        //else acceleration = 0.04f;


        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            isMoving = true;
            x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, acceleration);
        }
        else
        {
            isMoving = false;
            x = Mathf.SmoothDamp(x, Input.GetAxis("Horizontal"), ref yVelocity, decceleration);
        }

        Vector2 dir = new Vector2(x, y);
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

    }

    private void Jump()
    {
        //playerCollision.coyote = true;
        playerCollision.onGroundCoyote = false;
        playerAnim.Play("jump");
        wantToJump = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
        jumpBlock = true;
        Invoke("ResetJumpBlock", 0.4f);
        hasSquashed = false;
    }

    void ResetJumpBlock()
    {
        jumpBlock = false;
    }

    void ResetWantToJump()
    {
        wantToJump = false;
    }

    void RenderRotation()
    {

        if (isMoving)
        {
            if (facingLeft)
            {
                if (!playerCollision.onAir)
                    playerRenderer.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.3f);
                else
                    playerRenderer.transform.DOLocalRotate(new Vector3(0, 0, -10), 0.1f);
            }
            else if (facingRight)
            {
                if (!playerCollision.onAir)
                    playerRenderer.transform.DOLocalRotate(new Vector3(0, 0, -10), 0.3f);
                else
                    playerRenderer.transform.DOLocalRotate(new Vector3(0, 0, 10), 0.1f);
            }
        }
        else
        {
            playerRenderer.transform.DOLocalRotate(Vector3.zero, 0.3f);
        }

    }

    void SquashStretch()
    {
        if (!jumpBlock)
        {
            if (!playerCollision.onAir && !hasSquashed)
            {
                CancelInvoke("Cu");
                Invoke("Cu", 0.5f);
                renderAnimator.enabled = true;
                renderAnimator.Play("squash");
                hasSquashed = true;
            }
        }
    }

    void Cu()
    {
        renderAnimator.enabled = false;
    }

}
