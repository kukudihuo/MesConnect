using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ClassLib_ParaFile;
using Interface;

namespace ClassLib_Motion
{
    abstract public class OtherIOs :AbsMotion
    {
        //static public Dictionary<string, AbsIo> mapMark2Io = new Dictionary<string, AbsIo>(100);     //EXI0_0 TO AbsIo
        //static public Dictionary<string, string> mapName2Mark = new Dictionary<string, string>(100); //气源压力 TO EXI0_0 
        //static public Dictionary<string, string> mapMark2Name = new Dictionary<string, string>(100);  //EXI0_0 TO 气源压力
        public OtherIOs()
        {
            //this.g_Motion = g_Motion;
            //InitMotion();

        }
        ~OtherIOs()
        {
        }
        public override bool InitMotion(IMotion motion)
        {
            try
            {
                string IniFileName = Publics.GetMotionPath() + "Motion.ini";
                //获取主卡及扩展卡配置文件路径
                string sRead = ParaFileINI.ReadINI("CARD", "OxtCardTotal ", IniFileName);
                AbsMotion.OXT_CARD_TOTAL = Convert.ToInt16(sRead);

                if (AbsMotion.OXT_CARD_TOTAL == 0)
                    return false;

                //AbsMotion.vCard.Add(CreateCard(0, IniFileName));
                //生成扩展卡IO
                for (int iCard = 0; iCard < AbsMotion.OXT_CARD_TOTAL; iCard++)
                {
                    for (int iPort = 0; iPort < AbsMotion.IO_IN_CARD_OXT; iPort++)
                    {
                        string str = "OXI";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        Motion.mapMark2Io[str] = CreateIo(str);

                        str = "OXO";
                        str += iPort.ToString();
                        str += "_";
                        str += iCard.ToString();
                        Motion.mapMark2Io[str] = CreateIo(str);
                    }
                }
                //配置IO名称
                foreach (KeyValuePair<string, AbsIo> kvp in Motion.mapMark2Io)
                {
                    string str = ParaFileINI.ReadINI("IO", kvp.Key, IniFileName);
                    if (str != string.Empty)
                    {
                        string[] sst = str.Trim().Split(' ');
                        kvp.Value.m_sName = sst[0].Trim();
                        if (!Motion.mapMark2Name.ContainsKey(kvp.Key))
                            Motion.mapMark2Name.Add(kvp.Key, kvp.Value.m_sName);
                        if (Motion.mapName2Mark.Count(x => x.Key == kvp.Value.m_sName) == 0)
                            Motion.mapName2Mark.Add(kvp.Value.m_sName, kvp.Key);//注意如果IO显示内容重复时会抛出异常
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
            }
            return true;
        }
        //public void Decode(ref string sio)
        //{//解码，去除多余内容
        //    if (string.IsNullOrEmpty(sio))
        //        return;
        //    int iPos = sio.IndexOf("=");
        //    if (iPos >= 0)
        //    {//用来兼容ini内容
        //        sio = sio.Substring(0, iPos);
        //    }
        //    if (sio[0] == 'O')
        //        Thread.Sleep(1);
        //    if (sio[0] != 'E' && sio[0] != 'D' && sio[0] != 'H' && sio[0] != 'L' && sio[0] != 'O')
        //    {//前面是注释，需要解码
        //        iPos = sio.IndexOf("EX");
        //        if (iPos < 0)
        //        {
        //            iPos = sio.IndexOf("DX");
        //            if (iPos < 0)
        //            {
        //                iPos = sio.IndexOf("HOME");
        //                if (iPos < 0)
        //                {
        //                    iPos = sio.IndexOf("LIMIT");
        //                    if (iPos < 0)
        //                    {
        //                        iPos = sio.IndexOf("OX");
        //                    }
        //                }
        //            }
        //        }
        //        if (iPos > 0)
        //            sio = sio.Substring(iPos, sio.Length - iPos);
        //        else
        //            sio = "";
        //    }

        //    if (sio.Length > 7 && sio[sio.Length - 3] == 'N')
        //        sio = sio.Remove(sio.Length - 3, 1).Insert(6, "-");
        //    else if (sio.Length > 7 && sio[sio.Length - 3] == 'P')
        //        sio = sio.Remove(sio.Length - 3, 1).Insert(6, "+");

        //    if (sio.Length > 0 && sio[sio.Length - 2] != '_')
        //        sio += "_0";//第一张卡缩写时补齐
        //}
        //流程调用获取IO和轴对象
        //public AbsIo GetIo(string sio)
        //{
        //    Decode(ref sio);

        //    if (mapMark2Io.ContainsKey(sio))
        //        return mapMark2Io[sio];
        //    else if (mapName2Mark.ContainsKey(sio) && mapMark2Io.ContainsKey(mapName2Mark[sio]))
        //        return mapMark2Io[mapName2Mark[sio]];  //可以通过字符串解码或二级map的方式来定义注释
        //    else
        //    {
        //        //MSG.Show("错误的IO指令访问：" + sio.ToString(), "严重错误！", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        //        return null;
        //    }
        //}
        public override AbsCard CreateCard(int iCard, string IniFileName) { return null;}
        public override AbsIo CreateIo(string str){ return null;}
        public override AbsAxis CreateAxis(short virtualIndex, short index, string name, double pitch, double divide) { return null; }
    }
}
