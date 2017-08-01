using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text dateText;
    public Text moneyText;

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

    private String dateTimeFormat = "yyyy-MM-dd\nHH:mm";
    private int money = 10000;

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
        timeSimulation = new TimeSimulation(DateTime.MinValue, UpdateDateUI);
    }

    void UpdateDateUI(DateTime date)
    {
        dateText.text = date.ToString(dateTimeFormat);
    }

    void UpdateMoneyUI()
    {
        moneyText.text = money + "$";
    }
}
