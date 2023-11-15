using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundV2 : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus) {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused) {
        Silence(isPaused);
    }

    private void Silence(bool silence) {
        AudioListener.pause = silence;
        // Or / And
        AudioListener.volume = silence ? 0 : 1;
    }
}
