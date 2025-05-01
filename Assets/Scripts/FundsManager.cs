using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FundsManager : MonoBehaviour
{
    [Header("INT")]
    public int FundsAmount;
    public int RestoreHealthAmount;
    public int BombAmount;
    public int FreezeAmount;
    public int ShieldAmount;
    public int LevelGeneratedFunds;
    public int DodgeLevel;
    public int DodgeCost;
    public int AttackLevel = 0;
    public int AttackCost;
    public int ProgressLevel;
    public int ProgressCost;
    public int MinionState;

    [Header("Text")]
    public Text[] FundsText;
    public Text[] RestoreText;
    public Text[] BombText;
    public Text[] FreezeText;
    public Text[] ShieldText;
    public Text[] LevelGeneratedFundsText;
    public Text[] DodgeLevelText;
    public Text[] DodgeCostText;
    public Text[] AttackCostText;
    public Text[] AttackLevelText;
    public Text[] ProgressLevelText;
    public Text[] ProgressCostText;

    private const string Fundskey = "Funds";
    private const string RestoreKey = "Restore";
    private const string Bombkey = "Bomb";
    private const string FreezeKey = "Freeze";
    private const string ShieldKey = "Shield";
    private const string DodgeKey = "Dodge";
    private const string DodgeCostKey = "DodgeCost";
    private const string AttackCostKey = "AttackCost";
    private const string AttackLevelKey = "AttackLevel";
    private const string ProgressCostKey = "ProgressCost";
    private const string ProgressLevelKey = "ProgressLevel";
    private const string MinionPrefs = "MinionKey";

    public Button MinionButton;

    void Start()
    {
        LoadBalance();
        UpdateAllTexts(FundsText, FundsAmount);
        UpdateAllTexts(RestoreText, RestoreHealthAmount);
        UpdateAllTexts(BombText, BombAmount);
        UpdateAllTexts(FreezeText, FreezeAmount);
        UpdateAllTexts(ShieldText, ShieldAmount);
        UpdateAllTexts(LevelGeneratedFundsText, LevelGeneratedFunds);
        UpdateAllTexts(DodgeLevelText, DodgeLevel);
        UpdateAllTexts(DodgeCostText, DodgeCost);
        UpdateAllTexts(AttackCostText, AttackCost);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AttackLevelText[0].text = "+ " + AttackLevel.ToString() + " Damage";
            MinionButton.interactable = false;
            if (MinionState == 0)
            {
                MinionButton.interactable = true;
            }
           
        }
        if (MinionState == 1)
        {
            ProgressLevel += 50;
            SaveProgressLevel();
            MinionState = 2;
            LoadMinion();
            MinionButton.interactable = false;
        }

        UpdateAllTexts(ProgressCostText, ProgressCost);
        ProgressLevelText[0].text = ProgressLevel.ToString();
    }

    void Awake()
    {
        ProgressLevel = PlayerPrefs.GetInt(ProgressLevelKey, ProgressLevel);
        ProgressLevelText[0].text = ProgressLevel.ToString();
    }

    void Update()
    {
    }

    public void LoadBalance()
    {
        FundsAmount = PlayerPrefs.GetInt(Fundskey, FundsAmount);
        RestoreHealthAmount = PlayerPrefs.GetInt(RestoreKey, RestoreHealthAmount);
        BombAmount = PlayerPrefs.GetInt(Bombkey, BombAmount);
        FreezeAmount = PlayerPrefs.GetInt(FreezeKey, FreezeAmount);
        ShieldAmount = PlayerPrefs.GetInt(ShieldKey, ShieldAmount);
        DodgeLevel = PlayerPrefs.GetInt(DodgeKey, DodgeLevel);
        DodgeCost = PlayerPrefs.GetInt(DodgeCostKey, DodgeCost);
        AttackCost = PlayerPrefs.GetInt(AttackCostKey, AttackCost);
        AttackLevel = PlayerPrefs.GetInt(AttackLevelKey, AttackLevel);
        ProgressCost = PlayerPrefs.GetInt(ProgressCostKey, ProgressCost);
        MinionState = PlayerPrefs.GetInt(MinionPrefs, MinionState);
    }

    public void SaveFunds()
    {
        PlayerPrefs.SetInt(Fundskey, FundsAmount);
        PlayerPrefs.Save();
    }

    public void SaveRestorePower()
    {
        PlayerPrefs.SetInt(RestoreKey, RestoreHealthAmount);
        PlayerPrefs.Save();
    }

    public void SaveBombPower()
    {
        PlayerPrefs.SetInt(Bombkey, BombAmount);
        PlayerPrefs.Save();
    }

    public void SaveFreezePower()
    {
        PlayerPrefs.SetInt(FreezeKey, FreezeAmount);
        PlayerPrefs.Save();
    }

    public void SaveShieldPower()
    {
        PlayerPrefs.SetInt(ShieldKey, ShieldAmount);
        PlayerPrefs.Save();
    }

    public void SaveDodgeLevel()
    {
        PlayerPrefs.SetInt(DodgeKey, DodgeLevel);
        PlayerPrefs.Save();
    }

    public void SaveDodgeCost()
    {
        PlayerPrefs.SetInt(DodgeCostKey, DodgeCost);
        PlayerPrefs.Save();
    }

    public void SaveAttackCost()
    {
        PlayerPrefs.SetInt(AttackCostKey, AttackCost);
        PlayerPrefs.Save();
    }

    public void SaveAttackLevel()
    {
        PlayerPrefs.SetInt(AttackLevelKey, AttackLevel);
        PlayerPrefs.Save();
    }

    public void SaveProgressCost()
    {
        PlayerPrefs.SetInt(ProgressCostKey, ProgressCost);
        PlayerPrefs.Save();
    }

    public void SaveProgressLevel()
    {
        PlayerPrefs.SetInt(ProgressLevelKey, ProgressLevel);
        PlayerPrefs.Save();
    }

    public void LoadMinion()
    {
        PlayerPrefs.SetInt(MinionPrefs, MinionState);
        PlayerPrefs.Save();
    }

    public void GeneratedFunds()
    {
        FundsAmount += LevelGeneratedFunds;
        UpdateAllTexts(FundsText, FundsAmount);
        SaveFunds();
    }

    public void BuyPowers(string Tag)
    {
        if (FundsAmount >= 100)
        {
            if (Tag == "Restore" && FundsAmount >= 100)
            {
                FundsAmount -= 100;
                RestoreHealthAmount++;
                UpdateAllTexts(FundsText, FundsAmount);
                UpdateAllTexts(RestoreText, RestoreHealthAmount);
                SaveFunds();
                SaveRestorePower();
            }
            else if (Tag == "Bomb" && FundsAmount >= 100)
            {
                FundsAmount -= 100;
                BombAmount++;
                UpdateAllTexts(BombText, BombAmount);
                UpdateAllTexts(FundsText, FundsAmount);
                SaveFunds();
                SaveBombPower();
            }
            else if (Tag == "Freeze" && FundsAmount >= 100)
            {
                FundsAmount -= 100;
                FreezeAmount++;
                UpdateAllTexts(FreezeText, FreezeAmount);
                UpdateAllTexts(FundsText, FundsAmount);
                SaveFunds();
                SaveFreezePower();
            }
            else if (Tag == "Shield" && FundsAmount >= 100)
            {
                FundsAmount -= 100;
                ShieldAmount++;
                UpdateAllTexts(ShieldText, ShieldAmount);
                UpdateAllTexts(FundsText, FundsAmount);
                SaveFunds();
                SaveShieldPower();
            }
            else if (Tag == "Dodge" && FundsAmount >= DodgeCost)
            {
                FundsAmount -= DodgeCost;
                DodgeLevel++;
                DodgeCost += 50;
                SaveDodgeCost();
                SaveFunds();
                SaveDodgeLevel();
                UpdateAllTexts(FundsText, FundsAmount);
                UpdateAllTexts(DodgeLevelText, DodgeLevel);
                UpdateAllTexts(DodgeCostText, DodgeCost);
            }
            else if (Tag == "Attack" && FundsAmount >= AttackCost)
            {
                FundsAmount -= AttackCost;
                AttackCost += 50;
                AttackLevel+= 5;
                SaveAttackCost();
                SaveAttackLevel();
                SaveFunds();
                UpdateAllTexts(FundsText, FundsAmount);
               // UpdateAllTexts(AttackLevelText, AttackLevel);
                AttackLevelText[0].text = "+ " + AttackLevel.ToString() + " Damange";
                UpdateAllTexts(AttackCostText, AttackCost);
            }
            else if (Tag == "Progress" && FundsAmount >= ProgressCost)
            {
                FundsAmount -= ProgressCost;
                ProgressCost += 50;
                ProgressLevel += 25;
                SaveProgressCost();
                SaveProgressLevel();
                SaveFunds();
                UpdateAllTexts(FundsText, FundsAmount);
                ProgressLevelText[0].text = ProgressLevel.ToString();
                UpdateAllTexts(ProgressCostText, ProgressCost);
            }
            else if(Tag == "Minion" && FundsAmount >= 500)
            {
                FundsAmount -= 500;
                MinionState++;
                LoadMinion();
                SaveFunds();
                UpdateAllTexts(FundsText, FundsAmount);
                MinionButton.interactable = false;
            }
        }
    }

    void UpdateAllTexts(Text[] texts, int value)
    {
        foreach (Text t in texts)
        {
            t.text = value.ToString();
        }
    }
}
