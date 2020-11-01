using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownPlatform : MonoBehaviour
{
    bool playerOnTop;
    public bool keepSprite = false;
    private PlatformEffector2D effector;
    public float wait = 0.3f;
    float normalOffset;
    [SerializeField] Collider2D[] neighbors;
    BoxCollider2D boxcollider;
    ContactFilter2D contactFilter = new ContactFilter2D();
    void Start()
    {
        contactFilter.layerMask = 8;
        boxcollider = GetComponent<BoxCollider2D>();
        neighbors = new Collider2D[2];
        effector = GetComponent<PlatformEffector2D>();
        normalOffset = effector.rotationalOffset;
        boxcollider.OverlapCollider(contactFilter, neighbors);
        if (!keepSprite)
        {
            GetComponent<SpriteRenderer>().enabled = false;  
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerOnTop = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerOnTop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerOnTop = false;
        }
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") >= 0)
        {
            StartCoroutine(Wait());
        }

        if (Input.GetAxis("Vertical") <= -0.5f)
        {
            wait = 0;
            if (wait <= 0.3 && Input.GetKey(KeyCode.Space) && playerOnTop)
            {
                Twist();
                TwistNeighbors(null);   
            }
        }
        if (wait == 0.3f)
        {
            StopAllCoroutines();
            UnTwist();
            UnTwistNeighbors(null);
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        wait = 0.3f;
    }

    void TwistNeighbors(GameObject exclude)
    {
        foreach(Collider2D collider in neighbors)
        {
            if (collider != null)
            {
                if (collider.gameObject.GetComponent<DownPlatform>())
                {
                    collider.gameObject.GetComponent<DownPlatform>().Twist();
                    //if (collider.gameObject != exclude) collider.gameObject.GetComponent<DownPlatform>().TwistNeighbors(this.gameObject);
                }
            }
        }
    }

    void UnTwistNeighbors(GameObject exclude)
    {
        if (neighbors.Length > 0)
        {
            foreach (Collider2D collider in neighbors)
            {
                if (collider != null)
                {
                    if (collider.gameObject.GetComponent<DownPlatform>() != null)
                    {

                        collider.gameObject.GetComponent<DownPlatform>().UnTwist();
                        //if (collider.gameObject != exclude) collider.gameObject.GetComponent<DownPlatform>().UnTwistNeighbors(this.gameObject);
                    }
                }
            }
        }
    }

    public void Twist()
    {
        effector.rotationalOffset = normalOffset - 180;
        boxcollider.enabled = false;
    }

    public void UnTwist()
    {
        effector.rotationalOffset = normalOffset;
        boxcollider.enabled = true;
    }
}
