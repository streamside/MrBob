using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour
{
    public GameObject jobTilePrefab;
    private Transform jobHolder;
    private int startX = -275;
    private int startY = 150;
    private int marginX = 50;
    private int marginY = 50;
    private int tileWidth;
    private int tileHeight;

    // Use this for initialization
    void Start()
    {
        RectTransform rt = jobTilePrefab.GetComponent<RectTransform>();
        tileWidth = (int)rt.rect.width;
        tileHeight = (int)rt.rect.height;
        jobHolder = new GameObject("Jobs").transform;
        UpdateUI();
    }

    private void UpdateUI()
    {
        ClearJobs();

        int i = 1;
        foreach (Job job in GameManager.instance.marketManager.GetJobs())
        {
            GameObject jobTile = Instantiate(jobTilePrefab);

            jobTile.transform.position = GetTilePosition(i);

            UpdateTexts(job, jobTile);

            jobTile.transform.SetParent(jobHolder);

            // Add events
            GameObject takeJob = GameObjectUtils.FindInHierachyByName(jobTile, "TakeJob");
            takeJob.GetComponent<Button>().onClick.AddListener(() => TakeJob(job));

            i++;
        }
    }

    private void ClearJobs()
    {
        foreach (Transform child in jobHolder)
        {
            Destroy(child.gameObject);
        }
    }

    private Vector2 GetTilePosition(int position)
    {
        int x = 0, y = 0;
        switch (position)
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

        return new Vector2(x, y);
    }

    private void UpdateTexts(Job job, GameObject jobTile)
    {
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
                    text.text = "Size: " + job.Size.ToString();
                    break;
                case "Price":
                    text.text = "Price: " + job.Price.ToString();
                    break;
            }
        }

    }

    public void TakeJob(Job job)
    {
        GameManager.instance.jobManager.AddJob(job);
        GameManager.instance.marketManager.RemoveJob(job);
        UpdateUI();
    }

}
