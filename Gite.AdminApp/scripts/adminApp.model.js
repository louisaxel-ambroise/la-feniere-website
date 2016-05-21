if (typeof window.Reservations === 'undefined') {
    window.Reservations = {};
}

window.Reservations.model = (function () {
    var model = {
        reservations: [],
        alerts: {}
    };

    var loadFromApi = function () {
        $.get("http://localhost:20523/api/reservation", function (data) {
            return data;
        });
    };

    return {
        loadFromApi: loadFromApi
    };
})();