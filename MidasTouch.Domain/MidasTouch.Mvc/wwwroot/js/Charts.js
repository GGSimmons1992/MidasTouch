$('#btnCreateChart').click(function () {
    createStockChart();
});

function createStockChart() {
    google.charts.load('current', { 'packages': ['corechart', 'controls'] });
    google.charts.setOnLoadCallback(drawChart);
    var chart_div = document.getElementById('chart');
    var ticker = String(document.getElementById('ticker').value).toUpperCase();

    if (document.getElementById('startDate') == null) {
        var viewStartDate = new Date(Date.now());
        viewStartDate.setYear(viewStartDate.getFullYear() - 1);
        var viewEndDate = new Date(Date.now());
        var startDate = viewStartDate.toISOString().split('T')[0];
        var endDate = viewEndDate.toISOString().split('T')[0];
    }
    else {
        var startDate = document.getElementById('startDate').value;
        var endDate = document.getElementById('endDate').value;
        var viewStartDate = (new Date(startDate));
        var viewEndDate = new Date(endDate);
    }

    function drawChart() {
        var data = new google.visualization.DataTable();
        data.addColumn('datetime', 'Date');
        data.addColumn('number', 'Open');
        data.addColumn('number', 'High');
        data.addColumn('number', 'Low');
        data.addColumn('number', 'Close');
        data.addColumn('number', 'Volume');

        $.ajax({
            url: '/api/apiStockData/' + ticker + '/' + startDate + '/' + endDate + '/daily',
            method: 'GET',
            dataType: 'json'
        }).done(function (d) {
            d.forEach(function (di) {
                data.addRow([{ v: new Date(di.date), f: di.date }, di.open,
                di.high, di.low, di.close, di.volume]);
            });

            var dashboard = new google.visualization.Dashboard(chart_div);
            var control = new google.visualization.ControlWrapper({
                controlType: 'ChartRangeFilter',
                containerId: 'filter',
                options: {
                    filterColumnIndex: 0,
                    ui: {
                        chartType: 'LineChart',
                        chartOptions: {
                            chartArea: {
                                height: '80%', width: '85%', left: 100,
                                backgroundColor: { stroke: "gray", strokeWidth: 1 }
                            },
                            hAxis: { baselineColor: 'none' }
                        },
                        chartView: { columns: [0, 4] },
                    }
                },
                state: {
                    range: {
                        start: viewStartDate,
                        end: viewEndDate
                    }
                }
            });

            var chart = new google.visualization.ChartWrapper({
                chartType: 'CandlestickChart',
                containerId: 'candlestick',
                options: {
                    chartArea: {
                        height: '80%', width: '85%', left: 100,
                        backgroundColor: { stroke: "gray", strokeWidth: 1 }
                    },
                    hAxis: {
                        type: 'category', slantedText: false, maxTextLines: 1,
                        maxAlternation: 1
                    },
                    legend: { position: 'none' },
                    candlestick: {
                        fallingColor: { strokeWidth: 0, fill: '#a52714' },
                        risingColor: { strokeWidth: 0, fill: '#0f9d58' }
                    },
                    title: 'Stock Price: ' + ticker,
                },
                view: { columns: [0, 3, 1, 4, 2] }
            });

            var volume = new google.visualization.ChartWrapper({
                chartType: 'ColumnChart',
                containerId: 'volume',
                options: {
                    chartArea: {
                        height: '80%', width: '85%', left: 100, top: 40, bottom: 30,
                        backgroundColor: { stroke: "gray", strokeWidth: 1 }
                    },
                    hAxis: {
                        type: 'category', slantedText: false, maxTextLines: 1,
                        maxAlternation: 1
                    },
                    legend: { position: 'none' },
                    title: "Volume: " + ticker
                },
                view: { columns: [0, 5] }
            });

            dashboard.bind(control, [chart, volume]);
            dashboard.draw(data);

        }).fail(function () {
            alert('Failed to create the chart. Please try it again.');
            });
    }
}