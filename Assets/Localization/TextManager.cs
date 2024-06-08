using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class TextsData {
  public string game_name;
}

public class TextManager : MonoBehaviour {
  public TextsData textsDataOBJ;
  public static TextManager InstanceTextData;

  // Start is called before the first frame update
  void Start() {
    LoadJsonLocal("RUS");
  }

  private void Awake() {
    if (InstanceTextData == null) {
      transform.parent = null;
      DontDestroyOnLoad(gameObject);
      InstanceTextData = this;
    }
    else {
      Destroy(gameObject);
    }
  }

  public void LoadJsonLocal(string local) {
    string json_str;
    switch (local) {
      case "RUS":
        json_str = File.ReadAllText(Application.dataPath + "/Localization/local_RUS.json");
        break;
      case "ENG":
        json_str = File.ReadAllText(Application.dataPath + "/Localization/local_ENG.json");
        break;
      default:
        json_str = File.ReadAllText(Application.dataPath + "/Localization/local_RUS.json");
        break;
    }
    textsDataOBJ = JsonUtility.FromJson<TextsData>(json_str);
  }

}
