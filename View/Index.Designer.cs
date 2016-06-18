using System.Threading.Tasks;

namespace View
{
    partial class Index
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.TrainButton = new System.Windows.Forms.Button();
            this.errorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.RecognizeButton = new System.Windows.Forms.Button();
            this.ClearButtton = new System.Windows.Forms.Button();
            this.SymbolLabel = new System.Windows.Forms.Label();
            this.SymbolDrawBox = new ViewComponents.DrawBox();
            this.trainSymbolPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainSymbolPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TrainButton
            // 
            this.TrainButton.Location = new System.Drawing.Point(547, 325);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(87, 65);
            this.TrainButton.TabIndex = 0;
            this.TrainButton.Text = "Train";
            this.TrainButton.UseVisualStyleBackColor = true;
            this.TrainButton.Click += new System.EventHandler(this.TrainButton_Click);
            // 
            // errorChart
            // 
            chartArea1.Name = "ChartArea1";
            this.errorChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.errorChart.Legends.Add(legend1);
            this.errorChart.Location = new System.Drawing.Point(14, 284);
            this.errorChart.Name = "errorChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.errorChart.Series.Add(series1);
            this.errorChart.Size = new System.Drawing.Size(486, 125);
            this.errorChart.TabIndex = 1;
            this.errorChart.Text = "errorChart";
            // 
            // RecognizeButton
            // 
            this.RecognizeButton.Location = new System.Drawing.Point(120, 218);
            this.RecognizeButton.Name = "RecognizeButton";
            this.RecognizeButton.Size = new System.Drawing.Size(75, 23);
            this.RecognizeButton.TabIndex = 3;
            this.RecognizeButton.Text = "Recognize";
            this.RecognizeButton.UseVisualStyleBackColor = true;
            this.RecognizeButton.Click += new System.EventHandler(this.RecognizeButton_Click);
            // 
            // ClearButtton
            // 
            this.ClearButtton.Location = new System.Drawing.Point(201, 218);
            this.ClearButtton.Name = "ClearButtton";
            this.ClearButtton.Size = new System.Drawing.Size(75, 23);
            this.ClearButtton.TabIndex = 4;
            this.ClearButtton.Text = "Clear";
            this.ClearButtton.UseVisualStyleBackColor = true;
            this.ClearButtton.Click += new System.EventHandler(this.ClearButtton_Click);
            // 
            // SymbolLabel
            // 
            this.SymbolLabel.AutoSize = true;
            this.SymbolLabel.Location = new System.Drawing.Point(329, 78);
            this.SymbolLabel.Name = "SymbolLabel";
            this.SymbolLabel.Size = new System.Drawing.Size(41, 13);
            this.SymbolLabel.TabIndex = 5;
            this.SymbolLabel.Text = "Symbol";
            // 
            // SymbolDrawBox
            // 
            this.SymbolDrawBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SymbolDrawBox.Location = new System.Drawing.Point(76, 12);
            this.SymbolDrawBox.Name = "SymbolDrawBox";
            this.SymbolDrawBox.Size = new System.Drawing.Size(200, 200);
            this.SymbolDrawBox.TabIndex = 2;
            // 
            // trainSymbolPictureBox
            // 
            this.trainSymbolPictureBox.Location = new System.Drawing.Point(342, 250);
            this.trainSymbolPictureBox.Name = "trainSymbolPictureBox";
            this.trainSymbolPictureBox.Size = new System.Drawing.Size(28, 28);
            this.trainSymbolPictureBox.TabIndex = 7;
            this.trainSymbolPictureBox.TabStop = false;
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 422);
            this.Controls.Add(this.trainSymbolPictureBox);
            this.Controls.Add(this.SymbolLabel);
            this.Controls.Add(this.ClearButtton);
            this.Controls.Add(this.RecognizeButton);
            this.Controls.Add(this.SymbolDrawBox);
            this.Controls.Add(this.errorChart);
            this.Controls.Add(this.TrainButton);
            this.Name = "Index";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.errorChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trainSymbolPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TrainButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart errorChart;
        private ViewComponents.DrawBox SymbolDrawBox;
        private System.Windows.Forms.Button RecognizeButton;
        private System.Windows.Forms.Button ClearButtton;
        private System.Windows.Forms.Label SymbolLabel;
        private System.Windows.Forms.PictureBox trainSymbolPictureBox;
    }
}

