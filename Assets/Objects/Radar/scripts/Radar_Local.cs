using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Radar_Local : MonoBehaviour {
  public TextMeshProUGUI Radar_BUTT_Goal;
  public TextMeshProUGUI Radar_BUTT_Rotation;
  public TextMeshProUGUI Radar_BUTT_Tilt;
  public TextMeshProUGUI Radar_BUTT_WhereIAm;

  // Start is called before the first frame update
  void Start() {
    LoadLocalText();
  }

  // Update is called once per frame
  void Update() {

  }

  void LoadLocalText() {
    Radar_BUTT_Goal.text = TextManager.Inst_TextData.textsData.Radar_BUTT_Goal;
    Radar_BUTT_Rotation.text = TextManager.Inst_TextData.textsData.Radar_BUTT_Rotation;
    Radar_BUTT_Tilt.text = TextManager.Inst_TextData.textsData.Radar_BUTT_Tilt;
    Radar_BUTT_WhereIAm.text = TextManager.Inst_TextData.textsData.Radar_BUTT_WhereIAm;
  }
}
