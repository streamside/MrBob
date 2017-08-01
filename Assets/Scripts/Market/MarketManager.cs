using System.Collections.Generic;

public class MarketManager {

    private List<AvailableJob> jobs = new List<AvailableJob>();

	public MarketManager() {
        GenerateJobs();
	}
	
    private void GenerateJobs()
    {
        jobs.Add(new AvailableJob("Bathroom renovation", "Brand new bathroom with nice furniture", 250, 10000));
        jobs.Add(new AvailableJob("Broken sink", "My sink is not working. Help me!", 8, 500));
        jobs.Add(new AvailableJob("Broken sink again", "My sink is not working. Help me!", 8, 400));
    }

    public List<AvailableJob> GetJobs()
    {
        return jobs;
    }
}
