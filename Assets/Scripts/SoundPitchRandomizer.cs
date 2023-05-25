using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class SoundPitchRandomizer
{
    private static Dictionary<AudioSource, float> audioSourceBasePitches = new Dictionary<AudioSource, float>();

    public static void PlaySoundWithRandomPitch(AudioSource audioSource, AudioClip audioClip, float volumeScale, float pitchVariationRange)
    {
        if(!audioSourceBasePitches.TryGetValue(audioSource, out float basePitch))
        {
            basePitch = audioSource.pitch;

            audioSourceBasePitches.Add(audioSource, basePitch);
        }
        
        audioSource.pitch = basePitch + UnityEngine.Random.Range(basePitch - pitchVariationRange, basePitch + pitchVariationRange);

        audioSource.PlayOneShot(audioClip, volumeScale);
    }
}
