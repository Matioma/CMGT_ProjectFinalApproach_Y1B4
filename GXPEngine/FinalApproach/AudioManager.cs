using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class AudioManager:GameObject
{
    public static AudioManager Instance;

    public Dictionary<string, Sound> sounds;

    public SoundChannel currentSoundChannel; 
    public AudioManager() {
        Instance = this;
        sounds = new Dictionary<string, Sound>();
    }
    public void PlaySound(string key) {
        if (currentSoundChannel != null) {
            currentSoundChannel.Stop();
        }

        if (!sounds.ContainsKey(key))
        {
            sounds.Add(key, new Sound("Audio/" + key + ".wav"));
        }
        currentSoundChannel =sounds[key].Play();

    }



}

