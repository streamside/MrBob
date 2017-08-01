using System.Collections;

public enum JobStatus
{
    Waiting,
    WorkInProgress,
    Finished
}

public class Job
{
    public JobStatus Status { get; set; }

    // Total number of hours to complete job
    public int Size { get; set; }

    // Money awarded for completing job
    public int Price { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    // Number of hours spent on the job
    public int Progress { get; set; }

    // Quality of the performed job
    public int Quality { get; set; }
}
