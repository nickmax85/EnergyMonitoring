

function getGroups() {

    let url = '/api/Groups';

    $.getJSON(url)
        .done(function (data) {

            showSelectGroups(data);
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function postGroup(group) {

    let url = '/api/Groups';

    $.post(url, group)
        .done(function (data) {
            
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}



function showSelectGroups(data) {

    let container = $('#selectGroup');

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        $('#selectGroup').append($('<option></option>').val(item.GroupID).html(item.Name));

    });


    //let id = localStorage.getItem('Group');
    let id = 0;
    if ($("#selectGroup").prop('selectedIndex') > 0 || id > 0) {

        $('#selectGroup').val(id);
        $('#background').hide();

        getEquipments(id);

    }

    else {
        $('#background').show();
    }

}

function groupSelectionChange(Group) {

    $('#equipments').empty();


    let sel = $("#selectGroup option:selected").val();
    localStorage.setItem('Group', sel);

    if ($("#selectGroup").prop('selectedIndex') != 0) {
        $('#background').hide();
        getEquipments(sel);

    }
    else {
        $('#background').show();
    }


}

function showGroups2(data) {

    let container = $('#Groups');
    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        let card = $('<div class="card">');
        let header = $('<div class="card-header">').attr('id', 'heading' + i);
        let h2 = $('<h2 class="mb-0">');

        let btn = $('<button class="btn btn-link" type="button" data-toggle="collapse" aria-expanded="false"></button>')
            .html(item.Name)
            .attr('data-target', '#collapse' + i)
            .attr('aria-controls', 'collapse' + i)
            .attr('id', item.Equipment[i].EquipmentID);
        btn.click(function () {

            let id = btn.attr('id');


        });

        h2.append(btn);
        header.append(h2);

        let collapse = $('<div class="collapse hide">')
            .attr('id', 'collapse' + i)
            .attr('aria-labelledby', 'heading' + i)
            .attr('data-parent', '#' + container.attr('id'));



        let cardbody = $('<div class="card-body">');

        for (var i = 0; i < item.Equipment.length; i++) {

            let li = $('<li class="list-group-item">').attr('data-target', JSON.stringify(item.Equipment[i]));

            li.click(function () {

                let d = JSON.parse(li.attr('data-target'));
                getDevices(d.EquipmentID);

                //localStorage.setItem('Equipment', JSON.stringify(item.Equipment[li.attr('id')]));
                //localStorage.setItem('Group', JSON.stringify(item));
                //window.location.href = "devices.html";
            });
            li.hover(function () {

                $(this).attr('class', "list-group-item text-dark bg-primary")
                $(this).css("cursor", "pointer");
            }, function () {
                $(this).attr('class', "list-group-item")
                $(this).css("cursor", "default");
            });
            let text = $('<p class="card-text"></p>').html(item.Equipment[i].Number + "-" + item.Equipment[i].Name);

            li.append(text);
            cardbody.append(li);

        }

        collapse.append(cardbody);
        card.append(header, collapse);


        container.append(card);
    });
}


function showGroups(data) {

    let container = $('#Groups');
    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        let grid = $('<div class="col-sm-4">');

        let card = $('<div class="card text-dark bg-light mb-3">');

        let header = $('<div class="card-header">header</div>').html(item.Name);
        header.click(function () {
            localStorage.setItem('Group', JSON.stringify(item));
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
                localStorage.setItem('Group', JSON.stringify(item));



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