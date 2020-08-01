using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public Transform goal;
    public SpriteRenderer sprite;
    public Rigidbody2D rBody;
    float distance;
    public Color baseCol;
    bool orbit = false;
    float radius;
    float startBearing, bearing;
    public Text showOrbits;
    public Canvas canvas;
    int orbits;
  

    // Start is called before the first frame update
    void Start()
    {
        baseCol = sprite.color;
        //sprite = this.GetComponent<SpriteRenderer>();
        //rBody = this.GetComponent<Rigidbody2D>();
        showOrbits.color = Color.clear;
    }

    public void shoot() {
        startBearing = angleRange(Mathf.Atan2(transform.position.x, transform.position.y));

        transform.parent = canvas.transform;
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

        bearing = angleRange(Mathf.Atan2(transform.position.x, transform.position.y)) - startBearing;
        bearing = angleRange(bearing);
        
        
        showOrbits.rectTransform.localPosition = Vector3.zero+Vector3.up;

        if (Mathf.Abs(bearing - Mathf.PI) < 0.1f)
        {
            orbit = true;
        }
        if (orbit && bearing < 0.1f) {
            orbit = false;
            orbits++;
            showOrbits.text = "" + orbits ;
            Game.instance.addPoints(this);
            showOrbits.color = Color.white;
            StartCoroutine(show());

        }
       
        
       

    }
    IEnumerator show()
    {
        for (float i = 1; i >= 0; i-=0.1f)
        {
            showOrbits.color = new Color(1,1,1,i);
            yield return new WaitForSeconds(.1f);
        }
        showOrbits.color = Color.clear;
    }
    float angleRange(float a) {
        if (a < 0) {
            a += Mathf.PI * 2;
        }
        if (a > Mathf.PI * 2) {
            a -= Mathf.PI * 2;
        }
        return a;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.gameObject.tag);
        if (collision.gameObject.CompareTag("projectile"))
        {
            Game.instance.projectiles.Remove(this);
            Destroy(gameObject);
        }
    }
    
}

