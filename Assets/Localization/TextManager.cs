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
      textsData.local = LD_RUS.local;
      textsData.main_Load = LD_RUS.main_Load;
      textsData.main_GameName = LD_RUS.main_GameName;
      textsData.main_ButtonStart = LD_RUS.main_ButtonStart;
      textsData.main_ButtonContinue = LD_RUS.main_ButtonContinue;
      textsData.main_UserID = LD_RUS.main_UserID;

      textsData.main_PlDatStr_GamP = LD_RUS.main_PlDatStr_GamP;
      textsData.main_PlDatStr_Crys = LD_RUS.main_PlDatStr_Crys;
      textsData.main_PlDatStr_MaOx = LD_RUS.main_PlDatStr_MaOx;
      textsData.main_PlDatStr_Tech = LD_RUS.main_PlDatStr_Tech;
      textsData.main_PlDatStr_CurT = LD_RUS.main_PlDatStr_CurT;
      textsData.main_PlDatStr_WeRT = LD_RUS.main_PlDatStr_WeRT;
      textsData.main_PlDatStr_PSpe = LD_RUS.main_PlDatStr_PSpe;
      textsData.main_PlDatStr_Phea = LD_RUS.main_PlDatStr_Phea;
      textsData.main_PlDatStr_MaFu = LD_RUS.main_PlDatStr_MaFu;
      textsData.main_PlDatStr_PrFi = LD_RUS.main_PlDatStr_PrFi;

      textsData.MissionDialog_00 = LD_RUS.MissionDialog_00;
      textsData.MissionDialog_01 = LD_RUS.MissionDialog_01;
      textsData.MissionDialog_02 = LD_RUS.MissionDialog_02;
      textsData.MissionDialog_03 = LD_RUS.MissionDialog_03;
      textsData.MissionDialog_04 = LD_RUS.MissionDialog_04;
      textsData.MissionDialog_05 = LD_RUS.MissionDialog_05;
      textsData.MissionDialog_06 = LD_RUS.MissionDialog_06;
      textsData.MissionDialog_07 = LD_RUS.MissionDialog_07;
      textsData.MissionDialog_08 = LD_RUS.MissionDialog_08;
      textsData.MissionDialog_09 = LD_RUS.MissionDialog_09;
      textsData.MissionDialog_10 = LD_RUS.MissionDialog_10;
      textsData.MissionDialog_11 = LD_RUS.MissionDialog_11;

      textsData.button_CurrentTask = LD_RUS.button_CurrentTask;
      textsData.button_Shop = LD_RUS.button_Shop;
      textsData.button_Menu = LD_RUS.button_Menu;
      textsData.NewTask = LD_RUS.NewTask;

      textsData.fps = LD_RUS.fps;

      textsData.PlayMenu_Pause = LD_RUS.PlayMenu_Pause;
      textsData.PlayMenu_GraphicsQuality = LD_RUS.PlayMenu_GraphicsQuality;
      textsData.PlayMenu_BUTT_QualityLow = LD_RUS.PlayMenu_BUTT_QualityLow;
      textsData.PlayMenu_BUTT_QualityNormal = LD_RUS.PlayMenu_BUTT_QualityNormal;
      textsData.PlayMenu_BUTT_QualityHi = LD_RUS.PlayMenu_BUTT_QualityHi;
      textsData.PlayMenu_BUTT_MainMenu = LD_RUS.PlayMenu_BUTT_MainMenu;
      textsData.PlayMenu_BUTT_Close = LD_RUS.PlayMenu_BUTT_Close;

      textsData.Store_RechargeTime = LD_RUS.Store_RechargeTime;
      textsData.Store_Sec = LD_RUS.Store_Sec;
      textsData.Store_CurrentFieldLevel = LD_RUS.Store_CurrentFieldLevel;
      textsData.Store_Maximum4 = LD_RUS.Store_Maximum4;
      textsData.Store_WeaponUpgrade = LD_RUS.Store_WeaponUpgrade;
      textsData.Store_FieldUpgrade = LD_RUS.Store_FieldUpgrade;
      textsData.Store_BUTT_Buy = LD_RUS.Store_BUTT_Buy;

      textsData.Radar_BUTT_Goal = LD_RUS.Radar_BUTT_Goal;
      textsData.Radar_BUTT_Rotation = LD_RUS.Radar_BUTT_Rotation;
      textsData.Radar_BUTT_Tilt = LD_RUS.Radar_BUTT_Tilt;
      textsData.Radar_BUTT_WhereIAm = LD_RUS.Radar_BUTT_WhereIAm;
      textsData.Radar_CurrentGoal = LD_RUS.Radar_CurrentGoal;
      textsData.Radar_PressR = LD_RUS.Radar_PressR;

      textsData.Restart = LD_RUS.Restart;
      textsData.GameOver = LD_RUS.GameOver;
      textsData.Victory = LD_RUS.Victory;
    }
    if (local == "ENG") {
      textsData.local = LD_ENG.local;
      textsData.main_Load = LD_ENG.main_Load;
      textsData.main_GameName = LD_ENG.main_GameName;
      textsData.main_ButtonStart = LD_ENG.main_ButtonStart;
      textsData.main_ButtonContinue = LD_ENG.main_ButtonContinue;
      textsData.main_UserID = LD_ENG.main_UserID;

      textsData.main_PlDatStr_GamP = LD_ENG.main_PlDatStr_GamP;
      textsData.main_PlDatStr_Crys = LD_ENG.main_PlDatStr_Crys;
      textsData.main_PlDatStr_MaOx = LD_ENG.main_PlDatStr_MaOx;
      textsData.main_PlDatStr_Tech = LD_ENG.main_PlDatStr_Tech;
      textsData.main_PlDatStr_CurT = LD_ENG.main_PlDatStr_CurT;
      textsData.main_PlDatStr_WeRT = LD_ENG.main_PlDatStr_WeRT;
      textsData.main_PlDatStr_PSpe = LD_ENG.main_PlDatStr_PSpe;
      textsData.main_PlDatStr_Phea = LD_ENG.main_PlDatStr_Phea;
      textsData.main_PlDatStr_MaFu = LD_ENG.main_PlDatStr_MaFu;
      textsData.main_PlDatStr_PrFi = LD_ENG.main_PlDatStr_PrFi;

      textsData.MissionDialog_00 = LD_ENG.MissionDialog_00;
      textsData.MissionDialog_01 = LD_ENG.MissionDialog_01;
      textsData.MissionDialog_02 = LD_ENG.MissionDialog_02;
      textsData.MissionDialog_03 = LD_ENG.MissionDialog_03;
      textsData.MissionDialog_04 = LD_ENG.MissionDialog_04;
      textsData.MissionDialog_05 = LD_ENG.MissionDialog_05;
      textsData.MissionDialog_06 = LD_ENG.MissionDialog_06;
      textsData.MissionDialog_07 = LD_ENG.MissionDialog_07;
      textsData.MissionDialog_08 = LD_ENG.MissionDialog_08;
      textsData.MissionDialog_09 = LD_ENG.MissionDialog_09;
      textsData.MissionDialog_10 = LD_ENG.MissionDialog_10;
      textsData.MissionDialog_11 = LD_ENG.MissionDialog_11;

      textsData.button_CurrentTask = LD_ENG.button_CurrentTask;
      textsData.button_Shop = LD_ENG.button_Shop;
      textsData.button_Menu = LD_ENG.button_Menu;
      textsData.NewTask = LD_ENG.NewTask;

      textsData.fps = LD_ENG.fps;

      textsData.PlayMenu_Pause = LD_ENG.PlayMenu_Pause;
      textsData.PlayMenu_GraphicsQuality = LD_ENG.PlayMenu_GraphicsQuality;
      textsData.PlayMenu_BUTT_QualityLow = LD_ENG.PlayMenu_BUTT_QualityLow;
      textsData.PlayMenu_BUTT_QualityNormal = LD_ENG.PlayMenu_BUTT_QualityNormal;
      textsData.PlayMenu_BUTT_QualityHi = LD_ENG.PlayMenu_BUTT_QualityHi;
      textsData.PlayMenu_BUTT_MainMenu = LD_ENG.PlayMenu_BUTT_MainMenu;
      textsData.PlayMenu_BUTT_Close = LD_ENG.PlayMenu_BUTT_Close;

      textsData.Store_RechargeTime = LD_ENG.Store_RechargeTime;
      textsData.Store_Sec = LD_ENG.Store_Sec;
      textsData.Store_CurrentFieldLevel = LD_ENG.Store_CurrentFieldLevel;
      textsData.Store_Maximum4 = LD_ENG.Store_Maximum4;
      textsData.Store_WeaponUpgrade = LD_ENG.Store_WeaponUpgrade;
      textsData.Store_FieldUpgrade = LD_ENG.Store_FieldUpgrade;
      textsData.Store_BUTT_Buy = LD_ENG.Store_BUTT_Buy;

      textsData.Radar_BUTT_Goal = LD_ENG.Radar_BUTT_Goal;
      textsData.Radar_BUTT_Rotation = LD_ENG.Radar_BUTT_Rotation;
      textsData.Radar_BUTT_Tilt = LD_ENG.Radar_BUTT_Tilt;
      textsData.Radar_BUTT_WhereIAm = LD_ENG.Radar_BUTT_WhereIAm;
      textsData.Radar_CurrentGoal = LD_ENG.Radar_CurrentGoal;
      textsData.Radar_PressR = LD_ENG.Radar_PressR;

      textsData.Restart = LD_ENG.Restart;
      textsData.GameOver = LD_ENG.GameOver;
      textsData.Victory = LD_ENG.Victory;
    }
  }
}
