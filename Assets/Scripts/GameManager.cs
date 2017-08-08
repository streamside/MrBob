using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public TimeSimulation timeSimulation = new TimeSimulation();
    [HideInInspector]
    public MarketManager marketManager = new MarketManager();
    [HideInInspector]
    public JobManager jobManager = new JobManager();
    [HideInInspector]
    public EmployeeManager employeeManager = new EmployeeManager();

    private int money = Settings.StartingMoney;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void Start()
    {
        jobManager.Start();
    }

    void Update()
    {
        timeSimulation.Update(Time.deltaTime);
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        TimeSimulation.OnTimeChange += UpdateDateUI;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        TimeSimulation.OnTimeChange -= UpdateDateUI;
    }

    // Code executed when every scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find references in the newly loaded scene
        dateText = GameObject.Find(Settings.HUD.DateUIName).GetComponent<Text>();
        moneyText = GameObject.Find(Settings.HUD.MoneyUIName).GetComponent<Text>();

        UpdateDateUI(timeSimulation.GetDate());
        UpdateMoneyUI();
    }

    // Code executed once when the game is started
    void InitGame()
    {
    }

    void UpdateDateUI(DateTime date)
    {
        dateText.text = date.ToString(Settings.DateTimeFormat);
    }

    void UpdateMoneyUI()
    {
        moneyText.text = money + Settings.Currency;
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
