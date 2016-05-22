if (typeof window.HomePage === 'undefined') window.HomePage = {};
if (typeof window.Reservations === 'undefined') window.Reservations = {};
if (typeof window.Comptes === 'undefined') window.Comptes = {};

window.HomePage = (function ($) {
    var refresh = function () {
        $.get(baseUrl + "Incoming", function (data) { }); // TODO: load alerts.
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
    };

    var select = function (id, backTo) {
        window.Reservations.model.backTo = backTo;
        for (var i = 0 ; i < window.Reservations.model.Incoming.length ; i++) {
            if (window.Reservations.model.Incoming[i].Id === id) {
                window.Reservations.model.current = window.Reservations.model.Incoming[i];
                break;
            }
        }
    };

    var incoming = function () {
        $.get(baseUrl + "Incoming", function (data) {
            window.Reservations.model.Incoming = data;

            $("#reservations #incoming").html(data.length);
            $("#incomingReservation #list").html(IncomingReservations(data));
            $('#incomingReservation #list').listview('refresh');
        });
    };

    var pendingConfirmation = function () {
        $.get(baseUrl + "PendingConfirmation", function (data) {
            window.Reservations.model.Pending = data;

            $("#reservations #pending").html(data.length);
            $("#pendingReservation #list").html(PendingValidation(data));
            $('#pendingReservation #list').listview('refresh');
        });
    };

    var showDetails = function () {
        var current = window.Reservations.model.current;
        $("#reservationDetails #link").attr("href", "#" + window.Reservations.model.backTo);
        $("#reservationDetails #selectedId").html(current.CustomId);
        $("#reservationDetails #details").html(DisplayReservationDetails(current));
        $('#pendingReservation #details').panel('refresh')
    }

    var confirmCurrent = function () {
        var current = window.Reservations.model.current;
        $.ajax({
            url: baseUrl + 'PaymentReceived/' + current.Id,
            type: 'PUT',
            success: function () {
                $.mobile.changePage('#confirmedDialog', 'pop', true, true);
                refresh();
                $("#reservationDetails #link").trigger("click");
            },
            error: function () { alert("Meh."); }
        });
    }

    // Setup navigation
    $(document).on("pagebeforeshow", "#reservations", function () { window.Reservations.refresh(); });
    $(document).on("pagebeforeshow", "#reservationDetails", function () { window.Reservations.showDetails(); });

    return {
        refresh: refresh,
        incoming: incoming,
        select: select,
        showDetails: showDetails,
        confirmCurrent: confirmCurrent,
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