using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public int stage = 0;
    public GameObject textObj;
    private TMPro.TextMeshProUGUI text;

    public AudioSource music;

    private void Awake()
    {
        text = textObj.GetComponent<TMPro.TextMeshProUGUI>();
        music = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetStage(0);
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            SetStage(stage + 1);
        }
    }

    void SetStage(int stage)
    {
        this.stage = stage;
        if (stage == 0)
        {
            text.text = "You have nothing.\n\n" +
                    "Except a decent command of buzzwords, and an upcoming appointment at a venture capital firm.\n\n" +
                    "How much money can you raise?";
        } else if (stage == 1)
        {
            text.text = "Up/Down (or W/S): Choose who to talk to.\n\n" +
                        "Left/Right (or A/D): Choose what to talk about.\n\n" +
                        "Tap Enter (or J, Left Mouse Click) at the right time to yell out some buzzword and profit.";

        } else
        {
            SceneManager.LoadScene("Game");
        }
    }
}
