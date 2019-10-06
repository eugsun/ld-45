using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("States")]
    public int targetIndex;
    public int actionIndex;

    public const string RED = "#ff535c";
    public const string GREEN = "#63c74d";
    public const string BLUE = "#0095e9";

    public const float yTop = 1.61f;
    public const float yMid = 0.44f;
    public const float yBot = -0.79f;

    private Indicator indicator;
    public Animator animator;

    private void Awake()
    {
        indicator = GetComponentInChildren<Indicator>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        SetTargetIndex(0);
        SetActionIndex(0);
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                SetTargetIndex((targetIndex - 1 + GameManager.inst.numTargets) % GameManager.inst.numTargets);
            } else if (Input.GetAxisRaw("Vertical") < 0)
            {
                SetTargetIndex((targetIndex + 1) % GameManager.inst.numTargets);
            }
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                SetActionIndex((actionIndex - 1 + GameManager.inst.numActions) % GameManager.inst.numActions);
            } else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                SetActionIndex((actionIndex + 1) % GameManager.inst.numActions);
            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        GameManager.inst.InitiateAction(actionIndex);
    }

    void SetTargetIndex(int i)
    {
        targetIndex = i;
        var pos = indicator.transform.localPosition;
        indicator.transform.localPosition = new Vector3(pos.x, yTop, pos.z);
        if (i == 1)
        {
            indicator.transform.localPosition = new Vector3(pos.x, yMid, pos.z);
        } else if (i == 2)
        {
            indicator.transform.localPosition = new Vector3(pos.x, yBot, pos.z);
        }
    }

    void SetActionIndex(int i)
    {
        actionIndex = i;
        string color = RED;
        if (i == 1)
        {
            color = GREEN;
        } else if (i == 2)
        {
            color = BLUE;
        }
        indicator.SetColor(color);
    }
}
