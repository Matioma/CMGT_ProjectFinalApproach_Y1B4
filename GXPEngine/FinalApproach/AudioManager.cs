using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GXPEngine;
class AudioManager : GameObject
{
    private string InstantSoundPath = "SoundEffect/ButtonMenu";

    public static AudioManager Instance;

    public Dictionary<string, Sound> sounds;

    public Dictionary<string, Sound> soundsOnce;

    public SoundChannel NarratorSoundChannel;
    public Dictionary<string,SoundChannel> environmentSounds = new Dictionary<string,SoundChannel>();

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

        soundsOnce = new Dictionary<string, Sound>();
        sounds = new Dictionary<string, Sound>();
        sounds.Add("SoundEffect/ButtonMenu", new Sound("Audio/SoundEffect/ButtonMenu.wav"));
    }
    public void PlaySound(string key, string extension = ".wav") {
        if (NarratorSoundChannel != null) {
            NarratorSoundChannel.Stop();
        }
        if (!sounds.ContainsKey(key))
        {
            sounds.Add(key, new Sound("Audio/" + key + extension));
        }
        NarratorSoundChannel = sounds[key].Play();
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
    public void AddEnvironementSound(string key, string extension = ".wav") {
        if (!sounds.ContainsKey(key))
        {
            sounds.Add(key, new Sound("Audio/" + key + extension, true));
        }

        SoundChannel soundChannel = sounds[key].Play();
        environmentSounds.Add(key,soundChannel);
    }
    public void StopSounds() {
        foreach (var pair in environmentSounds) {
            pair.Value.Stop();
            //sound.Stop();
        }
        environmentSounds.Clear();
        //environmentSounds = new List<SoundChannel>();
    }

    public void StopSound(string key) {
        if (environmentSounds.ContainsKey(key)) {
            environmentSounds[key].Stop();
            environmentSounds.Remove(key);
        }
        else
        {
            Console.WriteLine("Key is missing when tried to stop the sound");

        }
    }

    public void PlayOnce(string key, string extension=".wav") 
    {    
        if(!soundsOnce.ContainsKey(key))
        { 
            soundsOnce.Add(key, new Sound("Audio/" + key + extension));
        }
        soundsOnce[key].Play();
    }

    public void ReduceVolume(float volume) {
        foreach (var sound in environmentSounds) {
            sound.Value.Volume = volume;
        }
    }
}

