using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver_Local : MonoBehaviour {
  public TextMeshProUGUI GameOver;
  public TextMeshProUGUI Restart;
  public TextMeshProUGUI main_Load;

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
    GameOver.text = TextManager.Inst_TextData.textsData.GameOver;
    Restart.text = TextManager.Inst_TextData.textsData.Restart;
    main_Load.text = TextManager.Inst_TextData.textsData.main_Load;
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
