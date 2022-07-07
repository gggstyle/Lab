
public class Rootobject
{
    public Post[] Posts { get; set; }
}

public class Post
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
