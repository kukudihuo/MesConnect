using System;
            //_ListIoLabel.Add(Label_IO_1);
            //_ListIoLabel.Add(Label_IO_2);
            //_ListIoLabel.Add(Label_IO_3);
            //_ListIoLabel.Add(Label_IO_4);
            //_ListIoLabel.Add(Label_IO_5);
            //_ListIoLabel.Add(Label_IO_6);
            //_ListIoLabel.Add(Label_IO_7);
            //_ListIoLabel.Add(Label_IO_8);
            //_ListIoLabel.Add(Label_IO_9);
            //_ListIoLabel.Add(Label_IO_10);
            //_ListIoLabel.Add(Label_IO_11);
            //_ListIoLabel.Add(Label_IO_12);
            //_ListIoLabel.Add(Label_IO_13);
            //_ListIoLabel.Add(Label_IO_14);
            //_ListIoLabel.Add(Label_IO_15);
            //_ListIoLabel.Add(Label_IO_16);
            for (int i = 1; i <= IoTotal; i++)
            {
                _ListIoLabel.Add((Label)(GetType().GetField("Label_IO_" + i.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this)));
                _ListIoLabel[i - 1].Visible = true;
            }
            //_ListShowLabel.Add(Label_Name_1);
            //_ListShowLabel.Add(Label_Name_2);
            //_ListShowLabel.Add(Label_Name_3);
            //_ListShowLabel.Add(Label_Name_4);
            //_ListShowLabel.Add(Label_Name_5);
            //_ListShowLabel.Add(Label_Name_6);
            //_ListShowLabel.Add(Label_Name_7);
            //_ListShowLabel.Add(Label_Name_8);
            //_ListShowLabel.Add(Label_Name_9);
            //_ListShowLabel.Add(Label_Name_10);
            //_ListShowLabel.Add(Label_Name_11);
            //_ListShowLabel.Add(Label_Name_12);
            //_ListShowLabel.Add(Label_Name_13);
            //_ListShowLabel.Add(Label_Name_14);
            //_ListShowLabel.Add(Label_Name_15);
            //_ListShowLabel.Add(Label_Name_16);
            for (int i = 1; i <= IoTotal; i++)
            {
                _ListShowLabel.Add((Label)(GetType().GetField("Label_Name_" + i.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this)));
                _ListShowLabel[i - 1].Visible = true;
            }
            {
                ((Label)(GetType().GetField("Label_IO_" + i.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this))).Visible = false;
                ((Label)(GetType().GetField("Label_Name_" + i.ToString(), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(this))).Visible = false;
            }
                string sIO = "";
                {
        {
            m_ArrayList_Controls.Clear();
            Publics.GetAllControlsRecursion(typeof(Control_IO), cControls, ref m_ArrayList_Controls);
            foreach (object obj in m_ArrayList_Controls)
            {
                ((Control_IO)obj).UpdateShow();
            }
        }