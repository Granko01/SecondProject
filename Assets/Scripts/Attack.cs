using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    [Header("Gameobjects")]
    public List<GameObject> Allies = new List<GameObject>();
    public List<GameObject> Enemies = new List<GameObject>();
    public GameObject CriticalHit;
    public GameObject NormalHit;

    [Header("Prefabs")]
    private GameObject[] AllyPrefabs;
    private GameObject[] EnemyPrefabs;

    [Header("Spawnpoints")]
    public Transform[] AllySpawnPoints;
    public Transform[] EnemySpawnPoints;

    [Header("Attack Animations")]
    public GameObject AttackGameObject;
    public GameObject Attack1GameObject;
    public GameObject DefendGameObject;

    public FundsManager fundsManager;
    public LevelLogic levelLogic;
    public int LevelCount = 0;
    public Button AttackButton;
    public Button DefendButton;

    public GameObject AlliesHolder;
    public GameObject EnemiesHolder;
    public GameObject MinionAllie;
    public int teamHPIncreasePerLevel = 50; 
    public float enemyCriticalChance = 0.1f;
    public int baseEnemyDamage = 25;
    public int WinFundsPerLevel;

    private int allySpawnedCount = 0;
    private int enemySpawnedCount = 0;

    [Header("Team HP")]
    public int AlliesMaxHP;
    public int AlliesCurrentHP;

    public int EnemiesMaxHP;
    public int EnemiesCurrentHP;

    public Scrollbar AlliesHealthSlider;
    public Text AlliesHealthText;

    public Scrollbar EnemiesHealthSlider;
    public Text EnemiesHealthText;

    public int UsedDefendButton = 0;
    public int UsedBomb = 0;

    public GameObject HealthImg;
    public GameObject BombImg;
    public GameObject FreezeImg;
    public GameObject DefendImg;

    public GameObject YouWin;
    public GameObject YouLose;

    void Start()
    {
        AllyPrefabs = Resources.LoadAll<GameObject>("instantiate/allies");
        EnemyPrefabs = Resources.LoadAll<GameObject>("instantiate/enemies");
        

        if (fundsManager != null && fundsManager.ProgressLevel > 0)
        {
            for (int i = 0; i < AlliesHolder.transform.childCount; i++)
            {
                GameObject ally = AlliesHolder.transform.GetChild(i).gameObject;
                Allies.Add(ally);
            }

            for (int i = 0; i < EnemiesHolder.transform.childCount; i++)
            {
                GameObject enemy = EnemiesHolder.transform.GetChild(i).gameObject;
                Enemies.Add(enemy);
            }
            if (fundsManager.MinionState == 1)
            {
                MinionAllie.gameObject.SetActive(true);
                AlliesMaxHP += 50;
            }
            
            AlliesMaxHP = fundsManager.ProgressLevel;
            AlliesCurrentHP = AlliesMaxHP;
            EnemiesMaxHP = 100;
            EnemiesCurrentHP = EnemiesMaxHP;

            RefreshHealthUI();
        }
        else
        {
            Debug.LogWarning("FundsManager not initialized yet!");
        }
    }

    public void HealthPower()
    {
        if (fundsManager.RestoreHealthAmount >= 1)
        {
            fundsManager.RestoreHealthAmount--;
            fundsManager.RestoreText[0].text = fundsManager.RestoreHealthAmount.ToString();
            fundsManager.SaveRestorePower();
            AlliesCurrentHP += 100;
            AlliesMaxHP = AlliesCurrentHP;
            AlliesHealthText.text = AlliesMaxHP.ToString();
            HealthImg.gameObject.SetActive(true);
           StartCoroutine(WaitForHealthImg());
        }
        else
        {
            Debug.Log("Not enough Restore health");
        }
    }

    public void RemoveHPtoEnemyPower()
    {
        if (fundsManager.BombAmount >= 1)
        {
            fundsManager.BombAmount--;
            fundsManager.BombText[0].text = fundsManager.BombAmount.ToString();
            fundsManager.SaveBombPower();
            EnemiesCurrentHP -= 75;
            EnemiesMaxHP = EnemiesCurrentHP;
            EnemiesHealthText.text = EnemiesMaxHP.ToString();
            UsedBomb++;
            BombImg.gameObject.SetActive(true);
            StartCoroutine(WaitForBombImg());
            if (EnemiesCurrentHP <= 0)
            {
                WinFundsPerLevel += 50;
                fundsManager.FundsAmount += WinFundsPerLevel;
                fundsManager.FundsText[0].text = WinFundsPerLevel.ToString();
                fundsManager.SaveFunds();
                ProceedToNextLevel();
            }
        }
        else
        {
            Debug.Log("Not enough Bomb amount");
        }
    }

    public void FreezeEnemy()
    {
        if (fundsManager.FreezeAmount >= 1)
        {
            fundsManager.FreezeAmount--;
            fundsManager.FreezeText[0].text = fundsManager.FreezeAmount.ToString();
            fundsManager.SaveFreezePower();
            EnemiesCurrentHP -= 25;
            EnemiesMaxHP = EnemiesCurrentHP;
            EnemiesHealthText.text = EnemiesMaxHP.ToString();
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemies");
            Debug.Log("Found enemies: " + allEnemies.Length);

            foreach (GameObject enemy in allEnemies)
            {
                Transform freezeImageTransform = enemy.transform.Find("FreezeImage");  

                if (freezeImageTransform != null)
                {
                    freezeImageTransform.gameObject.SetActive(true);
                }
            }

            FreezeImg.gameObject.SetActive(true);

            StartCoroutine(DeactivateEnemiesAfterDelay(allEnemies));
            StartCoroutine(WaitForFreezeImg());
        }
    }


    private IEnumerator DeactivateEnemiesAfterDelay(GameObject[] enemiesToDeactivate)
    {
        yield return new WaitForSeconds(3f);

        foreach (GameObject enemy in enemiesToDeactivate)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }

    public void Defend()
    {
        UsedDefendButton++;
        DefendGameObject.gameObject.SetActive(true);
        StartCoroutine(WaitForSecDefend());
    }

    void RefreshHealthUI()
    {
        if (AlliesHealthSlider != null)
            AlliesHealthSlider.size = AlliesMaxHP > 0 ? Mathf.Clamp01((float)AlliesCurrentHP / AlliesMaxHP) : 0;

        if (EnemiesHealthSlider != null)
            EnemiesHealthSlider.size = EnemiesMaxHP > 0 ? Mathf.Clamp01((float)EnemiesCurrentHP / EnemiesMaxHP) : 0;

        if (AlliesHealthText != null)
            AlliesHealthText.text = AlliesCurrentHP.ToString();

        if (EnemiesHealthText != null)
            EnemiesHealthText.text = EnemiesCurrentHP.ToString();
    }

    IEnumerator WaitForSecDefend()
    {
        AttackButton.interactable = false;
        yield return new WaitForSeconds(2f);
        DefendGameObject.gameObject.SetActive(false);
        AttackButton.interactable = false;
        UsedDefendButton--;
        EnemyAttackMethod();
    }

    IEnumerator WaitForHealthImg()
    {
        yield return new WaitForSeconds(2);
        HealthImg.gameObject.SetActive(false);
    }
    IEnumerator WaitForBombImg()
    {
        AttackButton.interactable = false;
        DefendButton.interactable = false;
        yield return new WaitForSeconds(2);
        BombImg.gameObject.SetActive(false);
        EnemyAttackMethod();
    }
    IEnumerator WaitForFreezeImg()
    {
        yield return new WaitForSeconds(2);
        FreezeImg.gameObject.SetActive(false);
    }
    IEnumerator WaitForDefendAllImg()
    {
        yield return new WaitForSeconds(2);
        DefendImg.gameObject.SetActive(false);
    }

    IEnumerator WaitForSec()
    {
        AttackButton.interactable = false;
        yield return new WaitForSeconds(2f);
        AttackGameObject.SetActive(false);
        NormalHit.SetActive(false);
        CriticalHit.SetActive(false);

        if (EnemiesCurrentHP > 0)
        {
            yield return new WaitForSeconds(1f);
            EnemyAttackMethod();
        }
        else
        {
            Debug.Log("All enemies defeated!");

            WinFundsPerLevel += 50;
            fundsManager.FundsAmount += WinFundsPerLevel;
            fundsManager.FundsText[0].text = WinFundsPerLevel.ToString();
            fundsManager.SaveFunds();
            ProceedToNextLevel();
        }

        AttackButton.interactable = true;
    }

    IEnumerator WaitForSec1()
    {
        UsedBomb--;
        yield return new WaitForSeconds(2f);
        Attack1GameObject.SetActive(false);
        NormalHit.SetActive(false);
        CriticalHit.SetActive(false);
        AttackButton.interactable = true;
        DefendButton.interactable = true;
    }

    IEnumerator WaitForEnemy()
    {
        yield return new WaitForSeconds(2f);
    }

    public void AttachMethod()
    {
            int damage = 50;
            if (UnityEngine.Random.value <= 0.2f)
            {

                damage += 50;
                CriticalHit.SetActive(true);
            }
            else
            {
                NormalHit.SetActive(true);
            }

            EnemiesCurrentHP -= damage + fundsManager.AttackLevel;
            if (EnemiesCurrentHP < 0) EnemiesCurrentHP = 0;
            AttackGameObject.SetActive(true);
            RefreshHealthUI();
            StartCoroutine(WaitForSec());
    }

    public void EnemyAttackMethod()
    {
        StartCoroutine(WaitForEnemy());
        int damage = baseEnemyDamage;
        if (UnityEngine.Random.value <= enemyCriticalChance)
        {
            if (UsedDefendButton == 1)
            {
                damage = 0;
                CriticalHit.SetActive(true);
            }
            else
            {
                damage += 50;
                CriticalHit.SetActive(true);
            }
        }
        else
        {
            NormalHit.SetActive(true);
        }

        AlliesCurrentHP -= damage;
        if (AlliesCurrentHP < 0) AlliesCurrentHP = 0;

        RefreshHealthUI();

        if (AlliesCurrentHP <= 0)
        {
            fundsManager.FundsAmount += WinFundsPerLevel;
            fundsManager.FundsText[0].text = WinFundsPerLevel.ToString();
            fundsManager.SaveFunds();
            YouLose.gameObject.SetActive(true);
            return;
        }
        Attack1GameObject.SetActive(true);
        StartCoroutine(WaitForSec1());
    }
    int allAlliesHP;
    int allEnemiesHP;
    void ProceedToNextLevel()
    {
        YouWin.gameObject.SetActive(true);
        bool addNewUnits = LevelCount < 7 && UnityEngine.Random.Range(0, 2) == 0;

        if (addNewUnits && allySpawnedCount < AllyPrefabs.Length && enemySpawnedCount < EnemyPrefabs.Length)
        {
            GameObject allyPrefab = AllyPrefabs[allySpawnedCount];
            Transform allySpawn = AllySpawnPoints[allySpawnedCount];
            GameObject newAlly = Instantiate(allyPrefab, allySpawn.position, Quaternion.identity, AlliesHolder.transform);
            Allies.Add(newAlly);
            allySpawnedCount++;

            GameObject enemyPrefab = EnemyPrefabs[enemySpawnedCount];
            Transform enemySpawn = EnemySpawnPoints[enemySpawnedCount];
            GameObject newEnemy = Instantiate(enemyPrefab, enemySpawn.position, Quaternion.identity, EnemiesHolder.transform);
            Enemies.Add(newEnemy);
            enemySpawnedCount++;
        }
        else
        {
            AlliesMaxHP += teamHPIncreasePerLevel;
            EnemiesMaxHP += teamHPIncreasePerLevel;
            AlliesCurrentHP = AlliesMaxHP;
            EnemiesCurrentHP = EnemiesMaxHP;
        }

        LevelCount++;

        if (LevelCount < levelLogic.LevelsTexts.Length)
        {
            levelLogic.LevelsTexts[LevelCount].gameObject.SetActive(true);
            if (LevelCount - 1 >= 0)
                levelLogic.LevelsTexts[LevelCount - 1].gameObject.SetActive(false);
        }

        AlliesHealthSlider.size = 1;
        EnemiesHealthSlider.size = 1;
        AlliesHealthText.text = AlliesMaxHP.ToString();
        EnemiesHealthText.text = EnemiesMaxHP.ToString();
    }

    public void WinPanel()
    {
        if (YouWin.activeSelf)
        {
            YouWin.gameObject.SetActive(false);
        }
        else
        {
            YouWin.gameObject.SetActive(true);
        }
    }

    public void LostPanel()
    {
        if (YouLose.activeSelf)
        {
            YouLose.gameObject.SetActive(false);
        }
        else
        {
            YouLose.gameObject.SetActive(true);
        }
    }

}
