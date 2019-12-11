

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


    let head = $('#headerEquipments');
    head.empty();

    let h1 = $('<h1>Equipments</h1>').html(data[0].Area.Name);
    head.append(h1);

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
        //let title = $('<h6 class="card-title">title</h6>');

        let text = $('<p class="card-text"></p>');

        let buttonLive = $('<a href="#" class="btn btn-link">Live</a>').click(function () {

            localStorage.setItem('Area', JSON.stringify(item));
            window.location.href = "equipments.html";

        });

        let buttonReport = $('<a href="#" class="btn btn-link">Auswertungen</a>').click(function () {

            localStorage.setItem('Equipment', JSON.stringify(item));
            debugger;
            window.location.href = "records.html";

        });

        body.append(text, buttonLive, buttonReport);
        card.append(header, body);
        grid.append(card);

        container.append(grid);
    });

}