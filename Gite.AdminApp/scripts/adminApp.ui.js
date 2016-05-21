if (typeof window.Reservations === 'undefined') {
    window.Reservations = {};
}

var model = {
    incoming: 0,
    past: 0
};

var updateIncomingPage = function (data) {
    var list = "";

    for (var i = 0; i < data.length ; i++) {
        var mail = "";
        var name = "";
        if (data[i].Contact !== null) {
            mail = data[i].Contact.Mail;
            name = data[i].Contact.Name;
        }

        list += "<div data-role=\"collapsible\">";
        list += "<h3>" + data[i].StartingOn.substr(0, 10) + " - " + data[i].EndingOn.substr(0, 10) + "</h3>";
        list += "<p>" + name + "(" + mail + ")</p>";
        list += "<p>Paiement reçu (" + data[i].Price + "€)</p>";
        list += "</div>";
    }

    $("#incomingList").html(list);
    $('#incomingList').trigger('create');
}
var updatePastPage = function (data) {
    var list = "";

    for (var i = 0; i < data.length ; i++) {
        var mail = "";
        var name = "";
        if (data[i].Contact !== null) {
            mail = data[i].Contact.Mail;
            name = data[i].Contact.Name;
        }

        list += "<div data-role=\"collapsible\">";
        list += "<h3>" + data[i].StartingOn.substr(0, 10) + " - " + data[i].EndingOn.substr(0, 10) + "</h3>";
        list += "<p>" + name + "</p>";
        list += "<p>" + mail + "</p>";
        list += "<p>Caution rendue (" + data[i].Price + "€)</p>";
        list += "</div>";
    }

    $("#pastList").html(list);
    $('#pastList').trigger('create');
}

var refresh = function () {
    $.get("http://localhost:20523/api/reservation/incoming", function (data) {
        model.incoming = data;
        $("#incLink").html("A venir (" + data.length + ")");
        updateIncomingPage(data);
    });
    $.get("http://localhost:20523/api/reservation/past", function (data) {
        model.past = data;
        $("#pastLink").html("Passées (" + data.length + ")");
    });
}

window.Reservations.ui = (function ($) {
    refresh();
})($);