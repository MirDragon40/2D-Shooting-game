using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float BoomTimer;
    public
    void Start()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");    // 복수형
        Debug.Log(enemies.Length);
        for (int i = 0; i< enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            enemy.Death();
            enemy.MakeItem();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        /*
        if (collider.tag == "Enemy")
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            
            if (enemy != null)
            {
                enemy.Death();

            }
        }
        */
        Enemy enemy = collider.GetComponent<Enemy>();
        enemy.Death();

    }

    private void Update()
    {
        BoomTimer += Time.deltaTime;
        if (BoomTimer >= 3f)
        {
            Destroy(this.gameObject);
        }
    }
}
