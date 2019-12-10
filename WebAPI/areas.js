

function getAreas() {

    let url = '/api/areas';

    $.getJSON(url)
        .done(function (data) {
            showAreas(data);
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function postArea(area) {

    let url = '/api/areas';

    $.post(url, area)
        .done(function (data) {
            showAreas(data);
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function showAreas(data) {

    let container = $('#areas');
    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        let grid = $('<div class="col-sm-4">');

        let card = $('<div class="card mb-3">');
        let header = $('<div class="card-header">header</div>').html(item.Name);
        let body = $('<div class="card-body">');
        //let title = $('<h6 class="card-title">title</h6>').html(item.Equipment.length + " Equipments");


        let li = $('<li class="list-group-item">');
        let text = $('<p class="card-text"></p>');

        for (var i = 0; i < item.Equipment.length; i++) {

            let text = $('<p class="card-text"></p>').html(item.Equipment[i].Name);

            li.append(text);
        }

        let button = $('<a href="#" class="btn btn-link">show</a>').click(function () {

            getEquipments(item.AreaID);

        });

        body.append(li, button);
        card.append(header, body);
        grid.append(card);

        container.append(grid);
    });

}