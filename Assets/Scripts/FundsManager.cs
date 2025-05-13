using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FundsManager : MonoBehaviour
{
    [Header("INT")]
    public int FundsAmount = 200;
    public int RestoreHealthAmount;
    public int RestoreCost = 100;
    public int BombAmount;
    public int BombCost = 100;
    public int FreezeAmount;
    public int FreezeCost = 100;
    public int ShieldAmount;
    public int ShieldCost = 100;
    public int LevelGeneratedFunds;
    public int DodgeLevel;
    public int DodgeCost;
    public int AttackLevel = 0;
    public int AttackCost;
    public int ProgressLevel;
    public int ProgressCost;
    public int MinionState;
    public int AllieCost = 500;

    [Header("Text")]
    public Text[] FundsText;
    public Text[] RestoreText;
    public Text[] RestoreCostText;
    public Text[] BombText;
    public Text[] BombCostText;
    public Text[] FreezeText;
    public Text[] FreezeCostText;
    public Text[] ShieldText;
    public Text[] ShieldCostText;
    public Text[] LevelGeneratedFundsText;
    public Text[] DodgeLevelText;
    public Text[] DodgeCostText;
    public Text[] AttackCostText;
    public Text[] AttackLevelText;
    public Text[] ProgressLevelText;
    public Text[] ProgressCostText;
    public Text[] AllieCostText;

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
    private const string RestoreCostkey = "RestoreCost";
    private const string BombCostkey = "BombCost";
    private const string FreezeCostkey = "FreezeCost";
    private const string ShieldCostkey = "ShieldCost";
    private const string AllieCostkey = "AllieCost";


    public Button MinionButton;
    public Farm farm;

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
        UpdateAllTexts(RestoreCostText, RestoreCost);
        UpdateAllTexts(BombCostText, BombCost);
        UpdateAllTexts(FreezeCostText, FreezeCost);
        UpdateAllTexts(ShieldCostText, ShieldCost);
        UpdateAllTexts(AllieCostText, AllieCost);
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            AttackLevelText[0].text = "+ " + AttackLevel.ToString() + " Damage";
            if (MinionState == 0)
            {
                MinionButton.interactable = true;
            }
            if (MinionState == 3)
            {
                MinionButton.interactable = false;
            }
           
        }
        //if (MinionState == 1)
        //{
        //    ProgressLevel += 50;
        //    attack.MinionAllie.gameObject.SetActive(true);
        //    attack.Enemies[0].gameObject.SetActive(true);
        //    SaveProgressLevel();
        //    LoadMinion();
        //}
        //if (MinionState == 2)
        //{
        //    ProgressLevel += 100;
        //    attack.MinionAllie.gameObject.SetActive(true);
        //    attack.Allie2.gameObject.SetActive(true);
        //    attack.Enemies[0].gameObject.SetActive(true);
        //    attack.Enemies[1].gameObject.SetActive(true);
        //    SaveProgressLevel();
        //    LoadMinion();
        //}
        //if (MinionState == 3)
        //{
        //    ProgressLevel += 150;
        //    SaveProgressLevel();
        //    LoadMinion();
        //    MinionButton.interactable = true;
        //}

        UpdateAllTexts(ProgressCostText, ProgressCost);
        ProgressLevelText[0].text = ProgressLevel.ToString();
    }

    void Awake()
    {
        ProgressLevel = PlayerPrefs.GetInt(ProgressLevelKey, ProgressLevel);
        LoadFunds();
        ProgressLevelText[0].text = ProgressLevel.ToString();
        MinionState = PlayerPrefs.GetInt(MinionPrefs, MinionState);
    }

    void Update()
    {
    }

    public void LoadBalance()
    {
        RestoreHealthAmount = PlayerPrefs.GetInt(RestoreKey, RestoreHealthAmount);
        BombAmount = PlayerPrefs.GetInt(Bombkey, BombAmount);
        FreezeAmount = PlayerPrefs.GetInt(FreezeKey, FreezeAmount);
        ShieldAmount = PlayerPrefs.GetInt(ShieldKey, ShieldAmount);
        DodgeLevel = PlayerPrefs.GetInt(DodgeKey, DodgeLevel);
        DodgeCost = PlayerPrefs.GetInt(DodgeCostKey, DodgeCost);
        AttackCost = PlayerPrefs.GetInt(AttackCostKey, AttackCost);
        AttackLevel = PlayerPrefs.GetInt(AttackLevelKey, AttackLevel);
        ProgressCost = PlayerPrefs.GetInt(ProgressCostKey, ProgressCost);
        RestoreCost = PlayerPrefs.GetInt(RestoreCostkey, RestoreCost);
        BombCost = PlayerPrefs.GetInt(BombCostkey, BombCost);
        FreezeCost = PlayerPrefs.GetInt(FreezeCostkey, FreezeCost);
        ShieldCost = PlayerPrefs.GetInt(ShieldCostkey, ShieldCost);
        AllieCost = PlayerPrefs.GetInt(AllieCostkey, AllieCost);
    }

    public void SaveFunds()
    {
        PlayerPrefs.SetInt(Fundskey, FundsAmount);
        PlayerPrefs.Save();
    }

    public void LoadFunds()
    {
        FundsAmount = PlayerPrefs.GetInt(Fundskey, FundsAmount);
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

    public void SaveRestoreCost()
    {
        PlayerPrefs.SetInt(RestoreCostkey, RestoreCost);
        PlayerPrefs.Save();
    }
    public void SaveBombCost()
    {
        PlayerPrefs.SetInt(BombCostkey, BombCost);
        PlayerPrefs.Save();
    }

    public void SaveFreezeCost()
    {
        PlayerPrefs.SetInt(FreezeCostkey, FreezeCost);
        PlayerPrefs.Save();
    }

    public void SaveShieldCost()
    {
        PlayerPrefs.SetInt(ShieldCostkey, ShieldCost);
        PlayerPrefs.Save();
    }

    public void SaveAllieCost()
    {
        PlayerPrefs.SetInt(ShieldCostkey, ShieldCost);
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
            if (Tag == "Restore" && FundsAmount >= RestoreCost)
            {
                FundsAmount -= RestoreCost;
                RestoreHealthAmount++;
                RestoreCost += 50;
                SaveRestoreCost();
                UpdateAllTexts(FundsText, FundsAmount);
                UpdateAllTexts(RestoreText, RestoreHealthAmount);
                SaveFunds();
                SaveRestorePower();
                RestoreCostText[0].text = RestoreCost.ToString();
            }
            else if (Tag == "Bomb" && FundsAmount >= BombCost)
            {
                FundsAmount -= BombCost;
                BombAmount++;
                BombCost += 50;
                SaveBombCost();
                UpdateAllTexts(BombText, BombAmount);
                UpdateAllTexts(FundsText, FundsAmount);
                SaveFunds();
                SaveBombPower();
                BombCostText[0].text = BombCost.ToString();
            }
            else if (Tag == "Freeze" && FundsAmount >= FreezeCost)
            {
                FundsAmount -= FreezeCost;
                FreezeAmount++;
                FreezeCost += 50;
                SaveFreezeCost();
                UpdateAllTexts(FreezeText, FreezeAmount);
                UpdateAllTexts(FundsText, FundsAmount);
                SaveFunds();
                SaveFreezePower();
                FreezeCostText[0].text = FreezeCost.ToString();
            }
            else if (Tag == "Shield" && FundsAmount >= ShieldCost)
            {
                FundsAmount -= ShieldCost;
                ShieldAmount++;
                ShieldCost += 50;
                SaveShieldCost();
                UpdateAllTexts(ShieldText, ShieldAmount);
                UpdateAllTexts(FundsText, FundsAmount);
                SaveFunds();
                SaveShieldPower();
                ShieldCostText[0].text = ShieldCost.ToString();
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
            else if(Tag == "Minion" && FundsAmount >= AllieCost)
            {
                FundsAmount -= AllieCost;
                MinionState++;
                AllieCost += 250;
                LoadMinion();
                SaveAllieCost();
                SaveFunds();
                UpdateAllTexts(FundsText, FundsAmount);
                AllieCostText[0].text = AllieCost.ToString();
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
