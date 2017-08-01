public class AvailableJob
{
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
