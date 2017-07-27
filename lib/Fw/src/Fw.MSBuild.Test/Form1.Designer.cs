namespace Fw.MSBuild.Test
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
            this.dialogSelectPath = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnDeserialize = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dialogSelectPath
            // 
            this.dialogSelectPath.FileName = "C:\\project\\";
            this.dialogSelectPath.FileOk += new System.ComponentModel.CancelEventHandler(this.dialogSelectPath_FileOk);
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(835, 22);
            this.btnSelectPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(52, 39);
            this.btnSelectPath.TabIndex = 1;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Location = new System.Drawing.Point(568, 118);
            this.btnRelease.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(197, 44);
            this.btnRelease.TabIndex = 2;
            this.btnRelease.Text = "Build Release";
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnDeserialize
            // 
            this.btnDeserialize.Location = new System.Drawing.Point(779, 118);
            this.btnDeserialize.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnDeserialize.Name = "btnDeserialize";
            this.btnDeserialize.Size = new System.Drawing.Size(163, 44);
            this.btnDeserialize.TabIndex = 3;
            this.btnDeserialize.Text = "Deserialize";
            this.btnDeserialize.UseVisualStyleBackColor = true;
            this.btnDeserialize.Click += new System.EventHandler(this.btnDeserialize_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(357, 118);
            this.btnDebug.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(197, 44);
            this.btnDebug.TabIndex = 4;
            this.btnDebug.Text = "Build Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // txtPath
            // 
            this.txtPath.FormattingEnabled = true;
            this.txtPath.Items.AddRange(new object[] {
            "C:\\gitprojects\\RentalWorksWeb\\src\\RentalWorksWeb\\JSAppBuilder.config",
            "C:\\gitprojects\\RentalWorksWeb\\src\\\\RentalWorksQuikScan\\JSAppBuilder.config",
            "C:\\gitprojects\\GateWorksWeb\\src\\GateWorksWeb\\JSAppBuilder.config",
            "C:\\gitprojects\\GateWorksWeb\\src\\GateWorksMobile\\JSAppBuilder.config",
            "C:\\gitprojects\\TransWorksWeb\\src\\TransWorksWeb\\JSAppBuilder.config",
            "C:\\project\\DBWorks\\web\\support.dbworks.com\\JSAppBuilder.config"});
            this.txtPath.Location = new System.Drawing.Point(24, 25);
            this.txtPath.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(793, 33);
            this.txtPath.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 185);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnDeserialize);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnSelectPath);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form1";
            this.Text = "JSAppBuilder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dialogSelectPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnDeserialize;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.ComboBox txtPath;
    }
}

