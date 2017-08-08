using UnityEngine;
using System;
using System.Collections.Generic;

public class JobManager
{
    private List<WorkingJob> jobs = new List<WorkingJob>();

    public delegate void JobChangeAction();
    public static event JobChangeAction OnJobChange;

    public void Start()
    {
        GenerateJobs();

        // TODO Unsubscribe never done
        TimeSimulation.OnHourChange += HourPassed;
    }

    private void GenerateJobs()
    {
        WorkingJob job = new WorkingJob("Bathroom renovation", "Brand new bathroom with nice furniture", 250, 10000);
        job.Quality = 10;
        job.Progress = 200;
        jobs.Add(job);

        // jobs.Add(new WorkingJob("Broken sink", "My sink is not working. Help me!", 8, 500));
        // jobs.Add(new WorkingJob("Broken sink again", "My sink is not working. Help me!", 8, 400));
    }

    public List<WorkingJob> GetJobs()
    {
        return jobs;
    }

    public void AddJob(Job job)
    {
        jobs.Add(new WorkingJob(job));

        if (OnJobChange != null)
        {
            OnJobChange();
        }
    }

    public void HourPassed(DateTime dateTime)
    {
        foreach (WorkingJob job in jobs)
        {
            job.HourPassed();
        }

        if (OnJobChange != null)
        {
            OnJobChange();
        }
    }
}
