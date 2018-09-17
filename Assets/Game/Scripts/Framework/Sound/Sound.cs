using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class Sound : Singleton<Sound>
{
    protected override void Awake()
    {
        base.Awake();

        m_BGM = gameObject.AddComponent<AudioSource>();
        m_BGM.playOnAwake = false;
        m_BGM.loop = true;

        m_effectSound = gameObject.AddComponent<AudioSource>();
    }

    public string ResourceDir = "";

    AudioSource m_BGM;
    AudioSource m_effectSound;

    //BGM Vol
    public float BGMVol
    {
        get { return m_BGM.volume; }
        set { m_BGM.volume = value; }
    }

    //Sound Effect Vol
    public float EffectSoundVol
    {
        get { return m_effectSound.volume; }
        set { m_effectSound.volume = value; }
    }

    //Play BGM
    public void PlayBGM(string audioName)
    {
        //The name of the BGM now
        string oldName;
        if (m_BGM.clip == null)
        {
            oldName = "";
        }
        else
        {
            oldName = m_BGM.clip.name;
        }

        if (oldName != audioName)
        {
            string path;
            if (string.IsNullOrEmpty(ResourceDir))
            {
                path = "";
            }
            else
            {
                path = ResourceDir + "/" + audioName;
            }
            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip != null)
            {
                m_BGM.clip = clip;
                m_BGM.Play();
            }
        }
    }

    //Stop BGM
    public void StopBGM()
    {
        m_BGM.Stop();
        m_BGM.clip = null;
    }

    //Play Sound Effect
    public void PlayEffect(string audioName)
    {
        string path;
        if (string.IsNullOrEmpty(ResourceDir))
        {
            path = "";
        }
        else
        {
            path = ResourceDir + "/" + audioName;
        }
        AudioClip clip = Resources.Load<AudioClip>(path);

        m_effectSound.PlayOneShot(clip);
    }
}
