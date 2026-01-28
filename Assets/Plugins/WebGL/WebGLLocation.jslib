mergeInto(LibraryManager.library, {
  RequestUserLocation: function () {
    if (!navigator.geolocation) {
      console.log("Geolocation not supported");
      return;
    }

    navigator.geolocation.getCurrentPosition(
      function (position) {
        var coords = position.coords.latitude + "," + position.coords.longitude;
        SendMessage("LocationReceiver", "OnLocationReceived", coords);
      },
      function (error) {
        console.log("Geolocation error: " + error.message);
        SendMessage("LocationReceiver", "OnLocationReceived", "error,error");
      }
    );
  }
});