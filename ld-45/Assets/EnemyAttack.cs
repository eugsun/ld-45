using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed = 1f;
    public int type = 0;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SetSpeed(speed);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        rb2d.velocity = new Vector2(-speed, 0);
    }

    public void SetType(int i)
    {
        type = i;
    }

    public int Value()
    {
        return (int) speed * 10000;
    }
}
