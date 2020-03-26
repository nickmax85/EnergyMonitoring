function getDevices(equipmentId) {

    let url = '/api/equipments/' + equipmentId + '/devices';

    $.getJSON(url)
        .done(function (data) {

            showDevices(data);

        })
        .fail(function (error) {
            alert("ERROR: " + error.status + ' ' + error.statusText);
        });

}

function showDevicesDiagnosis(data) {

    let container = $('#devices');
    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        let col = $('<div class="col-md-3">');

        let card = $('<div class="card card-primary">');
        col.append(card);

        let header = $('<div class="card-header">');
        header.append($('<h3 class="card-title">Expandable</h3>').html(item.Name + '<br/>' + item.IP));

        let tools = ($('<div class="card-tools">'));
        tools.append($('<button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>'));
        header.append(tools);

        let body = $('<div class="card-body" style="display: block;"></div >');

        let row1 = $('<div class="row">');

        let col1 = $('<div class="col-md-4">');
        let img = $('<img src="img/webio.png"/>');
        col1.append(img);

        let col2 = $('<div class="col-md-8">');
             
        row1.append(col1);
        body.append(row1);

        let row2 = $('<div class="row">');

        let labelEquipment = $('<label>' + item.Equipment.Number + ' - ' + item.Equipment.Name + '</label>');
        let labelStatus = $('<label id="' + item.DeviceID + '"></label>');
        row2.append(labelEquipment, labelStatus);
        body.append(row2);

        showDeviceState(img, labelStatus);

        card.append(header, body);
        container.append(col);

    });

}

function showDeviceState(img, labelStatus) {

    let state;

    data();

    function data() {

        img.css("padding", "5px");
        img.css("border", "1px transparent");
        img.css("border-radius", "5px");

        if (state == false) {
            img.css("background-color", "lightgreen");
            state = true;
        }
        else {

            img.css("background-color", "coral");
            state = false;
        }

        let date = new Date();

        labelStatus.html(date);

    }


    setInterval(function () {
        data();

        //url = 'http://' + item.IP + '/rest/json';

        //$.getJSON(url)
        //    .done(function (data) {
        //    })
        //    .fail(function (error) {
        //    });

    }, 1000);

}


function showDevices(data) {

    let container = $('#devices');

    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        let li = $('<li class="list-group-item">');
        let label = $('<label>').html(item.Name + "; <br/>");
        let ipLink = $('<a href="http://' + item.IP + '">&nbsp;' + item.IP + '</a> <br/>');

        let button = $('<button class="btn btn-info">').click(function () {

            localStorage.setItem("DeviceID", item.DeviceID);

            window.location.href = "records.html";

        }).html('Report');

        let outer = $('<div class="outer">');

        let gaugeChart1 = $('<div id="containerGauge1_' + i + '" class="chart-container"</div>');
        let gaugeChart2 = $('<div id="containerGauge2_' + i + '" class="chart-container"</div>');

        label.append(ipLink);
        li.append(label);
        li.append(outer);

        outer.append(gaugeChart1, gaugeChart2);
        outer.append(button);

        container.append(li);

        addGauge1(gaugeChart1.get(0).id, item);
        addGauge2(gaugeChart2.get(0).id, item);


    });

}
var solidGaugeOptions = {
    chart: {
        type: 'solidgauge'
    },

    title: null,

    pane: {
        center: ['50%', '85%'],
        size: '140%',
        startAngle: -90,
        endAngle: 90,
        background: {
            backgroundColor:
                Highcharts.defaultOptions.legend.backgroundColor || '#EEE',
            innerRadius: '60%',
            outerRadius: '100%',
            shape: 'arc'
        }
    },

    tooltip: {
        enabled: false
    },

    // the value axis
    yAxis: {
        stops: [
            [0.1, '#55BF3B'], // green
            [0.5, '#DDDF0D'], // yellow
            [0.9, '#DF5353'] // red
        ],
        lineWidth: 0,
        minorTickInterval: null,
        tickAmount: 2,
        title: {
            y: -70
        },
        labels: {
            y: 16
        }
    },

    plotOptions: {
        solidgauge: {
            dataLabels: {
                y: 5,
                borderWidth: 0,
                useHTML: true
            }
        }
    }
};
var gaugeOptions = {

    chart: {
        type: 'gauge',
        plotBackgroundColor: null,
        plotBackgroundImage: null,
        plotBorderWidth: 0,
        plotShadow: false
    },

    title: {
        text: null
    },

    pane: {
        startAngle: -150,
        endAngle: 150,
        background: [{
            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#FFF'],
                    [1, '#333']
                ]
            },
            borderWidth: 0,
            outerRadius: '109%'
        }, {
            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#333'],
                    [1, '#FFF']
                ]
            },
            borderWidth: 1,
            outerRadius: '107%'
        }, {
            // default background
        }, {
            backgroundColor: '#DDD',
            borderWidth: 0,
            outerRadius: '105%',
            innerRadius: '103%'
        }]
    },

    // the value axis
    yAxis: {
        min: 0,
        max: 200,

        minorTickInterval: 'auto',
        minorTickWidth: 1,
        minorTickLength: 10,
        minorTickPosition: 'inside',
        minorTickColor: '#666',

        tickPixelInterval: 30,
        tickWidth: 2,
        tickPosition: 'inside',
        tickLength: 10,
        tickColor: '#666',
        labels: {
            step: 2,
            rotation: 'auto'
        },
        title: {
            text: 'km/h'
        },
        plotBands: [{
            from: 0,
            to: 120,
            color: '#55BF3B' // green
        }, {
            from: 7,
            to: 8,
            color: '#DDDF0D' // yellow
        }, {
            from: 160,
            to: 200,
            color: '#DF5353' // red
        }]
    },

};

function addGauge1(container, item) {

    var pressureGauge = Highcharts.chart(container, Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 10,
            title: {
                text: 'Druck'
            },
            plotBands: [{
                from: 0,
                to: 7,
                color: '#55BF3B' // green
            }, {
                from: 7,
                to: 8,
                color: '#DDDF0D' // yellow
            }, {
                from: 8,
                to: 10,
                color: '#DF5353' // red
            }]
        },

        credits: {
            enabled: false
        },

        series: [{
            name: 'Druck',
            data: [0],
            dataLabels: {
                format:
                    '<div style="text-align:center">' +
                    '<span style="font-size:25px">{y}</span><br/>' +
                    '<span style="font-size:12px;opacity:0.4">bar</span>' +
                    '</div>'
            },
            tooltip: {
                valueSuffix: ' bar'
            }
        }]


    }));


    setInterval(function () {

        var point;

        url = 'http://' + item.IP + '/rest/json';

        $.getJSON(url)
            .done(function (data) {

                if (pressureGauge) {
                    point = pressureGauge.series[0].points[0];
                    point.update(data.iostate.input[0].value);
                }

            })
            .fail(function (error) {
                // alert("ERROR: " + error.status + ' ' + error.statusText);
            });


    }, 2000);


}
function addGauge2(container, item) {

    //The flow gauge
    var flowGauge = Highcharts.chart(container, Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 5000,
            title: {
                text: 'Durchfluss'
            },
            plotBands: [{
                from: 0,
                to: 200,
                color: '#55BF3B' // green
            }, {
                from: 200,
                to: 300,
                color: '#DDDF0D' // yellow
            }, {
                from: 300,
                to: 5000,
                color: '#DF5353' // red
            }]
        },

        series: [{
            name: 'Durchfluss',
            data: [0],
            dataLabels: {
                format:
                    '<div style="text-align:center">' +
                    '<span style="font-size:25px">{y:.1f}</span><br/>' +
                    '<span style="font-size:12px;opacity:0.4">' +
                    ' l / min' +
                    '</span>' +
                    '</div>'
            },
            tooltip: {
                valueSuffix: ' revolutions/min'
            }
        }]

    }));

    setInterval(function () {

        var point;

        url = 'http://' + item.IP + '/rest/json';
        $.getJSON(url)
            .done(function (data) {

                if (flowGauge) {
                    point = flowGauge.series[0].points[0];
                    point.update(data.iostate.input[1].value);
                }

            })
            .fail(function (error) {
                //  alert("ERROR: " + error.status + ' ' + error.statusText);
            });

    }, 2000);
}
function addGauge3(container, item) {

    var gauge = Highcharts.chart(container, Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 20,
            title: {
                text: 'Druck'
            }
        },

        credits: {
            enabled: false
        },

        series: [{
            name: 'Druck',
            data: [0],
            dataLabels: {
                format:
                    '<div style="text-align:center">' +
                    '<span style="font-size:25px">{y}</span><br/>' +
                    '<span style="font-size:12px;opacity:0.4">bar</span>' +
                    '</div>'
            },
            tooltip: {
                valueSuffix: ' bar'
            }
        }]

    }));


    setInterval(function () {

        var point;

        url = 'http://' + item.IP + '/rest/json';

        $.getJSON(url)
            .done(function (data) {

                if (gauge) {
                    point = gauge.series[0].points[0];
                    point.update(data.iostate.input[1].value);
                }

            })
            .fail(function (error) {
                // alert("ERROR: " + error.status + ' ' + error.statusText);
            });


    }, 2000);


}




