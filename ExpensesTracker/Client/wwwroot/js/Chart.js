function GeneratePieChart(Expenses)
{
    am4core.useTheme(am4themes_animated);

    var chart = am4core.create("chartdiv", am4charts.PieChart);

    chart.data = Expenses;

    var pieSeries = chart.series.push(new am4charts.PieSeries());
    pieSeries.dataFields.value = "totalExpenses";
    pieSeries.dataFields.category = "category";
    pieSeries.slices.template.stroke = am4core.color("#fff");
    pieSeries.slices.template.strokeOpacity = 1;

    // This creates initial animation
    pieSeries.hiddenState.properties.opacity = 1;
    pieSeries.hiddenState.properties.endAngle = -90;
    pieSeries.hiddenState.properties.startAngle = -90;
    pieSeries.legendSettings.labelText = "{category}: {value}€"
    pieSeries.legendSettings.valueText = " ";
    
    //Legend seetings
    chart.hiddenState.properties.radius = am4core.percent(0);
    chart.legend = new am4charts.Legend();
    pieSeries.labels.template.disabled = true;
    pieSeries.ticks.template.disabled = true;
    pieSeries.slices.template.tooltipText = "";

    
} 

