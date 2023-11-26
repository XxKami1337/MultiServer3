﻿namespace GraphicalUserInterface
{
    partial class MainForm
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
            richTextBoxLog = new RichTextBox();
            groupBoxWebServerManagement = new GroupBox();
            labelAdministratorRequired = new Label();
            buttonStopSVO = new Button();
            buttonStopSSFW = new Button();
            buttonStopHTTP = new Button();
            buttonStopHTTPS = new Button();
            buttonStartHTTPS = new Button();
            buttonStartSVO = new Button();
            buttonStartSSFW = new Button();
            buttonStartHTTP = new Button();
            buttonStartDNS = new Button();
            groupBoxAuxiliaryServerManagement = new GroupBox();
            buttonStopDNS = new Button();
            buttonStopHorizon = new Button();
            buttonStartHorizon = new Button();
            pictureBoxPSMSImage = new PictureBox();
            groupBoxWebServerManagement.SuspendLayout();
            groupBoxAuxiliaryServerManagement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPSMSImage).BeginInit();
            SuspendLayout();
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.Location = new Point(-2, 758);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.Size = new Size(1009, 225);
            richTextBoxLog.TabIndex = 0;
            richTextBoxLog.Text = "";
            // 
            // groupBoxWebServerManagement
            // 
            groupBoxWebServerManagement.Controls.Add(labelAdministratorRequired);
            groupBoxWebServerManagement.Controls.Add(buttonStopSVO);
            groupBoxWebServerManagement.Controls.Add(buttonStopSSFW);
            groupBoxWebServerManagement.Controls.Add(buttonStopHTTP);
            groupBoxWebServerManagement.Controls.Add(buttonStopHTTPS);
            groupBoxWebServerManagement.Controls.Add(buttonStartHTTPS);
            groupBoxWebServerManagement.Controls.Add(buttonStartSVO);
            groupBoxWebServerManagement.Controls.Add(buttonStartSSFW);
            groupBoxWebServerManagement.Controls.Add(buttonStartHTTP);
            groupBoxWebServerManagement.Location = new Point(12, 494);
            groupBoxWebServerManagement.Name = "groupBoxWebServerManagement";
            groupBoxWebServerManagement.Size = new Size(488, 234);
            groupBoxWebServerManagement.TabIndex = 1;
            groupBoxWebServerManagement.TabStop = false;
            groupBoxWebServerManagement.Text = "Web Server Management";
            // 
            // labelAdministratorRequired
            // 
            labelAdministratorRequired.AutoSize = true;
            labelAdministratorRequired.Location = new Point(269, 185);
            labelAdministratorRequired.Name = "labelAdministratorRequired";
            labelAdministratorRequired.Size = new Size(213, 20);
            labelAdministratorRequired.TabIndex = 7;
            labelAdministratorRequired.Text = "- Admin Required on Windows";
            // 
            // buttonStopSVO
            // 
            buttonStopSVO.Location = new Point(166, 181);
            buttonStopSVO.Name = "buttonStopSVO";
            buttonStopSVO.Size = new Size(106, 29);
            buttonStopSVO.TabIndex = 6;
            buttonStopSVO.Text = "Stop SVO";
            buttonStopSVO.UseVisualStyleBackColor = true;
            buttonStopSVO.Click += buttonStopSVO_Click;
            // 
            // buttonStopSSFW
            // 
            buttonStopSSFW.Location = new Point(166, 135);
            buttonStopSSFW.Name = "buttonStopSSFW";
            buttonStopSSFW.Size = new Size(106, 29);
            buttonStopSSFW.TabIndex = 5;
            buttonStopSSFW.Text = "Stop SSFW";
            buttonStopSSFW.UseVisualStyleBackColor = true;
            buttonStopSSFW.Click += buttonStopSSFW_Click;
            // 
            // buttonStopHTTP
            // 
            buttonStopHTTP.Location = new Point(166, 88);
            buttonStopHTTP.Name = "buttonStopHTTP";
            buttonStopHTTP.Size = new Size(106, 29);
            buttonStopHTTP.TabIndex = 4;
            buttonStopHTTP.Text = "Stop HTTP";
            buttonStopHTTP.UseVisualStyleBackColor = true;
            buttonStopHTTP.Click += buttonStopHTTP_Click;
            // 
            // buttonStopHTTPS
            // 
            buttonStopHTTPS.Location = new Point(166, 41);
            buttonStopHTTPS.Name = "buttonStopHTTPS";
            buttonStopHTTPS.Size = new Size(106, 29);
            buttonStopHTTPS.TabIndex = 3;
            buttonStopHTTPS.Text = "Stop HTTPS";
            buttonStopHTTPS.UseVisualStyleBackColor = true;
            buttonStopHTTPS.Click += buttonStopHTTPS_Click;
            // 
            // buttonStartHTTPS
            // 
            buttonStartHTTPS.Location = new Point(33, 41);
            buttonStartHTTPS.Name = "buttonStartHTTPS";
            buttonStartHTTPS.Size = new Size(114, 29);
            buttonStartHTTPS.TabIndex = 2;
            buttonStartHTTPS.Text = "Start HTTPS";
            buttonStartHTTPS.UseVisualStyleBackColor = true;
            buttonStartHTTPS.Click += buttonStartHTTPS_Click;
            // 
            // buttonStartSVO
            // 
            buttonStartSVO.Location = new Point(33, 181);
            buttonStartSVO.Name = "buttonStartSVO";
            buttonStartSVO.Size = new Size(114, 29);
            buttonStartSVO.TabIndex = 0;
            buttonStartSVO.Text = "Start SVO";
            buttonStartSVO.UseVisualStyleBackColor = true;
            buttonStartSVO.Click += buttonStartSVO_Click;
            // 
            // buttonStartSSFW
            // 
            buttonStartSSFW.Location = new Point(33, 135);
            buttonStartSSFW.Name = "buttonStartSSFW";
            buttonStartSSFW.Size = new Size(114, 29);
            buttonStartSSFW.TabIndex = 1;
            buttonStartSSFW.Text = "Start SSFW";
            buttonStartSSFW.UseVisualStyleBackColor = true;
            buttonStartSSFW.Click += buttonStartSSFW_Click;
            // 
            // buttonStartHTTP
            // 
            buttonStartHTTP.Location = new Point(33, 88);
            buttonStartHTTP.Name = "buttonStartHTTP";
            buttonStartHTTP.Size = new Size(114, 29);
            buttonStartHTTP.TabIndex = 1;
            buttonStartHTTP.Text = "Start HTTP";
            buttonStartHTTP.UseVisualStyleBackColor = true;
            buttonStartHTTP.Click += buttonStartHTTP_Click;
            // 
            // buttonStartDNS
            // 
            buttonStartDNS.Location = new Point(33, 88);
            buttonStartDNS.Name = "buttonStartDNS";
            buttonStartDNS.Size = new Size(114, 29);
            buttonStartDNS.TabIndex = 0;
            buttonStartDNS.Text = "Start DNS";
            buttonStartDNS.UseVisualStyleBackColor = true;
            buttonStartDNS.Click += buttonStartDNS_Click;
            // 
            // groupBoxAuxiliaryServerManagement
            // 
            groupBoxAuxiliaryServerManagement.Controls.Add(buttonStopDNS);
            groupBoxAuxiliaryServerManagement.Controls.Add(buttonStopHorizon);
            groupBoxAuxiliaryServerManagement.Controls.Add(buttonStartHorizon);
            groupBoxAuxiliaryServerManagement.Controls.Add(buttonStartDNS);
            groupBoxAuxiliaryServerManagement.Location = new Point(506, 494);
            groupBoxAuxiliaryServerManagement.Name = "groupBoxAuxiliaryServerManagement";
            groupBoxAuxiliaryServerManagement.Size = new Size(489, 234);
            groupBoxAuxiliaryServerManagement.TabIndex = 2;
            groupBoxAuxiliaryServerManagement.TabStop = false;
            groupBoxAuxiliaryServerManagement.Text = "Auxiliary Server Management";
            // 
            // buttonStopDNS
            // 
            buttonStopDNS.Location = new Point(167, 88);
            buttonStopDNS.Name = "buttonStopDNS";
            buttonStopDNS.Size = new Size(106, 29);
            buttonStopDNS.TabIndex = 7;
            buttonStopDNS.Text = "Stop DNS";
            buttonStopDNS.UseVisualStyleBackColor = true;
            buttonStopDNS.Click += buttonStopDNS_Click;
            // 
            // buttonStopHorizon
            // 
            buttonStopHorizon.Location = new Point(167, 41);
            buttonStopHorizon.Name = "buttonStopHorizon";
            buttonStopHorizon.Size = new Size(106, 29);
            buttonStopHorizon.TabIndex = 6;
            buttonStopHorizon.Text = "Stop Horizon";
            buttonStopHorizon.UseVisualStyleBackColor = true;
            buttonStopHorizon.Click += buttonStopHorizon_Click;
            // 
            // buttonStartHorizon
            // 
            buttonStartHorizon.Location = new Point(33, 41);
            buttonStartHorizon.Name = "buttonStartHorizon";
            buttonStartHorizon.Size = new Size(114, 29);
            buttonStartHorizon.TabIndex = 2;
            buttonStartHorizon.Text = "Start Horizon";
            buttonStartHorizon.UseVisualStyleBackColor = true;
            buttonStartHorizon.Click += buttonStartHorizon_Click;
            // 
            // pictureBoxPSMSImage
            // 
            pictureBoxPSMSImage.Image = Properties.Resources.multiserver2xplogo;
            pictureBoxPSMSImage.Location = new Point(12, 12);
            pictureBoxPSMSImage.Name = "pictureBoxPSMSImage";
            pictureBoxPSMSImage.Size = new Size(983, 476);
            pictureBoxPSMSImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPSMSImage.TabIndex = 3;
            pictureBoxPSMSImage.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1007, 982);
            Controls.Add(pictureBoxPSMSImage);
            Controls.Add(groupBoxAuxiliaryServerManagement);
            Controls.Add(groupBoxWebServerManagement);
            Controls.Add(richTextBoxLog);
            Name = "MainForm";
            Text = "MultiServer Graphical User Interface";
            groupBoxWebServerManagement.ResumeLayout(false);
            groupBoxWebServerManagement.PerformLayout();
            groupBoxAuxiliaryServerManagement.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxPSMSImage).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBoxLog;
        private GroupBox groupBoxWebServerManagement;
        private Button buttonStartHTTPS;
        private Button buttonStartHTTP;
        private Button buttonStartDNS;
        private GroupBox groupBoxAuxiliaryServerManagement;
        private Button buttonStartHorizon;
        private Button buttonStartSSFW;
        private Button buttonStartSVO;
        private PictureBox pictureBoxPSMSImage;
        private Button buttonStopSVO;
        private Button buttonStopSSFW;
        private Button buttonStopHTTP;
        private Button buttonStopHTTPS;
        private Button buttonStopDNS;
        private Button buttonStopHorizon;
        private Label labelAdministratorRequired;
    }
}
