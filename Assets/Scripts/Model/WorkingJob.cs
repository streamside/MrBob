﻿using System.Collections;

public enum JobStatus
{
    Waiting,
    WorkInProgress,
    Complete
}

public class WorkingJob : Job
{
    public WorkingJob(string title, string description, int size, int price)
        : base(title, description, size, price)
    {
        Status = JobStatus.Waiting;
        Progress = 0;
        Quality = 0;
    }

    public WorkingJob(Job job) : this(job.Title, job.Description, job.Size, job.Price) { }

    public JobStatus Status { get; set; }

    public string GetStatusToDisplay()
    {
        switch (Status)
        {
            case JobStatus.Waiting:
                return "Waiting";
            case JobStatus.WorkInProgress:
                return "In Progress";
            case JobStatus.Complete:
                return "Complete";
        }

        return "Unhandled status: " + Status;
    }

    // Number of hours spent on the job
    private int progress;
    public int Progress {
        get
        {
            return progress;
        }
        set
        {
            if (value < 0)
            {
                progress = 0;
            }
            else
            {
                progress = value > Size ? Size : value;
            }
        }
    }

    public void HourPassed()
    {
        Progress++;
        if (Progress > 0 && Status == JobStatus.Waiting)
        {
            Status = JobStatus.WorkInProgress;
        }
        else if (Progress == Size)
        {
            Status = JobStatus.Complete;
        }
    }

    /// <summary>
    /// Gets the progress in relative numbers. 0 means that nothing has started and 1 means that everything is done.
    /// </summary>
    /// <returns>The progress. 0 = nothing done, 1 = all done</returns>
    public float GetRelativeProgress()
    {
        return (float)Progress / (float)Size;
    }

    // Quality of the performed job
    public int Quality { get; set; }
}
