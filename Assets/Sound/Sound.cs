using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // ��� ��� ��� ���������� ����

        // �������� ������ ����������
        if (!Application.isFocused) {
            // ���������� ��������������� �����
            AudioListener.pause = true;
        }
        else {
            // ����������� ��������������� �����
            AudioListener.pause = false;
        }
    }

    private void OnApplicationPause(bool pauseStatus) {
        if (pauseStatus) {
            // ���������� ��������������� �����
            AudioListener.pause = true;
        }
        else {
            // ����������� ��������������� �����
            AudioListener.pause = false;
        }
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) {
            // ���������� ��������������� �����
            AudioListener.pause = true;
        }
        else {
            // ����������� ��������������� �����
            AudioListener.pause = false;
        }
    }


}
