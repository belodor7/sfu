using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace lab_5
{
     public class SettingsForm : Form
     {
         public List<double> Masses { get; private set; } = new List<double>();
         public List<double> PositionsX { get; private set; } = new List<double>();
         public List<double> PositionsY { get; private set; } = new List<double>();
         public List<double> Vx { get; private set; } = new List<double>();
         public List<double> Vy { get; private set; } = new List<double>();

         List<TextBox> massBoxes = new List<TextBox>();
         List<TextBox> xBoxes = new List<TextBox>();
         List<TextBox> yBoxes = new List<TextBox>();
         List<TextBox> vxBoxes = new List<TextBox>();
         List<TextBox> vyBoxes = new List<TextBox>();

         public SettingsForm(List<Body> bodies)
         {
             this.Text = "Настройки";
             this.Size = new Size(540,320);
             this.FormBorderStyle = FormBorderStyle.FixedDialog;
             this.StartPosition = FormStartPosition.CenterParent;

             var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
             this.Controls.Add(panel);

             // add header labels for columns
             var lblIndex = new Label { Text = "#", Location = new Point(10,10), Size = new Size(30,20) };
             var lblMass = new Label { Text = "Масса (0.1..5)", Location = new Point(50,10), Size = new Size(80,20) };
             var lblX = new Label { Text = "X", Location = new Point(140,10), Size = new Size(50,20) };
             var lblY = new Label { Text = "Y", Location = new Point(200,10), Size = new Size(50,20) };
             var lblVx = new Label { Text = "Vx (-2..2)", Location = new Point(260,10), Size = new Size(70,20) };
             var lblVy = new Label { Text = "Vy (-2..2)", Location = new Point(340,10), Size = new Size(70,20) };
             panel.Controls.AddRange(new Control[] { lblIndex, lblMass, lblX, lblY, lblVx, lblVy });

             int y =40;
             for (int i =0; i < bodies.Count; i++)
             {
                 var b = bodies[i];
                 var lbl = new Label { Text = $"{i}", Location = new Point(10, y), Size = new Size(30,20) };
                 panel.Controls.Add(lbl);

                 var tbM = new TextBox { Text = b.Mass.ToString("F2"), Location = new Point(50, y), Width =80 };
                 var tbX = new TextBox { Text = b.X.ToString("F2"), Location = new Point(140, y), Width =60 };
                 var tbY = new TextBox { Text = b.Y.ToString("F2"), Location = new Point(200, y), Width =60 };
                 var tbVx = new TextBox { Text = b.Vx.ToString("F2"), Location = new Point(260, y), Width =60 };
                 var tbVy = new TextBox { Text = b.Vy.ToString("F2"), Location = new Point(340, y), Width =60 };
                 panel.Controls.AddRange(new Control[] { tbM, tbX, tbY, tbVx, tbVy });

                 massBoxes.Add(tbM);
                 xBoxes.Add(tbX);
                 yBoxes.Add(tbY);
                 vxBoxes.Add(tbVx);
                 vyBoxes.Add(tbVy);

                 y +=30;
             }

             var btnSave = new Button { Text = "Сохранить", Location = new Point(70, y +10) };
             var btnCancel = new Button { Text = "Отменить", Location = new Point(170, y +10) };
             panel.Controls.Add(btnSave);
             panel.Controls.Add(btnCancel);

             btnSave.Click += (s, e) =>
             {
                var err = Parse();
                if (err == null) { this.DialogResult = DialogResult.OK; this.Close(); }
                else { MessageBox.Show(this, err, "Ошибка в данных", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
             };
             btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
         }

         // Parse returns null on success, otherwise an error message
         string Parse()
         {
             Masses.Clear(); PositionsX.Clear(); PositionsY.Clear(); Vx.Clear(); Vy.Clear();
             for (int i =0; i < massBoxes.Count; i++)
             {
                 if (!double.TryParse(massBoxes[i].Text, out double m)) return $"Масса тела {i} неверна";
                 if (!double.TryParse(xBoxes[i].Text, out double x)) return $"Координата X тела {i} неверна";
                 if (!double.TryParse(yBoxes[i].Text, out double y)) return $"Координата Y тела {i} неверна";
                 if (!double.TryParse(vxBoxes[i].Text, out double vx)) return $"Скорость Vx тела {i} неверна";
                 if (!double.TryParse(vyBoxes[i].Text, out double vy)) return $"Скорость Vy тела {i} неверна";
                 // validate ranges
                 if (m <0.1 || m >5.0) return $"Масса тела {i} должна быть в диапазоне0.1 ..5.0";
                 if (vx < -2.0 || vx >2.0) return $"Скорость Vx тела {i} должна быть в диапазоне -2 ..2";
                 if (vy < -2.0 || vy >2.0) return $"Скорость Vy тела {i} должна быть в диапазоне -2 ..2";
                 Masses.Add(m); PositionsX.Add(x); PositionsY.Add(y); Vx.Add(vx); Vy.Add(vy);
             }
             return null;
         }
     }
}
