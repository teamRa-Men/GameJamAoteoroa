using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game instance;
    public List<Projectile> projectiles;
    public Text showPoints, showProjectiles;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        projectiles = new List<Projectile>();
    }

    public void addPoints(int p) {
       
        StartCoroutine(add(projectiles.Count *p));
    }

    IEnumerator add(int count) {
        for (int i = 0; i < count; i++) {
            points++;
            showPoints.text = "" + points;
            yield return new WaitForSeconds(.05f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        showProjectiles.text = "x" + projectiles.Count;
    }
}