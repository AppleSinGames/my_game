using UnityEngine;

public class Saves
{
    public static void ClearNum(string _toClear)
    {
        PlayerPrefs.SetFloat(_toClear, 0f);
    }
    public static void SaveNum(string _toSave, float num)
    {
        PlayerPrefs.SetFloat(_toSave, num);
    }

    public static int SetHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            return PlayerPrefs.GetInt("HighScore");
        }

        return 0;
    }

    public static void ChangeHighScore(int Score)
    {
        if (Score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", Score);
        }
    }

    public static float ChangeSound()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            return PlayerPrefs.GetFloat("Music");
        }

        return 0;
    }
}
