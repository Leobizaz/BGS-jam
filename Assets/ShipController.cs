using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //variaveis publicas
    public float forwardSpeed = 1;
    public float maxVelocity = 5;
    
    //variaveis privadas
    [SerializeField] private float currentVelocity;

    //referencia publica
    public GameObject AreaClamp;
    public GameObject cursor;
    //referencias privadas
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            AimAtCursor();
            FreeMovement();
        }
        else
        {
            cursor.transform.position = transform.position;
        }

        MovementClamp();
    }

    void FreeMovement()
    {
        rb.AddRelativeForce(Vector3.up * forwardSpeed);
        currentVelocity = rb.velocity.magnitude;
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -5, 5), Mathf.Clamp(rb.velocity.y, -5, 5));
    }

    void AimAtCursor()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        ClampCursor();

    }

    void ClampCursor()
    {
        cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.localPosition = new Vector3(Mathf.Clamp(cursor.transform.localPosition.x, -3, 3), Mathf.Clamp(cursor.transform.localPosition.y, -3, 3), 0);

    }

    void MovementClamp()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -AreaClamp.transform.localScale.x/2, AreaClamp.transform.localScale.x/2), 
            Mathf.Clamp(transform.position.y, -AreaClamp.transform.localScale.y/2, AreaClamp.transform.localScale.y/2), 0);


    }

}
