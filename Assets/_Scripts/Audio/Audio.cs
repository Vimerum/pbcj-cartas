using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Audio {

    /*
     * Classe usada para configurar os efeitos sonoros/músicas
     */

    public string name;
    [Range(0f, 1f)]
    public float volume = 1f;
    public bool playOnAwake;
    public bool loop;
    public AudioClip clip;

    [ReadOnly]
    public AudioSource source;

    public override bool Equals(object obj) {
        var audio = obj as Audio;
        return audio != null &&
               name == audio.name;
    }

    public override int GetHashCode() {
        return 363513814 + EqualityComparer<string>.Default.GetHashCode(name);
    }
}
