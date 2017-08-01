using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages the time simulation.
/// </summary>
public class TimeSimulation
{
    public enum Event {
        Update
    }

    private Speed speed = Settings.Time.DefaultSpeed;

    private DateTime date = Settings.Time.DefaultStartDate;

    // Time (in seconds) since the simulation was lastly updated
    private float timeSinceUpdate = 0;

    // Event subscriptions
    private Dictionary<Event, List<Action<DateTime>>> subscriptions = new Dictionary<Event, List<Action<DateTime>>>();

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
            InvokeSubscriptions(Event.Update, date);
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

    // Use Events + delegates described on MSDN instead:
    // https://msdn.microsoft.com/en-us/library/aa645739(v=vs.71).aspx

    /// <summary>
    /// Register an subscription to an event
    /// </summary>
    /// <param name="event">Event to register to</param>
    /// <param name="action">The action to execute</param>
    public void On(Event e, Action<DateTime> action) {
        if (!subscriptions.ContainsKey(e))
        {
            subscriptions[e] = new List<Action<DateTime>>();
        }
        subscriptions[e].Add(action);
    }

    /// <summary>
    /// Removes a subscription for an event
    /// </summary>
    /// <param name="event">Event to unregister from</param>
    /// <param name="action">Action to remove</param>
    public void Off(Event e, Action<DateTime> action)
    {
        if (!subscriptions.ContainsKey(e))
        {
            subscriptions[e] = new List<Action<DateTime>>();
        }
        subscriptions[e].Remove(action);
    }

    private void InvokeSubscriptions(Event e, DateTime date)
    {
        List<Action<DateTime>> subs = subscriptions[e];
        if (subs == null)
        {
            return;
        }
        foreach (Action<DateTime> action in subs)
        {
            action(date);
        }
    }
}
