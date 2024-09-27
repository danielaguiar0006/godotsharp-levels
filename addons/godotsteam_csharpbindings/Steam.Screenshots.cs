namespace GodotSteam;

public static partial class Steam
{
    public static uint AddScreenshotToLibrary(string fileName, string thumbnailFileName, int width, int height)
    {
        return GetInstance().Call(Methods.AddScreenshotToLibrary, fileName, thumbnailFileName, width, height).AsUInt32();
    }
    
    public static uint AddVRScreenshotToLibrary(VRScreenshotType type, string fileName, string vrFileName)
    {
        return GetInstance().Call(Methods.AddVRScreenshotToLibrary, (int)type, fileName, vrFileName).AsUInt32();
    }
    
    public static void HookScreenshots(bool hook)
    {
        GetInstance().Call(Methods.HookScreenshots, hook);
    }
    
    public static bool IsScreenshotsHooked()
    {
        return GetInstance().Call(Methods.IsScreenshotsHooked).AsBool();
    }
    
    public static bool SetLocation(uint screenshot, string location)
    {
        return GetInstance().Call(Methods.SetLocation, screenshot, location).AsBool();
    }
    
    public static bool TagPublishedFile(uint screenshot, ulong fileId)
    {
        return GetInstance().Call(Methods.TagPublishedFile, screenshot, fileId).AsBool();
    }
    
    public static bool TagUser(uint screenshot, ulong steamId)
    {
        return GetInstance().Call(Methods.TagUser, screenshot, steamId).AsBool();
    }
    
    public static void TriggerScreenshot()
    {
        GetInstance().Call(Methods.TriggerScreenshot);
    }
    
    public static uint WriteScreenshot(byte[] rgb, int width, int height)
    {
        return GetInstance().Call(Methods.WriteScreenshot, rgb, width, height).AsUInt32();
    }
}