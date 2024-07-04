// Локализация главного меню

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu_Local : MonoBehaviour {
  public MainMenu script_MainMenu;
  
  public TextMeshProUGUI TLoc_main_Load;
  public TextMeshProUGUI TLoc_main_GameName;
  public TextMeshProUGUI TLoc_main_ButtonStart;
  public TextMeshProUGUI TLoc_main_ButtonContinue;
  // public TextMeshProUGUI TLoc_main_UserID;

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
    TLoc_main_Load.text = TextManager.Inst_TextData.textsData.main_Load;
    TLoc_main_GameName.text = TextManager.Inst_TextData.textsData.main_GameName;
    TLoc_main_ButtonStart.text = TextManager.Inst_TextData.textsData.main_ButtonStart;
    TLoc_main_ButtonContinue.text = TextManager.Inst_TextData.textsData.main_ButtonContinue;
    script_MainMenu.PlayerDataShowInMainMenu();
  }

  void CheckLocal() {
    if (TextManager.Inst_TextData.textsData.local == "ENG") {
      UI_ENG();
    }
    else UI_RUS();
  }
}
