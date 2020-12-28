#define VERSION35
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLib_ParaFile
{
    public partial class Publics// : UserControl
    {
#if VERSION35
        public const string DATA_ROW = "(type nvarchar(254), name nvarchar(254), parent nvarchar(254), X float, Y float, W float, Z float, PhotoIndex int,RowIndex int, ColIndex int, CroodIndex int, CF1 int, CF4 int, CF6 int, CFx int, ArrayIndex1 int, ArrayIndex2 int, ArrayIndex3 int, ArrayIndex4 int, NameIndex nvarchar(254), Instructions nvarchar(254), ID uniqueidentifier PRIMARY KEY ROWGUIDCOL)";
#else 
        public const string DATA_ROW = "(type nvarchar(254), name nvarchar(254), parent nvarchar(254), X float, Y float, W float, Z float, CF1 int, CF4 int, CF6 int, CFx int, NameIndex nvarchar(254), Instructions nvarchar(254), ID uniqueidentifier PRIMARY KEY ROWGUIDCOL)";
#endif
    }
    /// <summary>
    /// 索引信息说明：
    /// 通常：PhotoIndex = 拍照索引;;RowIndex = 行索引;;ColIndex = 列索引;;CroodIndex = 当前矩阵索引 
    /// StdCroodUp类内无特殊索引
    /// StdCroodUpHom类:
    /// m_vPhotoCount["PosCount1"]索引：PhotoIndex = 矩阵总数;;RowIndex = 行数;;ColIndex = 列数;;CroodIndex = 下吸头数
    /// m_vPhotoCount["PosCount"+i.ToString()]索引：PhotoIndex = 起始索引号;;RowIndex = 行数;;ColIndex = 列数;;CroodIndex = 吸头索引
    /// StdCroodDown类:
    /// m_vOffset["GlobalOffset"]索引：PhotoIndex = 吸料列数;;RowIndex = 吸料行数;;ColIndex = 是否启用吸料拍照;;CroodIndex = 是否启用贴合拍照
    /// </summary>
    public struct PosXYWZ : ICloneable
    {//结构是按值拷贝，不需要Clone
        //点位
        public double X;
        public double Y;
        public double W;
        public double Z;
        //索引信息
#if VERSION35
        public int PhotoIndex;
        public int RowIndex;
        public int ColIndex;
        public int CroodIndex;
#endif
        //姿态
        public int CF1;
        public int CF4;
        public int CF6;
        public int CFx;
        //扩展索引
#if VERSION35
        public int ArrayIndex1;
        public int ArrayIndex2;
        public int ArrayIndex3;
        public int ArrayIndex4;
#endif

        public string NameIndex;          //名称
        public string Instructions;  //描述
        public PosXYWZ(double x=0, double y = 0, double w = 0, double z = 0, string name = "", string instructions = "",
            int photoIndex = -1, int rowIndex = -1,int colIndex = -1, int croodIndex = -1,
            int cf1 = -1, int cf4 = -1, int cf6 = -1, int cfx = -1,
            int arrayIndex1 = -1, int arrayIndex2 = -1, int arrayIndex3 = -1, int arrayIndex4 = -1, bool roudpos = false, int DecimalLen = 6)
        {
            X = x;
            Y = y;
            W = w;
            Z = z;
            CF1 = cf1;
            CF4 = cf4;
            CF6 = cf6;
            CFx = cfx;

#if VERSION35
            PhotoIndex = photoIndex;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            CroodIndex = croodIndex;
            ArrayIndex1 = arrayIndex1;
            ArrayIndex2 = arrayIndex2;
            ArrayIndex3 = arrayIndex3;
            ArrayIndex4 = arrayIndex4;
#endif

            NameIndex = name;
            Instructions = instructions;

            ChangePos(x, y, w, z, roudpos, DecimalLen);
        }

        public PosXYWZ(bool bNone)
        {
            X = -1;
            Y = -1;
            W = -1;
            Z = -1;
            CF1 = -1;
            CF4 = -1;
            CF6 = -1;
            CFx = -1;
            #if VERSION35
            PhotoIndex = -1;
            RowIndex = -1;
            ColIndex = -1;
            CroodIndex = -1;
            ArrayIndex1 = -1;
            ArrayIndex2 = -1;
            ArrayIndex3 = -1;
            ArrayIndex4 = -1;
            #endif
            NameIndex = "none";
            Instructions = "空点位";
        }

        public PosXYWZ(string sValuePos)
        {
            X = 0;
            Y = 0;
            W = 0;
            Z = 0;
            CF1 = -1;
            CF4 = -1;
            CF6 = -1;
            CFx = -1;
#if VERSION35
            PhotoIndex = -1;
            RowIndex = -1;
            ColIndex = -1;
            CroodIndex = -1;
            ArrayIndex1 = -1;
            ArrayIndex2 = -1;
            ArrayIndex3 = -1;
            ArrayIndex4 = -1;
#endif
            NameIndex = "";
            Instructions = "";

            if (!string.IsNullOrEmpty(sValuePos))
            {
                string[] strs = sValuePos.Split(';');
                for (int i = 0; i < strs.Length - 1; i++)
                {
                    if (strs[i].Contains("X"))
                    {
                        X = Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Y"))
                    {
                        Y = Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("W"))
                    {
                        W = Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Z"))
                    {
                        Z = Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("N"))
                    {
                        NameIndex = strs[i].Substring(2);
                    }
                    else if (strs[i].Contains("I"))
                    {
                        Instructions = strs[i].Substring(2);
                    }
                    else if (strs[i].Contains("Fo"))
                    {
                        CF1 = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Ff"))
                    {
                        CF4 = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Fs"))
                    {
                        CF6 = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Fx"))
                    {
                        CFx = (int)Publics.GetFristNum(ref strs[i]);
                    }
#if VERSION35
                    else if (strs[i].Contains("Ph"))
                    {
                        PhotoIndex = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Ro"))
                    {
                        RowIndex = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Co"))
                    {
                        ColIndex = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Cr"))
                    {
                        CroodIndex = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Aa"))
                    {
                        ArrayIndex1 = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Ab"))
                    {
                        ArrayIndex2 = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Ac"))
                    {
                        ArrayIndex3 = (int)Publics.GetFristNum(ref strs[i]);
                    }
                    else if (strs[i].Contains("Ad"))
                    {
                        ArrayIndex4 = (int)Publics.GetFristNum(ref strs[i]);
                    }
#endif
                }
            }
        }

        public override string ToString()
        { 
            Round();
            return "X:" + X + ";Y:" + Y + ";W:" + W + ";Z:" + Z + ";N:" + NameIndex + ";I:" + Instructions +
#if VERSION35
                   ";Ph:" + PhotoIndex + ";Ro:" + RowIndex + ";Co:" + ColIndex + ";Cr:" + CroodIndex +
#endif
                    ";Fo:" + CF1 + ";Ff:" + CF4 + ";Fs:" + CF6 + ";Fx:" + CFx
#if VERSION35
                    //长度会超过100导致数据库出错，因为数据库的value长度设定为100
                    + ";Aa:" + ArrayIndex1 + ";Ab:" + ArrayIndex2 + ";Ac:" + ArrayIndex3 + ";Ad:" + ArrayIndex4
#endif
;
        }
        //浅拷贝
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //深拷贝
        public object DeepClone(bool roudpos = false, int DecimalLen = 6)
        {
#if VERSION35
            return new PosXYWZ(X, Y, W, Z, NameIndex, Instructions, 
                PhotoIndex, RowIndex, ColIndex, CroodIndex, CF1, CF4, CF6, CFx,
                ArrayIndex1, ArrayIndex2, ArrayIndex3, ArrayIndex4, roudpos, DecimalLen);
#else
            return new PosXYWZ(X, Y, W, Z, NameIndex, Instructions,
                -1, -1, -1, -1, CF1, CF4, CF6, CFx,
                -1, -1, -1, -1, roudpos, DecimalLen);
#endif
        }
        public bool NoValue()
        {
            try
            {
                if (X == -1 && Y == -1 && W == -1 && Z == -1)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
        public bool ZeroValue()
        {
            try
            {
                if (X == 0 && Y == 0 && W == 0 && Z == 0)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
        public bool NullValue()
        {
            try
            {
                if (NameIndex == null && Instructions == null)
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
        public bool IsNotSetValue()
        {
            return (NoValue() || ZeroValue() || NullValue());
        }
        public bool IsEqual(PosXYWZ other)
        {            
            return (X == other.X && Y == other.Y && W == other.W && Z == other.Z &&
                CF1 == other.CF1 && CF4 == other.CF4 && CF6 == other.CF6 && CFx == other.CFx
#if VERSION35 
                && PhotoIndex == other.PhotoIndex && RowIndex == other.RowIndex && ColIndex == other.ColIndex && CroodIndex == other.CroodIndex &&
                ArrayIndex1 == other.ArrayIndex1 && ArrayIndex2 == other.ArrayIndex2 && ArrayIndex3 == other.ArrayIndex3 && ArrayIndex4 == other.ArrayIndex4
#endif
                );
        }
        public void SavePos(string section, string posPath)
        {
            ParaFileINI.WriteINI(section, "X", X.ToString(), posPath);
            ParaFileINI.WriteINI(section, "Y", Y.ToString(), posPath);
            ParaFileINI.WriteINI(section, "W", W.ToString(), posPath);
            ParaFileINI.WriteINI(section, "Z", Z.ToString(), posPath);
        }
        public void ReadPos(string section, string posPath)
        {
            X = double.Parse(ParaFileINI.ReadINI(section, "X", posPath));
            Y = double.Parse(ParaFileINI.ReadINI(section, "Y", posPath));
            W = double.Parse(ParaFileINI.ReadINI(section, "W", posPath));
            Z = double.Parse(ParaFileINI.ReadINI(section, "Z", posPath));
        }

        public PosXYWZ Round(int DecimalLen = 3)
        {
            X = Math.Round(X, DecimalLen);
            Y = Math.Round(Y, DecimalLen);
            W = Math.Round(W, DecimalLen);
            Z = Math.Round(Z, DecimalLen);
            return this;
        }

        public PosXYWZ ChangePos(double x, double y, double w, double z, bool roudpos = true, int DecimalLen = 3)
        {
            if (!roudpos)
            {
                X = x;
                Y = y;
                W = w;
                Z = z;
            }
            else
            {
                X = Math.Round(x, DecimalLen);
                Y = Math.Round(y, DecimalLen);
                W = Math.Round(w, DecimalLen);
                Z = Math.Round(z, DecimalLen);
            }
            return this;
        }

        public PosXYWZ ChangeXY(double x, double y, bool roudpos = true, int DecimalLen = 3)
        {
            if (!roudpos)
            {
                X = x;
                Y = y;
            }
            else
            {
                X = Math.Round(x, DecimalLen);
                Y = Math.Round(y, DecimalLen);
            }
            return this;
        }

        public PosXYWZ ChangeW(double w, bool roudpos = true, int DecimalLen = 3)
        {
            if (!roudpos)
            {
                W = w;
            }
            else
            {
                W = Math.Round(w, DecimalLen);
            }
            return this;
        }

        public PosXYWZ ChangeZ(double z, bool roudpos = true, int DecimalLen = 3)
        {
            if (!roudpos)
            {
                Z = z;
            }
            else
            {
                Z = Math.Round(z, DecimalLen);
            }
            return this;
        }

        public PosXYWZ ChangeCF(int cf1, int cf4, int cf6, int cfx)
        {
            CF1 = cf1;
            CF4 = cf4;
            CF6 = cf6;
            CFx = cfx;
            return this;
        }

        public PosXYWZ ChangeCF1(int value)
        {
            CF1 = value;
            return this;
        }
        public PosXYWZ ChangeCF4(int value)
        {
            CF4 = value;
            return this;
        }
        public PosXYWZ ChangeCF6(int value)
        {
            CF6 = value;
            return this;
        }
        public PosXYWZ ChangeCFx(int value)
        {
            CFx = value;
            return this;
        }

#if VERSION35
        public PosXYWZ ChangeIndexs(int photoIndex, int rowIndex, int colIndex, int croodIndex)
        {
            PhotoIndex = photoIndex;
            RowIndex = rowIndex;
            ColIndex = colIndex;
            CroodIndex = croodIndex;
            return this;
        }
        public PosXYWZ ChangePhotoIndex(int value)
        {
            PhotoIndex = value;
            return this;
        }
        public PosXYWZ ChangeRowIndex(int value)
        {
            RowIndex = value;
            return this;
        }
        public PosXYWZ ChangeColIndex(int value)
        {
            ColIndex = value;
            return this;
        }
        public PosXYWZ ChangeCroodIndex(int value)
        {
            CroodIndex = value;
            return this;
        }
        public int GetPhotoIndex()
        {
            return PhotoIndex;
        }
        public int GetRowIndex()
        {
            return RowIndex;
        }
        public int GetColIndex()
        {
            return ColIndex;
        }
        public int GetCroodIndex()
        {
            return CroodIndex;
        }
        public PosXYWZ ChangeArrayIndexs(int arrayIndex1, int arrayIndex2, int arrayIndex3, int arrayIndex4)
        {
            ArrayIndex1 = arrayIndex1;
            ArrayIndex2 = arrayIndex2;
            ArrayIndex3 = arrayIndex3;
            ArrayIndex4 = arrayIndex4;
            return this;
        }
        public PosXYWZ ChangeArrayIndex1(int value)
        {
            ArrayIndex1 = value;
            return this;
        }
        public PosXYWZ ChangeArrayIndex2(int value)
        {
            ArrayIndex2 = value;
            return this;
        }
        public PosXYWZ ChangeArrayIndex3(int value)
        {
            ArrayIndex3 = value;
            return this;
        }
        public PosXYWZ ChangeArrayIndex4(int value)
        {
            ArrayIndex4 = value;
            return this;
        }
#else
        public PosXYWZ ChangeIndexs(int photoIndex, int rowIndex, int colIndex, int croodIndex)
        {
            return ChangeCF(photoIndex, rowIndex, colIndex, croodIndex);
        }
        public PosXYWZ ChangePhotoIndex(int value)
        {
            return ChangeCF1(value);

        }        
        public PosXYWZ ChangeRowIndex(int value)
        {
            return ChangeCF4(value);

        }
        public PosXYWZ ChangeColIndex(int value)
        {
            return ChangeCF6(value);

        }
        public PosXYWZ ChangeCroodIndex(int value)
        {
            return ChangeCFx(value);
        }

        public int PhotoIndex
        {
            get
            {
                return CF1;
            }
            set
            {
                CF1 = value;
            }
        }
        public int RowIndex
        {
            get
            {
                return CF4;
            }
            set
            {
                CF4 = value;
            }
        }
        public int ColIndex
        {
            get
            {
                return CF6;
            }
            set
            {
                CF6 = value;
            }
        }
        public int CroodIndex
        {
            get
            {
                return CFx;
            }
            set
            {
                CFx = value;
            }
        }
        public int GetPhotoIndex()
        {
            return CF1;
        }
        public int GetRowIndex()
        {
            return CF4;
        }
        public int GetColIndex()
        {
            return CF6;
        }
        public int GetCroodIndex()
        {
            return CFx;
        }
#endif
        public void SetMemberVaule(string sname, object VariableValue)
        { 
            if (sname == "X") 
                X = Convert.ToDouble(VariableValue);
                else if (sname == "Y") 
                Y = Convert.ToDouble(VariableValue);
            else if (sname == "W")
                W = Convert.ToDouble(VariableValue);
            else if (sname == "Z") 
                Z = Convert.ToDouble(VariableValue);
            else if (sname == "CF1") 
                CF1 = Convert.ToInt32(VariableValue);
            else if (sname == "CF4") 
                CF4 = Convert.ToInt32(VariableValue);
            else if (sname == "CF6") 
                CF6 = Convert.ToInt32(VariableValue);
            else if (sname == "CFx") 
                CFx = Convert.ToInt32(VariableValue);

#if VERSION35 
            else if (sname == "PhotoIndex")
                PhotoIndex = Convert.ToInt32(VariableValue);
            else if (sname == "RowIndex")
                RowIndex = Convert.ToInt32(VariableValue);
            else if (sname == "ColIndex")
                ColIndex = Convert.ToInt32(VariableValue);
            else if (sname == "CroodIndex")
                CroodIndex = Convert.ToInt32(VariableValue);
            else if (sname == "ArrayIndex1") 
                ArrayIndex1 = Convert.ToInt32(VariableValue);
            else if (sname == "ArrayIndex2") 
                ArrayIndex2 = Convert.ToInt32(VariableValue);
            else if (sname == "ArrayIndex3") 
                ArrayIndex3 = Convert.ToInt32(VariableValue);
            else if (sname == "ArrayIndex4")
                ArrayIndex4 = Convert.ToInt32(VariableValue);
#endif

            else if (sname == "NameIndex")
                NameIndex = VariableValue.ToString();
            else if (sname == "Instructions")
                Instructions = VariableValue.ToString();
        }
    }

    public class PosClass : ICloneable
    {//结构是按值拷贝，不需要Clone
        //点位
        public double X;
        public double Y;
        public double W;
        public double Z;

        //姿态
        public int CF1;
        public int CF4;
        public int CF6;
        public int CFx;
        public string NameIndex;          //名称
        public string Instructions;  //描述

        public PosClass()
        {
            X = 0;
            Y = 0;
            W = 0;
            Z = 0;
            CF1 = -1;
            CF4 = -1;
            CF6 = -1;
            CFx = -1;
            NameIndex = "";
            Instructions = "";
        }

        public PosClass(double x, double y, double w, double z, string name = "", string instructions = "", int cf1 = -1, int cf4 = -1, int cf6 = -1, int cfx = -1)
        {
            X = x;
            Y = y;
            W = w;
            Z = z;
            CF1 = cf1;
            CF4 = cf4;
            CF6 = cf6;
            CFx = cfx;
            NameIndex = name;
            Instructions = instructions;
        }
        //浅拷贝
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        //深拷贝
        public object DeepClone()
        {
            return new PosClass(X, Y, W, Z, NameIndex, Instructions, CF1, CF4, CF6, CFx);
        }

        public void Round(int DecimalLen = 3)
        {
            X = Math.Round(X, DecimalLen);
            Y = Math.Round(Y, DecimalLen);
            W = Math.Round(W, DecimalLen);
            Z = Math.Round(Z, DecimalLen);
        }
    }

    public class StdPos
    {
        public static PosXYWZ posXYWZ(double x, double y, double w, double z, string name = "", string instructions = "", int cf1 = -1, int cf4 = -1, int cf6 = -1, int cfx = -1)
        {
            return new PosXYWZ(x, y, w, z, name, instructions, cf1, cf4, cf6, cfx);
        }

        public static void CopyPartical(PosXYWZ src, ref PosXYWZ dec)
        {
            dec.X = src.X;
            dec.Y = src.Y;
            dec.W = src.W;
            dec.Z = src.Z;
        }
    }
}
