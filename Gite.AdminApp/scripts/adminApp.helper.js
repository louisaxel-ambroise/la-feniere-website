function IncomingReservations(data) {
    var res = "";
    for (var i = 0 ; i < data.length ; i++) {
        var icon = data[i].PaymentReceived ? "check" : data[i].PaymentDeclared ? "clock" : "delete";
        res += "<li data-iconpos=\"right\" data-icon=\"" + icon + "\"><a href=\"#\">" + data[i].Name + " (" + data[i].StartingOn + ")</a></li>";
    }

    return res;
}

function PendingValidation(data) {
    var res = "";
    for (var i = 0 ; i < data.length ; i++) {
        res += "<li data-iconpos=\"right\" data-icon=\"clock\"><a href=\"#\">" + data[i].Name + " (" + data[i].StartingOn + ")</a></li>";
    }

    return res;
}