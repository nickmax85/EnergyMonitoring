

function getSensors() {

    let url = '/api/Sensors';

    $.getJSON(url)
        .done(function (data) {


            return data;

        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function getSensor(sensorId) {

    let url = '/api/Sensors/' + sensorId;
  

    $.getJSON(url)
        .done(function (data) {
            callback(data);
       
        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

  

}

function postSensor(sensor) {

    let url = '/api/Sensors';

    $.post(url, sensor)
        .done(function (data) {

        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}


