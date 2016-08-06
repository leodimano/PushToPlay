namespace PushToPlay.WinForm
{
    partial class PushToPlayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.txtAuthCode = new System.Windows.Forms.TextBox();
            this.timerRunCallBack = new System.Windows.Forms.Timer(this.components);
            this.timerCheckForUpdate = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtPlatform = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSteamId = new System.Windows.Forms.TextBox();
            this.btnImportSteamPlayer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTwitchGameStream = new System.Windows.Forms.TextBox();
            this.btnTwitchChannel = new System.Windows.Forms.Button();
            this.btnStreamByGame = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTwitchChannel = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnChampionbyId = new System.Windows.Forms.Button();
            this.txtLoLChampionId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnSummonerByName = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLoLSummonerName = new System.Windows.Forms.TextBox();
            this.txtLoLSummonerID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLoLRegion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoLChampionList = new System.Windows.Forms.Button();
            this.btnLoLItemList = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLoLItemId = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Initialize SteamKit2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtAuthCode
            // 
            this.txtAuthCode.Location = new System.Drawing.Point(140, 30);
            this.txtAuthCode.Name = "txtAuthCode";
            this.txtAuthCode.Size = new System.Drawing.Size(108, 20);
            this.txtAuthCode.TabIndex = 1;
            // 
            // timerRunCallBack
            // 
            this.timerRunCallBack.Interval = 1000;
            this.timerRunCallBack.Tick += new System.EventHandler(this.timerRunCallBack_Tick);
            // 
            // timerCheckForUpdate
            // 
            this.timerCheckForUpdate.Interval = 5000;
            this.timerCheckForUpdate.Tick += new System.EventHandler(this.timerCheckForUpdate_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(155, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Import GiantBomb";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(283, 35);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Import Steam";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtPlatform
            // 
            this.txtPlatform.Location = new System.Drawing.Point(12, 38);
            this.txtPlatform.Name = "txtPlatform";
            this.txtPlatform.Size = new System.Drawing.Size(100, 20);
            this.txtPlatform.TabIndex = 4;
            this.txtPlatform.TextChanged += new System.EventHandler(this.txtPlatform_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Platform Id ( separate by \"|\" )";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Player SteamID";
            // 
            // txtSteamId
            // 
            this.txtSteamId.Location = new System.Drawing.Point(12, 37);
            this.txtSteamId.Name = "txtSteamId";
            this.txtSteamId.Size = new System.Drawing.Size(100, 20);
            this.txtSteamId.TabIndex = 1;
            // 
            // btnImportSteamPlayer
            // 
            this.btnImportSteamPlayer.Location = new System.Drawing.Point(283, 34);
            this.btnImportSteamPlayer.Name = "btnImportSteamPlayer";
            this.btnImportSteamPlayer.Size = new System.Drawing.Size(120, 23);
            this.btnImportSteamPlayer.TabIndex = 2;
            this.btnImportSteamPlayer.Text = "Import";
            this.btnImportSteamPlayer.UseVisualStyleBackColor = true;
            this.btnImportSteamPlayer.Click += new System.EventHandler(this.btnImportSteamPlayer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtAuthCode);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 65);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Steam Authentication";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Auth Code";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.txtPlatform);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 83);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 72);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game Import";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnImportSteamPlayer);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtSteamId);
            this.groupBox3.Location = new System.Drawing.Point(12, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 71);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Steam Player Import";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtTwitchGameStream);
            this.groupBox4.Controls.Add(this.btnTwitchChannel);
            this.groupBox4.Controls.Add(this.btnStreamByGame);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtTwitchChannel);
            this.groupBox4.Location = new System.Drawing.Point(12, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 100);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "TwitchTv";
            // 
            // txtTwitchGameStream
            // 
            this.txtTwitchGameStream.Location = new System.Drawing.Point(6, 70);
            this.txtTwitchGameStream.Name = "txtTwitchGameStream";
            this.txtTwitchGameStream.Size = new System.Drawing.Size(269, 20);
            this.txtTwitchGameStream.TabIndex = 4;
            // 
            // btnTwitchChannel
            // 
            this.btnTwitchChannel.Location = new System.Drawing.Point(281, 39);
            this.btnTwitchChannel.Name = "btnTwitchChannel";
            this.btnTwitchChannel.Size = new System.Drawing.Size(141, 23);
            this.btnTwitchChannel.TabIndex = 3;
            this.btnTwitchChannel.Text = "Get Channel";
            this.btnTwitchChannel.UseVisualStyleBackColor = true;
            this.btnTwitchChannel.Click += new System.EventHandler(this.btnTwitchChannel_Click);
            // 
            // btnStreamByGame
            // 
            this.btnStreamByGame.Location = new System.Drawing.Point(281, 68);
            this.btnStreamByGame.Name = "btnStreamByGame";
            this.btnStreamByGame.Size = new System.Drawing.Size(141, 23);
            this.btnStreamByGame.TabIndex = 2;
            this.btnStreamByGame.Text = "Stream by Game";
            this.btnStreamByGame.UseVisualStyleBackColor = true;
            this.btnStreamByGame.Click += new System.EventHandler(this.btnStreamByGame_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Parameter";
            // 
            // txtTwitchChannel
            // 
            this.txtTwitchChannel.Location = new System.Drawing.Point(6, 39);
            this.txtTwitchChannel.Name = "txtTwitchChannel";
            this.txtTwitchChannel.Size = new System.Drawing.Size(269, 20);
            this.txtTwitchChannel.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button6);
            this.groupBox5.Controls.Add(this.txtLoLItemId);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.btnLoLItemList);
            this.groupBox5.Controls.Add(this.btnLoLChampionList);
            this.groupBox5.Controls.Add(this.btnChampionbyId);
            this.groupBox5.Controls.Add(this.txtLoLChampionId);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.button5);
            this.groupBox5.Controls.Add(this.button4);
            this.groupBox5.Controls.Add(this.btnSummonerByName);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtLoLSummonerName);
            this.groupBox5.Controls.Add(this.txtLoLSummonerID);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtLoLRegion);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Location = new System.Drawing.Point(12, 344);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(428, 241);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "LoL";
            // 
            // btnChampionbyId
            // 
            this.btnChampionbyId.Location = new System.Drawing.Point(6, 181);
            this.btnChampionbyId.Name = "btnChampionbyId";
            this.btnChampionbyId.Size = new System.Drawing.Size(114, 23);
            this.btnChampionbyId.TabIndex = 11;
            this.btnChampionbyId.Text = "Champion by Id";
            this.btnChampionbyId.UseVisualStyleBackColor = true;
            this.btnChampionbyId.Click += new System.EventHandler(this.btnChampionbyId_Click);
            // 
            // txtLoLChampionId
            // 
            this.txtLoLChampionId.Location = new System.Drawing.Point(100, 100);
            this.txtLoLChampionId.Name = "txtLoLChampionId";
            this.txtLoLChampionId.Size = new System.Drawing.Size(175, 20);
            this.txtLoLChampionId.TabIndex = 10;
            this.txtLoLChampionId.Text = "81";
            this.txtLoLChampionId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Champion ID";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(246, 152);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Recent By ID";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(126, 152);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Stats by ID";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btnSummonerByName
            // 
            this.btnSummonerByName.Location = new System.Drawing.Point(6, 152);
            this.btnSummonerByName.Name = "btnSummonerByName";
            this.btnSummonerByName.Size = new System.Drawing.Size(114, 23);
            this.btnSummonerByName.TabIndex = 6;
            this.btnSummonerByName.Text = "Summoner by Name";
            this.btnSummonerByName.UseVisualStyleBackColor = true;
            this.btnSummonerByName.Click += new System.EventHandler(this.btnSummonerByName_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "SummonerName";
            // 
            // txtLoLSummonerName
            // 
            this.txtLoLSummonerName.Location = new System.Drawing.Point(100, 74);
            this.txtLoLSummonerName.Name = "txtLoLSummonerName";
            this.txtLoLSummonerName.Size = new System.Drawing.Size(175, 20);
            this.txtLoLSummonerName.TabIndex = 4;
            this.txtLoLSummonerName.Text = "leodimano";
            this.txtLoLSummonerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtLoLSummonerID
            // 
            this.txtLoLSummonerID.Location = new System.Drawing.Point(100, 48);
            this.txtLoLSummonerID.Name = "txtLoLSummonerID";
            this.txtLoLSummonerID.Size = new System.Drawing.Size(175, 20);
            this.txtLoLSummonerID.TabIndex = 3;
            this.txtLoLSummonerID.Text = "405399";
            this.txtLoLSummonerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "SummonerID";
            // 
            // txtLoLRegion
            // 
            this.txtLoLRegion.Location = new System.Drawing.Point(100, 22);
            this.txtLoLRegion.Name = "txtLoLRegion";
            this.txtLoLRegion.Size = new System.Drawing.Size(56, 20);
            this.txtLoLRegion.TabIndex = 1;
            this.txtLoLRegion.Text = "br";
            this.txtLoLRegion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Region";
            // 
            // btnLoLChampionList
            // 
            this.btnLoLChampionList.Location = new System.Drawing.Point(126, 181);
            this.btnLoLChampionList.Name = "btnLoLChampionList";
            this.btnLoLChampionList.Size = new System.Drawing.Size(114, 23);
            this.btnLoLChampionList.TabIndex = 12;
            this.btnLoLChampionList.Text = "Champion List";
            this.btnLoLChampionList.UseVisualStyleBackColor = true;
            this.btnLoLChampionList.Click += new System.EventHandler(this.btnLoLChampionList_Click);
            // 
            // btnLoLItemList
            // 
            this.btnLoLItemList.Location = new System.Drawing.Point(246, 181);
            this.btnLoLItemList.Name = "btnLoLItemList";
            this.btnLoLItemList.Size = new System.Drawing.Size(114, 23);
            this.btnLoLItemList.TabIndex = 13;
            this.btnLoLItemList.Text = "Item List";
            this.btnLoLItemList.UseVisualStyleBackColor = true;
            this.btnLoLItemList.Click += new System.EventHandler(this.btnLoLItemList_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Item ID";
            // 
            // txtLoLItemId
            // 
            this.txtLoLItemId.Location = new System.Drawing.Point(100, 126);
            this.txtLoLItemId.Name = "txtLoLItemId";
            this.txtLoLItemId.Size = new System.Drawing.Size(175, 20);
            this.txtLoLItemId.TabIndex = 15;
            this.txtLoLItemId.Text = "3009";
            this.txtLoLItemId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(6, 210);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(114, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "Item ID";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // PushToPlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 593);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PushToPlayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PushToPlay Engine";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtAuthCode;
        private System.Windows.Forms.Timer timerRunCallBack;
        private System.Windows.Forms.Timer timerCheckForUpdate;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtPlatform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSteamId;
        private System.Windows.Forms.Button btnImportSteamPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtTwitchGameStream;
        private System.Windows.Forms.Button btnTwitchChannel;
        private System.Windows.Forms.Button btnStreamByGame;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTwitchChannel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnSummonerByName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLoLSummonerName;
        private System.Windows.Forms.TextBox txtLoLSummonerID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLoLRegion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button btnChampionbyId;
        private System.Windows.Forms.TextBox txtLoLChampionId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnLoLChampionList;
        private System.Windows.Forms.Button btnLoLItemList;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox txtLoLItemId;
        private System.Windows.Forms.Label label9;
    }
}

