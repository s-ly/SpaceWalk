using UnityEngine;
[System.Serializable]
public class LocalData_RUS {
  string local = "RUS";
  string main_Load = "Загрузка ...";
  string main_GameName = "Космическая прогулка";
  string main_ButtonStart = "Играть сначала";
  string main_ButtonContinue = "Продолжить сохранённую";
  string main_UserID = "Ваш id: ";

  string main_PlDatStr_GamP = "Игровой прогресс: ";
  string main_PlDatStr_Crys = "Кристаллы: ";
  string main_PlDatStr_MaOx = "Максимальный запас кислорода: ";
  string main_PlDatStr_Tech = "Тех-контейнеры: ";
  string main_PlDatStr_CurT = "Текущее задание: ";
  string main_PlDatStr_WeRT = "Время перезарядки оружия: ";
  string main_PlDatStr_PSpe = "Скорость игрока: ";
  string main_PlDatStr_Phea = "Здоровье игрока: ";
  string main_PlDatStr_MaFu = "Максимальный запас топлива: ";
  string main_PlDatStr_PrFi = "Версия защитного поля: ";


  string MissionDialog_00 = "Жесткая посадка, нужно выяснить, что привело к крушению. " +
"Но сейчас другая проблема, заканчивается кислород. <b><color=red>Нужно срочно добежать до базы</color></b> - белый корпус на опорах." +
"На базе кислород пополняется. \n\n" +
"<b><color=orange>C</color></b> - разблокировать курсор мыши\n" +
"<b><color=orange>P, M, PAUSE</color></b> - меню и пауза\n" +
"<b><color=orange>AWSD + МЫШЬ</color></b> - передвижение\n" +
"<b><color=orange>ПРОБЕЛ</color></b> - прыжок и двойной прыжок\n" +
"Или сенсорная клавиатура";
  string MissionDialog_01 = "Запас кислорода слишком мал. На этой базе можно его увеличить.Собирай кристаллы и приноси их на базу, увеличивай запас кислорода, их много рядом. " +
"<b><color=red>Нужно собрать 10 кристаллов, принести их на базу и купить улучшение.</color></b> Кристаллы мало собрать, их нужно донести до базы. На месте собранного кристалла, со временем появится новый.\n\n" +
"<b><color=orange>Стрельба</color></b> - автоматическая\n" +
"<b><color=orange>E</color></b> - ускорение во время прыжка\n" +
"Или сенсорная клавиатура";
  string MissionDialog_02 = "Вокруг есть кристаллы, но их слишком мало для рейдов вглубь планеты. Тут есть множество кратеров, в них должно быть гораздо больше кристаллов. <b><color=red>Найди базу изучения кристаллов. </color></b>Она рядом с кратером, и отмечена на радаре. В кратере много кристаллов. Нужно подойти к радару, тогда появится сферическая голограмма карты. На ней есть маркер твоего положения и ориентации, красная стрелка. Ещё красным пунктирным кружком отмечено местоположение текущей цели.\n\n" +
"<b><color=orange>R</color></b> - использовать радар\n" +
"Или сенсорная клавиатура";
  string MissionDialog_03 = "Подозрительно много охраны. Нужно выяснить, что тут происходит. Но в начале нужно улучшить оружие, иначе не справится. Тут должна быть <b><color=red>«База вооружения», найди её</color></b>, думаю, на ней можно улучшить оружие. \nПопробуй двойной прыжок. Так, если подпрыгнуть, повторное нажатие на прыжок включит реактивный ранец, и подбросит вверх. Или можно подскочить вперёд, если нажать во время прыжка на ускорение. На это расходуется топливо.";
  string MissionDialog_04 = "\tСтановится всё больше боевых машин. Нужно скорее улетать с этой планеты. При аварийной посадке, я заметил <b><color=red>космодром.</color></b> Он был недалеко от радиолокационных тарелок. Надеюсь, там найдётся какой-нибудь транспорт. \n\tЭти турели не очень поворотливы, но после уничтожения, какая-то ремонтная система их снова восстанавливает. Что тут происходит?";
  string MissionDialog_05 = "\tПохоже, так просто не улететь. Этими комплексами кто-то или что-то управляет. Что бы выяснить, придётся разведать что дальше. Мне нужно улучшить топливные баки реактивного ранца. <b><color=red>Нужно найти станцию «Реактивных двигателей»,</color></b> там, я смогу улучшить снаряжение. \n\tТех-контейнеры, остающиеся после взорванных турелей тоже нужно сперва отнести на главную базу, что бы потом покупать улучшения.";
  string MissionDialog_06 = "\tПохоже, комплекс управляется неисправной «Системой безопасности». Нужно скорее улетать. Но для Шаттла нужно топливо. <b><color=red>На «Топливной станции» нужно получить ключ доступа.</color></b> Ключ доступа нужно донести до шаттла.\n\tИщи топливные баки, там можно заправить реактивный ранец.";
  string MissionDialog_07 = "\tЭто уже не смешно, слишком опасно. Нужно улучшить свою броню, иначе эти турели и роботы меня уничтожат. Мой скафандр оснащён защитным полем. Если я <b><color=red>найду «Кибернетическую лабораторию»</color></b>, я смогу улучшить его. \n\tЗащитное поле тратится, когда в него попадают выстрелы, и так защищает меня. Поле само медленно восстанавливается, но только если выйти из боя.";
  string MissionDialog_08 = "\tДля шаттла нужна энергия. <b><color=red>Ключ доступа должен быть на «Электростанции»</color></b>. Нужно найти её. Ключ доступа нужно донести до шаттла. \n\tНельзя просто так улететь, пострадает ещё кто-то. Нужно найти способ остановить безумную систему.";
  string MissionDialog_09 = "\tНа электростанции я выяснил, в южной части планеты расположен боевой комплекс, он и сбил мой корабль. Его нужно отключить. Комплекс питают <b><color=red>3 реактора, нужно уничтожить их все</color></b>.\n\tНа карте отмечены три точки, где расположены реакторы. Там полно охраны!";
  string MissionDialog_10 = "\tВсе три реактора уничтожены, и защита комплекса ослабла. Осталось <b><color=red>найти ключ доступа «Охранной системы» и добежать до шаттла</color></b>.\n\tСтранно, что никто не управляет станциями, что привело их в боевой режим, и почему они нападают? Нет времени думать, нужно хватать ключ и улетать.";
  string MissionDialog_11 = "\tПохоже, кто-то специально запрограммировал модули охраны на нападение. Кто и зачем? В следующий раз разберусь с этим. \n\t<b><color=red>Победа!</color></b>";

  string button_CurrentTask = "Текущее задание";
  string button_Shop = "Магазин";
  string button_Menu = "Меню";
  string NewTask = "Новое задание:";

  string fps = "Кадров в секунду: ";

  string PlayMenu_Pause = "ПАУЗА";
  string PlayMenu_GraphicsQuality = "Качество графики:";
  string PlayMenu_BUTT_QualityLow = "Низкое";
  string PlayMenu_BUTT_QualityNormal = "Нормальное";
  string PlayMenu_BUTT_QualityHi = "Высокое";
  string PlayMenu_BUTT_MainMenu = "Главное меню";
  string PlayMenu_BUTT_Close = "Закрыть";

  string Store_RechargeTime = "Время перезарядки: ";
  string Store_Sec = " сек";
  string Store_CurrentFieldLevel = "Текущий уровень поля: ";
  string Store_Maximum4 = " (максимальный: 4)";
  string Store_WeaponUpgrade = "Улучшение оружия";
  string Store_FieldUpgrade = "Улучшение защитного поля";
  string Store_BUTT_Buy = "Купить";

  string Radar_BUTT_Goal = "Где цель?";
  string Radar_BUTT_Rotation = "Вращение карты";
  string Radar_BUTT_Tilt = "Наклон карты";
  string Radar_BUTT_WhereIAm = "Где я?";
  string Radar_CurrentGoal = "Текущая цель: ";
  string Radar_PressR = "R - использовать радар"; //R - use radar

  string Restart = "Рестарт";
  string GameOver = "Проигрыш";
  string Victory = "Победа!";

  /// методы доступа к скрытым полям

  public string get_local() { return local; }
  public string get_main_Load() { return main_Load; }
  public string get_main_GameName() { return main_GameName; }
  public string get_main_ButtonStart() { return main_ButtonStart; }
  public string get_main_ButtonContinue() { return main_ButtonContinue; }
  public string get_main_UserID() { return main_UserID; }

  public string get_main_PlDatStr_GamP() { return main_PlDatStr_GamP; }
  public string get_main_PlDatStr_Crys() { return main_PlDatStr_Crys; }
  public string get_main_PlDatStr_MaOx() { return main_PlDatStr_MaOx; }
  public string get_main_PlDatStr_Tech() { return main_PlDatStr_Tech; }
  public string get_main_PlDatStr_CurT() { return main_PlDatStr_CurT; }
  public string get_main_PlDatStr_WeRT() { return main_PlDatStr_WeRT; }
  public string get_main_PlDatStr_PSpe() { return main_PlDatStr_PSpe; }
  public string get_main_PlDatStr_Phea() { return main_PlDatStr_Phea; }
  public string get_main_PlDatStr_MaFu() { return main_PlDatStr_MaFu; }
  public string get_main_PlDatStr_PrFi() { return main_PlDatStr_PrFi; }

  public string get_MissionDialog_00() { return MissionDialog_00; }
  public string get_MissionDialog_01() { return MissionDialog_01; }
  public string get_MissionDialog_02() { return MissionDialog_02; }
  public string get_MissionDialog_03() { return MissionDialog_03; }
  public string get_MissionDialog_04() { return MissionDialog_04; }
  public string get_MissionDialog_05() { return MissionDialog_05; }
  public string get_MissionDialog_06() { return MissionDialog_06; }
  public string get_MissionDialog_07() { return MissionDialog_07; }
  public string get_MissionDialog_08() { return MissionDialog_08; }
  public string get_MissionDialog_09() { return MissionDialog_09; }
  public string get_MissionDialog_10() { return MissionDialog_10; }
  public string get_MissionDialog_11() { return MissionDialog_11; }

  public string get_button_CurrentTask() { return button_CurrentTask; }
  public string get_button_Shop() { return button_Shop; }
  public string get_button_Menu() { return button_Menu; }
  public string get_NewTask() { return NewTask; }

  public string get_fps() { return fps; }

  public string get_PlayMenu_Pause() { return PlayMenu_Pause; }
  public string get_PlayMenu_GraphicsQuality() { return PlayMenu_GraphicsQuality; }
  public string get_PlayMenu_BUTT_QualityLow() { return PlayMenu_BUTT_QualityLow; }
  public string get_PlayMenu_BUTT_QualityNormal() { return PlayMenu_BUTT_QualityNormal; }
  public string get_PlayMenu_BUTT_QualityHi() { return PlayMenu_BUTT_QualityHi; }
  public string get_PlayMenu_BUTT_MainMenu() { return PlayMenu_BUTT_MainMenu; }
  public string get_PlayMenu_BUTT_Close() { return PlayMenu_BUTT_Close; }

  public string get_Store_RechargeTime() { return Store_RechargeTime; }
  public string get_Store_Sec() { return Store_Sec; }
  public string get_Store_CurrentFieldLevel() { return Store_CurrentFieldLevel; }
  public string get_Store_Maximum4() { return Store_Maximum4; }
  public string get_Store_WeaponUpgrade() { return Store_WeaponUpgrade; }
  public string get_Store_FieldUpgrade() { return Store_FieldUpgrade; }
  public string get_Store_BUTT_Buy() { return Store_BUTT_Buy; }

  public string get_Radar_BUTT_Goal() { return Radar_BUTT_Goal; }
  public string get_Radar_BUTT_Rotation() { return Radar_BUTT_Rotation; }
  public string get_Radar_BUTT_Tilt() { return Radar_BUTT_Tilt; }
  public string get_Radar_BUTT_WhereIAm() { return Radar_BUTT_WhereIAm; }
  public string get_Radar_CurrentGoal() { return Radar_CurrentGoal; }
  public string get_Radar_PressR() { return Radar_PressR; }

  public string get_Restart() { return Restart; }
  public string get_GameOver() { return GameOver; }
  public string get_Victory() { return Victory; }

}

/*

C - разблокировать мышку.
M, P, PAUSE - меню и пауза
R - использовать радар.
ASDW – перемещение,  МЫШКА – поворот.
ПРОБЕЛ – прыжок и двойной прыжок
E - ускорение в прыжке.
Стрельба автоматическая.
Присутствует сенсорная клавиатура.

Кислород заканчивается, запасы пополняются на главной базе.
Цели видно подойдя к радару.
Собирай кристаллы и покупай увеличение запаса кислорода.
Улачшай оружие и защитное поле на других базах.
//////////////////

*/
