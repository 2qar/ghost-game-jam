/// <summary>
/// Contains a bunch of preset messages to use in conjunction with the Message class w/ maybe some properties 
/// </summary>
public static class PresetMessages
{
    private static string[] startGhostMessage =
    {
        "hello",                        // 0
        "welcome to heck",              // 1
        "you gotta find your body",     // 2
        "if you wanna leave",           // 3
        "you could always stay though", // 4
        "i could use a pal",            // 5
        "oh well",                      // 6
        "have fun",                     // 7
        "i guess"                       // 8
    };

    // message for the first ghost the player sees to say
    private static Message StartGhostMessage()
    {
        float[] startGhostMessageLength = new float[startGhostMessage.Length];
        startGhostMessageLength.fillArray(Message.DefaultLength);
        startGhostMessageLength[8] = .15f;

        int[] startGhostMessageSize = new int[startGhostMessage.Length];
        startGhostMessageSize.fillArray(Message.DefaultSize);
        startGhostMessageSize[8] = 35;

        return new Message(startGhostMessage, startGhostMessageLength, startGhostMessageSize);
    }

    /// <summary>
    /// Generate a message based on the given line name.
    /// </summary>
    /// <returns>The message.</returns>
    /// <param name="lines">Line name.</param>
    public static Message GenerateMessage(string lines)
    {
        if (lines.ToLower() == "start")
            return StartGhostMessage();

        return new Message();
    }

}