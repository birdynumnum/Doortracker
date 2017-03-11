DoorTrackerApp.factory('bearerTokenContainer', function ($http, $location) {

    var container = {
        token: ""
    };

    var setToken = function (token) {
        container.token = token;
    };

    var getToken = function () {

        if (container.token === "") {
            if (localStorage.getItem("access_token") === null) {
                    var url = "https://localhost:44300/identity/connect/authorize?" +
                              "client_id=doortrackerimplicit&" +
                              "redirect_uri=" + encodeURI(window.location.protocol + "//" + window.location.host + "/callback.html") + "&" +
                              "response_type=token&" +
                              "scope=DoorTracker";

                window.location = url;
            }
            else {
                setToken(localStorage["access_token"]);
            }
        }
        return container;
    };

    return {
        setToken: setToken,
        getToken: getToken
    };
});