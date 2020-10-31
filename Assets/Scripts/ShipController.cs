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
    public int currentLife = 3;
    public Color cageColor;
    public Color CageDangerColor;
    [SerializeField] public static int currentCapacity = 0;
    public float currentMass = 0;

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
    public ParticleSystem FX_Thruster;
    public ParticleSystem FX_CageWall;
    public ParticleSystem FX_Release;
    public GameObject AreaClamp;
    public GameObject cursor;
    public GameObject CM_targetGroup;
    public GameObject GravCageCenter;
    public Collider2D gravCageCol;
    public GameObject CageBanish;
    public Animator shieldAnim;
    //referencias privadas
    Rigidbody2D rb;
    HingeJoint2D GravAnchorHinge;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GravAnchorHinge = GetComponent<HingeJoint2D>();
    }

    private void Start()
    {
        GameEvents.current.onCollectCargo += AddMass;
        GameEvents.current.onLoseCargo += LoseMass;
        GameEvents.current.onLoseCargo += UpdateCageEmitter;
        GameEvents.current.onCollectCargo += UpdateCageEmitter;

    }

    private void Update()
    {

        if(currentLife <= 0)
        {
            //ded

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ManualRelease();
        }

        if (Input.GetMouseButton(0))
        {
            isMoving = true;
            if(!FX_Thruster.isPlaying) FX_Thruster.Play();
        }
        else
        {
            if(FX_Thruster.isPlaying) FX_Thruster.Stop();
            isMoving = false;
            Freios();

            cursor.transform.position = transform.position;
            cursor.SetActive(false);
        }



        //MovementClamp();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            AimAtCursor();
            FreeMovement();

        }
    }

    void AddMass(float mass)
    {
        gravCageCol.enabled = true;
        currentMass += mass;
    }

    void LoseMass(float mass)
    {
        currentMass -= mass;
    }

    void ManualRelease()
    {
        gravCageCol.enabled = false;

        //FX_Release.Play();

        Vector3 explosionPos = GravCageCenter.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, 10);


        foreach (Collider2D hit in colliders)
        {

            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            Vector2 dir = hit.transform.position - explosionPos;
            if (rb != null)
                rb.AddForce(dir.normalized * 5, ForceMode2D.Impulse);

        }

        if (FX_CageWall.isPlaying) FX_CageWall.Stop();
    }

    void CageRelease()
    {
        gravCageCol.enabled = false;

        FX_Release.Play();

        Vector3 explosionPos = GravCageCenter.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, 10);


        foreach(Collider2D hit in colliders)
        {
            
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            Vector2 dir = hit.transform.position - explosionPos;
            if (rb != null)
                rb.AddForce(dir.normalized * 20, ForceMode2D.Impulse);

        }

        if (FX_CageWall.isPlaying) FX_CageWall.Stop();


        //CageBanish.SetActive(true);
        //Invoke("DeactivateBanish", 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Hazard")
        {
            shieldAnim.Play("shield_Hit");
            currentLife--;
        }
    }


    void UpdateCageEmitter(float mass)
    {
        Debug.Log(currentCapacity);
        Debug.Log(currentMass);
        if (currentCapacity > 0)
        {
            if(!FX_CageWall.isPlaying) FX_CageWall.Play();

            if(currentCapacity > 3)
            {
                //danger
                //FX_CageWall.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color", CageDangerColor);

                if(currentCapacity > 5)
                {
                    //CageRelease();
                }

            }
            else
            {
                //FX_CageWall.GetComponent<ParticleSystemRenderer>().material.SetColor("_Color", cageColor);
            }

        }
        else
        {
            if (FX_CageWall.isPlaying) FX_CageWall.Stop();
        }
    }

    void FreeMovement()
    {
        cursorDistance = Vector2.Distance(cursor.transform.position, this.transform.position);
        cursorDistance = Mathf.Clamp(cursorDistance, 0.1f, 30f);
        rb.AddRelativeForce(Vector3.up * forwardSpeed * (cursorDistance*2));
        currentVelocity = rb.velocity.magnitude;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -10, 10), Mathf.Clamp(rb.velocity.y, -10, 10));

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
