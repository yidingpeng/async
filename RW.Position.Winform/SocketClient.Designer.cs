
namespace RW.Position.Winform
{
    partial class SocketClient
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonconnect = new System.Windows.Forms.Button();
            this.labelconnectionstring = new System.Windows.Forms.Label();
            this.textBoxConnectstring = new System.Windows.Forms.TextBox();
            this.buttonrequest = new System.Windows.Forms.Button();
            this.buttonData = new System.Windows.Forms.Button();
            this.buttonDataStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRequestData = new System.Windows.Forms.Button();
            this.buttonDataConfirm = new System.Windows.Forms.Button();
            this.buttonStopConfirm = new System.Windows.Forms.Button();
            this.buttonOpenApply = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonAgreeNotOpen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonShoesDataRequest = new System.Windows.Forms.Button();
            this.buttonShoesDataPosition = new System.Windows.Forms.Button();
            this.buttonShoesDataStop = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonRequestAlarmData = new System.Windows.Forms.Button();
            this.textBoxLabelData = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonCarTrackStopData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonconnect
            // 
            this.buttonconnect.Location = new System.Drawing.Point(460, 81);
            this.buttonconnect.Name = "buttonconnect";
            this.buttonconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonconnect.TabIndex = 0;
            this.buttonconnect.Text = "连接";
            this.buttonconnect.UseVisualStyleBackColor = true;
            this.buttonconnect.Click += new System.EventHandler(this.buttonconnect_Click);
            // 
            // labelconnectionstring
            // 
            this.labelconnectionstring.AutoSize = true;
            this.labelconnectionstring.Location = new System.Drawing.Point(120, 87);
            this.labelconnectionstring.Name = "labelconnectionstring";
            this.labelconnectionstring.Size = new System.Drawing.Size(65, 12);
            this.labelconnectionstring.TabIndex = 1;
            this.labelconnectionstring.Text = "连接地址：";
            // 
            // textBoxConnectstring
            // 
            this.textBoxConnectstring.Location = new System.Drawing.Point(226, 82);
            this.textBoxConnectstring.Name = "textBoxConnectstring";
            this.textBoxConnectstring.Size = new System.Drawing.Size(207, 21);
            this.textBoxConnectstring.TabIndex = 2;
            // 
            // buttonrequest
            // 
            this.buttonrequest.Location = new System.Drawing.Point(755, 87);
            this.buttonrequest.Name = "buttonrequest";
            this.buttonrequest.Size = new System.Drawing.Size(75, 23);
            this.buttonrequest.TabIndex = 3;
            this.buttonrequest.Text = "发送请求";
            this.buttonrequest.UseVisualStyleBackColor = true;
            this.buttonrequest.Visible = false;
            this.buttonrequest.Click += new System.EventHandler(this.buttonrequest_Click);
            // 
            // buttonData
            // 
            this.buttonData.Location = new System.Drawing.Point(867, 87);
            this.buttonData.Name = "buttonData";
            this.buttonData.Size = new System.Drawing.Size(107, 23);
            this.buttonData.TabIndex = 4;
            this.buttonData.Text = "发送定位数据";
            this.buttonData.UseVisualStyleBackColor = true;
            this.buttonData.Visible = false;
            this.buttonData.Click += new System.EventHandler(this.buttonData_Click);
            // 
            // buttonDataStop
            // 
            this.buttonDataStop.Location = new System.Drawing.Point(999, 87);
            this.buttonDataStop.Name = "buttonDataStop";
            this.buttonDataStop.Size = new System.Drawing.Size(115, 23);
            this.buttonDataStop.TabIndex = 5;
            this.buttonDataStop.Text = "数据发送停止";
            this.buttonDataStop.UseVisualStyleBackColor = true;
            this.buttonDataStop.Click += new System.EventHandler(this.buttonDataStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(600, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "车辆所在股道发生变化时";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(564, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "定位系统请求车辆所在服道位置";
            // 
            // buttonRequestData
            // 
            this.buttonRequestData.Location = new System.Drawing.Point(755, 169);
            this.buttonRequestData.Name = "buttonRequestData";
            this.buttonRequestData.Size = new System.Drawing.Size(75, 23);
            this.buttonRequestData.TabIndex = 8;
            this.buttonRequestData.Text = "请求数据";
            this.buttonRequestData.UseVisualStyleBackColor = true;
            this.buttonRequestData.Click += new System.EventHandler(this.buttonRequestData_Click);
            // 
            // buttonDataConfirm
            // 
            this.buttonDataConfirm.Location = new System.Drawing.Point(867, 169);
            this.buttonDataConfirm.Name = "buttonDataConfirm";
            this.buttonDataConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonDataConfirm.TabIndex = 9;
            this.buttonDataConfirm.Text = "数据确认";
            this.buttonDataConfirm.UseVisualStyleBackColor = true;
            this.buttonDataConfirm.Click += new System.EventHandler(this.buttonDataConfirm_Click);
            // 
            // buttonStopConfirm
            // 
            this.buttonStopConfirm.Location = new System.Drawing.Point(999, 169);
            this.buttonStopConfirm.Name = "buttonStopConfirm";
            this.buttonStopConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonStopConfirm.TabIndex = 10;
            this.buttonStopConfirm.Text = "停止确认";
            this.buttonStopConfirm.UseVisualStyleBackColor = true;
            this.buttonStopConfirm.Click += new System.EventHandler(this.buttonStopConfirm_Click);
            // 
            // buttonOpenApply
            // 
            this.buttonOpenApply.Location = new System.Drawing.Point(755, 209);
            this.buttonOpenApply.Name = "buttonOpenApply";
            this.buttonOpenApply.Size = new System.Drawing.Size(95, 23);
            this.buttonOpenApply.TabIndex = 11;
            this.buttonOpenApply.Text = "发送打开申请";
            this.buttonOpenApply.UseVisualStyleBackColor = true;
            this.buttonOpenApply.Click += new System.EventHandler(this.buttonOpenApply_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(664, 214);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "打开铁鞋柜";
            // 
            // buttonAgreeNotOpen
            // 
            this.buttonAgreeNotOpen.Location = new System.Drawing.Point(867, 209);
            this.buttonAgreeNotOpen.Name = "buttonAgreeNotOpen";
            this.buttonAgreeNotOpen.Size = new System.Drawing.Size(87, 23);
            this.buttonAgreeNotOpen.TabIndex = 13;
            this.buttonAgreeNotOpen.Text = "是否同意打开";
            this.buttonAgreeNotOpen.UseVisualStyleBackColor = true;
   
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(647, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "铁鞋状态位置";
            // 
            // buttonShoesDataRequest
            // 
            this.buttonShoesDataRequest.Location = new System.Drawing.Point(755, 294);
            this.buttonShoesDataRequest.Name = "buttonShoesDataRequest";
            this.buttonShoesDataRequest.Size = new System.Drawing.Size(95, 23);
            this.buttonShoesDataRequest.TabIndex = 15;
            this.buttonShoesDataRequest.Text = "数据发送请求";
            this.buttonShoesDataRequest.UseVisualStyleBackColor = true;
            this.buttonShoesDataRequest.Click += new System.EventHandler(this.buttonShoesDataRequest_Click);
            // 
            // buttonShoesDataPosition
            // 
            this.buttonShoesDataPosition.Location = new System.Drawing.Point(867, 294);
            this.buttonShoesDataPosition.Name = "buttonShoesDataPosition";
            this.buttonShoesDataPosition.Size = new System.Drawing.Size(87, 23);
            this.buttonShoesDataPosition.TabIndex = 16;
            this.buttonShoesDataPosition.Text = "发送定位数据";
            this.buttonShoesDataPosition.UseVisualStyleBackColor = true;
            // 
            // buttonShoesDataStop
            // 
            this.buttonShoesDataStop.Location = new System.Drawing.Point(999, 294);
            this.buttonShoesDataStop.Name = "buttonShoesDataStop";
            this.buttonShoesDataStop.Size = new System.Drawing.Size(115, 23);
            this.buttonShoesDataStop.TabIndex = 18;
            this.buttonShoesDataStop.Text = "数据发送停止";
            this.buttonShoesDataStop.UseVisualStyleBackColor = true;
            this.buttonShoesDataStop.Click += new System.EventHandler(this.buttonShoesDataStop_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(633, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 19;
            this.label5.Text = "发送报警数据帧";
            // 
            // buttonRequestAlarmData
            // 
            this.buttonRequestAlarmData.Location = new System.Drawing.Point(755, 352);
            this.buttonRequestAlarmData.Name = "buttonRequestAlarmData";
            this.buttonRequestAlarmData.Size = new System.Drawing.Size(95, 23);
            this.buttonRequestAlarmData.TabIndex = 20;
            this.buttonRequestAlarmData.Text = "发送报警数据";
            this.buttonRequestAlarmData.UseVisualStyleBackColor = true;
            this.buttonRequestAlarmData.Click += new System.EventHandler(this.buttonRequestAlarmData_Click);
            // 
            // textBoxLabelData
            // 
            this.textBoxLabelData.Location = new System.Drawing.Point(202, 187);
            this.textBoxLabelData.Multiline = true;
            this.textBoxLabelData.Name = "textBoxLabelData";
            this.textBoxLabelData.Size = new System.Drawing.Size(264, 227);
            this.textBoxLabelData.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(588, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "调度系统请求车辆所在股道";
            // 
            // buttonCarTrackStopData
            // 
            this.buttonCarTrackStopData.Location = new System.Drawing.Point(755, 133);
            this.buttonCarTrackStopData.Name = "buttonCarTrackStopData";
            this.buttonCarTrackStopData.Size = new System.Drawing.Size(95, 23);
            this.buttonCarTrackStopData.TabIndex = 23;
            this.buttonCarTrackStopData.Text = "数据发送停止";
            this.buttonCarTrackStopData.UseVisualStyleBackColor = true;
            this.buttonCarTrackStopData.Click += new System.EventHandler(this.buttonCarTrackStopData_Click);
            // 
            // SocketClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 639);
            this.Controls.Add(this.buttonCarTrackStopData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxLabelData);
            this.Controls.Add(this.buttonRequestAlarmData);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonShoesDataStop);
            this.Controls.Add(this.buttonShoesDataPosition);
            this.Controls.Add(this.buttonShoesDataRequest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonAgreeNotOpen);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonOpenApply);
            this.Controls.Add(this.buttonStopConfirm);
            this.Controls.Add(this.buttonDataConfirm);
            this.Controls.Add(this.buttonRequestData);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDataStop);
            this.Controls.Add(this.buttonData);
            this.Controls.Add(this.buttonrequest);
            this.Controls.Add(this.textBoxConnectstring);
            this.Controls.Add(this.labelconnectionstring);
            this.Controls.Add(this.buttonconnect);
            this.Name = "SocketClient";
            this.Text = "socket客户端";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonconnect;
        private System.Windows.Forms.Label labelconnectionstring;
        private System.Windows.Forms.TextBox textBoxConnectstring;
        private System.Windows.Forms.Button buttonrequest;
        private System.Windows.Forms.Button buttonData;
        private System.Windows.Forms.Button buttonDataStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRequestData;
        private System.Windows.Forms.Button buttonDataConfirm;
        private System.Windows.Forms.Button buttonStopConfirm;
        private System.Windows.Forms.Button buttonOpenApply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonAgreeNotOpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonShoesDataRequest;
        private System.Windows.Forms.Button buttonShoesDataPosition;
        private System.Windows.Forms.Button buttonShoesDataStop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonRequestAlarmData;
        private System.Windows.Forms.TextBox textBoxLabelData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonCarTrackStopData;
    }
}

