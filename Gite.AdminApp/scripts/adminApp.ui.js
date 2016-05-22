if (typeof window.HomePage === 'undefined') window.HomePage = {};
if (typeof window.Reservations === 'undefined') window.Reservations = {};
if (typeof window.Comptes === 'undefined') window.Comptes = {};

window.HomePage = (function ($) {
    var refresh = function () {
        alert("toto");
    }

    return {
        refresh: refresh
    }
})($);

window.Reservations = (function ($) {
    var refresh = function () {
        incoming();
        pendingConfirmation();
        // TODO: cautions en attente
    }

    var incoming = function () {
        $.get(baseUrl + "Incoming", function (data) {
            window.Reservations.model.Incoming = data;

            $("#reservations #incoming").html(data.length);
            $("#incomingReservation #list").html(IncomingReservations(data));
            $('#incomingReservation #list').listview('refresh');
        });
    }

    var pendingConfirmation = function () {
        $.get(baseUrl + "PendingConfirmation", function (data) {
            window.Reservations.model.Pending = data;

            $("#reservations #pending").html(data.length);
            $("#pendingReservation #list").html(PendingValidation(data));
            $('#pendingReservation #list').listview('refresh');
        });
    }

    // Setup navigation
    $(document).on("pagebeforeshow", "#reservations", function () { window.Reservations.refresh(); });

    return {
        refresh: refresh,
        incoming: incoming,
        model: {}
    }
})($);


window.Comptes = (function ($) {
    var refresh = function () {
        $.get(baseUrl + "Accountancy", function (data) {
            $("#comptes #month").html(data.Month);
            $("#comptes #year").html(data.Year);
            $("#comptes #all").html(data.All);
        })
    }

    // Setup navigation
    $(document).on("pagebeforeshow", "#comptes", function () {window.Comptes.refresh(); });

    return {
        refresh: refresh
    }
})($);