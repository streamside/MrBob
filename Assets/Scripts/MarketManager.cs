using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketManager : MonoBehaviour {

    public GameObject jobTilePrefab;

    private List<AvailableJob> jobs = new List<AvailableJob>();
    private Transform jobHolder;
    private int startX = -275;
    private int startY = 150;
    private int marginX = 50;
    private int marginY = 50;
    private int tileWidth;
    private int tileHeight;

	// Use this for initialization
	void Start () {
        RectTransform rt = jobTilePrefab.GetComponent<RectTransform>();
        tileWidth = (int)rt.rect.width;
        tileHeight = (int)rt.rect.height;
        jobHolder = new GameObject("Jobs").transform;
        GenerateJobs();
        UpdateUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GenerateJobs()
    {
        jobs.Add(new AvailableJob("Bathroom renovation", "Brand new bathroom with nice furniture", 250, 10000));
        jobs.Add(new AvailableJob("Broken sink", "My sink is not working. Help me!", 8, 500));
        jobs.Add(new AvailableJob("Broken sink again", "My sink is not working. Help me!", 8, 500));
    }

    private void UpdateUI()
    {
        int x = startX;
        int y = startY;
        int i = 1;
        foreach (AvailableJob job in jobs) {
            GameObject jobTile = Instantiate(jobTilePrefab);

            switch (i)
            {
                case 1:
                    x = startX;
                    y = startY;
                    break;
                case 2:
                    x = startX + tileWidth + marginX;
                    y = startY;
                    break;
                case 3:
                    x = startX;
                    y = startY - (tileHeight + marginY);
                    break;
                case 4:
                    x = startX + tileWidth + marginX;
                    y = startY - (tileHeight + marginY);
                    break;
            }

            jobTile.transform.position = new Vector3(x, y);

            Text[] texts = jobTile.GetComponentsInChildren<Text>();

            foreach (Text text in texts)
            {
                switch (text.gameObject.name)
                {
                    case "Title":
                        text.text = job.Title;
                        break;
                    case "Description":
                        text.text = job.Description;
                        break;
                    case "Size":
                        text.text = "Size: "+ job.Size.ToString();
                        break;
                    case "Price":
                        text.text = "Price: "+ job.Price.ToString();
                        break;
                }
            }

            jobTile.transform.SetParent(jobHolder);

            i++;
        }
    }

    private class AvailableJob {

        public AvailableJob(string title, string description, int size, int price)
        {
            Title = title;
            Description = description;
            Size = size;
            Price = price;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        // Amount of hours to complete job
        public int Size { get; set; }

        public int Price { get; set; }
    }
}
