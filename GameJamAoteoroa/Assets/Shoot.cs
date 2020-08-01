using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootSpeed;

    bool shooting = false;
    Vector3 shootDirection;
    Vector3 mouse;
    public LineRenderer showShoot;
    public GameObject projectilePrefab;
    public Sprite[] sprites;
    //List<GameObject> projectiles = new List<GameObject>();
    public Transform goal;
    public float radius;
    GameObject currentProjectile;
    // Start is called before the first frame update
    void Start()
    {
        shootDirection = new Vector3(0, 0, 0);
        currentProjectile = Instantiate(projectilePrefab,transform);
        currentProjectile.transform.localPosition = Vector3.zero;
        currentProjectile.SetActive(true);
        currentProjectile.GetComponent<Rigidbody2D>().simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //currentProjectile.transform.position = transform.position;

        if (!shooting)
        {
            Vector3 p = transform.position;
            float bearing = Mathf.Atan2(mouse.y-goal.position.x, mouse.x-goal.position.x);
            transform.position = new Vector3(Mathf.Cos(bearing),Mathf.Sin(bearing),0)*radius+goal.position;
        }
        else
        {
            shootDirection = transform.position - mouse;
            showShoot.SetPositions(new Vector3[] { transform.position + shootDirection, transform.position });
            showShoot.enabled = true;
            if (Input.GetMouseButtonDown(1))
            {
                shooting = false;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!shooting)
            {
                shooting = true;
            }
            
           
        }
        if(Input.GetMouseButtonUp(0) && shooting)
        {



            Rigidbody2D rBody = currentProjectile.GetComponent<Rigidbody2D>();
            currentProjectile.GetComponent<Rigidbody2D>().simulated = true;
            currentProjectile.transform.parent = null;
            currentProjectile.GetComponent<Projectile>().shoot();
            rBody.velocity = shootSpeed * shootDirection;
            shooting = false;
            showShoot.enabled = false;

            currentProjectile = Instantiate(projectilePrefab, transform);
            currentProjectile.transform.localPosition = Vector3.zero;
            currentProjectile.transform.localScale *= Random.Range(0.5f, 1.5f);
            currentProjectile.GetComponent<SpriteRenderer>().sprite = sprites[(int)(Random.value * sprites.Length)];
            currentProjectile.SetActive(true);
            currentProjectile.GetComponent<Rigidbody2D>().simulated = false;
            Game.instance.projectiles.Add(currentProjectile.GetComponent<Projectile>());

        }



    }
}
