using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HtmlAgilityPack;
using Xamarin.Forms;

//namespace
using App2_0325.ViewModels;
using App2_0325.Models;
using App2_0325.Data;

//drawing add
using SkiaSharp;
using SkiaSharp.Views.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace App2_0325
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        DB_VER_1 DB_Ver_1 = new DB_VER_1();

        const int _days_ex = 120;
        stock_[] stock = new stock_[_days_ex];
        bool AI_Learn_flg = false;
        bool chart_erase = false;

        //Global
        string jusik_code = "";

        //Add 0415 Skiasharp
        SKPaint blackFillPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = SKColors.Black,
            StrokeWidth = 5
        };

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new html_addr();
            
        }

        
        //STEP 1) Select Quant Search
        //STEP 2) MarketCap, PER, PBR, PCR Parameter Get
        //STEP 3) REST API send to My Server and My App
        async private void Button_Clicked_Search(object sender, EventArgs e)
        {
            html_addr HA = (html_addr)BindingContext;

            /*21-02-12*/
            EDI1.Text = "";
             
            EDI1.Text += await HA.Quent_Ver_1(0,0,0,0);
       
            AI_Learn_flg = true;
            chart_erase = true;
        }

        //STEP 1) DB SAVE
        async private void Button_Clicked_DB_SAVE(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Would you save this Data?", "Yes", "No");
            html_addr HA = (html_addr)BindingContext;
            
            //HA.DB_SAVE(sender, e, ref DB_Ver_1);

            if(answer)
            {
                HA.DB_SAVE(sender, e, ref DB_Ver_1);
            }
        }

        private void Button_Clicked_erase(object sender, EventArgs e)
        {
            html_addr HA = (html_addr)BindingContext;
            EDI1.Text = "CLEAR";
            HA.html_png_erase();

            AI_Learn_flg = false;
            chart_erase = false;
            CLEAR stk_clr = new CLEAR();

            stk_clr.CLEAR_STOCK(ref stock, Kind_Constants.test_120days);
        }

        //60days Learn
        private void Button_Clicked_Test(object sender, EventArgs e)
        {
            double Learn_Result = 0;
            if (AI_Learn_flg == false) { EDI1.Text += "DATA READ FAIL"; return; }

            BP_Learn BP = new BP_Learn();

            //1. martget price
            //2. high price
            //3. low price
            //4. transaction price
            //5. closing price (output)

            //Version2
            Learn_Result = BP.BP_START_STOCK_VERSION2(ref stock, Kind_Constants.test_60days);
            EDI1.Text = "종가 예측: " + (int)Learn_Result;
        }

        //120days
        private void Button_Clicked_Test_120(object sender, EventArgs e)
        {
            double Learn_Result = 0;

            if (AI_Learn_flg == false) { EDI1.Text += "DATA READ FAIL"; return; }

            BP_Learn BP = new BP_Learn();

            //1. martget price
            //2. high price
            //3. low price
            //4. transaction price
            //5. closing price (output)

            //Version2
            Learn_Result = BP.BP_START_STOCK_VERSION2(ref stock, Kind_Constants.test_120days);
            EDI1.Text = "종가 예측: " + (int)Learn_Result;
        }

        private void Button_Clicked_Save(object sender, EventArgs e)
        {

        }

        //Tap Page
        async private void Button_Clicked_Next(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NextPage1(jusik_code));
        }

        private void Button_Clicked_Day(object sender, EventArgs e)
        {
            /*21-02-13*/
            html_addr HA = (html_addr)BindingContext;
            HA.html_png_parsing(jusik_code, Kind_Constants.kinds_day);
        }
        private void Button_Clicked_Week(object sender, EventArgs e)
        {
            /*21-02-13*/
            html_addr HA = (html_addr)BindingContext;
            HA.html_png_parsing(jusik_code, Kind_Constants.kinds_week);
        }
        private void Button_Clicked_Month(object sender, EventArgs e)
        {
            /*21-02-13*/
            html_addr HA = (html_addr)BindingContext;
            HA.html_png_parsing(jusik_code, Kind_Constants.kinds_month);
        }
    }
}
