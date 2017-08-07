using System.Collections.Generic;

public class MarketManager {

    private List<Job> jobs = new List<Job>();

	public MarketManager() {
        GenerateJobs();
	}
	
    private void GenerateJobs()
    {
        jobs.Add(new Job("Bathroom renovation", "Brand new bathroom with nice furniture", 250, 10000));
        jobs.Add(new Job("Broken sink", "My sink is not working. Help me!", 8, 500));
        jobs.Add(new Job("Broken sink again", "My sink is not working. Help me!", 8, 400));
    }

    public List<Job> GetJobs()
    {
        return jobs;
    }
}
