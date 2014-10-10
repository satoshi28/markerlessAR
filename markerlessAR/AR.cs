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
        private String info;
        public void drawAR(Graphics g) 
        {

            //フォントオブジェクトの作成
            Font fnt = new Font("MS UI Gothic", 20);
            //文字列を位置(0,0)、青色で表示
            g.DrawString("これは" + info + "です." , fnt, Brushes.Red, 0, 0);

            //リソースを解放する
            fnt.Dispose();

        }
        public void setData(String info) 
        {
            this.info = info;
        }
    }
}
