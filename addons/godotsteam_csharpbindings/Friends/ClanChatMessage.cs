namespace GodotSteam;

public class ClanChatMessage
{
    public bool Ret { get; set; }
    public string Text { get; set; }
    public ClanChatMessageType Type { get; set; }
    public ulong Chatter { get; set; }
}