mergeInto(LibraryManager.library, {

  JS_LogUserID: async function () {
    console.log("-----> User ID:");
    await YaGames.init(); // Ожидание инициализации Yandex SDK
    console.log('Yandex SDK initialized (Unity log)');
    await initPlayer(); // Ожидание инициализации player
    if (player !== undefined) {
      console.log(player.getUniqueID());
      myGameInstance.SendMessage('Yandex', 'Show_LogUserID', player.getUniqueID());
    } else {
      console.log("----------------->Player is still not initialized");
    }
    console.log("----->|");
  },

  // Сохраняет данные на сервере Yandex.
  JS_Save: function (data) {
    console.log("-----> Save");
    var dataString = UTF8ToString(data);
    console.log(dataString);
    var myobj = JSON.parse(dataString);
    player.setData(myobj);
    console.log("----->|");
  },

  // Загружает данные с сервера Yandex, потом вызывает метод в Unity.
  // который висит на объекте Yandex.
  JS_Load: async function () {
    console.log("-----> Load");
    await YaGames.init(); // Ожидание инициализации Yandex SDK
    console.log('Yandex SDK initialized (Unity log)');
    await initPlayer(); // Ожидание инициализации player
    if (player !== undefined) {
      player.getData().then(_data => {
        const myJSON = JSON.stringify(_data);
        console.log(myJSON);
        myGameInstance.SendMessage('Yandex', 'LoadFromJS', myJSON);
      });
    } else {
      console.log("Player is still not initialized");
    }
    console.log("----->|");
  },

  JS_DeviceInfo: function () {
    console.log("----999999999999999999999999999->|");
    console.log(ysdk.deviceInfo.type);
    myGameInstance.SendMessage('Manager_DeviceInfo', 'Touch_Keyboard_SetActive', ysdk.deviceInfo.type);
  },

  JS_MyWebLog: function (my_log) {
    console.log("----zzzzzzzzzzzzzzzzz->|");
    var my_String = UTF8ToString(my_log);
    console.log(my_String);
  },

  JS_ShowAdv: function () {
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function (wasShown) {
          // some action after close
          console.log("-------onClose ADV->|");
          myGameInstance.SendMessage('GameOwer', 'StartReload');
        },
        onError: function (error) {
          // some action on error
          console.log("-------onError ADV->|");
          myGameInstance.SendMessage('GameOwer', 'StartReload');
        }
      }
    })
  },

});