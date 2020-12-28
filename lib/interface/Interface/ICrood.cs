using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface
{
    public interface ICrood
    {
        /// <summary>
        /// 坐标名称
        /// </summary>
        string Name
        {
            get;
        }

        int GetIndex();

        IMoveAll GetMoveAll();

        int GetAxisNumX();
        int GetAxisNumY();
        int GetAxisNumW();
        int GetAxisNumZ();
    }
}
