namespace JSAppBuilderGui
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
            this.txtPath = new System.Windows.Forms.ComboBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.btnBuildRelease = new System.Windows.Forms.Button();
            this.btnDeserialize = new System.Windows.Forms.Button();
            this.dialogSelectPath = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // txtPath
            // 
            this.txtPath.FormattingEnabled = true;
            this.txtPath.Location = new System.Drawing.Point(33, 25);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(752, 40);
            this.txtPath.TabIndex = 0;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(806, 25);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(63, 46);
            this.btnSelectPath.TabIndex = 1;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(234, 100);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(204, 46);
            this.btnDebug.TabIndex = 2;
            this.btnDebug.Text = "Build Debug";
            this.btnDebug.UseVisualStyleBackColor = true;
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // btnBuildRelease
            // 
            this.btnBuildRelease.Location = new System.Drawing.Point(454, 100);
            this.btnBuildRelease.Name = "btnBuildRelease";
            this.btnBuildRelease.Size = new System.Drawing.Size(200, 46);
            this.btnBuildRelease.TabIndex = 3;
            this.btnBuildRelease.Text = "Build Release";
            this.btnBuildRelease.UseVisualStyleBackColor = true;
            this.btnBuildRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnDeserialize
            // 
            this.btnDeserialize.Location = new System.Drawing.Point(669, 100);
            this.btnDeserialize.Name = "btnDeserialize";
            this.btnDeserialize.Size = new System.Drawing.Size(200, 46);
            this.btnDeserialize.TabIndex = 4;
            this.btnDeserialize.Text = "Deserialize";
            this.btnDeserialize.UseVisualStyleBackColor = true;
            this.btnDeserialize.Click += new System.EventHandler(this.btnDeserialize_Click);
            // 
            // dialogSelectPath
            // 
            this.dialogSelectPath.FileOk += new System.ComponentModel.CancelEventHandler(this.dialogSelectPath_FileOk);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 185);
            this.Controls.Add(this.btnDeserialize);
            this.Controls.Add(this.btnBuildRelease);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.txtPath);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox txtPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Button btnBuildRelease;
        private System.Windows.Forms.Button btnDeserialize;
        private System.Windows.Forms.OpenFileDialog dialogSelectPath;
    }
}

