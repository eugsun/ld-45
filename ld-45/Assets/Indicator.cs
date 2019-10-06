using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool good = false;
    public GameObject activeAttack;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SetColor(string colorStr)
    {
        Color color;
        ColorUtility.TryParseHtmlString(colorStr, out color);
        spriteRenderer.color = color;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        good = false;
        activeAttack = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        good = true;
        activeAttack = collision.gameObject;
    }
}
