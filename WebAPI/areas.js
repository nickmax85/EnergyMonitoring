

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

        let card = $('<div class="card text-dark bg-light mb-3">');

        let header = $('<div class="card-header">header</div>').html(item.Name);
        header.click(function () {
            localStorage.setItem('Area', JSON.stringify(item));
            window.location.href = "equipments.html";
        });
        header.hover(function () {
            $(this).attr('class', "card-header text-dark bg-primary")
            $(this).css("cursor", "pointer");
        }, function () {
            $(this).attr('class', "card-header text-dark bg-light")
            $(this).css("cursor", "default");
        });

        let body = $('<div class="card-body">');
        let title = $('<h6 class="card-title">title</h6>').html(item.Equipment.length + " Equipments");
        body.append(title);

        for (var i = 0; i < item.Equipment.length; i++) {

            let li = $('<li class="list-group-item">').attr('id', i);
            li.click(function () {

                localStorage.setItem('Equipment', JSON.stringify(item.Equipment[li.attr('id')]));
                localStorage.setItem('Area', JSON.stringify(item));
                window.location.href = "devices.html";
            });
            li.hover(function () {

                $(this).attr('class', "list-group-item text-dark bg-info")
                $(this).css("cursor", "pointer");
            }, function () {
                $(this).attr('class', "list-group-item")
                $(this).css("cursor", "default");
            });
            let text = $('<p class="card-text"></p>').html(item.Equipment[i].Name);

            li.append(text);
            body.append(li);
        }

        card.append(header, body);
        grid.append(card);

        container.append(grid);
    });

}