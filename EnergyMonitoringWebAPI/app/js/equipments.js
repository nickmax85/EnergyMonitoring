var gaugeOptions = {

    chart: {
        height: 100 + '%', // 16:9 ratio   
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

function getEquipments(groupId) {

    let url = '/api/groups/' + groupId + '/equipments';

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

        let grid = $('<div class="col-sm-6 mb-3">');
        let card = $('<div class="card">');
        let header = $('<div class="card-header">header</div>').html(item.Number + ' - ' + item.Name);
        //let editButton = $('<a href="#" class="btn btn-primary">edit</a>');
        let body = $('<div class="card-body">');
        //let title = $('<h6 class="card-title">title</h6>').html(item.Equipment.length + " Equipments");

        let text = $('<p class="card-text">text</p>');

        // GaugeChart
        let row = $('<div class="row">');
        let gaugeChart1 = $('<div id="containerGauge1' + i + '" class="chart-container col-lg-6"</div>');
        let gaugeChart2 = $('<div id="containerGauge2' + i + '" class="chart-container col-lg-6"</div>');
        row.append(gaugeChart1, gaugeChart2);


        // Buttons    
        let buttonDevices = $('<a href="#" class="btn btn-primary col-sm-6 mb-1">Sensoren</a>').click(function () {

            localStorage.setItem('Equipment', JSON.stringify(item));
            window.location.href = "devices.html";

        });

        let buttonCharts = $('<a href="#" class="btn btn-primary col-sm-12 mb-0">Charts</a>').click(function () {

            localStorage.setItem('Equipment', JSON.stringify(item));

            window.location.href = "charts.html";

        });

        body.append(row, buttonCharts);
        card.append(header, body);
        grid.append(card);

        container.append(grid);

        if (item.Device[0] != null) {
            addGaugeChart1(gaugeChart1.get(0).id, item.Device[0]);
            addGaugeChart2(gaugeChart2.get(0).id, item.Device[0]);
        }


    });

}


function showCardCollapsable(data) {

    let container = $('#equipments');
    container.empty();

    if (!Array.isArray(data))
        return;

    data.forEach(function (item, i) {

        //let element = $('<div class="col-md-3">');
        //element.append($('<div class="card card-primary">'));
        //element.append($('<div class="card-header">'));
        //element.append($('<h3 class="card-title">Expandable</h3>'));
        //element.append($('<div class="card-tools">'));
        //element.append($('<button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button >'));
        //element.append($('<div class="card-body" style="display: block;">The body of the card</div >'));


        let col = $('<div class="col-md-3">');

        let card = $('<div class="card card-primary">');
        col.append(card);

        let header = $('<div class="card-header">');
        header.append($('<h3 class="card-title">Expandable</h3>'));
        header.append($('<div class="card-tools">'));
        header.append($('<button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>'));
        card.append(header);

        let body = $('<div class="card-body" style="display: block;">The body of the card</div >');
        card.append(body);

        container.append(card);
    });

}


function addGaugeChart1(container, item) {

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
                    '<span style="font-size:18px">{y:.1f}</span><br/>' +
                    '<span style="font-size:12px;opacity:0.4">' +
                    ' l / min' +
                    '</span>' +
                    '</div>'
            },
            tooltip: {
                valueSuffix: ' l / min'
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



function addGaugeChart2(container, item) {

    var pressureGauge = Highcharts.chart(container, Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 10,
            title: {
                text: 'Druck',
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
                    '<span style="font-size:18px">{y:.1f}</span><br/>' +
                    '<span style="font-size:12px;opacity:0.4">' +
                    ' bar' +
                    '</span>' +
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
                //  alert("ERROR: " + error.status + ' ' + error.statusText);
            });

    }, 2000);
}


