using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victory_Local : MonoBehaviour {
  public TextMeshProUGUI Victory;

  void Start() {
    CheckLocal();
  }

  public void UI_RUS() {
    // TextManager.Inst_TextData.LoadJsonLocal("RUS");
    TextManager.Inst_TextData.LoadLocal("RUS");
    UpgradeUI();
  }

  public void UI_ENG() {
    // TextManager.Inst_TextData.LoadJsonLocal("ENG");
    TextManager.Inst_TextData.LoadLocal("ENG");
    UpgradeUI();
  }

  public void UpgradeUI() {
    Victory.text = TextManager.Inst_TextData.textsData.Victory;
  }

  void CheckLocal() {
    if (TextManager.Inst_TextData.textsData.local == "ENG") {
      UI_ENG();
    }
    else {
      UI_RUS();
    }
  }
}
