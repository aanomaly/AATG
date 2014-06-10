namespace Markow20
{
    sealed partial class Form1
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
            this.newBoardButton = new System.Windows.Forms.Button();
            this.xTrackBar = new System.Windows.Forms.TrackBar();
            this.yTrackBar = new System.Windows.Forms.TrackBar();
            this.xlabel = new System.Windows.Forms.Label();
            this.ylabel = new System.Windows.Forms.Label();
            this.calculatePolicyButton = new System.Windows.Forms.Button();
            this.rlabel = new System.Windows.Forms.Label();
            this.rTextbox = new System.Windows.Forms.TextBox();
            this.gammaLabel = new System.Windows.Forms.Label();
            this.gammaTextbox = new System.Windows.Forms.TextBox();
            this.calculateUtilityButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // newBoardButton
            // 
            this.newBoardButton.Location = new System.Drawing.Point(-1, 1);
            this.newBoardButton.Name = "newBoardButton";
            this.newBoardButton.Size = new System.Drawing.Size(110, 103);
            this.newBoardButton.TabIndex = 3;
            this.newBoardButton.Text = "Create New Board";
            this.newBoardButton.UseVisualStyleBackColor = true;
            this.newBoardButton.Click += new System.EventHandler(this.NewBoardButtonClick);
            // 
            // xTrackBar
            // 
            this.xTrackBar.Location = new System.Drawing.Point(115, 1);
            this.xTrackBar.Maximum = 20;
            this.xTrackBar.Minimum = 1;
            this.xTrackBar.Name = "xTrackBar";
            this.xTrackBar.Size = new System.Drawing.Size(146, 45);
            this.xTrackBar.TabIndex = 4;
            this.xTrackBar.Value = 4;
            this.xTrackBar.Scroll += new System.EventHandler(this.XTrackBarScroll);
            // 
            // yTrackBar
            // 
            this.yTrackBar.Location = new System.Drawing.Point(115, 52);
            this.yTrackBar.Maximum = 20;
            this.yTrackBar.Minimum = 1;
            this.yTrackBar.Name = "yTrackBar";
            this.yTrackBar.Size = new System.Drawing.Size(146, 45);
            this.yTrackBar.TabIndex = 5;
            this.yTrackBar.Value = 3;
            this.yTrackBar.Scroll += new System.EventHandler(this.YTrackBarScroll);
            // 
            // xlabel
            // 
            this.xlabel.AutoSize = true;
            this.xlabel.Location = new System.Drawing.Point(267, 9);
            this.xlabel.Name = "xlabel";
            this.xlabel.Size = new System.Drawing.Size(30, 13);
            this.xlabel.TabIndex = 6;
            this.xlabel.Text = "x = 4";
            // 
            // ylabel
            // 
            this.ylabel.AutoSize = true;
            this.ylabel.Location = new System.Drawing.Point(267, 52);
            this.ylabel.Name = "ylabel";
            this.ylabel.Size = new System.Drawing.Size(30, 13);
            this.ylabel.TabIndex = 7;
            this.ylabel.Text = "y = 3";
            // 
            // calculatePolicyButton
            // 
            this.calculatePolicyButton.Location = new System.Drawing.Point(310, 1);
            this.calculatePolicyButton.Name = "calculatePolicyButton";
            this.calculatePolicyButton.Size = new System.Drawing.Size(115, 58);
            this.calculatePolicyButton.TabIndex = 8;
            this.calculatePolicyButton.Text = "Calculate Strategy";
            this.calculatePolicyButton.UseVisualStyleBackColor = true;
            this.calculatePolicyButton.Click += new System.EventHandler(this.Button4Click);
            // 
            // rlabel
            // 
            this.rlabel.AutoSize = true;
            this.rlabel.Location = new System.Drawing.Point(432, 9);
            this.rlabel.Name = "rlabel";
            this.rlabel.Size = new System.Drawing.Size(15, 13);
            this.rlabel.TabIndex = 9;
            this.rlabel.Text = "R";
            // 
            // rTextbox
            // 
            this.rTextbox.Location = new System.Drawing.Point(453, 6);
            this.rTextbox.Name = "rTextbox";
            this.rTextbox.Size = new System.Drawing.Size(49, 20);
            this.rTextbox.TabIndex = 10;
            this.rTextbox.Text = "-0.04";
            this.rTextbox.Leave += new System.EventHandler(this.TextBox1Leave);
            // 
            // gammaLabel
            // 
            this.gammaLabel.AutoSize = true;
            this.gammaLabel.Location = new System.Drawing.Point(431, 39);
            this.gammaLabel.Name = "gammaLabel";
            this.gammaLabel.Size = new System.Drawing.Size(15, 13);
            this.gammaLabel.TabIndex = 11;
            this.gammaLabel.Text = "D";
            // 
            // gammaTextbox
            // 
            this.gammaTextbox.Location = new System.Drawing.Point(453, 39);
            this.gammaTextbox.Name = "gammaTextbox";
            this.gammaTextbox.Size = new System.Drawing.Size(49, 20);
            this.gammaTextbox.TabIndex = 12;
            this.gammaTextbox.Text = "1";
            this.gammaTextbox.Leave += new System.EventHandler(this.TextBox2Leave);
            // 
            // calculateUtilityButton
            // 
            this.calculateUtilityButton.Location = new System.Drawing.Point(310, 65);
            this.calculateUtilityButton.Name = "calculateUtilityButton";
            this.calculateUtilityButton.Size = new System.Drawing.Size(115, 39);
            this.calculateUtilityButton.TabIndex = 13;
            this.calculateUtilityButton.Text = "CalculateUtility";
            this.calculateUtilityButton.UseVisualStyleBackColor = true;
            this.calculateUtilityButton.Click += new System.EventHandler(this.CalculateUtilityButtonClick);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(435, 69);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(66, 34);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButtonClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 342);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.calculateUtilityButton);
            this.Controls.Add(this.gammaTextbox);
            this.Controls.Add(this.gammaLabel);
            this.Controls.Add(this.rTextbox);
            this.Controls.Add(this.rlabel);
            this.Controls.Add(this.calculatePolicyButton);
            this.Controls.Add(this.ylabel);
            this.Controls.Add(this.xlabel);
            this.Controls.Add(this.yTrackBar);
            this.Controls.Add(this.xTrackBar);
            this.Controls.Add(this.newBoardButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.xTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button newBoardButton;
        private System.Windows.Forms.TrackBar xTrackBar;
        private System.Windows.Forms.TrackBar yTrackBar;
        private System.Windows.Forms.Label xlabel;
        private System.Windows.Forms.Label ylabel;
        private System.Windows.Forms.Button calculatePolicyButton;
        private System.Windows.Forms.Label rlabel;
        private System.Windows.Forms.TextBox rTextbox;
        private System.Windows.Forms.Label gammaLabel;
        private System.Windows.Forms.TextBox gammaTextbox;
        private System.Windows.Forms.Button calculateUtilityButton;
        private System.Windows.Forms.Button clearButton;


    }
}

