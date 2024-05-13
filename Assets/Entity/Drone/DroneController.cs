using REnemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    [SerializeField] private float speed;
    [SerializeField] private float radius;
    [SerializeField] private float distance;

    private float angle = 0f;
    private bool showEnemy;
    private Rigidbody2D rb;

    private void Start()
    {
        Player = GameObject.Find("Player");
        rb= GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (showEnemy)
        {
            MoveToEnemy(Enemy.transform.position);
        }
        else
        {
            if (Vector2.Distance(transform.position, Player.transform.position) > distance)
            {
                MoveToEnemy(Player.transform.position);
            }
            if (Vector2.Distance(transform.position, Player.transform.position)< distance)
                MoveToCircle();
        }
        if (Enemy != null)
        {
            CheckDistanceToEnemy();
        }
    }

    private void CheckDistanceToEnemy()
    {
        if (Vector2.Distance(transform.position, Enemy.transform.position) < 0.1f)
        {
            if (Enemy.GetComponent<Enemy>().ReadOnly_Enemy_Attacking.ApplyDamageCheck(100))
            {
                Destroy(gameObject);
            }
        }
    }

    private void MoveToCircle()
    {
            angle += speed * Time.deltaTime;
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            transform.position = Player.transform.position + offset;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Enemy = collision.gameObject;
            showEnemy = true;
        }
    }

    private void MoveToEnemy(Vector3 target)
    {
        Vector2 direction = -(transform.position - target).normalized;
        direction *= speed;
        rb.velocity = direction;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            showEnemy = false;
        }
     }
 }
