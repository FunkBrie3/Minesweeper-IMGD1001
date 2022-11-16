using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private static Dictionary<string, AudioClip> m_clips = new Dictionary<string, AudioClip>();
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        foreach (AudioClip c in clips) m_clips.Add(c.name, c);
    }

    public enum AudioType
    {
        Blast,
        BreakTool,
        ChallengeComplete,
        Click,
        Explode,
        GrassBreak,
        GravelBreak,
        FireworkLaunch,
        FireworkBlast,
        FireworkTwinkle,
        Pop,
        SandBreak,
        SnowBreak,
        StoneBreak,
        WoodBreak,
        WoodClick
    }

    public static void Play(AudioType audioType)
    {
        switch(audioType)
        {

        }
    }
    private static void PlayInternal(string[] sounds)
    {

    }
}
