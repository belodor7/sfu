namespace lab_5
{
     partial class Form1
     {
         private System.ComponentModel.IContainer components = null;
         protected override void Dispose(bool disposing)
         {
             if (disposing && (components != null))
             {
                components.Dispose();
             }
             base.Dispose(disposing);
         }

                #region Windows Form Designer generated code
                private void InitializeComponent()
                {
                    panelScene = new DoubleBufferedPanel();
                    comboBoxCount = new ComboBox();
                    checkBoxTrails = new CheckBox();
                    buttonStart = new Button();
                    buttonStop = new Button();
                    buttonSettings = new Button();
                    numericUpDownSpeed = new NumericUpDown();
                    labelSpeed = new Label();
                    ((System.ComponentModel.ISupportInitialize)numericUpDownSpeed).BeginInit();
                    SuspendLayout();
                    // 
                    // panelScene
                    // 
                    panelScene.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    panelScene.Location = new Point(12, 45);
                    panelScene.Name = "panelScene";
                    panelScene.Size = new Size(760, 400);
                    panelScene.TabIndex = 0;
                    // 
                    // comboBoxCount
                    // 
                    comboBoxCount.FormattingEnabled = true;
                    comboBoxCount.Location = new Point(12, 12);
                    comboBoxCount.Name = "comboBoxCount";
                    comboBoxCount.Size = new Size(60, 23);
                    comboBoxCount.TabIndex = 1;
                    // 
                    // checkBoxTrails
                    // 
                    checkBoxTrails.AutoSize = true;
                    checkBoxTrails.Location = new Point(90, 14);
                    checkBoxTrails.Name = "checkBoxTrails";
                    checkBoxTrails.Size = new Size(162, 19);
                    checkBoxTrails.TabIndex = 2;
                    checkBoxTrails.Text = "Отображать траекторию";
                    checkBoxTrails.UseVisualStyleBackColor = true;
                    // 
                    // buttonStart
                    // 
                    buttonStart.Location = new Point(270, 12);
                    buttonStart.Name = "buttonStart";
                    buttonStart.Size = new Size(75, 23);
                    buttonStart.TabIndex = 3;
                    buttonStart.Text = "Старт";
                    buttonStart.UseVisualStyleBackColor = true;
                    // 
                    // buttonStop
                    // 
                    buttonStop.Location = new Point(350, 12);
                    buttonStop.Name = "buttonStop";
                    buttonStop.Size = new Size(75, 23);
                    buttonStop.TabIndex = 4;
                    buttonStop.Text = "Стоп";
                    buttonStop.UseVisualStyleBackColor = true;
                    // 
                    // buttonSettings
                    // 
                    buttonSettings.Location = new Point(430, 12);
                    buttonSettings.Name = "buttonSettings";
                    buttonSettings.Size = new Size(75, 23);
                    buttonSettings.TabIndex = 5;
                    buttonSettings.Text = "Настройки";
                    buttonSettings.UseVisualStyleBackColor = true;
                    // 
                    // numericUpDownSpeed
                    // 
                    numericUpDownSpeed.Location = new Point(722, 10);
                    numericUpDownSpeed.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
                    numericUpDownSpeed.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
                    numericUpDownSpeed.Name = "numericUpDownSpeed";
                    numericUpDownSpeed.Size = new Size(50, 23);
                    numericUpDownSpeed.TabIndex = 6;
                    numericUpDownSpeed.Value = new decimal(new int[] { 5, 0, 0, 0 });
                    // 
                    // labelSpeed
                    // 
                    labelSpeed.AutoSize = true;
                    labelSpeed.Location = new Point(657, 14);
                    labelSpeed.Name = "labelSpeed";
                    labelSpeed.Size = new Size(59, 15);
                    labelSpeed.TabIndex = 7;
                    labelSpeed.Text = "Скорость";
                    // 
                    // Form1
                    // 
                    AutoScaleDimensions = new SizeF(7F, 15F);
                    AutoScaleMode = AutoScaleMode.Font;
                    ClientSize = new Size(784, 461);
                    Controls.Add(labelSpeed);
                    Controls.Add(numericUpDownSpeed);
                    Controls.Add(buttonSettings);
                    Controls.Add(buttonStop);
                    Controls.Add(buttonStart);
                    Controls.Add(checkBoxTrails);
                    Controls.Add(comboBoxCount);
                    Controls.Add(panelScene);
                    Name = "Form1";
                    Text = "Form1";
                    ((System.ComponentModel.ISupportInitialize)numericUpDownSpeed).EndInit();
                    ResumeLayout(false);
                    PerformLayout();
                }
                #endregion

         private DoubleBufferedPanel panelScene;
         private System.Windows.Forms.ComboBox comboBoxCount;
         private System.Windows.Forms.CheckBox checkBoxTrails;
         private System.Windows.Forms.Button buttonStart;
         private System.Windows.Forms.Button buttonStop;
         private System.Windows.Forms.Button buttonSettings;
         private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
         private System.Windows.Forms.Label labelSpeed;
     }
}
