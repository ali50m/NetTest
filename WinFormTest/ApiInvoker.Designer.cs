namespace WinFormTest
{
    partial class ApiInvoker
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
            this.txtHttpMethod = new System.Windows.Forms.TextBox();
            this.bsInvokeData = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbHistory = new System.Windows.Forms.ListBox();
            this.txtRequestData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtResponseData = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnInvoke = new System.Windows.Forms.Button();
            this.txtBaseUrl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvokeData)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHttpMethod
            // 
            this.txtHttpMethod.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "HttpMethod", true));
            this.txtHttpMethod.Location = new System.Drawing.Point(794, 62);
            this.txtHttpMethod.Name = "txtHttpMethod";
            this.txtHttpMethod.Size = new System.Drawing.Size(100, 21);
            this.txtHttpMethod.TabIndex = 0;
            // 
            // bsInvokeData
            // 
            this.bsInvokeData.DataSource = typeof(WinFormTest.InvokeData);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(728, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "HttpMethod";
            // 
            // txtUrl
            // 
            this.txtUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "Url", true));
            this.txtUrl.Location = new System.Drawing.Point(270, 99);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(612, 21);
            this.txtUrl.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Url";
            // 
            // lbHistory
            // 
            this.lbHistory.DataSource = this.bsInvokeData;
            this.lbHistory.DisplayMember = "Url";
            this.lbHistory.FormattingEnabled = true;
            this.lbHistory.ItemHeight = 12;
            this.lbHistory.Location = new System.Drawing.Point(1, 14);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.Size = new System.Drawing.Size(189, 520);
            this.lbHistory.TabIndex = 2;
            this.lbHistory.ValueMember = "Id";
            // 
            // txtRequestData
            // 
            this.txtRequestData.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "RequestData", true));
            this.txtRequestData.Location = new System.Drawing.Point(219, 207);
            this.txtRequestData.Multiline = true;
            this.txtRequestData.Name = "txtRequestData";
            this.txtRequestData.Size = new System.Drawing.Size(675, 137);
            this.txtRequestData.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Data";
            // 
            // txtToken
            // 
            this.txtToken.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "Token", true));
            this.txtToken.Location = new System.Drawing.Point(304, 138);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(578, 21);
            this.txtToken.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "BearerToken";
            // 
            // txtResponseData
            // 
            this.txtResponseData.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "ResponseData", true));
            this.txtResponseData.Location = new System.Drawing.Point(219, 397);
            this.txtResponseData.Multiline = true;
            this.txtResponseData.Name = "txtResponseData";
            this.txtResponseData.Size = new System.Drawing.Size(675, 137);
            this.txtResponseData.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(217, 370);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "ResponseData";
            // 
            // btnInvoke
            // 
            this.btnInvoke.Location = new System.Drawing.Point(807, 541);
            this.btnInvoke.Name = "btnInvoke";
            this.btnInvoke.Size = new System.Drawing.Size(75, 23);
            this.btnInvoke.TabIndex = 3;
            this.btnInvoke.Text = "运行";
            this.btnInvoke.UseVisualStyleBackColor = true;
            this.btnInvoke.Click += new System.EventHandler(this.btnInvoke_Click);
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "BaseUrl", true));
            this.txtBaseUrl.Location = new System.Drawing.Point(270, 62);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.Size = new System.Drawing.Size(433, 21);
            this.txtBaseUrl.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "BaseUrl";
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsInvokeData, "Name", true));
            this.txtName.Location = new System.Drawing.Point(270, 25);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(433, 21);
            this.txtName.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "Name";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(718, 541);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "新建";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsInvokeData, "UseJson", true));
            this.checkBox1.Location = new System.Drawing.Point(807, 180);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Send Json";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ApiInvoker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 618);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnInvoke);
            this.Controls.Add(this.lbHistory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtBaseUrl);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.txtResponseData);
            this.Controls.Add(this.txtRequestData);
            this.Controls.Add(this.txtHttpMethod);
            this.Name = "ApiInvoker";
            this.Text = "ApiInvoker";
            ((System.ComponentModel.ISupportInitialize)(this.bsInvokeData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHttpMethod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbHistory;
        private System.Windows.Forms.TextBox txtRequestData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtResponseData;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnInvoke;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingSource bsInvokeData;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}