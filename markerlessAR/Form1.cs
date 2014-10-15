using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Video;
using AForge.Video.DirectShow;

using OpenCvSharp;
using OpenCvSharp.CPlusPlus;

using objectRecognition;

namespace markerlessAR
{
    public partial class Form1 : Form
    {
        //カメラ画像取得用
        VideoCapture videoCapture = new VideoCapture();
        private FilterInfoCollection videoDevices;          //接続されている全てのビデオデバイス情報を格納する変数
        private VideoCaptureDevice videoSource;      //使用するビデオデバイス
        Bitmap pictureImage;
        //AR
        AR ar = new AR();
        bool initilizeFlag = false;
        bool arFlag = false;
        int ID;
        String[] info = new String[2];

        //特徴量の取得
        ConnectingDB db = new ConnectingDB();
        //取得用データテーブル
        System.Data.DataTable descTable = new System.Data.DataTable("data");
        System.Data.DataTable keyPointTable = new System.Data.DataTable("data");
        System.Data.DataTable infoTable = new System.Data.DataTable("data");
        WrappedObjectRecognition recognition = new WrappedObjectRecognition();

        System.Windows.Forms.Timer timer = new Timer();


        public Form1()
        {
            InitializeComponent();
           

            videoCapture.Connect(out videoDevices, out videoSource);
            //イベントの登録
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            
           
            //DBから物体情報の取得
            db.loadDB(1, 1, ref descTable, ref keyPointTable, ref infoTable);
            //物体情報を保存
            initilizeFlag = recognition.setData(descTable, keyPointTable);

        }

        //新しいフレームが来た時
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureImage = (Bitmap)eventArgs.Frame;
            
            //認識処理など
            ///
            //描画先とするImageオブジェクトを作成する
            if (initilizeFlag == true)
            {
                
                if (arFlag == true)
                {
                    Bitmap cameraImage = (Bitmap)pictureImage.Clone();

                    ID = recognition.findObject(cameraImage);
                    info = searchInfo(ID, infoTable);
                    arFlag = false;

                    cameraImage.Dispose();
                }

                //ImageオブジェクトのGraphicsオブジェクトを作成する
                Graphics g = Graphics.FromImage(pictureImage);

                
                //物体IDを設定
                ar.setData(info);
                //描画処理
                ar.drawAR(g);                                                           //ARの描画

                g.Dispose();

                //PictureBox1に表示する
                pictureBox1.Image = pictureImage;
               // pictureImage.Dispose();
            }
            else //情報がない場合はカメラ画像を表示
            {
                pictureBox1.Image = pictureImage;
            }
            

            //一秒間（1000ミリ秒）停止する
            System.Threading.Thread.Sleep(20);
        }

        //ウィンドウを閉じる時
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            videoCapture.cameraClose(ref videoSource);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (initilizeFlag == true)
            {
                arFlag = true;
               // int ID = recognition.findObject(cameraImage);
               // info = searchInfo(ID, infoTable);
            }
        }

        private String[] searchInfo(int ID, System.Data.DataTable table)
        {

            String[] info = new String[2];

            if (ID > 0)
            {
                var rows = (
                from row in table.AsEnumerable()
                let column = row.Field<int>("ID")
                where column == ID
                select row
                ).ToArray();

                info[0] = (String)rows[0]["name"];
                info[1] = (String)rows[0]["info"];
            }
            else 
            {
                info[1] = "見つかりませんでした";
            }
            return info;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            arFlag = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

    }
}
