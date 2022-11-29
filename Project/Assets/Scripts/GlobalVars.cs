using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    public static Color bgcolor = new Color(194f / 255, 153f / 255, 196f / 255);
    public static ThemeSet themeSet = ThemeSet.Logs;
    public static int width = 12;
    public static int height = 12;
    public static int mineCount = 22;
    public static float cameraSize = 7.5f;
    public static string GetThemeDirectory() { return themeSet.ToString().ToLower(); }
    public static bool isLined = false;
    public enum ThemeSet
    {
        Clay, Logs, Ores
    }
}
