using System;
using System.Collections.Generic;

/// <summary>
/// Manages the time simulation.
/// </summary>
public class TimeSimulation
{
    private Speed speed = Settings.Time.DefaultSpeed;

    private DateTime date = Settings.Time.DefaultStartDate;

    // Time (in seconds) since the simulation was lastly updated
    private float timeSinceUpdate = 0;

    public delegate void HourChangeAction(DateTime datetime);
    public static event HourChangeAction OnHourChange;

    public delegate void TimeChangeAction(DateTime datetime);
    public static event TimeChangeAction OnTimeChange;

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
            if (OnHourChange != null)
            {
                OnHourChange(date);
            }
            if (OnTimeChange != null)
            {
                OnTimeChange(date);
            }
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

    public void SetDate(DateTime date)
    {
        this.date = date;
    }
}
