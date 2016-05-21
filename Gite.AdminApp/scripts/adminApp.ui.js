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
        list += "<div data-role=\"collapsible\">";
        list += "<h3>" + data[i].StartingOn.substr(0, 10) + " - " + data[i].EndingOn.substr(0, 10) + "</h3>";
        list += "<p>" + data[i].Name + "(" + data[i].Mail + ")</p>";
        if (data[i].PaymentReceived)
            list += "<p>Paiement reçu (" + data[i].Price + "€)</p>";
        else
            list += "<p style='color:red'>Paiement non reçu (" + data[i].Price + "€)</p>";
        list += "</div>";
    }

    $("#incomingList").html(list);
    $('#incomingList').trigger('create');
}
var updatePastPage = function (data) {
    var list = "";

    for (var i = 0; i < data.length ; i++) {
        list += "<div data-role=\"collapsible\">";
        list += "<h3>" + data[i].StartingOn.substr(0, 10) + " - " + data[i].EndingOn.substr(0, 10) + "</h3>";
        list += "<p>" + data[i].Name + "(" + data[i].Mail + ")</p>";
        if (data[i].CautionRefunded)
            list += "<p>Caution rendue.</p>";
        else
            list += "<p style='color:red'>Caution non rendue (" + data[i].Price + "€)</p>";
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