using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    public GameObject amountObj;
    private TMPro.TextMeshProUGUI amountText;

    public GameObject conclusion;
    private TMPro.TextMeshProUGUI conclusionText;

    public GameObject arrow;
    public bool allowRestart = false;

    private void Awake()
    {
        amountText = amountObj.GetComponent<TMPro.TextMeshProUGUI>();
        conclusionText = conclusion.GetComponent<TMPro.TextMeshProUGUI>();
        arrow.SetActive(false);
        allowRestart = false;
        StartCoroutine("EnableRestart");
    }

    IEnumerator EnableRestart()
    {
        yield return new WaitForSeconds(1);
        allowRestart = true;
        arrow.SetActive(true);
    }

    void Start()
    {
        amountText.text = GameManager.inst.FormatFund();
        int amount = GameManager.inst.amount;
        if (amount < 100000)
        {
            conclusionText.text = "Mom is still proud of you.";
        } else if (amount < 200000)
        {
            conclusionText.text = "Enough for a few months of living in San Francisco.";
        } else if (amount < 500000)
        {
            conclusionText.text = "Enough to actually start a company. Of course, you hated that responsibility.";
        } else if (amount < 1000000)
        {
            conclusionText.text = "Dedication paid off. You are now the protagonist of Bad Blood.";
        } else
        {
            conclusionText.text = "Please take a screenshot and reach out to me. I'll buy you a beer.";
        }
    }

    void Update()
    {
        if (allowRestart && Input.GetButtonUp("Fire1"))
        {
            Destroy(GameManager.inst);
            GameManager.inst = null;
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }

}
