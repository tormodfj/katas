namespace Reversi
{
    partial class ReversiKataRunner
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
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.sample1Button = new System.Windows.Forms.Button();
            this.sample2Button = new System.Windows.Forms.Button();
            this.sample3Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.Color.White;
            this.inputTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(12, 12);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(200, 200);
            this.inputTextBox.TabIndex = 0;
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.Color.White;
            this.outputTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.Location = new System.Drawing.Point(363, 12);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(200, 200);
            this.outputTextBox.TabIndex = 1;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(251, 189);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(75, 23);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Run!";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // sample1Button
            // 
            this.sample1Button.Location = new System.Drawing.Point(251, 12);
            this.sample1Button.Name = "sample1Button";
            this.sample1Button.Size = new System.Drawing.Size(75, 23);
            this.sample1Button.TabIndex = 3;
            this.sample1Button.Text = "Sample 1";
            this.sample1Button.UseVisualStyleBackColor = true;
            this.sample1Button.Click += new System.EventHandler(this.sample1Button_Click);
            // 
            // sample2Button
            // 
            this.sample2Button.Location = new System.Drawing.Point(251, 41);
            this.sample2Button.Name = "sample2Button";
            this.sample2Button.Size = new System.Drawing.Size(75, 23);
            this.sample2Button.TabIndex = 4;
            this.sample2Button.Text = "Sample 2";
            this.sample2Button.UseVisualStyleBackColor = true;
            this.sample2Button.Click += new System.EventHandler(this.sample2Button_Click);
            // 
            // sample3Button
            // 
            this.sample3Button.Location = new System.Drawing.Point(251, 70);
            this.sample3Button.Name = "sample3Button";
            this.sample3Button.Size = new System.Drawing.Size(75, 23);
            this.sample3Button.TabIndex = 5;
            this.sample3Button.Text = "Sample 3";
            this.sample3Button.UseVisualStyleBackColor = true;
            this.sample3Button.Click += new System.EventHandler(this.sample3Button_Click);
            // 
            // ReversiKataRunner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 226);
            this.Controls.Add(this.sample3Button);
            this.Controls.Add(this.sample2Button);
            this.Controls.Add(this.sample1Button);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReversiKataRunner";
            this.Text = "Reversi Kata";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button sample1Button;
        private System.Windows.Forms.Button sample2Button;
        private System.Windows.Forms.Button sample3Button;
    }
}

