using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using AForge.Video;
using AForge.Video.DirectShow;
/*
 カメラ初期化用クラス
 */
namespace markerlessAR
{
    class VideoCapture
    {
        //カメラ画像取得用変数

        private VideoCapabilities[] videoCapabilities;      //ビデオデバイスの提供する機能一覧を配列に格納

        // Connect to camera
        public void Connect(out FilterInfoCollection videoDevices,out VideoCaptureDevice videoSource)
        {
            // collect cameras list
            try
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    throw new ApplicationException();
                }
            }
            catch (ApplicationException)
            {
                videoDevices = null;
            }

            // connect to camera
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoCapabilities = videoSource.VideoCapabilities;
            /*
            //提供されているフレームサイズをコンボボックスのアイテムに追加
            foreach (VideoCapabilities capabilty in videoCapabilities)
            {
                cmb_frameSize.Items.Add(string.Format("{0} x {1}", capabilty.FrameSize.Width, capabilty.FrameSize.Height));
            }
            */
            //コンボボックスで選択されているのフレームサイズに設定
            videoSource.VideoResolution = videoCapabilities[0];

            
            //動画の取得
            videoSource.Start();
        }

        public void cameraClose(ref VideoCaptureDevice videoSource) 
        {
            if (videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource = null;
            }
        }
    }
}
