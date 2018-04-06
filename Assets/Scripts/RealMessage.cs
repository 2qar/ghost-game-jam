/// <summary>
/// A message to be said by a ghost; has its own text, play length, and text size properties.
/// </summary>
[System.Serializable]
public class RealMessage
{
    private static float _defaultLength = .1f;
    public static float defaultLength
    {
        get { return _defaultLength; }
    }
    private static int _defaultSize = 50;
    public static int defaultSize
    {
        get { return _defaultSize; }
    }

    public string message;
    public float playLength;
    public int size;

    public RealMessage()
    {
        this.playLength = defaultLength;
        this.size = defaultSize;
    }

    // print the message, this is probably never gonna get used
    // but it's here just in case ya know? ;)
    public override string ToString()
    {
        return "Message: " + this.message + 
                                 " | Length: " + this.playLength + 
                                 " | Size: " + this.size;
    }

}
