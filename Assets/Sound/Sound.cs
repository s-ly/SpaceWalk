using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // Ваш код для обновления игры

        // Проверка фокуса приложения
        if (!Application.isFocused) {
            // Остановить воспроизведение звука
            AudioListener.pause = true;
        }
        else {
            // Возобновить воспроизведение звука
            AudioListener.pause = false;
        }
    }

    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            // Остановить воспроизведение звука
            AudioListener.pause = true;
        }
        else {
            // Возобновить воспроизведение звука
            AudioListener.pause = false;
        }
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) {
            // Остановить воспроизведение звука
            AudioListener.pause = true;
        }
        else {
            // Возобновить воспроизведение звука
            AudioListener.pause = false;
        }
    }


}
