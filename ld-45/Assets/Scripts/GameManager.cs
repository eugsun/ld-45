using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    // - Red: AI => Machine learning => Deep learning => Neural Network
    // - Green: Bitcoin => Blockchain => Altcoin => Satoshi
    // - Blue: Cloud => AWS => Microservices => Containers
    public string[] aWords = {"AI", "Machine Learning", "Deep Learning", "Neural Network"};
    public string[] bWords = {"Bitcoin", "Blockchain", "Altcoin", "Satoshi"};
    public string[] cWords = {"AWS", "Microservices", "Cloud", "Containers"};

    public static GameManager inst;

    [Header("Global States")]
    public int blood = 3;
    public int currLevel = 0;
    public int numActions = 3;
    public int numTargets = 3;
    public GameObject hearts;

    [Header("Attacks")]
    public GameObject ai;
    public GameObject blockchain;
    public GameObject cloud;
    public float attackSustain;
    public GameObject indicator;
    public Indicator indicatorScript;

    [Header("Entities")]
    public GameObject main;
    public GameObject junior;
    public GameObject sexy;
    public GameObject senior;

    [Header("Difficulty")]
    public float minInterval;
    public float maxInterval;
    public int maxSpeed = 4;

    [Header("Fund")]
    public int amount = 0;
    public GameObject fund;
    private TMPro.TextMeshProUGUI fundText;

    [Header("Sounds")]
    public AudioClip hitAudio;
    public AudioClip missAudio;
    public AudioClip pickupAudio;

    private void Awake()
    {
        if (inst == null)
        {
            DontDestroyOnLoad(gameObject);
            inst = this;
        } else if (inst != null)
        {
            Destroy(gameObject);
        }
        if (fund)
        {
            fundText = fund.GetComponent<TMPro.TextMeshProUGUI>();
        }
        if (indicator)
        {
            indicatorScript = indicator.GetComponent<Indicator>();
        }
    }

    void Start()
    {
        if (ai)
            ai.SetActive(false);
        if (blockchain)
            blockchain.SetActive(false);
        if (cloud)
            cloud.SetActive(false);
        if (fundText)
            UpdateFund();
        StartCoroutine("EnemyAttack");
        StartCoroutine("MakeHarder");
    }

    void Update()
    {

    }

    public void InitiateAction(int i)
    {
        main.GetComponent<Animator>().SetTrigger("attack");
        StartCoroutine("PlayerAttack", i);
    }

    IEnumerator PlayerAttack(int i)
    {
        bool success = false;
        GameObject attack = GetAction(i);
        if (indicatorScript.good && indicatorScript.activeAttack != null)
        {
            var att = indicatorScript.activeAttack.GetComponent<EnemyAttack>();
            if (att && att.type == i)
            {
                AudioSource.PlayClipAtPoint(pickupAudio, transform.position);
                success = true;
                amount += (int) (att.Value() * (float) Mathf.Min(1 + Time.timeSinceLevelLoad / 20 / 10, 2));
                UpdateFund();
                Destroy(indicatorScript.activeAttack);
            }
        }

        if (!success)
        {
            AudioSource.PlayClipAtPoint(missAudio, transform.position);
        }
        string words = "AI";
        if (i == 0)
        {
            words = aWords[(int)Random.Range(0, aWords.Length)];
        } else if (i == 1)
        {
            words = bWords[(int)Random.Range(0, bWords.Length)];
        } else if (i == 2)
        {
            words = cWords[(int)Random.Range(0, cWords.Length)];
        }

        attack.GetComponent<TMPro.TextMeshProUGUI>().text = words;
        attack.SetActive(true);
        yield return new WaitForSeconds(attackSustain);
        if (attack)
            attack.SetActive(false);
    }

    IEnumerator EnemyAttack()
    {
        var waitTime = Random.Range(minInterval, maxInterval);
        yield return new WaitForSeconds(waitTime);
        var enemyI = (int)Random.Range(0, numTargets);
        GameObject enemy = GetEnemy(enemyI);
        var attackSpeed = (float)Random.Range(1, maxSpeed);
        if (enemy)
        {
            enemy.GetComponent<Enemy>().Attack(attackSpeed);
            enemy.GetComponent<Animator>().SetTrigger("attack");
        }
        StartCoroutine("EnemyAttack");
    }

    IEnumerator MakeHarder()
    {
        yield return new WaitForSeconds(20);
        minInterval = Mathf.Max(1f, minInterval - 0.2f);
        maxInterval = Mathf.Max(2f, maxInterval - 0.2f);
        if (Time.timeSinceLevelLoad > 120)
        {
            maxSpeed += 1;
        }
        StartCoroutine("MakeHarder");
    }

    public GameObject GetEnemy(int i)
    {
        switch (i)
        {
            case 0:
                return junior;
            case 1:
                return sexy;
            case 2:
                return senior;
            default:
                return senior;
        }
    }

    public GameObject GetAction(int i)
    {
        switch (i)
        {
            case 0:
                return ai;
            case 1:
                return blockchain;
            case 2:
                return cloud;
            default:
                return cloud;
        }
    }

    public void TakeDamage()
    {
        AudioSource.PlayClipAtPoint(hitAudio, transform.position);
        main.GetComponent<Animator>().SetTrigger("hit");
        SetBlood(blood - 1);
        if (blood <= 0)
        {
            var audioSource = GetComponent<AudioSource>();
            audioSource.Stop();
            SceneManager.LoadScene("End");
        }
    }

    void SetBlood(int blood)
    {
        this.blood = blood;
        hearts.GetComponent<Hearts>().SetHearts(this.blood);
    }

    void UpdateFund()
    {
        var formatted = FormatFund();
        fundText.SetText(formatted);
    }

    public string FormatFund()
    {
        string amountStr = amount.ToString();
        string formatted = "";

        int counter = 0;
        for (int i = amountStr.Length - 1; i >= 0; i--)
        {
            formatted = formatted.Insert(0, amountStr[i].ToString());
            counter++;
            if (counter % 3 == 0 && i != 0)
            {
                formatted = formatted.Insert(0, ",");
            }
        }
        formatted = formatted.Insert(0, "$ ");
        return formatted;
    }
}
