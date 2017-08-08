using System;
using UnityEngine;
using UnityEngine.UI;

public class JobUI : MonoBehaviour {

    public GameObject jobTilePrefab;

    private Transform jobHolder;
    private int startX = 0;
    private int startY = 180;
    private int marginY = 25;
    private int tileHeight;
    private int progressBarMaxSize = 450;

    // Use this for initialization
    void Start ()
    {
        RectTransform rt = jobTilePrefab.GetComponent<RectTransform>();
        tileHeight = (int)rt.rect.height;
        jobHolder = new GameObject("Jobs").transform;
        UpdateUI();
    }

    void OnEnable()
    {
        JobManager.OnJobChange += UpdateUI;
    }

    void OnDisable()
    {
        JobManager.OnJobChange -= UpdateUI;
    }

    void UpdateUI()
    {
        ClearJobs();

        int pos = 0;
        foreach (WorkingJob job in GameManager.instance.jobManager.GetJobs())
        {
            GameObject jobTile = Instantiate(jobTilePrefab);

            int x = startX;
            int y = startY - pos * (tileHeight + marginY);

            jobTile.transform.position = new Vector3(x, y);

            Text[] texts = jobTile.GetComponentsInChildren<Text>();

            foreach (Text text in texts)
            {
                switch (text.gameObject.name)
                {
                    case "Title":
                        text.text = job.Title;
                        break;
                    case "Status":
                        text.text = "Status: " + job.GetStatusToDisplay();
                        break;
                    case "Quality":
                        text.text = "Quality: " + job.Quality.ToString();
                        break;
                    case "ProgressText":
                        // Text
                        text.text = Math.Round(job.GetRelativeProgress()*100) + "%";
                        // Progress bar
                        GameObject progressPart = GameObjectUtils.FindInHierachyByName(text.transform.parent.gameObject, "ProgressPart");
                        RectTransform rt = progressPart.GetComponent<RectTransform>();
                        rt.sizeDelta = new Vector2(progressBarMaxSize * job.GetRelativeProgress(), rt.rect.height);
                        break;
                }
            }

            jobTile.transform.SetParent(jobHolder);

            pos++;
        }
    }

    private void ClearJobs()
    {
        if (jobHolder == null)
        {
            return;
        }

        foreach (Transform child in jobHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
