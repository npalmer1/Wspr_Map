using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Maidenhead;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Security;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using WSPR_Map;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;


namespace Wspr_Map
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        GMapOverlay markers = new GMapOverlay("markers");
        GMapOverlay routes = new GMapOverlay("routes");

        MessageClass Msg = new MessageClass();

        public struct decoded_data
        {
            public DateTime datetime;
            public Int16 band;
            public string tx_sign;
            public string tx_loc;
            public double frequency;
            public Int16 power;
            public int snr;
            public Int16 drift;
            public int distance;
            public Int16 azimuth;
            public string reporter;
            public string reporter_loc;
            public float dt;
        }
        decoded_data DX = new decoded_data();


        public struct RX_data
        {
            public Int64 id;
            public DateTime time;
            public Int16 band;
            public string rx_sign;
            public float rx_lat;
            public float rx_lon;
            public string rx_loc;
            public string tx_sign;
            public float tx_lat;
            public float tx_lon;
            public string tx_loc;
            public int distance;
            public int azimuth;
            public int rx_azimuth;
            public int frequency;
            public int power;
            public int snr;
            public int drift;
            public string version;
            public int code;
        }
        RX_data RX = new RX_data();

        string locator = "";
        string call = "";
        double mylat;
        double mylon;

        string server = "127.0.0.1";
        string user = "admin";
        string pass = "wspr";

        bool connectionError = false;
        private async void Form1_Load(object sender, EventArgs e)
        {
            System.Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string ver = "0.1.2";
            string header = "WSPR Scheduler Map                       V." + ver + "    GNU GPLv3 License";
            MessageForm mForm = new MessageForm();
            Msg.TCMessageBox("Initialising WSPR Scheduler Map", "WS Map", 20000, mForm);
            passtextBox.Text = pass;
            radioButton1.Checked = true;
            bandlistBox.SelectedIndex = 0; //all bands
            periodlistBox.SelectedIndex = 3; //last 10 minutes
            clutterlistBox.SelectedIndex = 0; //default to 0
            pathcheckBox.Checked = true;    //seelct path lines by default

            gmap.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
            //gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gmap.Position = new PointLatLng(30, 0); // equator at 0 degrees

            //gmap.Anchor = AnchorStyles.Top;

            gmap.MinZoom = 2;
            gmap.MaxZoom = 16;

            gmap.CanDragMap = true;
            gmap.DragButton = MouseButtons.Left;

            gmap.ShowCenter = false;


            int i = table_countRX();
            if (i > 0)
            {
                await find_reportedRX(i);    //find own call and locator from database
            }
            else
            {
                MessageBox.Show("Database error or no data in database");
            }

            addOwn();   //add own marker to map
            gmap.Overlays.Add(markers);

            gmap.Zoom = 2;
            gmap.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            Zoomlabel.Text = gmap.Zoom.ToString("F1");
            this.Text = "Reports for station: " + call;
            if (locator != "" || locator != null)
            {
                this.Text = this.Text + "  at: " + locator;
            }
            this.Text = this.Text + "                  " + header;

            if (!getUserandPassword())
            {
                passtextBox.Text = "";
            }            
            mForm.Dispose();

            //GMaps.Instance.Mode = AccessMode.CacheOnly;
        }
        private async Task addMarker(double lat, double lon, GMarkerGoogleType pin, string tag)
        {

            GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(lat, lon),
                pin);
            marker.Tag = tag;
            markers.Markers.Add(marker);
          

        }

        private async Task addOwn()
        {


            LatLng latlong = MaidenheadLocator.LocatorToLatLng(locator);

            mylat = latlong.Lat;
            mylon = latlong.Long;

            GMarkerGoogleType pin = GMarkerGoogleType.yellow_small;

            addMarker(mylat, mylon, pin, call);


        }

        private async Task addPath(double mylat, double mylon, double lat, double lon, Color C)
        {

            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(mylat, mylon));
            points.Add(new PointLatLng(lat, lon));

            GMapRoute route = new GMapRoute(points, "Path");
            route.Stroke = new Pen(C, 1);
            routes.Routes.Add(route);
            //gmap.Overlays.Add(routes);
        }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            pinlabel.BringToFront();
            pinlabel.Location = e.Location;
            pinlabel.Text = (string)item.Tag;

        }



        private int table_countRX()
        {
            int count;
            string connectionString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=wspr_rpt";

            try
            {
                //string connectionString = "Server=server;Port=3306;Database=wspr;User ID=user;Password=pass;";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT COUNT(*) FROM received", connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close();
                }
                return count;

            }
            catch
            {
                return 0;
            }
        }
        private int table_countTX()
        {
            int count;
            string connectionString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=wspr_rx";

            try
            {
                //string connectionString = "Server=server;Port=3306;Database=wspr;User ID=user;Password=pass;";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand("SELECT COUNT(*) FROM reported", connection))
                    {
                        count = Convert.ToInt32(command.ExecuteScalar());
                    }
                    connection.Close();
                }
                return count;

            }
            catch
            {
                return 0;
            }
        }

        private string find_band()
        {
            int s = bandlistBox.SelectedIndex;
            string b = "";
            switch (s)
            {
                case -1:
                    b = ""; //all
                    break;
                case 0:
                    b = ""; //all
                    break;
                case 1:
                    b = "0.136";
                    break;
                case 2:
                    b = "0.47";
                    break;
                case 3:
                    b = "1.8";
                    break;
                case 4:
                    b = "3.5";
                    break;
                case 5:
                    b = "5.";
                    break;
                case 6:
                    b = "7.";
                    break;
                case 7:
                    b = "10";
                    break;
                case 8:
                    b = "14";
                    break;
                case 9:
                    b = "18";
                    break;
                case 10:
                    b = "21";
                    break;
                case 11:
                    b = "24";
                    break;
                case 12:
                    b = "28";
                    break;
                case 13:
                    b = "50";
                    break;
                case 14:
                    b = "70";
                    break;
                case 15:
                    b = "144";
                    break;
                case 16:
                    b = "432";
                    break;
                case 17:
                    b = "1296";
                    break;
                default:
                    b = "";
                    break;
            }
            return b;
        }

        private int get_band(int bandno)
        {
            int b = -2; //all
            switch (bandno)
            {
                case 0:
                    b = -2; //all
                    break;
                case 1:
                    b = -1; //lf
                    break;
                case 2:
                    b = 0;  //mf
                    break;
                case 3:
                    b = 1;  //1.8
                    break;
                case 4:
                    b = 3;
                    break;
                case 5:
                    b = 5;
                    break;
                case 6:
                    b = 7;
                    break;
                case 7:
                    b = 10;
                    break;
                case 8:
                    b = 14;
                    break;
                case 9:
                    b = 18;
                    break;
                case 10:
                    b = 21;
                    break;
                case 11:
                    b = 24;
                    break;
                case 12:
                    b = 28;
                    break;
                case 13:
                    b = 50;
                    break;
                case 14:
                    b = 70;
                    break;
                case 15:
                    b = 144;
                    break;
                case 16:
                    b = 432;
                    break;
                case 17:
                    b = 1296;
                    break;
                default:
                    b = -2; //all
                    break;
            }
            return b;
        }
        private string get_reverse_band(int bandno)
        {
            string b = "all";
            switch (bandno)
            {             
                case -1:
                    b = "LF"; //lf
                    break;
                case 0:
                    b = "MF";  //mf
                    break;
                case 1:
                    b = "160";  //1.8
                    break;
                case 3:
                    b = "80";
                    break;
                case 5:
                    b = "60";
                    break;
                case 7:
                    b = "40";
                    break;
                case 10:
                    b = "30";
                    break;
                case 14:
                    b = "20";
                    break;
                case 18:
                    b = "17";
                    break;
                case 21:
                    b = "15";
                    break;
                case 24:
                    b = "12";
                    break;
                case 28:
                    b = "10";
                    break;
                case 50:
                    b = "6";
                    break;
                case 70:
                    b = "4";
                    break;
                case 144:
                    b = "2";
                    break;
                case 432:
                    b = "70cm";
                    break;
                case 1296:
                    b = "23cm";
                    break;
                default:
                    b = "?";
                    break;
            }
            return b;
        }

        private double findPeriod()
        {
            int s = periodlistBox.SelectedIndex;
            string t = "";
            switch (s)
            {
                case -1:
                    return 0;
                case 0:
                    return 0.1;
                case 1:
                    return 0.2;
                case 2:
                    return 0.5;
                case 3:
                    return 1;
                case 4:
                    return 2;
                case 5:
                    return 3;
                case 6:
                    return 6;
                case 7:
                    return 12;
                case 8:
                    return 24;
                case 9:
                    return 48;
                default:
                    return 0;
            }
        }


        private void filterbutton_Click(object sender, EventArgs e)
        {
            MessageForm mForm = new MessageForm();
            Msg.TCMessageBox("Please wait....", "", 30000, mForm);
            filter_results();
            
            mForm.Dispose();
        }

        private async void initial_map(int min)
        {
            DateTime end = DateTime.Now.ToUniversalTime();
            DateTime start = DateTime.Now.AddMinutes(-min);
            string to = end.ToString("yyyy-MM-dd HH:mm:00");
            string from = start.ToString("yyyy-MM-dd HH:mm:00");
            int rows = table_countRX();
            if (rows > 0)
            {
                if (radioButton1.Checked || radioButton3.Checked)
                {
                    await find_selectedRX(from, to, "", rows);
                }

            }
            rows = table_countTX();
            if (rows > 0)
            {

                if (radioButton1.Checked || radioButton2.Checked)
                {
                    await find_selectedTX(from, to, -2, rows); //-2 means all bands
                }

            }
        }
        private async void filter_results()
        {
            double zoom = gmap.Zoom;
            gmap.Overlays.Clear();
            routes.Routes.Clear();
            markers.Markers.Clear();                      
           
            pinlabel.Text = "";
         
            DateTime dtNow = DateTime.Now.ToUniversalTime();
            DateTime dtPrev = dtNow;
            double p = findPeriod();
            if (p < 1 && p > 0)
            {
                p = p * -1 * 60;
                dtPrev = dtPrev.AddMinutes(p);
            }
            else if (p >= 1 && p < 24)
            {
                p = p * -1;
                dtPrev = dtPrev.AddHours(p);
            }
            else if (p == 24)
            {
                dtPrev = dtPrev.AddDays(-1);
            }
            else if (p == 48)
            {
                dtPrev = dtPrev.AddDays(-2);
            }
            else
            {
                dtPrev = dtNow;
            }
            string now = dtNow.ToString("yyyy-MM-dd HH:mm:00");
            string prev = dtPrev.ToString("yyyy-MM-dd HH:mm:00");
            int rows = table_countRX();
            string mhz = find_band();
            int band = get_band(bandlistBox.SelectedIndex);
            addOwn();
            
            if (rows > 0)
            {
                if (radioButton1.Checked || radioButton3.Checked)
                {
                    await find_selectedRX(prev, now, mhz, rows);
                }
              
            }
            rows = table_countTX();
            if (rows > 0)
            {

                if (radioButton1.Checked || radioButton2.Checked)
                {
                    await find_selectedTX(prev, now, band, rows); //-2 means all bands
                }
               
            }

            gmap.Zoom = 3;
            gmap.Overlays.Add(markers);
            gmap.Overlays.Add(routes);
         
            gmap.Zoom = zoom;


        }


        private async Task find_selectedRX(string datetime1, string datetime2, string mhz, int tablecount) //find a slot row for display in grid from the database corresponding to the date/time from the slot
        {   //received by own station of other station txns

            //gmap.Zoom = 3;
            double txlat = 0;
            double txlon = 0;

            DataTable Slots = new DataTable();
            //DateTime d = new DateTime();
            int i = 0;
            bool found = false;
            string myConnectionString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=wspr_rpt";

            int maxrows = 1000; //max rows to return
            string and = "";
            string bandstr = "";
            string q = "";
            if (mhz != "")
            {
                bandstr = " AND frequency LIKE '" + mhz + "%' ";
                and = "AND";
            }

            string fromstr = "";
            if (clutterlistBox.SelectedIndex >= 0)
            {
                fromstr = clutterlistBox.SelectedItem.ToString();
                if (!kmcheckBox.Checked)    //miles
                {
                    double km = Convert.ToDouble(fromstr);
                    km = km * 1.60934;
                    fromstr = km.ToString("F0");
                }
            }


            try
            {
                MySqlConnection connection = new MySqlConnection(myConnectionString);

                connection.Open();

                MySqlCommand command = connection.CreateCommand();



                command.CommandText = "SELECT * FROM received WHERE datetime >= '" + datetime1 + "' AND datetime <= '" + datetime2 + "'" + bandstr + " AND distance >= '" + fromstr + "' ORDER BY datetime DESC";


                MySqlDataReader Reader;
                Reader = command.ExecuteReader();
                string bandS = "";
                int index = bandlistBox.SelectedIndex;
                while (Reader.Read())
                {
                    found = true;

                    if (i < tablecount)   //only show first maxrows rows, or to length of reported table
                    {

                        DX.datetime = (DateTime)Reader["datetime"];
                        DX.band = (Int16)Reader["band"];

                        DX.tx_sign = (string)Reader["tx_sign"];
                        DX.tx_loc = (string)Reader["tx_loc"];

                        DX.frequency = (double)Reader["frequency"];
                        DX.power = (Int16)Reader["power"];
                        DX.snr = (int)Reader["snr"];
                        DX.drift = (Int16)Reader["drift"];
                        DX.distance = (int)Reader["distance"];
                        DX.azimuth = (Int16)Reader["azimuth"];
                        DX.reporter = (string)Reader["reporter"];
                        DX.reporter_loc = (string)Reader["reporter_loc"];
                        DX.dt = (float)Reader["dt"];


                        GMarkerGoogleType pin = GMarkerGoogleType.blue_small;
                        bool special = false;
                        if (QcheckBox.Checked && specialCall(DX.tx_sign))
                        {                       
                            special = true;
                        }
                        if (DX.tx_sign != "nil rcvd" && !special) 
                        {
                            LatLng latlong = MaidenheadLocator.LocatorToLatLng(DX.tx_loc);

                            txlat = latlong.Lat;
                            txlon = latlong.Long;
                            bandS = "";
                            if (index == 0) //if all bands selected
                            {                                
                                bandS = get_reverse_band(DX.band);
                                bandS = " (" + bandS + ")";
                            }
                            await addMarker(txlat, txlon, pin, DX.tx_sign+bandS);
                            if (pathcheckBox.Checked)
                            {
                                await addPath(mylat, mylon, txlat, txlon, Color.Green);
                            }
                        }
                        i++;
                    }
                    else
                    {
                        break;
                    }


                }
                Reader.Close();
                connection.Close();
                //gmap.Zoom = 2;

            }
            catch
            {
                found = false;

            }

        }

        private bool specialCall(string call)
        {
            
            if (call.StartsWith("0"))
            {
                return true;
            }
            if (call.StartsWith("Q"))
            {
                return true;
            }
          
            if (call.StartsWith("1"))
            {
                return true;
            }
            if (call.Length >1)
            {
                if (char.IsDigit(call[0]) && char.IsDigit(call[1]))
                {
                    return true;
                }
            }

            return false;

        }


        private async Task<bool> find_reportedRX(int tablecount) //find a slot row for display in grid from the database corresponding to the date/time from the slot
        {   //find own call and locator from database

            DataTable Slots = new DataTable();
            //DateTime d = new DateTime();
            int i = 0;
            bool found = false;
            string myConnectionString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=wspr_rpt";


            try
            {
                MySqlConnection connection = new MySqlConnection(myConnectionString);

                connection.Open();

                MySqlCommand command = connection.CreateCommand();


                command.CommandText = "SELECT reporter, reporter_loc FROM received ORDER BY datetime DESC";
                MySqlDataReader Reader;
                Reader = command.ExecuteReader();

                while (Reader.Read() && !found)
                {

                    if (i < tablecount)    //only show first maxrows rows, or to length of reported table
                    {

                        if (locator == "")
                        {
                            locator = (string)Reader["reporter_loc"];
                        }
                        if (call == "")
                        {
                            call = (string)Reader["reporter"];
                        }

                        if (locator != "" && call != "")
                        {
                            found = true;
                        }

                    }
                    else
                    {
                        break;
                    }
                    i++;

                }
                Reader.Close();
                connection.Close();

            }
            catch
            {
                //databaseError = true; //stop wasting time trying to connect if database error - ignore for present
                found = false;
                MessageBox.Show("Error connecting to database");
            }
            return found;
        }


        private async Task find_selectedTX(string time1, string time2, int band, int tablecount) //find a slot row for display in grid from the database corresponding to the date/time from the slot
        { //reports from other stations about own txns
            //gmap.Zoom = 3;
            double rxlat = 0;
            double rxlon = 0;
            int maxrows = 1000;
            DataTable Slots = new DataTable();
            //DateTime d = new DateTime();
            int i = 0;
            bool found = false;
            string myConnectionString = "server=" + server + ";user id=" + user + ";password=" + pass + ";database=wspr_rx";

            string bandstr = "";
            string q = "";
            if (band == -2) //all bands
            {
                bandstr = "-1";
                q = ">";
            }
            else
            {
                bandstr = band.ToString();
                q = "=";
            }
            string fromstr = "";
            if (clutterlistBox.SelectedIndex > 0)
            {
                fromstr = clutterlistBox.SelectedItem.ToString();
                if (!kmcheckBox.Checked)    //miles
                {
                    double km = Convert.ToDouble(fromstr);
                    km = km * 1.60934;
                    fromstr = km.ToString("F1");
                }
            }

            try
            {
                MySqlConnection connection = new MySqlConnection(myConnectionString);

                connection.Open();

                MySqlCommand command = connection.CreateCommand();


                command.CommandText = "SELECT * FROM reported WHERE time >= '" + time1 + "' AND time <= '" + time2 + "' AND band " + q + " '" + bandstr + "' AND distance >= '" + fromstr + "' ORDER BY time DESC";




                MySqlDataReader Reader;
                Reader = command.ExecuteReader();
                string bandS = "";
                int index = bandlistBox.SelectedIndex;
                while (Reader.Read())
                {
                    found = true;

                    if (i < tablecount)   //only show first maxrows rows, or to length of reported table
                    {

                        RX.time = (DateTime)Reader["time"];
                        RX.band = (Int16)Reader["band"];
                        RX.rx_sign = (string)Reader["rx_sign"];
                        RX.rx_loc = (string)Reader["rx_loc"];
                        RX.tx_sign = (string)Reader["tx_sign"];
                        RX.tx_loc = (string)Reader["tx_loc"];
                        RX.distance = (int)Reader["distance"];
                        RX.azimuth = (int)Reader["azimuth"];
                        RX.frequency = (int)Reader["frequency"];
                        RX.power = (Int16)Reader["power"];
                        RX.snr = (Int16)Reader["snr"];
                        RX.drift = (Int16)Reader["drift"];
                        RX.version = (string)Reader["version"];

                       
                        LatLng latlong = MaidenheadLocator.LocatorToLatLng(RX.rx_loc);

                        rxlat = latlong.Lat;
                        rxlon = latlong.Long;

                        GMarkerGoogleType pin = GMarkerGoogleType.red_small;
                        bandS = "";
                        if (index == 0) //all bands 
                        {
                            bandS = get_reverse_band(RX.band);
                            bandS = " (" + bandS + ")";
                        }
                        await addMarker(rxlat, rxlon, pin, RX.rx_sign+bandS);
                        if (pathcheckBox.Checked)
                        {
                            await addPath(mylat, mylon, rxlat, rxlon, Color.Purple);
                        }

                        i++;
                    }
                    else
                    {
                        break;
                    }

                }
                Reader.Close();
                connection.Close();


            }
            catch
            {

                found = false;

            }
        }

        private void configbutton_Click(object sender, EventArgs e)
        {


            locatortextBox.Text = locator;
            calltextBox.Text = call;

            groupBox1.Visible = true;

        }

        private void pathcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!pathcheckBox.Checked)
            {
                routes.Routes.Clear();
                //gmap.Overlays.Add(routes);
            }
        }

        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            pinlabel.Text = "";
            //Zoomlabel.Text = gmap.Zoom.ToString("F1");
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            saveUserandPassword("receiver", passtextBox.Text.Trim());
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void kmcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (kmcheckBox.Checked)
            {
                mlslabel.Text = "km";
                clutterlistBox.Items.Clear();
                clutterlistBox.Items.Add("0");
                clutterlistBox.Items.Add("80");
                clutterlistBox.Items.Add("160");
                clutterlistBox.Items.Add("320");
                clutterlistBox.Items.Add("640");
                clutterlistBox.Items.Add("960");
                clutterlistBox.Items.Add("1300");
                clutterlistBox.Items.Add("1600");
                clutterlistBox.Items.Add("1900");
                clutterlistBox.Items.Add("2400");
                clutterlistBox.Items.Add("2900");

                clutterlistBox.SelectedIndex = 0; //default to 0km

            }
            else
            {
                clutterlistBox.Items.Clear();
                clutterlistBox.Items.Add("0");
                clutterlistBox.Items.Add("50");
                clutterlistBox.Items.Add("100");
                clutterlistBox.Items.Add("200");
                clutterlistBox.Items.Add("400");
                clutterlistBox.Items.Add("600");
                clutterlistBox.Items.Add("800");
                clutterlistBox.Items.Add("1000");
                clutterlistBox.Items.Add("1200");
                clutterlistBox.Items.Add("1500");
                clutterlistBox.Items.Add("1800");
                mlslabel.Text = "mls";
                clutterlistBox.SelectedIndex = 0; //default to 0mls
            }
        }

        private void gmap_MouseMove(object sender, MouseEventArgs e)
        {
            Zoomlabel.Text = gmap.Zoom.ToString("F1");
        }

        private void recentrebutton_Click(object sender, EventArgs e)
        {
            recentre();
        }
        private void recentre()
        {
            gmap.Position = new PointLatLng(30, 0); // equator at 0 degrees
            gmap.Zoom = 2;
        }

        private void showcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (showcheckBox.Checked)
            {
                passtextBox.PasswordChar = '\0'; //show password
            }
            else
            {
                passtextBox.PasswordChar = '*'; //hide password
            }
        }

        private bool saveUserandPassword(string user, string password)
        {
            string key = "wsproundtheworld";
            Encryption enc = new Encryption();
            string encryptedpassword = enc.Encrypt(password, key);

            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filepath = homeDirectory;
            string content = "db_user: " + user + " db_pass: " + encryptedpassword;
            if (Path.Exists(filepath))
            {
                string slash = "\\";
                if (filepath.EndsWith("\\"))
                {
                    slash = "";
                }
                filepath = filepath + slash + "DBmapcredential";
                try
                {
                    using (StreamWriter writer = new StreamWriter(filepath, false))
                    {
                        writer.WriteLine(content);
                        writer.Close();
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        private bool getUserandPassword()
        {
            string key = "wsproundtheworld";
            Encryption enc = new Encryption();
            string encryptedpassword;
            string content = "";
            bool ok = false;

            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filepath = homeDirectory;
            //string content = "db_user: " + user + " db_pass: " + passwordhash;

            if (Path.Exists(filepath))
            {
                string slash = "\\";
                if (filepath.EndsWith("\\"))
                {
                    slash = "";
                }
                filepath = filepath + slash + "DBmapcredential";
                if (File.Exists(filepath))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(filepath))
                        {
                            content = reader.ReadLine();
                            reader.Close();
                        }
                        if (content != null || content != "")
                        {
                            if (content.Contains("db_pass:"))
                            {
                                encryptedpassword = content.Substring(content.IndexOf("db_pass: ") + "db_pass: ".Length);
                                string password = enc.Decrypt(encryptedpassword, key);
                                if (password.Length > 0 && password != null)
                                {
                                    pass = password;
                                    passtextBox.Text = password;

                                    ok = true;
                                }
                            }
                        }

                        if (!ok)
                        {
                            MessageBox.Show("Unable to read database credentials", "");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to read database credentials", "");
                        return false;
                    }
                }
            }


            return ok;
        }

        private void autocheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (autocheckBox.Checked)
            {
                periodlistBox.SelectedIndex = 3;  //60 mins default
                //bandlistBox.SelectedIndex = 0;
                //clutterlistBox.SelectedIndex = 1;
                pathcheckBox.Checked = true;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
           filter_results();
            recentre();
        }

        private void gmap_Load(object sender, EventArgs e)
        {
        }

       

    
    }
}

