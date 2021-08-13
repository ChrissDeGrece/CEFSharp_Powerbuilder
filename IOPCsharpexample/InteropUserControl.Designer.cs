namespace IOPCsharpexample
{
    partial class InteropUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TB1 = new System.Windows.Forms.TrackBar();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TB1)).BeginInit();
            this.SuspendLayout();
            // 
            // TB1
            // 
            this.TB1.BackColor = System.Drawing.Color.Yellow;
            this.TB1.Location = new System.Drawing.Point(0, 0);
            this.TB1.Margin = new System.Windows.Forms.Padding(0);
            this.TB1.Maximum = 100;
            this.TB1.Name = "TB1";
            this.TB1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TB1.Size = new System.Drawing.Size(56, 185);
            this.TB1.TabIndex = 0;
            this.TB1.TickFrequency = 10;
            this.TB1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TB1.Scroll += new System.EventHandler(this.TB1_Scroll);
            this.TB1.ValueChanged += new System.EventHandler(this.TB1_ValueChanged);
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
// TODO: Code generation for '' failed because of Exception 'Invalid Primitive Type: System.IntPtr. Consider using CodeObjectCreateExpression.'.
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(114, 57);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(710, 417);
            this.chromiumWebBrowser1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(721, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // InteropUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chromiumWebBrowser1);
            this.Controls.Add(this.TB1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InteropUserControl";
            this.Size = new System.Drawing.Size(866, 501);
            ((System.ComponentModel.ISupportInitialize)(this.TB1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar TB1;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private System.Windows.Forms.Button button1;
    }
}

