using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //variaveis publicas
    public float forwardSpeed = 1;
    public float breakDecceleration = 1;
    public float rotationSpeed = 150;
    public float maxVelocity = 5;
    public Color32 cageColor;
    public Color32 CageDangerColor;
    [SerializeField] public static int currentCapacity = 0;

    //variaveis privadas
    [SerializeField] private float currentVelocity;
    [SerializeField] private float cursorX;
    [SerializeField] private float cursorY;
    private float cursorDistance;
    private float yVelocity;
    private float xVelocity;
    private float zVelocity;
    private bool isMoving;

    //referencia publica
    public ParticleSystem CageWall;
    public ParticleSystem CageProjection;
    public ParticleSystem FX_Thruster;
    public GameObject AreaClamp;
    public GameObject cursor;
    public GameObject CM_targetGroup;
    //referencias privadas
    Rigidbody2D rb;
    HingeJoint2D GravAnchorHinge;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GravAnchorHinge = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isMoving = true;
            FX_Thruster.Play();
        }
        else
        {
            FX_Thruster.Stop();
            isMoving = false;
            Freios();

            cursor.transform.position = transform.position;
            cursor.SetActive(false);
        }

        MovementClamp();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            AimAtCursor();
            FreeMovement();

        }

        HingeClamp();
    }

    void HingeClamp()
    {
    }

    void FreeMovement()
    {
        cursorDistance = Vector2.Distance(cursor.transform.position, this.transform.position);
        cursorDistance = Mathf.Clamp(cursorDistance, 0.1f, 30f);
        rb.AddRelativeForce(Vector3.up * forwardSpeed * (cursorDistance*2));
        currentVelocity = rb.velocity.magnitude;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5, 5), Mathf.Clamp(rb.velocity.y, -5, 5));

        //damp

        rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0, ref xVelocity, breakDecceleration), Mathf.SmoothDamp(rb.velocity.y, 0, ref yVelocity, breakDecceleration));

    }

    void AimAtCursor()
    {
        cursor.SetActive(true);
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle - 90), Time.deltaTime * rotationSpeed);
        //transform.rotation = Quaternion.Euler(0, 0, 0);

        cursorX = Input.mousePosition.x;
        cursorY = Input.mousePosition.y;

        cursorX = Mathf.Clamp(cursorX, 0, 1920);
        cursorY = Mathf.Clamp(cursorY, 0, 1080);

        Vector2 mousePos = new Vector2(cursorX, cursorY);

        cursor.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, 0);


        CM_targetGroup.transform.localPosition = new Vector3(
            Mathf.Clamp(CM_targetGroup.transform.localPosition.x, -3, 3),
            Mathf.Clamp(CM_targetGroup.transform.localPosition.y, -3, 3), 0);

    }

    void Freios()
    {
        rb.velocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0, ref xVelocity, breakDecceleration), Mathf.SmoothDamp(rb.velocity.y, 0, ref yVelocity, breakDecceleration));
    }

    void MovementClamp()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -AreaClamp.transform.localScale.x/2, AreaClamp.transform.localScale.x/2), 
            Mathf.Clamp(transform.position.y, -AreaClamp.transform.localScale.y/2, AreaClamp.transform.localScale.y/2), 0);


    }

}
