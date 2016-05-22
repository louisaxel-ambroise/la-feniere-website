function IncomingReservations(data) {
    var res = "";
    for (var i = 0 ; i < data.length ; i++) {
        var icon = data[i].PaymentReceived ? "check" : data[i].PaymentDeclared ? "clock" : "delete";
        res += "<li data-iconpos=\"right\" data-icon=\"" + icon + "\"><a href=\"#reservationDetails\" onclick=\"window.Reservations.select('" + data[i].Id + "', 'incomingReservation');\" data-transition=\"slide\">" + data[i].Name + " (" + data[i].StartingOn + ")</a></li>";
    }

    return res;
}

function PendingValidation(data) {
    var res = "";
    for (var i = 0 ; i < data.length ; i++) {
        res += "<li data-iconpos=\"right\" data-icon=\"clock\"><a href=\"#reservationDetails\" onclick=\"window.Reservations.select('" + data[i].Id + "', 'pendingReservation');\" data-transition=\"slide\">" + data[i].Name + " (" + data[i].StartingOn + ")</a></li>";
    }

    return res;
}

function DisplayReservationDetails(data) {
    var ret = "<p>Virement attendu: <strong>" + (data.Price + data.Caution) + "€</strong><ul><li>Prix: " + data.Price + "€</li><li>Caution: " + data.Caution + "€</li></ul></p>";
    ret += "<p>Référence de réservation: <strong>" + data.CustomId + "</strong></p>";
    if (data.PaymentReceived) ret += "<p>Le paiement a été annoncé comme reçu.</p>";
    else {
        if (data.PaymentDeclared) ret += "<p>Le paiement a été annoncé comme fait.</p>";
        ret += "<a href=\"#\" class=\"ui-btn ui-shadow ui-btn-corner-all ui-mini ui-btn-inline ui-btn-up-b\" onclick=\"window.Reservations.confirmCurrent()\">Paiement reçu</a>"
    }

    return ret;
}