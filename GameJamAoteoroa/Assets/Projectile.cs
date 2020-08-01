using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform goal;
    public SpriteRenderer sprite;
    public Rigidbody2D rBody;
    float distance;
    public Color baseCol;
    bool orbit = false;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        baseCol = sprite.color;
        //sprite = this.GetComponent<SpriteRenderer>();
        //rBody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, goal.position);
        sprite.color = baseCol / distance * 2;

        radius = Vector3.Distance(transform.position, goal.position);
        if (radius < 4)
        {
            Vector3 r = goal.position - transform.position;
            rBody.AddForce(r);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.gameObject.tag);
        if (collision.gameObject.CompareTag("projectile"))
        {
            Destroy(gameObject);
        }
    }
    
}

