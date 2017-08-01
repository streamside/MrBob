using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JobManager
{
    private List<Job> jobs = new List<Job>();


    public List<Job> GetJobs()
    {
        return jobs;
    }

    public void AddJob(Job job)
    {
        jobs.Add(job);
    }
}
