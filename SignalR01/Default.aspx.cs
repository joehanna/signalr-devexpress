using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;

using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

namespace SignalR01
{
  public partial class _Default
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    Series series { get { return WebChartControl1.Series[0]; } }
    PieSeriesLabel label { get { return (PieSeriesLabel)series.Label; } }
    PieSeriesViewBase view { get { return (PieSeriesViewBase)series.View; } }

    protected void WebChartControl1_ObjectSelected(object sender, HotTrackEventArgs e)
    {
      Series series = e.Object as Series;
      if (series != null)
      {
        ExplodedSeriesPointCollection explodedPoints = ((PieSeriesViewBase)series.View).ExplodedPoints;
        SeriesPoint point = (SeriesPoint)e.AdditionalObject;
        explodedPoints.ToggleExplodedState(point);
      }
      e.Cancel = series == null;
    }
    protected void WebChartControl1_CustomCallback1(object sender, CustomCallbackEventArgs e)
    {
      //if (e.Parameter == "LabelPosition")
      //  PerformLabelPositionAction();
      //else if (e.Parameter == "ValueAsPercent")
      //  PerformValueAsPercentAction();
      //else if (e.Parameter == "ExplodedPoints")
      //  PerformExplodedPointsAction();
      //else if (e.Parameter == "ShowLabels")
      //  PerformShowLabelsAction();
    }

  }
}


