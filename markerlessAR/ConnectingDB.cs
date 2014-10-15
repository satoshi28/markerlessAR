using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Data.OleDb;

namespace markerlessAR
{
    class ConnectingDB
    {

        /* Pattern内部の情報をデータベースに追加する */
        public bool loadDB(double latitude, double longitude, ref System.Data.DataTable descTable, ref System.Data.DataTable keyPointTable, ref System.Data.DataTable infoTable)
        {
            //conect
            System.String strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\satoshi\\Documents\\Visual Studio 2012\\DB\\hiraizumi.accdb";
	        //System.String strCommand ="SELECT tb_ロケーション情報.ID, tb_特徴量.[1], tb_特徴量.[2], tb_特徴量.[3], tb_特徴量.[4], tb_特徴量.[5], tb_特徴量.[6], tb_特徴量.[7], tb_特徴量.[8], tb_特徴量.[9], tb_特徴量.[10], tb_特徴量.[11], tb_特徴量.[12], tb_特徴量.[13], tb_特徴量.[14], tb_特徴量.[15], tb_特徴量.[16], tb_特徴量.[17], tb_特徴量.[18], tb_特徴量.[19], tb_特徴量.[20], tb_特徴量.[21], tb_特徴量.[22], tb_特徴量.[23], tb_特徴量.[24], tb_特徴量.[25], tb_特徴量.[26], tb_特徴量.[27], tb_特徴量.[28], tb_特徴量.[29], tb_特徴量.[30], tb_特徴量.[31], tb_特徴量.[32], tb_特徴量.[33], tb_特徴量.[34], tb_特徴量.[35], tb_特徴量.[36], tb_特徴量.[37], tb_特徴量.[38], tb_特徴量.[39], tb_特徴量.[40], tb_特徴量.[41], tb_特徴量.[42], tb_特徴量.[43], tb_特徴量.[44], tb_特徴量.[45], tb_特徴量.[46], tb_特徴量.[47], tb_特徴量.[48], tb_特徴量.[49], tb_特徴量.[50], tb_特徴量.[51], tb_特徴量.[52], tb_特徴量.[53], tb_特徴量.[54], tb_特徴量.[55], tb_特徴量.[56], tb_特徴量.[57], tb_特徴量.[58], tb_特徴量.[59], tb_特徴量.[60], tb_特徴量.[61], tb_特徴量.[62], tb_特徴量.[63], tb_特徴量.[64] FROM (tb_名前 INNER JOIN (tb_ロケーション情報 INNER JOIN tb_特徴量 ON tb_ロケーション情報.[ID] = tb_特徴量.[ID]) ON tb_名前.[name_no] = tb_ロケーション情報.[name_no]) INNER JOIN tb_情報 ON tb_ロケーション情報.[ID] = tb_情報.[ID]";
	        System.String strCommandDesc ="SELECT tb_特徴量.[ID], tb_特徴量.[1], tb_特徴量.[2], tb_特徴量.[3], tb_特徴量.[4], tb_特徴量.[5], tb_特徴量.[6], tb_特徴量.[7], tb_特徴量.[8], tb_特徴量.[9], tb_特徴量.[10], tb_特徴量.[11], tb_特徴量.[12], tb_特徴量.[13], tb_特徴量.[14], tb_特徴量.[15], tb_特徴量.[16], tb_特徴量.[17], tb_特徴量.[18], tb_特徴量.[19], tb_特徴量.[20], tb_特徴量.[21], tb_特徴量.[22], tb_特徴量.[23], tb_特徴量.[24], tb_特徴量.[25], tb_特徴量.[26], tb_特徴量.[27], tb_特徴量.[28], tb_特徴量.[29], tb_特徴量.[30], tb_特徴量.[31], tb_特徴量.[32], tb_特徴量.[33], tb_特徴量.[34], tb_特徴量.[35], tb_特徴量.[36], tb_特徴量.[37], tb_特徴量.[38], tb_特徴量.[39], tb_特徴量.[40], tb_特徴量.[41], tb_特徴量.[42], tb_特徴量.[43], tb_特徴量.[44], tb_特徴量.[45], tb_特徴量.[46], tb_特徴量.[47], tb_特徴量.[48], tb_特徴量.[49], tb_特徴量.[50], tb_特徴量.[51], tb_特徴量.[52], tb_特徴量.[53], tb_特徴量.[54], tb_特徴量.[55], tb_特徴量.[56], tb_特徴量.[57], tb_特徴量.[58], tb_特徴量.[59], tb_特徴量.[60], tb_特徴量.[61], tb_特徴量.[62], tb_特徴量.[63], tb_特徴量.[64] FROM tb_特徴量 ORDER BY tb_特徴量.ID";
	        System.String strCommandKeypoint ="SELECT tb_特徴点.[ID], tb_特徴点.[px], tb_特徴点.[py] FROM tb_特徴点";
            System.String strCommandInfo = "SELECT tb_情報.[ID], tb_情報.[name], tb_情報.[info] FROM tb_情報";
	        
	        //位置情報に基づいて接続文字列の作成
	        //createQueryString(strCommand , latitude, longitude);

	        OleDbConnection conn = new OleDbConnection(strConn);
	        conn.Open();

	        //トランザクションの開始
	        OleDbTransaction transaction = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
	        try{

                //特徴量テーブルの抽出
	        	OleDbDataAdapter descAdapter = new OleDbDataAdapter(strCommandDesc, strConn);
	        	OleDbCommandBuilder descBuilder = new OleDbCommandBuilder(descAdapter);
                descAdapter.Fill(descTable);

                //特徴点テーブルの取得
	        	OleDbDataAdapter keypointAdapter = new OleDbDataAdapter(strCommandKeypoint, strConn);
	        	OleDbCommandBuilder keypointBuilder = new OleDbCommandBuilder(keypointAdapter);
                keypointAdapter.Fill(keyPointTable);

                //infoテーブルの取得
                OleDbDataAdapter infoAdapter = new OleDbDataAdapter(strCommandInfo, strConn);
                OleDbCommandBuilder infoBuilder = new OleDbCommandBuilder(infoAdapter);
                infoAdapter.Fill(infoTable);

	        	//トランザクションをコミットします。
                transaction.Commit();

	        	return true;
	        }
	        catch(System.Exception){
	        	//トランザクションのロールバック
	        	transaction.Rollback();

	        	return false;
	        }
	        finally
	        {
	        	conn.Close();
	        }
       }
        

    }
}
