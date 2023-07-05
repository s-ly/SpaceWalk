mergeInto(LibraryManager.library, {

    // Вызывает метод отображения ID пользователя в Unity
    // Передаёт строку id, которую получает у player из Yndex SDK.
    JS_LogUserID: function () {
      console.log("-----> User ID:");
      console.log(player.getUniqueID());
      myGameInstance.SendMessage('Yandex', 'Show_LogUserID', player.getUniqueID());
      console.log("----->|");
    },

    // Сохраняет данные на сервере Yandex.
    JS_Save: function (data) 
    {
      console.log("-----> Save");
      var dataString = UTF8ToString(data);
      console.log(dataString);
      var myobj = JSON.parse(dataString);
      player.setData(myobj);
      console.log("----->|");
    },

    // Загружает данные с сервера Yandex, потом вызывает метод в Unity.
    // который висит на объекте Yandex.
    JS_Load: function () 
    {
      console.log("-----> Load");
      player.getData().then(_data => {
        const myJSON = JSON.stringify(_data);
        console.log(myJSON);
        myGameInstance.SendMessage('Yandex', 'LoadFromJS', myJSON);
      });
      console.log("----->|");
    },
  
  });