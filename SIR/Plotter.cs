using System;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
using Microsoft.FSharp.Core;
using Microsoft.FSharp.Collections;

public class Plotter
{
	public Plotter()
	{

	}

	public void draw_Graph()
	{
        LinearAxis xAxis = new LinearAxis();
        xAxis.SetValue("title", "xAxis");
        xAxis.SetValue("zerolinecolor", "#ffff");
        xAxis.SetValue("gridcolor", "#ffff");
        xAxis.SetValue("showline", true);
        xAxis.SetValue("zerolinewidth", 2);

        LinearAxis yAxis = new LinearAxis();
        yAxis.SetValue("title", "yAxis");
        yAxis.SetValue("zerolinecolor", "#ffff");
        yAxis.SetValue("gridcolor", "#ffff");
        yAxis.SetValue("showline", true);
        yAxis.SetValue("zerolinewidth", 2);

        Layout layout = new Layout();
        layout.SetValue("xaxis", xAxis);
        layout.SetValue("yaxis", yAxis);
        layout.SetValue("title", "A Figure Specified by DynamicObj");
        layout.SetValue("plot_bgcolor", "#e5ecf6");
        layout.SetValue("showlegend", true);

        Trace trace = new Trace("bar");
        trace.SetValue("x", new[] { 1, 2, 3 });
        trace.SetValue("y", new[] { 1, 3, 2 });

        var fig = GenericChart.Figure.create(ListModule.OfSeq(new[] { trace }), layout);
        GenericChart.fromFigure(fig);
    }
}


