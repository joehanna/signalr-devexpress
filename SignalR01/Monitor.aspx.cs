using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.Web;
using System.Diagnostics;
using System.Threading;

namespace SignalR01
{
  public partial class Monitor : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ASPxCallback1_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {

      Debug.WriteLine($"In callback with {e.Parameter}");

      var x = e.Parameter;
      double percentage = 0;

      double.TryParse(x, out percentage);

      if ((percentage >= 0) && (percentage <= 100))
      {
        var chart = this.WebChartControl1;
        var series = chart.Series[0];

        //var points = new List<SeriesPoint>() { new SeriesPoint(0), new SeriesPoint(percentage) };
        //series.Points.Clear();
        //series.Points.AddRange(points.ToArray());

        var values = new List<double>() { percentage }

        //https://www.devexpress.com/Support/Center/Question/Details/Q536386
        series.Points[1].Values = percentage;


        chart.RefreshData();
      }
    }

    protected void CallbackPanel_Callback(object sender, DevExpress.Web.CallbackEventArgsBase e)
    {
      ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
      NoCommentsLabel.Visible = false;
      Thread.Sleep(3000);

      ASPxLabel comment = new ASPxLabel()
      {
        Text = string.Format("[{0}]\n{1}\n\n",
              DateTime.Now.ToLocalTime(),
              !string.IsNullOrEmpty(TextBox.Text) ? TextBox.Text : "Empty comment"
          ),
        CssClass = "comment"
      };

      List<ASPxLabel> comments = (List<ASPxLabel>)Session["comments"] ?? new List<ASPxLabel>();

      comments.Add(comment);
      panel.Controls.Add(comment);
      Session["comments"] = comments;

      CountLabel.Text = "Comments Count : " + comments.Count;
    }

    protected void CallbackPanel_Init(object sender, EventArgs e)
    {
      if (!IsPostBack && !IsCallback)
        Session.Clear();

      ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
      if (panel.ClientInstanceName == "CommentCallbackPanel")
      {
        RecreateComments(sender);
      }
    }

    private void RecreateComments(object sender)
    {
      List<ASPxLabel> comments;
      if ((comments = (List<ASPxLabel>)Session["comments"]) != null)
      {
        CountLabel.Text = "Comments Count : " + comments.Count;
        ASPxCallbackPanel panel = (ASPxCallbackPanel)sender;
        NoCommentsLabel.Visible = false;
        foreach (ASPxLabel comment in comments)
        {
          panel.Controls.Add(comment);
        }
      }
    }


  }
}