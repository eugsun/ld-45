using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack Types")]
    public GameObject red;
    public GameObject green;
    public GameObject blue;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine("Attack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(float speed)
    {
        int i = (int) Random.Range(0, 3);
        GameObject attack = GetAttack(i);
        GameObject inst = Instantiate(attack, transform);
        inst.GetComponent<EnemyAttack>().SetSpeed(speed);
        inst.GetComponent<EnemyAttack>().SetType(i);
    }

    GameObject GetAttack(int i)
    {
        switch (i)
        {
            case 0:
                return red;
            case 1:
                return green;
            case 2:
                return blue;
            default:
                return blue;
        }
    }
}
