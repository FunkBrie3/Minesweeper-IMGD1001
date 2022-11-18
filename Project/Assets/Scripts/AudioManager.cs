using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private static Dictionary<string, AudioClip> m_clips = new Dictionary<string, AudioClip>();
    public static GameObject singleton = null;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        singleton = this.gameObject;
        foreach (AudioClip c in singleton.GetComponent<AudioManager>().clips) m_clips.Add(c.name, c);
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

    public static bool Play(AudioType audioType) => audioType switch
    {
        AudioType.Blast => PlayInternal("blast1", "blast_far1"),
        AudioType.BreakTool => PlayInternal("break"),
        AudioType.ChallengeComplete => PlayInternal("challenge_complete"),
        AudioType.Click => PlayInternal("click"),
        AudioType.Explode => PlayInternal("explode1", "explode2", "explode3", "explode4"),
        AudioType.GrassBreak => PlayInternal("grass1", "grass2", "grass3", "grass4"),
        AudioType.GravelBreak => PlayInternal("gravel1", "gravel2", "gravel3", "gravel4"),
        AudioType.FireworkLaunch => PlayInternal("launch1"),
        AudioType.FireworkBlast => PlayInternal("largeblast1", "largeblast_far1"),
        AudioType.FireworkTwinkle => PlayInternal("twinkle1", "twinkle_far1"),
        AudioType.Pop => PlayInternal("pop"),
        AudioType.SandBreak => PlayInternal("sand1", "sand2", "sand3", "sand4"),
        AudioType.SnowBreak => PlayInternal("snow1", "snow2", "snow3", "snow4"),
        AudioType.StoneBreak => PlayInternal("stone1", "stone2", "stone3", "stone4"),
        AudioType.WoodBreak => PlayInternal("wood1", "wood2", "wood3", "wood4"),
        AudioType.WoodClick => PlayInternal("wood_click"),
        _ => false
    };

    private static bool PlayInternal(params string[] sounds)
    {
        AudioClip c;
        if (m_clips.Count == 0 || singleton == null) return false;
        m_clips.TryGetValue(sounds[Random.Range(0, sounds.Length)], out c);
        AudioSource source = singleton.AddComponent<AudioSource>();
        source.clip = c;
        source.Play();
        Destroy(source, c.length);
        return true;
    }
}