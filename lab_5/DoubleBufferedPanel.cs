using System.Windows.Forms;

namespace lab_5
{
     public class DoubleBufferedPanel : Panel
     {
         public DoubleBufferedPanel()
         {
             this.DoubleBuffered = true;
             this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
         }
     }
}
