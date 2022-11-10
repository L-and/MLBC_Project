using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance = null; // 싱글톤패턴

    private int money;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Money"))
            PlayerPrefs.SetInt("Money", 0);

        money = PlayerPrefs.GetInt("Money");
    }

    public static int GetMoney()
    {
        return Instance.money;
    }

    public static void AddMoney(int addedMoney)
    {
        Instance.money += addedMoney;
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Money", Instance.money);
    }
}
