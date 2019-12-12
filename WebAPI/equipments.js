

function getEquipments(areaId) {

    let url = '/api/areas/' + areaId + '/equipments';

    $.getJSON(url)
        .done(function (data) {

            showEquipments(data);
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function postEquipment(equipment) {

    let url = '/api/equipments';

    $.post(url, area)
        .done(function (data) {
            showEquipments(data);
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function showEquipments(data) {

    showCard(data);

}


function showCard(data) {

    let container = $('#equipments');
    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        let grid = $('<div class="col-sm-4">');

        let card = $('<div class="card mb-3">');
        let header = $('<div class="card-header">header</div>').html(item.Name);
        //let editButton = $('<a href="#" class="btn btn-primary">edit</a>');
        let body = $('<div class="card-body">');
        //let title = $('<h6 class="card-title">title</h6>').html(item.Equipment.length + " Equipments");

        let text = $('<p class="card-text"></p>');

        let buttonDevices = $('<a href="#" class="btn btn-link">Sensoren</a>').click(function () {

            localStorage.setItem('Equipment', JSON.stringify(item));
            window.location.href = "devices.html";

        });

        let buttonReport = $('<a href="#" class="btn btn-link">Auswertungen</a>').click(function () {

            localStorage.setItem('Equipment', JSON.stringify(item));
            window.location.href = "records.html";

        });

        body.append(text, buttonDevices, buttonReport);
        card.append(header, body);
        grid.append(card);

        container.append(grid);
    });

}


