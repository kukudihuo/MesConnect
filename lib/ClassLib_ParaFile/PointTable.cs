using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLib_ParaFile
{
    public class PointTable
    {
        static public void TableFromPoints_3(double LeftTop_X, double LeftTop_Y,
                                             double RightTop_X, double RightTop_Y,
                                             double LeftBottom_X, double LeftBottom_Y,
                                             int rowCounter, int columnCounter,
                                             ref ArrayList arrayListGenerate_X, ref ArrayList arrayListGenerate_Y,
                                             ref ArrayList arrayListGenerate_R, ref ArrayList arrayListGenerate_C)
        {
            arrayListGenerate_X.Clear();
            arrayListGenerate_Y.Clear();
            arrayListGenerate_R.Clear();
            arrayListGenerate_C.Clear();
            if (rowCounter <= 0 | columnCounter <= 0)
                return;
            if (rowCounter < 2 && columnCounter < 2)
            {
                arrayListGenerate_X.Add(LeftTop_X);
                arrayListGenerate_Y.Add(LeftTop_Y);
                arrayListGenerate_R.Add(1);
                arrayListGenerate_C.Add(1);
            }
            else
            {
                if (rowCounter < 2)
                {
                    for (int i = 0; i <= columnCounter - 1; i++)
                    {
                        double X = LeftTop_X + i * ((RightTop_X - LeftTop_X) / (columnCounter - 1.0));
                        double Y = LeftTop_Y + i * ((RightTop_Y - LeftTop_Y) / (columnCounter - 1.0));
                        arrayListGenerate_X.Add(X);
                        arrayListGenerate_Y.Add(Y);
                        arrayListGenerate_R.Add(1);
                        arrayListGenerate_C.Add(i + 1);
                    }
                }
                else
                {
                    if (columnCounter < 2)
                    {
                        for (int j = 0; j <= rowCounter - 1; j++)
                        {
                            double X = LeftTop_X + j * ((LeftBottom_X - LeftTop_X) / (rowCounter - 1.0));
                            double Y = LeftTop_Y + j * ((LeftBottom_Y - LeftTop_Y) / (rowCounter - 1.0));
                            arrayListGenerate_X.Add(X);
                            arrayListGenerate_Y.Add(Y);
                            arrayListGenerate_R.Add(j + 1);
                            arrayListGenerate_C.Add(1);
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= rowCounter - 1; i++)
                        {
                            for (int j = 0; j <= columnCounter - 1; j++)
                            {
                                double X = LeftTop_X + i * (LeftBottom_X - LeftTop_X) / (rowCounter - 1) + j * ((RightTop_X - LeftTop_X) / (columnCounter - 1));
                                double Y = LeftTop_Y + i * ((LeftBottom_Y - LeftTop_Y) / (rowCounter - 1)) + j * ((RightTop_Y - LeftTop_Y) / (columnCounter - 1));
                                arrayListGenerate_X.Add(X);
                                arrayListGenerate_Y.Add(Y);
                                arrayListGenerate_R.Add(i + 1);
                                arrayListGenerate_C.Add(j + 1);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 生成料盘拍照位表列
        /// </summary>
        /// <remarks></remarks>
        static public void TableFromPoints_3(double LeftTop_X, double LeftTop_Y,
                                            double RightTop_X, double RightTop_Y,
                                            double LeftBottom_X, double LeftBottom_Y,
                                            double rowCounter, double columnCounter,
                                            ref ArrayList arrayListCoordinate)
        {
            try
            {
                ArrayList arrayListGenerate_X = new ArrayList(100);
                ArrayList arrayListGenerate_Y = new ArrayList(100);

                TableFromPoints_3(LeftTop_X, LeftTop_Y,
                                  RightTop_X, RightTop_Y,
                                  LeftBottom_X, LeftBottom_Y,
                                  rowCounter, columnCounter,
                                  ref arrayListGenerate_X, ref  arrayListGenerate_Y);

                arrayListCoordinate.Clear();
                PosXYWZ pos = new PosXYWZ();
                for (int k = 0; k < arrayListGenerate_X.Count; k++)
                {
                    pos.X = (double)arrayListGenerate_X[k];
                    pos.Y = (double)arrayListGenerate_Y[k];
                    arrayListCoordinate.Add(pos);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("TableFromPoints_3() 转换失败");
            }
        }

        /// <summary>
        /// 生成料盘拍照位表列
        /// </summary>
        /// <remarks></remarks>
        static public void TableFromPoints_3(double LeftTop_X, double LeftTop_Y,
                                            double RightTop_X, double RightTop_Y,
                                            double LeftBottom_X, double LeftBottom_Y,
                                            double rowCounter, double columnCounter,
                                            ref ArrayList arrayListGenerate_X, ref ArrayList arrayListGenerate_Y)
        {
            double X, Y;
            arrayListGenerate_X.Clear();
            arrayListGenerate_Y.Clear();

            //----------- 加入rowCounter<2或columnCounter<2的的判断 ------------
            if ((rowCounter < 2) && (columnCounter < 2))
            {
                X = LeftTop_X;
                Y = LeftTop_Y;
                arrayListGenerate_X.Add(X);
                arrayListGenerate_Y.Add(Y);
            }
            else if (rowCounter < 2)
            {
                for (int j = 0; j < columnCounter; j++)
                {
                    X = LeftTop_X + (j * ((RightTop_X - LeftTop_X) / (columnCounter - 1)));
                    Y = LeftTop_Y + (j * ((RightTop_Y - LeftTop_Y) / (columnCounter - 1)));
                    arrayListGenerate_X.Add(X);
                    arrayListGenerate_Y.Add(Y);
                }
            }
            else if (columnCounter < 2)
            {
                for (int i = 0; i < rowCounter; i++)
                {
                    X = LeftTop_X + (i * ((LeftBottom_X - LeftTop_X) / (rowCounter - 1)));
                    Y = LeftTop_Y + (i * ((LeftBottom_Y - LeftTop_Y) / (rowCounter - 1)));
                    arrayListGenerate_X.Add(X);
                    arrayListGenerate_Y.Add(Y);
                }
            }
            else
            {
                for (int i = 0; i < rowCounter; i++)
                {
                    for (int j = 0; j < columnCounter; j++)
                    {
                        X = LeftTop_X + (i * (LeftBottom_X - LeftTop_X) / (rowCounter - 1)) + (j * ((RightTop_X - LeftTop_X) / (columnCounter - 1)));
                        Y = LeftTop_Y + (i * ((LeftBottom_Y - LeftTop_Y) / (rowCounter - 1))) + (j * ((RightTop_Y - LeftTop_Y) / (columnCounter - 1)));

                        arrayListGenerate_X.Add(X);
                        arrayListGenerate_Y.Add(Y);
                    }
                }
            }
        }

        static public void OffsetPoints_3(double LeftTop_X, double LeftTop_Y,
                                            double RightTop_X, double RightTop_Y,
                                            double LeftBottom_X, double LeftBottom_Y,
                                            double LeftTop_X2, double LeftTop_Y2,
                                            ref double RightTop_X2, ref double RightTop_Y2,
                                            ref double LeftBottom_X2, ref double LeftBottom_Y2)
        {
            double offX = LeftTop_X - LeftTop_X2;
            double offY = LeftTop_Y - LeftTop_Y2;
            RightTop_X2 = RightTop_X - offX;
            RightTop_Y2 = RightTop_Y - offY;
            LeftBottom_X2 = LeftBottom_X - offX;
            LeftBottom_Y2 = LeftBottom_Y - offY;
        }

        static public void OffsetPoints_N(List<double> arrayListX, List<double> arrayListY,
                                          ref List<double> arrayListX2, ref List<double> arrayListY2)
        {
            if (arrayListX.Count < 1 || arrayListY.Count < 1 || arrayListX2.Count < 1 || arrayListY2.Count < 1)
                return;

            double offX = arrayListX[0] - arrayListX2[0];
            double offY =  arrayListY[0] - arrayListY2[0];
            arrayListX2.Clear();
            foreach (var x in arrayListX)
            {
                arrayListX2.Add(x-offX);
            }
            arrayListY2.Clear();
            foreach (var y in arrayListY)
            {
                arrayListY2.Add(y-offY);
            }
        }
        /// <summary>
        /// 从2个点创建一列点
        /// </summary>
        /// <param name="left">左点</param>
        /// <param name="right">右点</param>
        /// <param name="colCount">列数</param>
        /// <param name="arrayListCoordinate">生成的表列</param>
        /// <returns></returns>
        /// <remarks></remarks>
        static public bool ArrayFrom2Points(double left,
                                            double right,
                                            int colCount,
                                            ref ArrayList arrayListCoordinate)
        {
            double X;
            arrayListCoordinate.Clear();

            if (colCount <= 0) return false;
            else if (colCount == 1)
            {
                arrayListCoordinate.Add(left);
                return true;
            }

            for (int j = 0; j < colCount; j++)
            {
                X = left + (j * ((right - left) / (colCount - 1)));
                arrayListCoordinate.Add(X);
            }

            return true;
        }

        static public bool ArrayFrom2Points(PosXYWZ left,
                                            PosXYWZ right,
                                            int colCount,
                                            ref List<PosXYWZ> arrayListCoordinate)
        {
            arrayListCoordinate.Clear();

            ArrayList arrayListX = new ArrayList(100);
            ArrayList arrayListY = new ArrayList(100);
            ArrayList arrayListW = new ArrayList(100);
            ArrayList arrayListZ = new ArrayList(100);
            ArrayFrom2Points(left.X, right.X, colCount, ref arrayListX);
            ArrayFrom2Points(left.X, right.Y, colCount, ref arrayListY);
            ArrayFrom2Points(left.X, right.W, colCount, ref arrayListW);
            ArrayFrom2Points(left.X, right.Z, colCount, ref arrayListZ);

            PosXYWZ pos = new PosXYWZ();
            for (int j = 0; j < colCount; j++)
            {
                pos.X = (double)arrayListX[j];
                pos.Y = (double)arrayListY[j];
                pos.W = (double)arrayListW[j];
                pos.Z = (double)arrayListZ[j];
                arrayListCoordinate.Add(pos);
            }

            return true;
        }
    }
}
