namespace PluginHelper
{
    partial class Form1
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.btnUninject = new System.Windows.Forms.Button();
            this.btnInject = new System.Windows.Forms.Button();
            this.textDllPath = new System.Windows.Forms.TextBox();
            this.textModulesView = new System.Windows.Forms.TextBox();
            this.checkBoxFlag = new System.Windows.Forms.CheckBox();
            this.textProcessNameFilter = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.comboBoxProcessList = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.textMemAddr = new System.Windows.Forms.TextBox();
            this.btnReadMem = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.textBoxMsgValue = new System.Windows.Forms.TextBox();
            this.textBoxMsgTitle = new System.Windows.Forms.TextBox();
            this.btnMsgInject = new System.Windows.Forms.Button();
            this.btnInjectCode = new System.Windows.Forms.Button();
            this.textAsmCode = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnGdi = new System.Windows.Forms.Button();
            this.btnGdiPlu = new System.Windows.Forms.Button();
            this.btnNetGdi = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(456, 517);
            this.tabControl.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnOpenFile);
            this.tabPage1.Controls.Add(this.btnUninject);
            this.tabPage1.Controls.Add(this.btnInject);
            this.tabPage1.Controls.Add(this.textDllPath);
            this.tabPage1.Controls.Add(this.textModulesView);
            this.tabPage1.Controls.Add(this.checkBoxFlag);
            this.tabPage1.Controls.Add(this.textProcessNameFilter);
            this.tabPage1.Controls.Add(this.btnRefresh);
            this.tabPage1.Controls.Add(this.comboBoxProcessList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(448, 491);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "进程相关";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(303, 420);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(80, 20);
            this.btnOpenFile.TabIndex = 13;
            this.btnOpenFile.Text = "选择DLL文件";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // btnUninject
            // 
            this.btnUninject.Location = new System.Drawing.Point(152, 458);
            this.btnUninject.Name = "btnUninject";
            this.btnUninject.Size = new System.Drawing.Size(80, 20);
            this.btnUninject.TabIndex = 12;
            this.btnUninject.Text = "DLL卸载";
            this.btnUninject.UseVisualStyleBackColor = true;
            this.btnUninject.Click += new System.EventHandler(this.btnUninject_Click);
            // 
            // btnInject
            // 
            this.btnInject.Location = new System.Drawing.Point(43, 458);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(80, 20);
            this.btnInject.TabIndex = 11;
            this.btnInject.Text = "DLL注入";
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // textDllPath
            // 
            this.textDllPath.Location = new System.Drawing.Point(6, 420);
            this.textDllPath.Name = "textDllPath";
            this.textDllPath.Size = new System.Drawing.Size(276, 21);
            this.textDllPath.TabIndex = 10;
            // 
            // textModulesView
            // 
            this.textModulesView.Location = new System.Drawing.Point(6, 87);
            this.textModulesView.Multiline = true;
            this.textModulesView.Name = "textModulesView";
            this.textModulesView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textModulesView.Size = new System.Drawing.Size(400, 313);
            this.textModulesView.TabIndex = 9;
            this.textModulesView.TextChanged += new System.EventHandler(this.textModulesView_TextChanged);
            // 
            // checkBoxFlag
            // 
            this.checkBoxFlag.Location = new System.Drawing.Point(6, 46);
            this.checkBoxFlag.Name = "checkBoxFlag";
            this.checkBoxFlag.Size = new System.Drawing.Size(87, 20);
            this.checkBoxFlag.TabIndex = 7;
            this.checkBoxFlag.Text = "进程名过滤";
            this.checkBoxFlag.UseVisualStyleBackColor = true;
            // 
            // textProcessNameFilter
            // 
            this.textProcessNameFilter.Location = new System.Drawing.Point(99, 45);
            this.textProcessNameFilter.Name = "textProcessNameFilter";
            this.textProcessNameFilter.Size = new System.Drawing.Size(74, 21);
            this.textProcessNameFilter.TabIndex = 6;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(303, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 20);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "刷新进程";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // comboBoxProcessList
            // 
            this.comboBoxProcessList.FormattingEnabled = true;
            this.comboBoxProcessList.Location = new System.Drawing.Point(6, 20);
            this.comboBoxProcessList.Name = "comboBoxProcessList";
            this.comboBoxProcessList.Size = new System.Drawing.Size(291, 20);
            this.comboBoxProcessList.TabIndex = 4;
            this.comboBoxProcessList.SelectedIndexChanged += new System.EventHandler(this.comboBoxProcessList_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.comboBoxType);
            this.tabPage2.Controls.Add(this.textMemAddr);
            this.tabPage2.Controls.Add(this.btnReadMem);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(448, 491);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "内存读写";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {"数字", "字节", "字符串", "图片"});
            this.comboBoxType.Location = new System.Drawing.Point(156, 28);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(82, 20);
            this.comboBoxType.TabIndex = 2;
            this.comboBoxType.Text = "选择类型";
            // 
            // textMemAddr
            // 
            this.textMemAddr.Location = new System.Drawing.Point(27, 28);
            this.textMemAddr.Name = "textMemAddr";
            this.textMemAddr.Size = new System.Drawing.Size(123, 21);
            this.textMemAddr.TabIndex = 1;
            // 
            // btnReadMem
            // 
            this.btnReadMem.Location = new System.Drawing.Point(244, 29);
            this.btnReadMem.Name = "btnReadMem";
            this.btnReadMem.Size = new System.Drawing.Size(75, 20);
            this.btnReadMem.TabIndex = 0;
            this.btnReadMem.Text = "读取";
            this.btnReadMem.UseVisualStyleBackColor = true;
            this.btnReadMem.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBoxLog);
            this.tabPage3.Controls.Add(this.textBoxMsgValue);
            this.tabPage3.Controls.Add(this.textBoxMsgTitle);
            this.tabPage3.Controls.Add(this.btnMsgInject);
            this.tabPage3.Controls.Add(this.btnInjectCode);
            this.tabPage3.Controls.Add(this.textAsmCode);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(448, 491);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "代码注入";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(20, 367);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(409, 98);
            this.textBoxLog.TabIndex = 5;
            // 
            // textBoxMsgValue
            // 
            this.textBoxMsgValue.Location = new System.Drawing.Point(304, 83);
            this.textBoxMsgValue.Name = "textBoxMsgValue";
            this.textBoxMsgValue.Size = new System.Drawing.Size(124, 21);
            this.textBoxMsgValue.TabIndex = 4;
            this.textBoxMsgValue.Text = "this is value";
            // 
            // textBoxMsgTitle
            // 
            this.textBoxMsgTitle.Location = new System.Drawing.Point(304, 56);
            this.textBoxMsgTitle.Name = "textBoxMsgTitle";
            this.textBoxMsgTitle.Size = new System.Drawing.Size(124, 21);
            this.textBoxMsgTitle.TabIndex = 3;
            this.textBoxMsgTitle.Text = "this is title";
            // 
            // btnMsgInject
            // 
            this.btnMsgInject.Location = new System.Drawing.Point(304, 110);
            this.btnMsgInject.Name = "btnMsgInject";
            this.btnMsgInject.Size = new System.Drawing.Size(140, 23);
            this.btnMsgInject.TabIndex = 2;
            this.btnMsgInject.Text = "MessageBoxA";
            this.btnMsgInject.UseVisualStyleBackColor = true;
            this.btnMsgInject.Click += new System.EventHandler(this.btnMsgInject_Click);
            // 
            // btnInjectCode
            // 
            this.btnInjectCode.Location = new System.Drawing.Point(196, 338);
            this.btnInjectCode.Name = "btnInjectCode";
            this.btnInjectCode.Size = new System.Drawing.Size(75, 23);
            this.btnInjectCode.TabIndex = 1;
            this.btnInjectCode.Text = "注入";
            this.btnInjectCode.UseVisualStyleBackColor = true;
            this.btnInjectCode.Click += new System.EventHandler(this.btnInjectCode_Click);
            // 
            // textAsmCode
            // 
            this.textAsmCode.Location = new System.Drawing.Point(20, 41);
            this.textAsmCode.Multiline = true;
            this.textAsmCode.Name = "textAsmCode";
            this.textAsmCode.Size = new System.Drawing.Size(267, 291);
            this.textAsmCode.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnNetGdi);
            this.tabPage4.Controls.Add(this.btnGdi);
            this.tabPage4.Controls.Add(this.btnGdiPlu);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(448, 491);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "D3D";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnGdi
            // 
            this.btnGdi.Location = new System.Drawing.Point(152, 71);
            this.btnGdi.Name = "btnGdi";
            this.btnGdi.Size = new System.Drawing.Size(91, 23);
            this.btnGdi.TabIndex = 2;
            this.btnGdi.Text = "GDI普通绘制";
            this.btnGdi.UseVisualStyleBackColor = true;
            this.btnGdi.Click += new System.EventHandler(this.btnGdi_Click);
            // 
            // btnGdiPlu
            // 
            this.btnGdiPlu.Location = new System.Drawing.Point(267, 71);
            this.btnGdiPlu.Name = "btnGdiPlu";
            this.btnGdiPlu.Size = new System.Drawing.Size(104, 23);
            this.btnGdiPlu.TabIndex = 0;
            this.btnGdiPlu.Text = "GDI内存缓冲绘制";
            this.btnGdiPlu.UseVisualStyleBackColor = true;
            this.btnGdiPlu.Click += new System.EventHandler(this.btnGdiPlu_Click);
            // 
            // btnNetGdi
            // 
            this.btnNetGdi.Location = new System.Drawing.Point(24, 71);
            this.btnNetGdi.Name = "btnNetGdi";
            this.btnNetGdi.Size = new System.Drawing.Size(91, 23);
            this.btnNetGdi.TabIndex = 3;
            this.btnNetGdi.Text = "NET Gdi";
            this.btnNetGdi.UseVisualStyleBackColor = true;
            this.btnNetGdi.Click += new System.EventHandler(this.btnNetGdi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(480, 541);
            this.Controls.Add(this.tabControl);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button btnNetGdi;

        private System.Windows.Forms.Button btnGdi;
        private System.Windows.Forms.Button btnGdiPlu;


        private System.Windows.Forms.TabPage tabPage4;

        private System.Windows.Forms.ComboBox comboBoxType;

        private System.Windows.Forms.Button btnReadMem;
        private System.Windows.Forms.TextBox textMemAddr;

        private System.Windows.Forms.TextBox textBoxLog;

        private System.Windows.Forms.Button btnMsgInject;
        private System.Windows.Forms.TextBox textBoxMsgTitle;
        private System.Windows.Forms.TextBox textBoxMsgValue;

        private System.Windows.Forms.Button btnInjectCode;

        private System.Windows.Forms.TextBox textAsmCode;

        private System.Windows.Forms.TabPage tabPage3;

        private System.Windows.Forms.TabControl tabControl;

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

        private System.Windows.Forms.TextBox textDllPath;
        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.Button btnUninject;
        private System.Windows.Forms.Button btnOpenFile;

        private System.Windows.Forms.TextBox textModulesView;

        private System.Windows.Forms.CheckBox checkBoxFlag;

        private System.Windows.Forms.TextBox textProcessNameFilter;

        private System.Windows.Forms.ComboBox comboBoxProcessList;
        private System.Windows.Forms.Button btnRefresh;

        #endregion
    }
}