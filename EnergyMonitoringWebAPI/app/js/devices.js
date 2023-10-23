function showDevicesDiagnosis(data) {
  let container = $("#devices");
  container.empty();

  if (!Array.isArray(data)) return;

  showGraphic(container, data);
}

function showFaultDevices(data) {
  let container = $("#faultdevices");
  container.empty();

  if (!Array.isArray(data)) return;

  showList(container, data);

 
}

function showList(container, data) {

  let state;

  let ul = $('<ul class="list-group">');

  data.forEach(function (item, i) {

    url = "http://" + item.IP + "/rest/json/system";

    $.ajaxSetup({
      timeout: 1000, //Time in milliseconds
    });

    $.getJSON(url)
      .done(function (data) {
       /*  state = true;
        console.log(item.IP + ' NOK');
        li = $('<li>');
        li.text(item.IP);
        li.css("background-color", "green");
        ul.append(li); */


      })
      .fail(function (error) {
        state = false;
       
        let li = $('<li class="list-group-item">');
        li.text(item.Equipment.Number +  ' - ' + item.Equipment.Name + ' - ' + item.IP +
        " - " +
        item.Name);
       
        li.attr("href", "http://" + item.IP);
        li.css("background-color", "");
        ul.append(li);
      });

  });

  container.append(ul);
}

function showGraphic(container, data) {
  data.forEach(function (item, i) {
    let col = $('<div class="col-md-3">');

    let card = $('<div class="card card-primary">');
    col.append(card);

    let state = "";
    if (!item.Active) state = "[DEACTIVATED]";
    let header = $('<div class="card-header">');
    header.append(
      $('<h3 class="card-title">Expandable</h3>').html(
        item.Name + "<br/>" + item.IP + " " + state
      )
    );

    let tools = $('<div class="card-tools">');
    tools.append(
      $(
        '<button type="button" class="btn btn-tool" data-card-widget="collapse"><i class="fas fa-minus"></i></button>'
      )
    );
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

    let labelEquipment = $(
      "<label>" +
      item.Equipment.Number +
      " - " +
      item.Equipment.Name +
      "</label>"
    );
    row2.append(labelEquipment);

    let row3 = $('<div class="row">');
    let labelStatus = $('<label id="' + item.DeviceID + '"></label>');
    row3.append(labelStatus);

    let row4 = $('<div class="row">');
    let labelSystemTime = $(
      '<label id="' + item.DeviceID + ' systemtime"></label>'
    );
    row4.append(labelSystemTime);

    body.append(row2, row3, row4);

    showDeviceState(img, labelStatus, labelSystemTime, item);

    card.append(header, body);
    container.append(col);
  });
}

function showDeviceState(img, labelStatus, labelSystemTime, item) {
  let state;

  data();

  function data() {
    img.css("padding", "8px");
    img.css("border", "1px transparent");
    img.css("border-radius", "5px");
    img.css("background-color", "lightgreen");
    labelStatus.html("Verbindungsaufbau ...");
   // labelSystemTime.html("Verbindungsaufbau ...");

    url = "http://" + item.IP + "/rest/json/system";

    $.ajaxSetup({
      //timeout: 3000, //Time in milliseconds
    });

    $.getJSON(url)
      .done(function (data) {
        state = true;
        img.css("background-color", "lightgreen");
        labelStatus.html("Gerät erreichbar");
        labelSystemTime.html(
          "Systemzeit:  <br />" + data.system.diagarchive[0].time
        );
      })
      .fail(function (error) {
        state = false;
        img.css("background-color", "coral");
        labelStatus.html("Gerät nicht erreichbar");
        labelSystemTime.html("Systemzeit: <br />" + "##.##.#### ##:##:##");
       
      });
  }

  // setInterval(function () {

  //     data();

  // }, 4000);
}

var solidGaugeOptions = {
  chart: {
    type: "solidgauge",
  },

  title: null,

  pane: {
    center: ["50%", "85%"],
    size: "140%",
    startAngle: -90,
    endAngle: 90,
    background: {
      backgroundColor:
        Highcharts.defaultOptions.legend.backgroundColor || "#EEE",
      innerRadius: "60%",
      outerRadius: "100%",
      shape: "arc",
    },
  },

  tooltip: {
    enabled: false,
  },

  // the value axis
  yAxis: {
    stops: [
      [0.1, "#55BF3B"], // green
      [0.5, "#DDDF0D"], // yellow
      [0.9, "#DF5353"], // red
    ],
    lineWidth: 0,
    minorTickInterval: null,
    tickAmount: 2,
    title: {
      y: -70,
    },
    labels: {
      y: 16,
    },
  },

  plotOptions: {
    solidgauge: {
      dataLabels: {
        y: 5,
        borderWidth: 0,
        useHTML: true,
      },
    },
  },
};

var gaugeOptions = {
  chart: {
    type: "gauge",
    plotBackgroundColor: null,
    plotBackgroundImage: null,
    plotBorderWidth: 0,
    plotShadow: false,
  },

  title: {
    text: null,
  },

  pane: {
    startAngle: -150,
    endAngle: 150,
    background: [
      {
        backgroundColor: {
          linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
          stops: [
            [0, "#FFF"],
            [1, "#333"],
          ],
        },
        borderWidth: 0,
        outerRadius: "109%",
      },
      {
        backgroundColor: {
          linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
          stops: [
            [0, "#333"],
            [1, "#FFF"],
          ],
        },
        borderWidth: 1,
        outerRadius: "107%",
      },
      {
        // default background
      },
      {
        backgroundColor: "#DDD",
        borderWidth: 0,
        outerRadius: "105%",
        innerRadius: "103%",
      },
    ],
  },

  // the value axis
  yAxis: {
    min: 0,
    max: 200,

    minorTickInterval: "auto",
    minorTickWidth: 1,
    minorTickLength: 10,
    minorTickPosition: "inside",
    minorTickColor: "#666",

    tickPixelInterval: 30,
    tickWidth: 2,
    tickPosition: "inside",
    tickLength: 10,
    tickColor: "#666",
    labels: {
      step: 2,
      rotation: "auto",
    },
    title: {
      text: "km/h",
    },
    plotBands: [
      {
        from: 0,
        to: 120,
        color: "#55BF3B", // green
      },
      {
        from: 7,
        to: 8,
        color: "#DDDF0D", // yellow
      },
      {
        from: 160,
        to: 200,
        color: "#DF5353", // red
      },
    ],
  },
};
