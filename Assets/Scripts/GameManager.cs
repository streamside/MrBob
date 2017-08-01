using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Text dateText;

    private TimeSimulation timeSimulation;

    void Awake()
    {
        timeSimulation = new TimeSimulation(DateTime.MinValue, OnTimeUpdate);
    }

    void Start()
    {
        UpdateDateUI(timeSimulation.GetDate());
    }

    void Update () {
        timeSimulation.Update(Time.deltaTime);
	}

    void OnTimeUpdate(DateTime date)
    {
        UpdateDateUI(date);
    }

    void UpdateDateUI(DateTime date)
    {
        string text = date.ToString("yyyy-MM-dd") + "\n" + date.ToString("HH:mm");
        dateText.text = text;
    }

    /// <summary>
    /// Manages the time simulation.
    /// </summary>
    public class TimeSimulation
    {
        private DateTime defaultStartDate = new DateTime(2017, 1, 1, 0, 0, 0);

        private Speed speed = Speed.Normal;

        public enum Speed
        {
            // Game is paused
            Paused,
            // 1 second == 1 in-game hour
            Normal,
            // 0.5 second == 1 in game hour
            Faster,
            // 0.2 second == 1 in game hour
            Fastest
        }

        private DateTime date;

        // Time (in seconds) since the simulation was lastly updated
        private float timeSinceUpdate = 0;

        // Callback when time changes
        private Action<DateTime> onUpdate;

        public TimeSimulation(DateTime startTime, Action<DateTime> onUpdate)
        {
            date = startTime == DateTime.MinValue ? defaultStartDate : startTime;
            this.onUpdate = onUpdate;
        }

        public void Update(float deltaTime)
        {
            if (speed == Speed.Paused)
            {
                return;
            }

            timeSinceUpdate += deltaTime;
            float factor = 0f;
            switch (speed)
            {
                case Speed.Normal:
                    factor = 1f;
                    break;
                case Speed.Faster:
                    factor = 0.5f;
                    break;
                case Speed.Fastest:
                    factor = 0.2f;
                    break;
            }
            int hoursToAdd = (int)(timeSinceUpdate / factor);
            if (hoursToAdd > 0)
            {
                date = date.AddHours(hoursToAdd);
                timeSinceUpdate = timeSinceUpdate % factor;
                onUpdate(date);
            }
        }

        public void SetSpeed(Speed speed)
        {
            this.speed = speed;
            timeSinceUpdate = 0;
        }

        public DateTime GetDate()
        {
            return date;
        }
    }
}
