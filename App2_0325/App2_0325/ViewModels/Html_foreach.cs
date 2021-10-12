using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using HtmlAgilityPack;
using System.Xml;
using System.Net;

using App2_0325.Models;
using App2_0325.Data;

namespace App2_0325.ViewModels
{
    public class CompareList
    {
        public CompareList() {}
        public int Compare(object Obj1, object Obj2)
        {
            Quant_Ver_1 Q1 = (Quant_Ver_1)Obj1;
            Quant_Ver_1 Q2 = (Quant_Ver_1)Obj2;

            return Q1.s_PER.CompareTo(Q1.s_PER);
        }
    }

    public class Global_days
    {
        public const int _days = 60;
        public const int divide_days = 10;
        public const int _days_ex = 120;
    }

    public class ConstantValue
    {
        public const int stock_count = 2462;

    }

    public struct stock_
    {
        public int s_date;
        public int s_dcp_int;
        public int s_dtv_int;
        public int s_dmp_int;
        public int s_dhp_int;
        public int s_dlp_int;
    }
    public struct stock_v2
    {
        public int s_date;
        public int s_dcp_int;
        public int s_dmp_int;
        public int s_dhp_int;
        public int s_dlp_int;
    }

    public struct Quant_Ver_1
    {
        public int s_CODE;
        public int s_MARKETCAP;
        public int s_PER;
        public int s_PCR;
        public int s_PBR;
        public string s_NAME;
    }

    public class C_Quant_Ver_1
    {
        public int s_CODE;
        public int s_MARKETCAP;
        public int s_PER;
        public int s_PCR;
        public int s_PBR;
        public string s_NAME;
        
        public C_Quant_Ver_1(int s_CODE, int s_MARKETCAP, int s_PER, int s_PCR, int s_PBR, string s_NAME)
        {
            this.s_CODE = s_CODE;
            this.s_MARKETCAP = s_MARKETCAP;
            this.s_PER = s_PER;
            this.s_PCR = s_PCR;
            this.s_PBR = s_PBR;
            this.s_NAME = s_NAME;
        }
        public int CompareTo(object obj)
        {
            C_Quant_Ver_1 target = (C_Quant_Ver_1)obj;
            if(this.s_MARKETCAP > target.s_MARKETCAP)
            {
                return 1;
            } 
            else if (this.s_MARKETCAP == target.s_MARKETCAP)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
    public class MarketCapSortClass : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            C_Quant_Ver_1 left = x as C_Quant_Ver_1;
            C_Quant_Ver_1 right = y as C_Quant_Ver_1;

            return left.s_MARKETCAP.CompareTo(right.s_MARKETCAP);
        }
    }

    public class PerSortClass : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            C_Quant_Ver_1 left = x as C_Quant_Ver_1;
            C_Quant_Ver_1 right = y as C_Quant_Ver_1;

            return left.s_PER.CompareTo(right.s_PER);
        }
    }

    public class PcrSortClass : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            C_Quant_Ver_1 left = x as C_Quant_Ver_1;
            C_Quant_Ver_1 right = y as C_Quant_Ver_1;

            return left.s_PCR.CompareTo(right.s_PCR);
        }
    }

    public class PbrSortClass : IComparer
    {
        int IComparer.Compare(object x, object y)
        {
            C_Quant_Ver_1 left = x as C_Quant_Ver_1;
            C_Quant_Ver_1 right = y as C_Quant_Ver_1;

            return left.s_PBR.CompareTo(right.s_PBR);
        }
    }

    class Html_foreach
    {
        public stock_ html_get_event(stock_ get)
        {

            return get;
        }

    }
    public class html_addr : INotifyPropertyChanged
    {
        readonly DB_Manager DB_manager;
        readonly DB_VER_1 DB_ver_1;
        public html_addr()
        {

        }
        #region notifyproperty
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        string candleday;
        public string CandleDay
        {
            get => candleday;
            set => candleday = value;
        }
        public void html_png_erase()
        {
            candleday = "";
            NotifyPropertyChanged(nameof(CandleDay));
        }
        public void html_png_parsing(string jusik_code, int kinds)
        {
            //https://ssl.pstatic.net/imgfinance/chart/item/candle/day/005380.png
            //https://finance.naver.com/item/main.nhn?code=005380# 
            //https://finance.naver.com/item/main.nhn?code=005380
            //https://ssl.pstatic.net/imgfinance/chart/item/candle/day/005380.png
            var html = "";

            switch (kinds)
            {
                case Kind_Constants.kinds_day:
                    html = @"https://ssl.pstatic.net/imgfinance/chart/item/candle/day/";
                    break;
                case Kind_Constants.kinds_week:
                    html = @"https://ssl.pstatic.net/imgfinance/chart/item/candle/week/";
                    break;
                case Kind_Constants.kinds_month:
                    html = @"https://ssl.pstatic.net/imgfinance/chart/item/candle/month/";
                    break;
                default:
                    break;
            }
            var test = jusik_code + ".png";
            html += test;
            candleday = html.ToString();
            NotifyPropertyChanged(nameof(CandleDay));
        }

        public string html_data_parsing(string jusik_code, ref stock_[] stock_days, int kind, int kind_version)
        {
            //Initial
            stock_[] stock = new stock_[Global_days.divide_days];
            string put = "";
            stock = stock_days; //ref

            int carry = 0;

            //Method Set
            MethodClass call_method = new MethodClass();

            var html = "";
            var test = "";
            switch (kind)
            {
                case Kind_Constants.days_10:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=1";
                    carry = 0;
                    break;
                case Kind_Constants.days_20:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=2";
                    carry = 10;
                    break;
                case Kind_Constants.days_30:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=3";
                    carry = 20;
                    break;
                case Kind_Constants.days_40:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=4";
                    carry = 30;
                    break;
                case Kind_Constants.days_50:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=5";
                    carry = 40;
                    break;
                case Kind_Constants.days_60:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=6";
                    carry = 50;
                    break;
                case Kind_Constants.days_70:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=7";
                    carry = 60;
                    break;
                case Kind_Constants.days_80:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=8";
                    carry = 70;
                    break;
                case Kind_Constants.days_90:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=9";
                    carry = 80;
                    break;
                case Kind_Constants.days_100:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=10";
                    carry = 90;
                    break;
                case Kind_Constants.days_110:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=11";
                    carry = 100;
                    break;
                case Kind_Constants.days_120:
                    html = @"https://finance.naver.com/item/sise_day.nhn?code=";
                    test = jusik_code + "&page=12";
                    carry = 110;
                    break;
            }
            html += test; // 주식 정보 종합

            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            var HtmlDoc = web.Load(html);

            HtmlAgilityPack.HtmlNodeCollection[] htmlNodes = new HtmlAgilityPack.HtmlNodeCollection[Global_days.divide_days];

            //3,4,5,6,7
            //11,12,13,14,15
            for (int i = 0; i < Global_days.divide_days; i++)
            {
                int jump = 3;
                if (i >= 5)
                {
                    // ex) i=5 + jump -> 11 
                    jump = 6;
                }
                jump += i;
                htmlNodes[i] = HtmlDoc.DocumentNode.SelectNodes("//body/table[1]/tr[" + jump + "]");
                if (htmlNodes[i] == null) { return i + jump + "err"; }

                //td1 날짜, td2 종가, td3 전일비, td4 시가, td5 고가, td6 저가 td7 거래량
                foreach (var node in htmlNodes[i])
                {

                    if (node != null)
                    {
                        switch (kind_version)
                        {
                            case Kind_Constants.version_1:
                                var data_date = node.SelectSingleNode("td[1]").InnerText;
                                var data_closing_price = node.SelectSingleNode("td[2]").InnerText;
                                var data_market_price = node.SelectSingleNode("td[4]").InnerText;
                                var data_high_price = node.SelectSingleNode("td[5]").InnerText;
                                var data_low_price = node.SelectSingleNode("td[6]").InnerText;
                                var data_transaction_volume = node.SelectSingleNode("td[7]").InnerText;

                                put += "Date:" + data_date + " 종가:" + data_closing_price + " 시가:" + data_market_price +
                                   " 고가:" + data_high_price + " 저가:" + data_low_price + " 거래량:" + data_transaction_volume + Environment.NewLine;

                                //stock[carry].s_date = call_method.CnvStringToInt_4(data_date);
                                stock_days[carry].s_date = call_method.CnvStringToInt_4(data_date);
                                stock_days[carry].s_dcp_int = call_method.CnvStringToInt(data_closing_price);
                                stock_days[carry].s_dtv_int = call_method.CnvStringToInt(data_transaction_volume);
                                stock_days[carry].s_dmp_int = call_method.CnvStringToInt(data_market_price);
                                stock_days[carry].s_dhp_int = call_method.CnvStringToInt(data_high_price);
                                stock_days[carry].s_dlp_int = call_method.CnvStringToInt(data_low_price);
                                carry++;
                                break;
                            case Kind_Constants.version_2:
                                var data_date2 = node.SelectSingleNode("td[1]").InnerText;
                                var data_closing_price2 = node.SelectSingleNode("td[2]").InnerText;
                                var data_market_price2 = node.SelectSingleNode("td[4]").InnerText;
                                var data_high_price2 = node.SelectSingleNode("td[5]").InnerText;
                                var data_low_price2 = node.SelectSingleNode("td[6]").InnerText;
                                var data_transaction_volume2 = node.SelectSingleNode("td[7]").InnerText;

                                put += "Date:" + data_date2 + " 종가:" + data_closing_price2 + " 시가:" + data_market_price2 +
                                   " 고가:" + data_high_price2 + " 저가:" + data_low_price2 + " 거래량:" + data_transaction_volume2 + Environment.NewLine;

                                //stock[carry].s_date = call_method.CnvStringToInt_4(data_date);
                                stock_days[carry].s_date = call_method.CnvStringToInt_4(data_date2);
                                stock_days[carry].s_dcp_int = call_method.CnvStringToInt(data_closing_price2);
                                stock_days[carry].s_dmp_int = call_method.CnvStringToInt(data_market_price2);
                                stock_days[carry].s_dhp_int = call_method.CnvStringToInt(data_high_price2);
                                stock_days[carry].s_dlp_int = call_method.CnvStringToInt(data_low_price2);
                                carry++;
                                break;
                        }
                    }
                }
            }
            return put;
        }

        //https://finance.naver.com/item/sise.naver?code=

        // STEP 1) Parsing Stock Data
        // STEP 2) sort Array
        // STEP 3) Result Setting..

        public async Task<string> Quent_Ver_1(float N1, float N2, float N3, float N4, )
        {
            string put = "";
            return put;
        }
        public string Parsing_C_Quent_Ver_1(float N1, float N2, float N3, float N4)
        {
            string put = "";
            int MarketCap_Length = 615;
            int PER_Length = (int)((MarketCap_Length / 10) * 2.5);

            //C_Quant_Ver_1[] quant_Ver_1 = new C_Quant_Ver_1[ConstantValue.stock_count];
            sangjang sj = new sangjang();

            //Method Set
            MethodClass call_method = new MethodClass();

            //Array
            ArrayList ar = new ArrayList();

            // STEP 1)
            //for (int i = 0; i < sj.Number_public; i++)
            for (int i = 0; i < 100; i++)
            {
                var html = @"https://finance.naver.com/item/sise.naver?code=";
                html = html + sj.company[i].ToString("D6");

                HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
                var HtmlDoc = web.Load(html);

                HtmlNode htmlNode = HtmlDoc.DocumentNode.SelectSingleNode("//body/div[3]/div[2]/div[2]/div[1]/div[2]/div[1]/table/tbody/tr[12]");
                HtmlNode htmlNode_PER = HtmlDoc.DocumentNode.SelectSingleNode("//body/div[3]/div[2]/div[2]/div[1]/div[2]/div[1]/table/tbody/tr[10]");
                if (htmlNode == null || htmlNode_PER == null)
                {
                    return "err";
                }

                var data_marketcap = htmlNode.SelectSingleNode("td[1]").InnerText;
                int data_MARKETCAP = call_method.CnvStringToInt(data_marketcap);

                var data_per = htmlNode_PER.SelectSingleNode("td[1]").InnerText;
                data_per = data_per.Trim();
                if ("N/A".Equals(data_per))
                {
                    data_per = "1000";
                }
                int data_PER = call_method.CnvStringToInt(data_per);

                ar.Add(new C_Quant_Ver_1(sj.company[i], data_MARKETCAP, data_PER, 0, 0, sj.jongmok[i]));
            }

            //https://stackoverflow.com/questions/12026344/how-to-use-array-sort-to-sort-an-array-of-structs-by-a-specific-element

            //ar.Sort(); //why error?

            // STEP 2)
            MarketCapSortClass marketCapSort = new MarketCapSortClass();
            PerSortClass perSort = new PerSortClass();
            PbrSortClass pbrSort = new PbrSortClass();
            PcrSortClass pcrSort = new PcrSortClass();

            ar.Sort(0, 99, marketCapSort);
            ar.Sort(0, 20, perSort);
            //ar.Sort(0, 20, pbrSort);
            //ar.Sort(0, 20, pcrSort);

            C_Quant_Ver_1[] quant_Ver_1 = new C_Quant_Ver_1[20];
            for (int i = 0; i < quant_Ver_1.Length; i++)
            {
                quant_Ver_1[i] = new C_Quant_Ver_1(0, 0, 0, 0, 0, "test");
            }

            int cnt = 0;
            //foreach (C_Quant_Ver_1 CQ in ar)
            //{
            //    if (cnt == 20) break;
            //    quant_Ver_1[cnt].s_CODE = CQ.s_CODE;
            //    quant_Ver_1[cnt].s_MARKETCAP = CQ.s_MARKETCAP;
            //    quant_Ver_1[cnt].s_PBR = CQ.s_PBR;
            //    quant_Ver_1[cnt].s_PCR = CQ.s_PCR;
            //    quant_Ver_1[cnt].s_PER = CQ.s_PER;
            //    quant_Ver_1[cnt].s_NAME = CQ.s_NAME;
            //    cnt++;
            //}

            //Enable Code?
            foreach(C_Quant_Ver_1 CQ in ar)
            {
                if (cnt == 20) break;
                quant_Ver_1[cnt] = new C_Quant_Ver_1(CQ.s_CODE, CQ.s_MARKETCAP, CQ.s_PER, CQ.s_PCR, CQ.s_PBR, CQ.s_NAME);
                cnt++;
            }

            for (int i = 0; i < 20; i++)
            {
                put += "종목: " + quant_Ver_1[i].s_NAME + Environment.NewLine;
            }

            return put;
        }
        async public void DB_SAVE(object sender, EventArgs e, ref DB_VER_1 DB_Ver_1)
        {
            DB_Ver_1.Ver1_CODE = 0;
            DB_Ver_1.Ver1_NAME = "TEST";

            var test = await DB_manager.Add_Ver1(DB_Ver_1.Ver1_CODE, DB_Ver_1.Ver1_NAME);
            return;
        }
    }
}