using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
class AudioManager : GameObject
{
    public static AudioManager Instance;

    public Dictionary<string, Sound> sounds;

    public SoundChannel NarratorSoundChannel;
    public List<SoundChannel> environmentSounds = new List<SoundChannel>();

    Action onSoundEnd;

    void Update() {
        if (NarratorSoundChannel != null) {
            if (onSoundEnd != null) {
                onSoundEnd.Invoke();
            }
        }
    }


    public AudioManager() {
        Instance = this;
        sounds = new Dictionary<string, Sound>();
    }
    public void PlaySound(string key, string extension = ".wav") {
        if (NarratorSoundChannel != null) {
            NarratorSoundChannel.Stop();
        }
        if (!sounds.ContainsKey(key))
        {
            sounds.Add(key, new Sound("Audio/" + key + extension));
        }
        NarratorSoundChannel =sounds[key].Play();
    }

    public void PlayerDialogText(string key, Action onSoundEnd, string extension = ".wav")
    {
        this.onSoundEnd = onSoundEnd;
        if (NarratorSoundChannel != null)
        {
            NarratorSoundChannel.Stop();
            if (onSoundEnd != null)
            {
                onSoundEnd.Invoke();
            }
        }
        if (!sounds.ContainsKey(key))
        {
            sounds.Add(key, new Sound("Audio/" + key + extension));
        }
        NarratorSoundChannel = sounds[key].Play();

    }
    public void AddEnvironementSound(string key , string extension =".wav") {
        if (!sounds.ContainsKey(key))
        {
            sounds.Add(key, new Sound("Audio/" + key + extension, true));
        }

        SoundChannel soundChannel = sounds[key].Play();
        environmentSounds.Add(soundChannel);
    }
    public void StopSounds() {
        foreach (var sound in environmentSounds) {
            sound.Stop();
        }
        environmentSounds = new List<SoundChannel>();
    }

    public void ReduceVolume(float volume) {
        foreach (var sound in environmentSounds) {
            sound.Volume = volume;
        }
    }
}

