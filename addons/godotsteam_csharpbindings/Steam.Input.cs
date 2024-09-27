namespace GodotSteam;

public static partial class Steam
{
    public static void ActivateActionSet(ulong inputHandle, long actionSetHandle)
    {
        GetInstance().Call(Methods.ActivateActionSet, inputHandle, actionSetHandle);
    }
    
    public static void ActivateActionSetLayer(ulong inputHandle, long actionSetLayerHandle)
    {
        GetInstance().Call(Methods.ActivateActionSetLayer, inputHandle, actionSetLayerHandle);
    }
    
    public static void DeactivateActionSetLayer(ulong inputHandle, long actionSetHandle)
    {
        GetInstance().Call(Methods.DeactivateActionSetLayer, inputHandle, actionSetHandle);
    }
    
    public static void DeactivateAllActionSetLayers(long inputHandle)
    {
        GetInstance().Call(Methods.DeactivateAllActionSetLayers, inputHandle);
    }
    
    public static ulong GetActionSetHandle(string actionSetName)
    {
        return GetInstance().Call(Methods.GetActionSetHandle, actionSetName).AsUInt64();
    }
    
    public static InputActionOrigin GetActionOriginFromXboxOrigin(ulong inputHandle, long origin)
    {
        return (InputActionOrigin)GetInstance().Call(Methods.GetActionOriginFromXboxOrigin, inputHandle, origin).AsInt64();
    }
    
    public static Godot.Collections.Array GetActiveActionSetLayers(long inputHandle)
    {
        return GetInstance().Call(Methods.GetActiveActionSetLayers, inputHandle).AsGodotArray();
    }
    
    public static Godot.Collections.Dictionary GetAnalogActionData(ulong inputHandle, long analogActionHandle)
    {
        return GetInstance().Call(Methods.GetAnalogActionData, inputHandle, analogActionHandle).AsGodotDictionary();
    }
    
    public static ulong GetAnalogActionHandle(string actionName)
    {
        return GetInstance().Call(Methods.GetAnalogActionHandle, actionName).AsUInt16();
    }
    
    public static Godot.Collections.Array GetAnalogActionOrigins(ulong inputHandle, ulong actionSetHandle, long analogActionHandle)
    {
        return GetInstance().Call(Methods.GetAnalogActionOrigins, inputHandle, actionSetHandle, analogActionHandle).AsGodotArray();
    }
    
    public static Godot.Collections.Array GetConnectedControllers()
    {
        return GetInstance().Call(Methods.GetConnectedControllers).AsGodotArray();
    }
    
    public static ulong GetControllerForGamepadIndex(long index)
    {
        return GetInstance().Call(Methods.GetControllerForGamepadIndex, index).AsUInt16();
    }
    
    public static ulong GetCurrentActionSet(long inputHandle)
    {
        return GetInstance().Call(Methods.GetCurrentActionSet, inputHandle).AsUInt16();
    }
    
    public static Godot.Collections.Array GetDeviceBindingRevision(long inputHandle)
    {
        return GetInstance().Call(Methods.GetDeviceBindingRevision, inputHandle).AsGodotArray();
    }
    
    public static Godot.Collections.Dictionary GetDigitalActionData(ulong inputHandle, long digitalActionHandle)
    {
        return GetInstance().Call(Methods.GetDigitalActionData, inputHandle, digitalActionHandle).AsGodotDictionary();
    }
    
    public static ulong GetDigitalActionHandle(string actionName)
    {
        return GetInstance().Call(Methods.GetDigitalActionHandle, actionName).AsUInt64();
    }
    
    public static Godot.Collections.Array GetDigitalActionOrigins(ulong inputHandle, ulong actionSetHandle, long digitalActionHandle)
    {
        return GetInstance().Call(Methods.GetDigitalActionOrigins, inputHandle, actionSetHandle, digitalActionHandle).AsGodotArray();
    }
    
    public static int GetGamepadIndexForController(long inputHandle)
    {
        return GetInstance().Call(Methods.GetGamepadIndexForController, inputHandle).AsInt32();
    }
    
    public static string GetGlyphForActionOrigin(InputActionOrigin origin)
    {
        return GetInstance().Call(Methods.GetGlyphForActionOrigin, (long)origin).AsString();
    }
    
    public static string GetInputTypeForHandle(long inputHandle)
    {
        return GetInstance().Call(Methods.GetInputTypeForHandle, inputHandle).AsString();
    }
    
    public static Godot.Collections.Dictionary GetMotionData(long inputHandle)
    {
        return GetInstance().Call(Methods.GetMotionData, inputHandle).AsGodotDictionary();
    }
    
    public static int GetRemotePlaySessionID(long inputHandle)
    {
        return GetInstance().Call(Methods.GetRemotePlaySessionID, inputHandle).AsInt32();
    }
    
    public static string GetStringForActionOrigin(InputActionOrigin origin)
    {
        return GetInstance().Call(Methods.GetStringForActionOrigin, (long)origin).AsString();
    }
    
    public static bool InputInit(bool explicitlyCallRunframe = false)
    {
        return GetInstance().Call(Methods.InputInit, explicitlyCallRunframe).AsBool();
    }
    
    public static bool InputShutdown()
    {
        return GetInstance().Call(Methods.InputShutdown).AsBool();
    }
    
    public static void RunFrame(bool reservedValue = true)
    {
        GetInstance().Call(Methods.RunFrame, reservedValue);
    }
    
    public static void SetLEDColor(ulong inputHandle, int colorR, int colorG, int colorB, long flags)
    {
        GetInstance().Call(Methods.SetLEDColor, inputHandle, colorR, colorG, colorB, flags);
    }
    
    public static bool ShowBindingPanel(long inputHandle)
    {
        return GetInstance().Call(Methods.ShowBindingPanel, inputHandle).AsBool();
    }
    
    public static void StopAnalogActionMomentum(ulong inputHandle, long action)
    {
        GetInstance().Call(Methods.StopAnalogActionMomentum, inputHandle, action);
    }
    
    public static int TranslateActionOrigin(InputType destinationInput, InputActionOrigin sourceOrigin)
    {
        return GetInstance().Call(Methods.TranslateActionOrigin, (long)destinationInput, (long)sourceOrigin).AsInt32();
    }
    
    public static void TriggerHapticPulse(ulong inputHandle, int targetPad, long duration)
    {
        GetInstance().Call(Methods.TriggerHapticPulse, inputHandle, targetPad, duration);
    }
    
    public static void TriggerRepeatedHapticPulse(ulong inputHandle, int targetPad, int duration, int offset, int repeat, long flags)
    {
        GetInstance().Call(Methods.TriggerRepeatedHapticPulse, inputHandle, targetPad, duration, offset, repeat, flags);
    }
    
    public static void TriggerVibration(ulong inputHandle, int leftSpeed, long rightSpeed)
    {
        GetInstance().Call(Methods.TriggerVibration, inputHandle, leftSpeed, rightSpeed);
    }
    
    public static bool SetInputActionManifestFilePath(string manifestPath)
    {
        return GetInstance().Call(Methods.SetInputActionManifestFilePath, manifestPath).AsBool();
    }
    
    public static void SetDualSenseTriggerEffect(ulong inputHandle, int parameters, int triggerMask, ScePadTriggerEffectMode effectMode, int position, int amplitude, long frequency)
    {
        GetInstance().Call(Methods.SetDualSenseTriggerEffect, inputHandle, parameters, triggerMask, (long)effectMode, position, amplitude, frequency);
    }
    
    public static bool WaitForData(bool waitForever, long timeout)
    {
        return GetInstance().Call(Methods.WaitForData, waitForever, timeout).AsBool();
    }
    
    public static bool NewDataAvailable()
    {
        return GetInstance().Call(Methods.NewDataAvailable).AsBool();
    }
    
    public static void EnableDeviceCallbacks()
    {
        GetInstance().Call(Methods.EnableDeviceCallbacks);
    }
    
    public static string GetGlyphPNGForActionOrigin(InputActionOrigin origin, InputGlyphSize size, long flags)
    {
        return GetInstance().Call(Methods.GetGlyphPNGForActionOrigin, (long)origin, (long)size, flags).AsString();
    }
    
    public static string GetGlyphSVGForActionOrigin(InputActionOrigin origin, long flags)
    {
        return GetInstance().Call(Methods.GetGlyphSVGForActionOrigin, (long)origin, flags).AsString();
    }
    
    public static void TriggerVibrationExtended(ulong inputHandle, int leftSpeed, int rightSpeed, int leftTriggerSpeed, long rightTriggerSpeed)
    {
        GetInstance().Call(Methods.TriggerVibrationExtended, inputHandle, leftSpeed, rightSpeed, leftTriggerSpeed, rightTriggerSpeed);
    }
    
    public static void TriggerSimpleHapticEvent(ulong inputHandle, int hapticLocation, int intensity, string gainDb, int otherIntensity, string otherGainDb)
    {
        GetInstance().Call(Methods.TriggerSimpleHapticEvent, inputHandle, hapticLocation, intensity, gainDb, otherIntensity, otherGainDb);
    }
    
    public static string GetStringForXboxOrigin(long origin)
    {
        return GetInstance().Call(Methods.GetStringForXboxOrigin, origin).AsString();
    }
    
    public static string GetGlyphForXboxOrigin(long origin)
    {
        return GetInstance().Call(Methods.GetGlyphForXboxOrigin, origin).AsString();
    }
    
    public static long GetSessionInputConfigurationSettings()
    {
        return GetInstance().Call(Methods.GetSessionInputConfigurationSettings).AsInt64();
    }
    
    public static string GetStringForDigitalActionName(long actionHandle)
    {
        return GetInstance().Call(Methods.GetStringForDigitalActionName, actionHandle).AsString();
    }
    
    public static string GetStringForAnalogActionName(long actionHandle)
    {
        return GetInstance().Call(Methods.GetStringForAnalogActionName, actionHandle).AsString();
    }

    public enum InputActionEventType : long
    {
        DigitalAction = 0,
        AnalogAction = 1
    }

    public enum InputActionOrigin : long
    {
        None = 0,
        SteamcontrollerA = 1,
        SteamcontrollerB = 2,
        SteamcontrollerX = 3,
        SteamcontrollerY = 4,
        SteamcontrollerLeftbumper = 5,
        SteamcontrollerRightbumper = 6,
        SteamcontrollerLeftgrip = 7,
        SteamcontrollerRightgrip = 8,
        SteamcontrollerStart = 9,
        SteamcontrollerBack = 10,
        SteamcontrollerLeftpadTouch = 11,
        SteamcontrollerLeftpadSwipe = 12,
        SteamcontrollerLeftpadClick = 13,
        SteamcontrollerLeftpadDpadnorth = 14,
        SteamcontrollerLeftpadDpadsouth = 15,
        SteamcontrollerLeftpadDpadwest = 16,
        SteamcontrollerLeftpadDpadeast = 17,
        SteamcontrollerRightpadTouch = 18,
        SteamcontrollerRightpadSwipe = 19,
        SteamcontrollerRightpadClick = 20,
        SteamcontrollerRightpadDpadnorth = 21,
        SteamcontrollerRightpadDpadsouth = 22,
        SteamcontrollerRightpadDpadwest = 23,
        SteamcontrollerRightpadDpadeast = 24,
        SteamcontrollerLefttriggerPull = 25,
        SteamcontrollerLefttriggerClick = 26,
        SteamcontrollerRighttriggerPull = 27,
        SteamcontrollerRighttriggerClick = 28,
        SteamcontrollerLeftstickMove = 29,
        SteamcontrollerLeftstickClick = 30,
        SteamcontrollerLeftstickDpadnorth = 31,
        SteamcontrollerLeftstickDpadsouth = 32,
        SteamcontrollerLeftstickDpadwest = 33,
        SteamcontrollerLeftstickDpadeast = 34,
        SteamcontrollerGyroMove = 35,
        SteamcontrollerGyroPitch = 36,
        SteamcontrollerGyroYaw = 37,
        SteamcontrollerGyroRoll = 38,
        SteamcontrollerReserved0 = 39,
        SteamcontrollerReserved1 = 40,
        SteamcontrollerReserved2 = 41,
        SteamcontrollerReserved3 = 42,
        SteamcontrollerReserved4 = 43,
        SteamcontrollerReserved5 = 44,
        SteamcontrollerReserved6 = 45,
        SteamcontrollerReserved7 = 46,
        SteamcontrollerReserved8 = 47,
        SteamcontrollerReserved9 = 48,
        SteamcontrollerReserved10 = 49,
        Ps4X = 50,
        Ps4Circle = 51,
        Ps4Triangle = 52,
        Ps4Square = 53,
        Ps4Leftbumper = 54,
        Ps4Rightbumper = 55,
        Ps4Options = 56,
        Ps4Share = 57,
        Ps4LeftpadTouch = 58,
        Ps4LeftpadSwipe = 59,
        Ps4LeftpadClick = 60,
        Ps4LeftpadDpadnorth = 61,
        Ps4LeftpadDpadsouth = 62,
        Ps4LeftpadDpadwest = 63,
        Ps4LeftpadDpadeast = 64,
        Ps4RightpadTouch = 65,
        Ps4RightpadSwipe = 66,
        Ps4RightpadClick = 67,
        Ps4RightpadDpadnorth = 68,
        Ps4RightpadDpadsouth = 69,
        Ps4RightpadDpadwest = 70,
        Ps4RightpadDpadeast = 71,
        Ps4CenterpadTouch = 72,
        Ps4CenterpadSwipe = 73,
        Ps4CenterpadClick = 74,
        Ps4CenterpadDpadnorth = 75,
        Ps4CenterpadDpadsouth = 76,
        Ps4CenterpadDpadwest = 77,
        Ps4CenterpadDpadeast = 78,
        Ps4LefttriggerPull = 79,
        Ps4LefttriggerClick = 80,
        Ps4RighttriggerPull = 81,
        Ps4RighttriggerClick = 82,
        Ps4LeftstickMove = 83,
        Ps4LeftstickClick = 84,
        Ps4LeftstickDpadnorth = 85,
        Ps4LeftstickDpadsouth = 86,
        Ps4LeftstickDpadwest = 87,
        Ps4LeftstickDpadeast = 88,
        Ps4RightstickMove = 89,
        Ps4RightstickClick = 90,
        Ps4RightstickDpadnorth = 91,
        Ps4RightstickDpadsouth = 92,
        Ps4RightstickDpadwest = 93,
        Ps4RightstickDpadeast = 94,
        Ps4DpadNorth = 95,
        Ps4DpadSouth = 96,
        Ps4DpadWest = 97,
        Ps4DpadEast = 98,
        Ps4GyroMove = 99,
        Ps4GyroPitch = 100,
        Ps4GyroYaw = 101,
        Ps4GyroRoll = 102,
        Ps4DpadMove = 103,
        Ps4Reserved1 = 104,
        Ps4Reserved2 = 105,
        Ps4Reserved3 = 106,
        Ps4Reserved4 = 107,
        Ps4Reserved5 = 108,
        Ps4Reserved6 = 109,
        Ps4Reserved7 = 110,
        Ps4Reserved8 = 111,
        Ps4Reserved9 = 112,
        Ps4Reserved10 = 113,
        XboxoneA = 114,
        XboxoneB = 115,
        XboxoneX = 116,
        XboxoneY = 117,
        XboxoneLeftbumper = 118,
        XboxoneRightbumper = 119,
        XboxoneMenu = 120,
        XboxoneView = 121,
        XboxoneLefttriggerPull = 122,
        XboxoneLefttriggerClick = 123,
        XboxoneRighttriggerPull = 124,
        XboxoneRighttriggerClick = 125,
        XboxoneLeftstickMove = 126,
        XboxoneLeftstickClick = 127,
        XboxoneLeftstickDpadnorth = 128,
        XboxoneLeftstickDpadsouth = 129,
        XboxoneLeftstickDpadwest = 130,
        XboxoneLeftstickDpadeast = 131,
        XboxoneRightstickMove = 132,
        XboxoneRightstickClick = 133,
        XboxoneRightstickDpadnorth = 134,
        XboxoneRightstickDpadsouth = 135,
        XboxoneRightstickDpadwest = 136,
        XboxoneRightstickDpadeast = 137,
        XboxoneDpadNorth = 138,
        XboxoneDpadSouth = 139,
        XboxoneDpadWest = 140,
        XboxoneDpadEast = 141,
        XboxoneDpadMove = 142,
        XboxoneLeftgripLower = 143,
        XboxoneLeftgripUpper = 144,
        XboxoneRightgripLower = 145,
        XboxoneRightgripUpper = 146,
        XboxoneShare = 147,
        XboxoneReserved6 = 148,
        XboxoneReserved7 = 149,
        XboxoneReserved8 = 150,
        XboxoneReserved9 = 151,
        XboxoneReserved10 = 152,
        Xbox360A = 153,
        Xbox360B = 154,
        Xbox360X = 155,
        Xbox360Y = 156,
        Xbox360Leftbumper = 157,
        Xbox360Rightbumper = 158,
        Xbox360Start = 159,
        Xbox360Back = 160,
        Xbox360LefttriggerPull = 161,
        Xbox360LefttriggerClick = 162,
        Xbox360RighttriggerPull = 163,
        Xbox360RighttriggerClick = 164,
        Xbox360LeftstickMove = 165,
        Xbox360LeftstickClick = 166,
        Xbox360LeftstickDpadnorth = 167,
        Xbox360LeftstickDpadsouth = 168,
        Xbox360LeftstickDpadwest = 169,
        Xbox360LeftstickDpadeast = 170,
        Xbox360RightstickMove = 171,
        Xbox360RightstickClick = 172,
        Xbox360RightstickDpadnorth = 173,
        Xbox360RightstickDpadsouth = 174,
        Xbox360RightstickDpadwest = 175,
        Xbox360RightstickDpadeast = 176,
        Xbox360DpadNorth = 177,
        Xbox360DpadSouth = 178,
        Xbox360DpadWest = 179,
        Xbox360DpadEast = 180,
        Xbox360DpadMove = 181,
        Xbox360Reserved1 = 182,
        Xbox360Reserved2 = 183,
        Xbox360Reserved3 = 184,
        Xbox360Reserved4 = 185,
        Xbox360Reserved5 = 186,
        Xbox360Reserved6 = 187,
        Xbox360Reserved7 = 188,
        Xbox360Reserved8 = 189,
        Xbox360Reserved9 = 190,
        Xbox360Reserved10 = 191,
        SwitchA = 192,
        SwitchB = 193,
        SwitchX = 194,
        SwitchY = 195,
        SwitchLeftbumper = 196,
        SwitchRightbumper = 197,
        SwitchPlus = 198,
        SwitchMinus = 199,
        SwitchCapture = 200,
        SwitchLefttriggerPull = 201,
        SwitchLefttriggerClick = 202,
        SwitchRighttriggerPull = 203,
        SwitchRighttriggerClick = 204,
        SwitchLeftstickMove = 205,
        SwitchLeftstickClick = 206,
        SwitchLeftstickDpadnorth = 207,
        SwitchLeftstickDpadsouth = 208,
        SwitchLeftstickDpadwest = 209,
        SwitchLeftstickDpadeast = 210,
        SwitchRightstickMove = 211,
        SwitchRightstickClick = 212,
        SwitchRightstickDpadnorth = 213,
        SwitchRightstickDpadsouth = 214,
        SwitchRightstickDpadwest = 215,
        SwitchRightstickDpadeast = 216,
        SwitchDpadNorth = 217,
        SwitchDpadSouth = 218,
        SwitchDpadWest = 219,
        SwitchDpadEast = 220,
        SwitchProgyroMove = 221,
        SwitchProgyroPitch = 222,
        SwitchProgyroYaw = 223,
        SwitchProgyroRoll = 224,
        SwitchDpadMove = 225,
        SwitchReserved1 = 226,
        SwitchReserved2 = 227,
        SwitchReserved3 = 228,
        SwitchReserved4 = 229,
        SwitchReserved5 = 230,
        SwitchReserved6 = 231,
        SwitchReserved7 = 232,
        SwitchReserved8 = 233,
        SwitchReserved9 = 234,
        SwitchReserved10 = 235,
        SwitchRightgyroMove = 236,
        SwitchRightgyroPitch = 237,
        SwitchRightgyroYaw = 238,
        SwitchRightgyroRoll = 239,
        SwitchLeftgyroMove = 240,
        SwitchLeftgyroPitch = 241,
        SwitchLeftgyroYaw = 242,
        SwitchLeftgyroRoll = 243,
        SwitchLeftgripLower = 244,
        SwitchLeftgripUpper = 245,
        SwitchRightgripLower = 246,
        SwitchRightgripUpper = 247,
        SwitchJoyconButtonN = 248,
        SwitchJoyconButtonE = 249,
        SwitchJoyconButtonS = 250,
        SwitchJoyconButtonW = 251,
        SwitchReserved15 = 252,
        SwitchReserved16 = 253,
        SwitchReserved17 = 254,
        SwitchReserved18 = 255,
        SwitchReserved19 = 256,
        SwitchReserved20 = 257,
        Ps5X = 258,
        Ps5Circle = 259,
        Ps5Triangle = 260,
        Ps5Square = 261,
        Ps5Leftbumper = 262,
        Ps5Rightbumper = 263,
        Ps5Option = 264,
        Ps5Create = 265,
        Ps5Mute = 266,
        Ps5LeftpadTouch = 267,
        Ps5LeftpadSwipe = 268,
        Ps5LeftpadClick = 269,
        Ps5LeftpadDpadnorth = 270,
        Ps5LeftpadDpadsouth = 271,
        Ps5LeftpadDpadwest = 272,
        Ps5LeftpadDpadeast = 273,
        Ps5RightpadTouch = 274,
        Ps5RightpadSwipe = 275,
        Ps5RightpadClick = 276,
        Ps5RightpadDpadnorth = 277,
        Ps5RightpadDpadsouth = 278,
        Ps5RightpadDpadwest = 279,
        Ps5RightpadDpadeast = 280,
        Ps5CenterpadTouch = 281,
        Ps5CenterpadSwipe = 282,
        Ps5CenterpadClick = 283,
        Ps5CenterpadDpadnorth = 284,
        Ps5CenterpadDpadsouth = 285,
        Ps5CenterpadDpadwest = 286,
        Ps5CenterpadDpadeast = 287,
        Ps5LefttriggerPull = 288,
        Ps5LefttriggerClick = 289,
        Ps5RighttriggerPull = 290,
        Ps5RighttriggerClick = 291,
        Ps5LeftstickMove = 292,
        Ps5LeftstickClick = 293,
        Ps5LeftstickDpadnorth = 294,
        Ps5LeftstickDpadsouth = 295,
        Ps5LeftstickDpadwest = 296,
        Ps5LeftstickDpadeast = 297,
        Ps5RightstickMove = 298,
        Ps5RightstickClick = 299,
        Ps5RightstickDpadnorth = 300,
        Ps5RightstickDpadsouth = 301,
        Ps5RightstickDpadwest = 302,
        Ps5RightstickDpadeast = 303,
        Ps5DpadNorth = 304,
        Ps5DpadSouth = 305,
        Ps5DpadWest = 306,
        Ps5DpadEast = 307,
        Ps5GyroMove = 308,
        Ps5GyroPitch = 309,
        Ps5GyroYaw = 310,
        Ps5GyroRoll = 311,
        Ps5DpadMove = 312,
        Ps5Leftgrip = 313,
        Ps5Rightgrip = 314,
        Ps5Leftfn = 315,
        Ps5Rightfn = 316,
        Ps5Reserved5 = 317,
        Ps5Reserved6 = 318,
        Ps5Reserved7 = 319,
        Ps5Reserved8 = 320,
        Ps5Reserved9 = 321,
        Ps5Reserved10 = 322,
        Ps5Reserved11 = 323,
        Ps5Reserved12 = 324,
        Ps5Reserved13 = 325,
        Ps5Reserved14 = 326,
        Ps5Reserved15 = 327,
        Ps5Reserved16 = 328,
        Ps5Reserved17 = 329,
        Ps5Reserved18 = 330,
        Ps5Reserved19 = 331,
        Ps5Reserved20 = 332,
        SteamdeckA = 333,
        SteamdeckB = 334,
        SteamdeckX = 335,
        SteamdeckY = 336,
        SteamdeckL1 = 337,
        SteamdeckR1 = 338,
        SteamdeckMenu = 339,
        SteamdeckView = 340,
        SteamdeckLeftpadTouch = 341,
        SteamdeckLeftpadSwipe = 342,
        SteamdeckLeftpadClick = 343,
        SteamdeckLeftpadDpadnorth = 344,
        SteamdeckLeftpadDpadsouth = 345,
        SteamdeckLeftpadDpadwest = 346,
        SteamdeckLeftpadDpadeast = 347,
        SteamdeckRightpadTouch = 348,
        SteamdeckRightpadSwipe = 349,
        SteamdeckRightpadClick = 350,
        SteamdeckRightpadDpadnorth = 351,
        SteamdeckRightpadDpadsouth = 352,
        SteamdeckRightpadDpadwest = 353,
        SteamdeckRightpadDpadeast = 354,
        SteamdeckL2Softpull = 355,
        SteamdeckL2 = 356,
        SteamdeckR2Softpull = 357,
        SteamdeckR2 = 358,
        SteamdeckLeftstickMove = 359,
        SteamdeckL3 = 360,
        SteamdeckLeftstickDpadnorth = 361,
        SteamdeckLeftstickDpadsouth = 362,
        SteamdeckLeftstickDpadwest = 363,
        SteamdeckLeftstickDpadeast = 364,
        SteamdeckLeftstickTouch = 365,
        SteamdeckRightstickMove = 366,
        SteamdeckR3 = 367,
        SteamdeckRightstickDpadnorth = 368,
        SteamdeckRightstickDpadsouth = 369,
        SteamdeckRightstickDpadwest = 370,
        SteamdeckRightstickDpadeast = 371,
        SteamdeckRightstickTouch = 372,
        SteamdeckL4 = 373,
        SteamdeckR4 = 374,
        SteamdeckL5 = 375,
        SteamdeckR5 = 376,
        SteamdeckDpadMove = 377,
        SteamdeckDpadNorth = 378,
        SteamdeckDpadSouth = 379,
        SteamdeckDpadWest = 380,
        SteamdeckDpadEast = 381,
        SteamdeckGyroMove = 382,
        SteamdeckGyroPitch = 383,
        SteamdeckGyroYaw = 384,
        SteamdeckGyroRoll = 385,
        SteamdeckReserved1 = 386,
        SteamdeckReserved2 = 387,
        SteamdeckReserved3 = 388,
        SteamdeckReserved4 = 389,
        SteamdeckReserved5 = 390,
        SteamdeckReserved6 = 391,
        SteamdeckReserved7 = 392,
        SteamdeckReserved8 = 393,
        SteamdeckReserved9 = 394,
        SteamdeckReserved10 = 395,
        SteamdeckReserved11 = 396,
        SteamdeckReserved12 = 397,
        SteamdeckReserved13 = 398,
        SteamdeckReserved14 = 399,
        SteamdeckReserved15 = 400,
        SteamdeckReserved16 = 401,
        SteamdeckReserved17 = 402,
        SteamdeckReserved18 = 403,
        SteamdeckReserved19 = 404,
        SteamdeckReserved20 = 405,
        Count = 406,
        MaximumPossibleValue = 32767
    }

    [System.Flags]
    public enum InputConfigurationEnableType : long
    {
        None = 0,
        Playstation = 1,
        Xbox = 2,
        Generic = 4,
        Switch = 8
    }

    public enum InputGlyphSize : long
    {
        Small = 0,
        Medium = 1,
        Large = 2,
        Count = 3
    }

    [System.Flags]
    public enum InputGlyphStyle : long
    {
        Knockout = 0,
        Light = 1,
        Dark = 2,
        NeutralColorAbxy = 16,
        SolidAbxy = 32
    }

    public enum InputLedFlag : long
    {
        SetColor = 0,
        RestoreUserDefault = 1
    }

    public enum InputSourceMode : long
    {
        None = 0,
        Dpad = 1,
        Buttons = 2,
        FourButtons = 3,
        AbsoluteMouse = 4,
        RelativeMouse = 5,
        JoystickMove = 6,
        JoystickMouse = 7,
        JoystickCamera = 8,
        ScrollWheel = 9,
        Trigger = 10,
        TouchMenu = 11,
        MouseJoystick = 12,
        MouseRegion = 13,
        RadialMenu = 14,
        SingleButton = 15,
        Switch = 16
    }

    public enum InputType : long
    {
        Unknown = 0,
        SteamController = 1,
        Xbox360Controller = 2,
        XboxoneController = 3,
        GenericXinput = 4,
        Ps4Controller = 5,
        AppleMfiController = 6,
        AndroidController = 7,
        SwitchJoyconPair = 8,
        SwitchJoyconSingle = 9,
        SwitchProController = 10,
        MobileTouch = 11,
        Ps3Controller = 12,
        Ps5Controller = 13,
        SteamDeckController = 14,
        Count = 15,
        MaximumPossibleValue = 255
    }
}