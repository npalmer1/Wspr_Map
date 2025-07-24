namespace Wspr_Map
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gmap = new GMap.NET.WindowsForms.GMapControl();
            pinlabel = new Label();
            periodlistBox = new ListBox();
            bandlistBox = new ListBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            filterbutton = new Button();
            configbutton = new Button();
            pathcheckBox = new CheckBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            groupBox1 = new GroupBox();
            showcheckBox = new CheckBox();
            cancelbutton = new Button();
            savebutton = new Button();
            label1 = new Label();
            locatortextBox = new TextBox();
            label5 = new Label();
            calltextBox = new TextBox();
            passtextBox = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            clutterlistBox = new ListBox();
            mlslabel = new Label();
            kmcheckBox = new CheckBox();
            label9 = new Label();
            Zoomlabel = new Label();
            recentrebutton = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // gmap
            // 
            gmap.Bearing = 0F;
            gmap.CanDragMap = true;
            gmap.EmptyTileColor = Color.Navy;
            gmap.GrayScaleMode = false;
            gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            gmap.LevelsKeepInMemory = 5;
            gmap.Location = new Point(2, 2);
            gmap.MarkersEnabled = true;
            gmap.MaxZoom = 18;
            gmap.MinZoom = 2;
            gmap.MouseWheelZoomEnabled = true;
            gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gmap.Name = "gmap";
            gmap.NegativeMode = false;
            gmap.PolygonsEnabled = true;
            gmap.RetryLoadTile = 0;
            gmap.RoutesEnabled = true;
            gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            gmap.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
            gmap.ShowTileGridLines = false;
            gmap.Size = new Size(1092, 730);
            gmap.TabIndex = 0;
            gmap.Zoom = 13D;
            gmap.OnMarkerClick += gmap_OnMarkerClick;
            gmap.MouseClick += gmap_MouseClick;
            gmap.MouseMove += gmap_MouseMove;
            // 
            // pinlabel
            // 
            pinlabel.AutoSize = true;
            pinlabel.Location = new Point(1145, 660);
            pinlabel.Name = "pinlabel";
            pinlabel.Size = new Size(22, 15);
            pinlabel.TabIndex = 1;
            pinlabel.Text = "---";
            // 
            // periodlistBox
            // 
            periodlistBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            periodlistBox.FormattingEnabled = true;
            periodlistBox.ItemHeight = 13;
            periodlistBox.Items.AddRange(new object[] { "10 mins", "20 mins", "30 mins", "1 hour", "2 hours", "3 hours", "6 hours", "12 hours", "24 hours", "2 days" });
            periodlistBox.Location = new Point(1100, 55);
            periodlistBox.Name = "periodlistBox";
            periodlistBox.Size = new Size(52, 134);
            periodlistBox.TabIndex = 2;
            // 
            // bandlistBox
            // 
            bandlistBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bandlistBox.FormattingEnabled = true;
            bandlistBox.ItemHeight = 13;
            bandlistBox.Items.AddRange(new object[] { "All", "LF", "MF", "160m", "80m", "60m", "40m", "30m", "20m", "17m", "15m", "12m", "10m", "6m", "4m", "2m", "70cm", "23cm" });
            bandlistBox.Location = new Point(1172, 55);
            bandlistBox.Name = "bandlistBox";
            bandlistBox.Size = new Size(45, 238);
            bandlistBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(1099, 39);
            label2.Name = "label2";
            label2.Size = new Size(40, 13);
            label2.TabIndex = 4;
            label2.Text = "Period";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(1172, 39);
            label3.Name = "label3";
            label3.Size = new Size(33, 13);
            label3.TabIndex = 5;
            label3.Text = "Band";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1103, 487);
            label4.Name = "label4";
            label4.Size = new Size(75, 45);
            label4.TabIndex = 6;
            label4.Text = "Red - my TX\r\nBlue - my RX\r\nYellow - QTH";
            // 
            // filterbutton
            // 
            filterbutton.Location = new Point(1136, 11);
            filterbutton.Name = "filterbutton";
            filterbutton.Size = new Size(52, 23);
            filterbutton.TabIndex = 7;
            filterbutton.Text = "Apply";
            filterbutton.UseVisualStyleBackColor = true;
            filterbutton.Click += filterbutton_Click;
            // 
            // configbutton
            // 
            configbutton.Location = new Point(1126, 687);
            configbutton.Name = "configbutton";
            configbutton.Size = new Size(62, 23);
            configbutton.TabIndex = 8;
            configbutton.Text = "Config";
            configbutton.UseVisualStyleBackColor = true;
            configbutton.Click += configbutton_Click;
            // 
            // pathcheckBox
            // 
            pathcheckBox.AutoSize = true;
            pathcheckBox.Font = new Font("Segoe UI", 8.25F);
            pathcheckBox.Location = new Point(1100, 204);
            pathcheckBox.Name = "pathcheckBox";
            pathcheckBox.Size = new Size(55, 17);
            pathcheckBox.TabIndex = 9;
            pathcheckBox.Text = "paths";
            pathcheckBox.UseVisualStyleBackColor = true;
            pathcheckBox.CheckedChanged += pathcheckBox_CheckedChanged;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Font = new Font("Segoe UI", 8.25F);
            radioButton1.Location = new Point(1100, 229);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(64, 17);
            radioButton1.TabIndex = 10;
            radioButton1.TabStop = true;
            radioButton1.Text = "TX + RX";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new Font("Segoe UI", 8.25F);
            radioButton2.Location = new Point(1100, 254);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(62, 17);
            radioButton2.TabIndex = 11;
            radioButton2.TabStop = true;
            radioButton2.Text = "TX only";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Font = new Font("Segoe UI", 8.25F);
            radioButton3.Location = new Point(1099, 279);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(63, 17);
            radioButton3.TabIndex = 12;
            radioButton3.TabStop = true;
            radioButton3.Text = "RX only";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Info;
            groupBox1.Controls.Add(showcheckBox);
            groupBox1.Controls.Add(cancelbutton);
            groupBox1.Controls.Add(savebutton);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(locatortextBox);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(calltextBox);
            groupBox1.Controls.Add(passtextBox);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label7);
            groupBox1.FlatStyle = FlatStyle.Popup;
            groupBox1.Location = new Point(266, 128);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(544, 296);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            groupBox1.Visible = false;
            // 
            // showcheckBox
            // 
            showcheckBox.AutoSize = true;
            showcheckBox.Location = new Point(372, 158);
            showcheckBox.Name = "showcheckBox";
            showcheckBox.Size = new Size(108, 19);
            showcheckBox.TabIndex = 18;
            showcheckBox.Text = "Show password";
            showcheckBox.UseVisualStyleBackColor = true;
            showcheckBox.CheckedChanged += showcheckBox_CheckedChanged;
            // 
            // cancelbutton
            // 
            cancelbutton.Location = new Point(372, 201);
            cancelbutton.Name = "cancelbutton";
            cancelbutton.Size = new Size(75, 23);
            cancelbutton.TabIndex = 17;
            cancelbutton.Text = "Cancel";
            cancelbutton.UseVisualStyleBackColor = true;
            cancelbutton.Click += cancelbutton_Click;
            // 
            // savebutton
            // 
            savebutton.Location = new Point(240, 201);
            savebutton.Name = "savebutton";
            savebutton.Size = new Size(75, 23);
            savebutton.TabIndex = 16;
            savebutton.Text = "Save";
            savebutton.UseVisualStyleBackColor = true;
            savebutton.Click += savebutton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(119, 116);
            label1.Name = "label1";
            label1.Size = new Size(87, 15);
            label1.TabIndex = 15;
            label1.Text = "Station locator:";
            // 
            // locatortextBox
            // 
            locatortextBox.Location = new Point(215, 113);
            locatortextBox.Name = "locatortextBox";
            locatortextBox.ReadOnly = true;
            locatortextBox.Size = new Size(82, 23);
            locatortextBox.TabIndex = 14;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(119, 75);
            label5.Name = "label5";
            label5.Size = new Size(90, 15);
            label5.TabIndex = 13;
            label5.Text = "Station callsign:";
            // 
            // calltextBox
            // 
            calltextBox.Location = new Point(215, 72);
            calltextBox.Name = "calltextBox";
            calltextBox.ReadOnly = true;
            calltextBox.Size = new Size(100, 23);
            calltextBox.TabIndex = 12;
            // 
            // passtextBox
            // 
            passtextBox.Location = new Point(215, 156);
            passtextBox.Name = "passtextBox";
            passtextBox.PasswordChar = '*';
            passtextBox.Size = new Size(132, 23);
            passtextBox.TabIndex = 11;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(105, 201);
            label6.Name = "label6";
            label6.Size = new Size(0, 15);
            label6.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(98, 159);
            label7.Name = "label7";
            label7.Size = new Size(111, 15);
            label7.TabIndex = 9;
            label7.Text = "Database password:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(1100, 311);
            label8.Name = "label8";
            label8.Size = new Size(58, 13);
            label8.TabIndex = 14;
            label8.Text = "De-clutter";
            // 
            // clutterlistBox
            // 
            clutterlistBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            clutterlistBox.FormattingEnabled = true;
            clutterlistBox.ItemHeight = 13;
            clutterlistBox.Items.AddRange(new object[] { "0", "50", "100", "200", "400", "600", "800", "1000", "1200", "1500", "1800" });
            clutterlistBox.Location = new Point(1103, 329);
            clutterlistBox.Name = "clutterlistBox";
            clutterlistBox.Size = new Size(34, 147);
            clutterlistBox.TabIndex = 15;
            // 
            // mlslabel
            // 
            mlslabel.AutoSize = true;
            mlslabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            mlslabel.Location = new Point(1143, 333);
            mlslabel.Name = "mlslabel";
            mlslabel.Size = new Size(24, 13);
            mlslabel.TabIndex = 16;
            mlslabel.Text = "mls";
            // 
            // kmcheckBox
            // 
            kmcheckBox.AutoSize = true;
            kmcheckBox.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            kmcheckBox.Location = new Point(1148, 358);
            kmcheckBox.Name = "kmcheckBox";
            kmcheckBox.Size = new Size(62, 17);
            kmcheckBox.TabIndex = 17;
            kmcheckBox.Text = "use km";
            kmcheckBox.UseVisualStyleBackColor = true;
            kmcheckBox.CheckedChanged += kmcheckBox_CheckedChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(1103, 552);
            label9.Name = "label9";
            label9.Size = new Size(42, 13);
            label9.TabIndex = 18;
            label9.Text = "Zoom: ";
            // 
            // Zoomlabel
            // 
            Zoomlabel.AutoSize = true;
            Zoomlabel.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Zoomlabel.Location = new Point(1136, 552);
            Zoomlabel.Name = "Zoomlabel";
            Zoomlabel.Size = new Size(44, 13);
            Zoomlabel.TabIndex = 19;
            Zoomlabel.Text = "label10";
            // 
            // recentrebutton
            // 
            recentrebutton.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            recentrebutton.Location = new Point(1121, 604);
            recentrebutton.Name = "recentrebutton";
            recentrebutton.Size = new Size(67, 23);
            recentrebutton.TabIndex = 20;
            recentrebutton.Text = "Recentre";
            recentrebutton.UseVisualStyleBackColor = true;
            recentrebutton.Click += recentrebutton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1223, 731);
            Controls.Add(recentrebutton);
            Controls.Add(Zoomlabel);
            Controls.Add(label9);
            Controls.Add(kmcheckBox);
            Controls.Add(mlslabel);
            Controls.Add(clutterlistBox);
            Controls.Add(label8);
            Controls.Add(groupBox1);
            Controls.Add(radioButton3);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(pathcheckBox);
            Controls.Add(configbutton);
            Controls.Add(filterbutton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(bandlistBox);
            Controls.Add(periodlistBox);
            Controls.Add(pinlabel);
            Controls.Add(gmap);
            Name = "Form1";
            Text = "Reports for this station";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private Label pinlabel;
        private ListBox periodlistBox;
        private ListBox bandlistBox;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button filterbutton;
        private Button configbutton;
        private CheckBox pathcheckBox;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private GroupBox groupBox1;
        private Button cancelbutton;
        private Button savebutton;
        private Label label1;
        private TextBox locatortextBox;
        private Label label5;
        private TextBox calltextBox;
        private TextBox passtextBox;
        private Label label6;
        private Label label7;
        private Label label8;
        private ListBox clutterlistBox;
        private Label mlslabel;
        private CheckBox kmcheckBox;
        private Label label9;
        private Label Zoomlabel;
        private Button recentrebutton;
        private CheckBox showcheckBox;
    }
}
