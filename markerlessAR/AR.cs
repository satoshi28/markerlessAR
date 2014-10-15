using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace markerlessAR
{
    class AR
    {
        private String[] info;
        Rectangle rectName = new Rectangle(20, 20, 300, 30);
        Rectangle rectInfo = new Rectangle(5, 300, 630, 175);

        public void drawAR(Graphics g) 
        {
            if (info[1] != null)
            {
                // Create pen.
                Pen blackPen = new Pen(Color.Black, 3);
                SolidBrush opaqueBrush = new SolidBrush(Color.FromArgb(128, 51, 153, 255));
                
                // Draw rectangle to screen.
                //g.DrawRectangle(blackPen, rectInfo);
                g.FillRectangle(opaqueBrush, rectInfo);

                //フォントオブジェクトの作成
                Font fnt = new Font("メイリオ", 11);
                //文字列を位置(0,0)、青色で表示
                g.DrawString("これは" + info[0] + "です.", fnt, Brushes.Red, rectName);
                g.DrawString(info[1], fnt, Brushes.Red, rectInfo);

                //リソースを解放する
                fnt.Dispose();
            }
        }
        public void setData(String[] info) 
        {
            this.info = info;
        }
    }
}
