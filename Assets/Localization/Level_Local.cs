// Локализует интерфейсы игры

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Local : MonoBehaviour {
  public TextMeshProUGUI TLoc_button_CurrentTask;
  public TextMeshProUGUI TLoc_button_Shop;
  public TextMeshProUGUI TLoc_button_Menu;

  public TextMeshProUGUI TLoc_PlayMenu_Pause;
  public TextMeshProUGUI TLoc_PlayMenu_GraphicsQuality;
  public TextMeshProUGUI TLoc_PlayMenu_BUTT_QualityLow;
  public TextMeshProUGUI TLoc_PlayMenu_BUTT_QualityNormal;
  public TextMeshProUGUI TLoc_PlayMenu_BUTT_QualityHi;
  public TextMeshProUGUI TLoc_PlayMenu_BUTT_MainMenu;
  public TextMeshProUGUI TLoc_PlayMenu_BUTT_Close;

  public TextMeshProUGUI TLoc_Store_WeaponUpgrade;
  public TextMeshProUGUI TLoc_Store_FieldUpgrade;
  public TextMeshProUGUI TLoc_Store_BUTT_Buy_01;
  public TextMeshProUGUI TLoc_Store_BUTT_Buy_02;
  public TextMeshProUGUI TLoc_Store_BUTT_Buy_03;
  public TextMeshProUGUI TLoc_Store_BUTT_Buy_04;

  public TextMeshProUGUI TLoc_NewTask;

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
    TLoc_button_CurrentTask.text = TextManager.Inst_TextData.textsData.button_CurrentTask;
    TLoc_button_Shop.text = TextManager.Inst_TextData.textsData.button_Shop;
    TLoc_button_Menu.text = TextManager.Inst_TextData.textsData.button_Menu;

    TLoc_PlayMenu_Pause.text = TextManager.Inst_TextData.textsData.PlayMenu_Pause;
    TLoc_PlayMenu_GraphicsQuality.text = TextManager.Inst_TextData.textsData.PlayMenu_GraphicsQuality;
    TLoc_PlayMenu_BUTT_QualityLow.text = TextManager.Inst_TextData.textsData.PlayMenu_BUTT_QualityLow;
    TLoc_PlayMenu_BUTT_QualityNormal.text = TextManager.Inst_TextData.textsData.PlayMenu_BUTT_QualityNormal;
    TLoc_PlayMenu_BUTT_QualityHi.text = TextManager.Inst_TextData.textsData.PlayMenu_BUTT_QualityHi;
    TLoc_PlayMenu_BUTT_MainMenu.text = TextManager.Inst_TextData.textsData.PlayMenu_BUTT_MainMenu;
    TLoc_PlayMenu_BUTT_Close.text = TextManager.Inst_TextData.textsData.PlayMenu_BUTT_Close;

    TLoc_Store_WeaponUpgrade.text = TextManager.Inst_TextData.textsData.Store_WeaponUpgrade;
    TLoc_Store_FieldUpgrade.text = TextManager.Inst_TextData.textsData.Store_FieldUpgrade;
    TLoc_Store_BUTT_Buy_01.text = TextManager.Inst_TextData.textsData.Store_BUTT_Buy;
    TLoc_Store_BUTT_Buy_02.text = TextManager.Inst_TextData.textsData.Store_BUTT_Buy;
    TLoc_Store_BUTT_Buy_03.text = TextManager.Inst_TextData.textsData.Store_BUTT_Buy;
    TLoc_Store_BUTT_Buy_04.text = TextManager.Inst_TextData.textsData.Store_BUTT_Buy;

    TLoc_NewTask.text = TextManager.Inst_TextData.textsData.NewTask;
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
