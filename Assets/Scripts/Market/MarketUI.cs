using System.Collections;
using UnityEngine;
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
        int x = startX;
        int y = startY;
        int i = 1;
        foreach (Job job in GameManager.instance.marketManager.GetJobs())
        {
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
                        text.text = "Size: " + job.Size.ToString();
                        break;
                    case "Price":
                        text.text = "Price: " + job.Price.ToString();
                        break;
                }
            }

            jobTile.transform.SetParent(jobHolder);

            i++;
        }
    }

}
