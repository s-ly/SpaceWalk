using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[System.Serializable]
public class TextsData {
  public string local;
  public string main_Load;
  public string main_GameName;
  public string main_ButtonStart;
  public string main_ButtonContinue;
  public string main_UserID;

  public string main_PlDatStr_GamP;
  public string main_PlDatStr_Crys;
  public string main_PlDatStr_MaOx;
  public string main_PlDatStr_Tech;
  public string main_PlDatStr_CurT;
  public string main_PlDatStr_WeRT;
  public string main_PlDatStr_PSpe;
  public string main_PlDatStr_Phea;
  public string main_PlDatStr_MaFu;
  public string main_PlDatStr_PrFi;

  public string MissionDialog_00;
  public string MissionDialog_01;
  public string MissionDialog_02;
  public string MissionDialog_03;
  public string MissionDialog_04;
  public string MissionDialog_05;
  public string MissionDialog_06;
  public string MissionDialog_07;
  public string MissionDialog_08;
  public string MissionDialog_09;
  public string MissionDialog_10;
  public string MissionDialog_11;

  public string button_CurrentTask;
  public string button_Shop;
  public string button_Menu;
  public string NewTask;

  public string fps;

  public string PlayMenu_Pause;
  public string PlayMenu_GraphicsQuality;
  public string PlayMenu_BUTT_QualityLow;
  public string PlayMenu_BUTT_QualityNormal;
  public string PlayMenu_BUTT_QualityHi;
  public string PlayMenu_BUTT_MainMenu;
  public string PlayMenu_BUTT_Close;

  public string Store_RechargeTime;
  public string Store_Sec;
  public string Store_CurrentFieldLevel;
  public string Store_Maximum4;
  public string Store_WeaponUpgrade;
  public string Store_FieldUpgrade;
  public string Store_BUTT_Buy;

  public string Radar_BUTT_Goal;
  public string Radar_BUTT_Rotation;
  public string Radar_BUTT_Tilt;
  public string Radar_BUTT_WhereIAm;
  public string Radar_CurrentGoal;
  public string Radar_PressR;

  public string Restart;
  public string GameOver;
  public string Victory;
}

public class TextManager : MonoBehaviour {
  public LocalData_RUS LD_RUS;
  public LocalData_ENG LD_ENG;
  public TextsData textsData;
  public static TextManager Inst_TextData;

  void Start() {
    LoadLocal("RUS");
  }

  private void Awake() {
    if (Inst_TextData == null) {
      transform.parent = null;
      DontDestroyOnLoad(gameObject);
      Inst_TextData = this;
    }
    else {
      Destroy(gameObject);
    }
  }

  public void LoadLocal(string local) {
    if (local == "RUS") {
      textsData.local = LD_RUS.get_local();
      textsData.main_Load = LD_RUS.get_main_Load();
      textsData.main_GameName = LD_RUS.get_main_GameName();
      textsData.main_ButtonStart = LD_RUS.get_main_ButtonStart();
      textsData.main_ButtonContinue = LD_RUS.get_main_ButtonContinue();
      textsData.main_UserID = LD_RUS.get_main_UserID();

      textsData.main_PlDatStr_GamP = LD_RUS.get_main_PlDatStr_GamP();
      textsData.main_PlDatStr_Crys = LD_RUS.get_main_PlDatStr_Crys();
      textsData.main_PlDatStr_MaOx = LD_RUS.get_main_PlDatStr_MaOx();
      textsData.main_PlDatStr_Tech = LD_RUS.get_main_PlDatStr_Tech();
      textsData.main_PlDatStr_CurT = LD_RUS.get_main_PlDatStr_CurT();
      textsData.main_PlDatStr_WeRT = LD_RUS.get_main_PlDatStr_WeRT();
      textsData.main_PlDatStr_PSpe = LD_RUS.get_main_PlDatStr_PSpe();
      textsData.main_PlDatStr_Phea = LD_RUS.get_main_PlDatStr_Phea();
      textsData.main_PlDatStr_MaFu = LD_RUS.get_main_PlDatStr_MaFu();
      textsData.main_PlDatStr_PrFi = LD_RUS.get_main_PlDatStr_PrFi();

      textsData.MissionDialog_00 = LD_RUS.get_MissionDialog_00();
      textsData.MissionDialog_01 = LD_RUS.get_MissionDialog_01();
      textsData.MissionDialog_02 = LD_RUS.get_MissionDialog_02();
      textsData.MissionDialog_03 = LD_RUS.get_MissionDialog_03();
      textsData.MissionDialog_04 = LD_RUS.get_MissionDialog_04();
      textsData.MissionDialog_05 = LD_RUS.get_MissionDialog_05();
      textsData.MissionDialog_06 = LD_RUS.get_MissionDialog_06();
      textsData.MissionDialog_07 = LD_RUS.get_MissionDialog_07();
      textsData.MissionDialog_08 = LD_RUS.get_MissionDialog_08();
      textsData.MissionDialog_09 = LD_RUS.get_MissionDialog_09();
      textsData.MissionDialog_10 = LD_RUS.get_MissionDialog_10();
      textsData.MissionDialog_11 = LD_RUS.get_MissionDialog_11();

      textsData.button_CurrentTask = LD_RUS.get_button_CurrentTask();
      textsData.button_Shop = LD_RUS.get_button_Shop();
      textsData.button_Menu = LD_RUS.get_button_Menu();
      textsData.NewTask = LD_RUS.get_NewTask();

      textsData.fps = LD_RUS.get_fps();

      textsData.PlayMenu_Pause = LD_RUS.get_PlayMenu_Pause();
      textsData.PlayMenu_GraphicsQuality = LD_RUS.get_PlayMenu_GraphicsQuality();
      textsData.PlayMenu_BUTT_QualityLow = LD_RUS.get_PlayMenu_BUTT_QualityLow();
      textsData.PlayMenu_BUTT_QualityNormal = LD_RUS.get_PlayMenu_BUTT_QualityNormal();
      textsData.PlayMenu_BUTT_QualityHi = LD_RUS.get_PlayMenu_BUTT_QualityHi();
      textsData.PlayMenu_BUTT_MainMenu = LD_RUS.get_PlayMenu_BUTT_MainMenu();
      textsData.PlayMenu_BUTT_Close = LD_RUS.get_PlayMenu_BUTT_Close();

      textsData.Store_RechargeTime = LD_RUS.get_Store_RechargeTime();
      textsData.Store_Sec = LD_RUS.get_Store_Sec();
      textsData.Store_CurrentFieldLevel = LD_RUS.get_Store_CurrentFieldLevel();
      textsData.Store_Maximum4 = LD_RUS.get_Store_Maximum4();
      textsData.Store_WeaponUpgrade = LD_RUS.get_Store_WeaponUpgrade();
      textsData.Store_FieldUpgrade = LD_RUS.get_Store_FieldUpgrade();
      textsData.Store_BUTT_Buy = LD_RUS.get_Store_BUTT_Buy();

      textsData.Radar_BUTT_Goal = LD_RUS.get_Radar_BUTT_Goal();
      textsData.Radar_BUTT_Rotation = LD_RUS.get_Radar_BUTT_Rotation();
      textsData.Radar_BUTT_Tilt = LD_RUS.get_Radar_BUTT_Tilt();
      textsData.Radar_BUTT_WhereIAm = LD_RUS.get_Radar_BUTT_WhereIAm();
      textsData.Radar_CurrentGoal = LD_RUS.get_Radar_CurrentGoal();
      textsData.Radar_PressR = LD_RUS.get_Radar_PressR();

      textsData.Restart = LD_RUS.get_Restart();
      textsData.GameOver = LD_RUS.get_GameOver();
      textsData.Victory = LD_RUS.get_Victory();
    }
    if (local == "ENG") {
      textsData.local = LD_ENG.get_local();
      textsData.main_Load = LD_ENG.get_main_Load();
      textsData.main_GameName = LD_ENG.get_main_GameName();
      textsData.main_ButtonStart = LD_ENG.get_main_ButtonStart();
      textsData.main_ButtonContinue = LD_ENG.get_main_ButtonContinue();
      textsData.main_UserID = LD_ENG.get_main_UserID();

      textsData.main_PlDatStr_GamP = LD_ENG.get_main_PlDatStr_GamP();
      textsData.main_PlDatStr_Crys = LD_ENG.get_main_PlDatStr_Crys();
      textsData.main_PlDatStr_MaOx = LD_ENG.get_main_PlDatStr_MaOx();
      textsData.main_PlDatStr_Tech = LD_ENG.get_main_PlDatStr_Tech();
      textsData.main_PlDatStr_CurT = LD_ENG.get_main_PlDatStr_CurT();
      textsData.main_PlDatStr_WeRT = LD_ENG.get_main_PlDatStr_WeRT();
      textsData.main_PlDatStr_PSpe = LD_ENG.get_main_PlDatStr_PSpe();
      textsData.main_PlDatStr_Phea = LD_ENG.get_main_PlDatStr_Phea();
      textsData.main_PlDatStr_MaFu = LD_ENG.get_main_PlDatStr_MaFu();
      textsData.main_PlDatStr_PrFi = LD_ENG.get_main_PlDatStr_PrFi();

      textsData.MissionDialog_00 = LD_ENG.get_MissionDialog_00();
      textsData.MissionDialog_01 = LD_ENG.get_MissionDialog_01();
      textsData.MissionDialog_02 = LD_ENG.get_MissionDialog_02();
      textsData.MissionDialog_03 = LD_ENG.get_MissionDialog_03();
      textsData.MissionDialog_04 = LD_ENG.get_MissionDialog_04();
      textsData.MissionDialog_05 = LD_ENG.get_MissionDialog_05();
      textsData.MissionDialog_06 = LD_ENG.get_MissionDialog_06();
      textsData.MissionDialog_07 = LD_ENG.get_MissionDialog_07();
      textsData.MissionDialog_08 = LD_ENG.get_MissionDialog_08();
      textsData.MissionDialog_09 = LD_ENG.get_MissionDialog_09();
      textsData.MissionDialog_10 = LD_ENG.get_MissionDialog_10();
      textsData.MissionDialog_11 = LD_ENG.get_MissionDialog_11();

      textsData.button_CurrentTask = LD_ENG.get_button_CurrentTask();
      textsData.button_Shop = LD_ENG.get_button_Shop();
      textsData.button_Menu = LD_ENG.get_button_Menu();
      textsData.NewTask = LD_ENG.get_NewTask();

      textsData.fps = LD_ENG.get_fps();

      textsData.PlayMenu_Pause = LD_ENG.get_PlayMenu_Pause();
      textsData.PlayMenu_GraphicsQuality = LD_ENG.get_PlayMenu_GraphicsQuality();
      textsData.PlayMenu_BUTT_QualityLow = LD_ENG.get_PlayMenu_BUTT_QualityLow();
      textsData.PlayMenu_BUTT_QualityNormal = LD_ENG.get_PlayMenu_BUTT_QualityNormal();
      textsData.PlayMenu_BUTT_QualityHi = LD_ENG.get_PlayMenu_BUTT_QualityHi();
      textsData.PlayMenu_BUTT_MainMenu = LD_ENG.get_PlayMenu_BUTT_MainMenu();
      textsData.PlayMenu_BUTT_Close = LD_ENG.get_PlayMenu_BUTT_Close();

      textsData.Store_RechargeTime = LD_ENG.get_Store_RechargeTime();
      textsData.Store_Sec = LD_ENG.get_Store_Sec();
      textsData.Store_CurrentFieldLevel = LD_ENG.get_Store_CurrentFieldLevel();
      textsData.Store_Maximum4 = LD_ENG.get_Store_Maximum4();
      textsData.Store_WeaponUpgrade = LD_ENG.get_Store_WeaponUpgrade();
      textsData.Store_FieldUpgrade = LD_ENG.get_Store_FieldUpgrade();
      textsData.Store_BUTT_Buy = LD_ENG.get_Store_BUTT_Buy();

      textsData.Radar_BUTT_Goal = LD_ENG.get_Radar_BUTT_Goal();
      textsData.Radar_BUTT_Rotation = LD_ENG.get_Radar_BUTT_Rotation();
      textsData.Radar_BUTT_Tilt = LD_ENG.get_Radar_BUTT_Tilt();
      textsData.Radar_BUTT_WhereIAm = LD_ENG.get_Radar_BUTT_WhereIAm();
      textsData.Radar_CurrentGoal = LD_ENG.get_Radar_CurrentGoal();
      textsData.Radar_PressR = LD_ENG.get_Radar_PressR();

      textsData.Restart = LD_ENG.get_Restart();
      textsData.GameOver = LD_ENG.get_GameOver();
      textsData.Victory = LD_ENG.get_Victory();
    }
  }
}
