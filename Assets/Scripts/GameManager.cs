using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Main class for managing the game. It contains references to sub modules, e.g. job market,
/// current jobs, of the games which is responsible for sub sections of the game.
/// </summary>
public class GameManager : MonoBehaviour {
    private Text dateText;
    private Text moneyText;

    [HideInInspector]
    public static GameManager instance = null;

    [HideInInspector]
    public MarketManager marketManager;
    [HideInInspector]
    public JobManager jobManager;
    [HideInInspector]
    public EmployeeManager employeeManager;

    [HideInInspector]
    public TimeSimulation timeSimulation;

    private int money = Settings.StartingMoney;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void Start()
    {
        dateText = GameObject.Find(Settings.HUD.DateUIName).GetComponent<Text>();
        moneyText = GameObject.Find(Settings.HUD.MoneyUIName).GetComponent<Text>();
        UpdateDateUI(timeSimulation.GetDate());
        UpdateMoneyUI();
    }

    void Update ()
    {
        timeSimulation.Update(Time.deltaTime);
	}

    void InitGame()
    {
        marketManager = new MarketManager();
        jobManager = new JobManager();
        employeeManager = new EmployeeManager();
        timeSimulation = new TimeSimulation();
        timeSimulation.On(TimeSimulation.Event.Update, UpdateDateUI);
    }

    void UpdateDateUI(DateTime date)
    {
        dateText.text = date.ToString(Settings.DateTimeFormat);
    }

    void UpdateMoneyUI()
    {
        moneyText.text = money + Settings.Currency;
    }
}
