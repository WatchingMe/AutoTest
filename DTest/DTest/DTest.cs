using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using CaseUnit;
using System.Collections;
using System.Threading;
using CCWin;


namespace DTest
{
    public partial class DTest : Skin_Mac
    {
        string info_ = null;
        string s;
        string flag;
        string Classstr;
        String[] strClass;
        String[] SstrClass;
        //String[] log_info=null;
        int ClassNum;
        public string path = null;
        public DTest()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        Thread drawThread = null;
        delegate RichTextBox drawDelegate();
        private void DTest_Resize(object sender, EventArgs e)
        {
            this.List_case.Columns[0].Width = this.List_case.Width / 4;
            this.List_case.Columns[2].Width = this.List_case.Width / 2;
            this.List_case.Columns[1].Width = this.List_case.Width / 4;
        }


        //private RichTextBox test()
        //{
        //    if (richTextBox_Log.InvokeRequired)
        //    {
        //        drawDelegate d = new drawDelegate(test);
        //        BeginInvoke(d, null);
        //        return richTextBox_Log;
        //    }
        //    else
        //    {
        //        //richTextBox_Log.AppendText(i.ToString() + "\r\n");
        //        return richTextBox_Log;
        //    }
        //}
        RichTextBox R1;
        private void runTool()
        {
            R1 = new RichTextBox();
            R1.TextChanged += new EventHandler(R1_TextChanged);
            foreach (ListViewItem item in List_case.Items)
            {
                if ((item.BackColor == Color.Green || item.BackColor == Color.Red) && item.Checked == true)
                {
                    item.BackColor = Color.White;
                    item.ForeColor = Color.Black;
                }
            }
            {
                int count = List_case.CheckedItems.Count;
                for (int i2 = 0; i2 < count; i2++)
                {
                    //MessageBox.Show(List_case.CheckedItems[i2].SubItems.());
                    int i = List_case.CheckedItems[i2].SubItems.Count;
                    if (i == 2)
                    {
                        List_case.CheckedItems[i2].SubItems.RemoveAt(1);
                    }

                }
                //TestLog.TestLogInit(richTextBox_Log);

                //var info = typeof();//提取方法所在的类名
                toolStripProgressBar2.Value = 0;
                for (int i = 0; i < count; i++)
                {

                    double shares = 100 / count;
                    R1.Clear();
                    Assembly t = Assembly.LoadFile(path);
                    string FileName = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);//在私有变量里存储源文件名
                    //if (flag == 1)
                    //{
                    //richTextBox_Log.Clear();
                    //    Type b = t.GetType(FileName + "." + "TestCase" + "." + "TestLog");
                    //    MethodInfo log = b.GetMethod("TestLogInit");
                    //    log.Invoke(null, new RichTextBox[] { richTextBox_Log });
                    //}
                    int group = List_case.Groups.Count;//获得组的个数
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        //MessageBox.Show((strClass[j].Remove(strClass[j].Length - 6)));
                        //MessageBox.Show(strClass[j]);
                        if (List_case.CheckedItems[i].Group.Header.Contains(strClass[j]))
                        {
                            //try
                            //{
                            //richTextBox_Log.Clear();
                            toolStripLabel1.Text = "正在执行" + List_case.CheckedItems[i].Text;
                            Type a = t.GetType(FileName + "." + "TestCase" + "." + (strClass[j]));
                            //Type a = t.GetType(FileName + "." + "TestCase" + "." + (strClass[j]));
                            object obj = t.CreateInstance(FileName + "." + "TestCase" + "." + (strClass[j]));
                            MethodInfo ini = a.GetMethod("TestCaseEnter");//用GetMethod()方法来获取字符串的同名方法
                            Object[] parametors = new Object[] { R1, toolStripProgressBar1 };
                            //MessageBox.Show(richTextBox_Log.Text);
                            ini.Invoke(obj, parametors);
                            info_ = R1.Text + List_case.CheckedItems[i].Text;
                            //ini.Invoke(obj, new RichTextBox[] { richTextBox_Log });
                            //ini.Invoke(obj, new ToolStripProgressBar[] { toolStripProgressBar1 });
                            //MessageBox.Show(FileName + "." + "TestCase" + "." + (strClass[j]));
                            //MessageBox.Show("已经进入" + List_case.CheckedItems[i].Text + "等待执行");
                            MethodInfo mt = a.GetMethod(List_case.CheckedItems[i].Text);//用GetMethod()方法来获取字符串的同名方法
                            try
                            {
                                object b = mt.Invoke(null, null);
                                //All_inform = richTextBox_Log.Text;
                                //if (richTextBox_Log.Text != "")
                                //{
                                //    log_info[i] = List_case.CheckedItems[i].Text + "记录信息为： " + richTextBox_Log.Text;
                                //}
                                //richTextBox1.Clear();
                                //richTextBox1.Text = All_inform;
                                string type = b.GetType().ToString();
                                //MessageBox.Show(type);
                                //MessageBox.Show(b.ToString());
                                int num = 0;
                                int RealNum = 0;
                                string substring = null;
                                if (type == "System.Boolean")
                                {
                                    if (b.ToString() == "True")
                                    {
                                        List_case.CheckedItems[i].SubItems.Add("Pass");
                                        List_case.CheckedItems[i].BackColor = Color.Green;
                                        List_case.CheckedItems[i].ForeColor = Color.White;

                                    }
                                    else
                                    {
                                        List_case.CheckedItems[i].SubItems.Add("Failed");
                                        List_case.CheckedItems[i].BackColor = Color.Red;
                                        List_case.CheckedItems[i].ForeColor = Color.White;
                                    }
                                }
                                else if (type == "System.String")
                                {
                                    string[] sArray0 = b.ToString().Split('+');
                                    int strNum = sArray0.Length;
                                    for (int N = 0; N < strNum; N++)
                                    {
                                        string[] sArray = sArray0[N].ToString().Split('*');
                                        string Snum = sArray[0];
                                        substring = sArray[1];
                                        num = Convert.ToInt32(Snum);
                                        RealNum = SubstringCount(R1.Text, substring);
                                        if (RealNum == num)
                                        {
                                            flag = "Pass";
                                        }
                                        else
                                        {
                                            flag = "Falied";
                                            break;
                                        }

                                    }
                                    //string[] sArray = b.ToString().Split('*');
                                    //string Snum = sArray[0];
                                    //string substring = sArray[1];
                                    //MessageBox.Show(sArray[0]);
                                    //MessageBox.Show(sArray[1]);
                                    //int num = Convert.ToInt32(Snum);
                                    //int RealNum = SubstringCount(richTextBox_Log.Text, substring);
                                    //MessageBox.Show(RealNum.ToString());
                                    if (flag == "Pass")
                                    {
                                        List_case.CheckedItems[i].SubItems.Add("Pass");
                                        //List_case.CheckedItems[i].ForeColor= Color.Black;
                                        List_case.CheckedItems[i].BackColor = Color.Green;
                                        List_case.CheckedItems[i].ForeColor = Color.White;

                                    }
                                    else
                                    {
                                        if (type == "System.String")
                                        {

                                            List_case.CheckedItems[i].SubItems.Add("Failed");
                                            List_case.CheckedItems[i].SubItems.Add("String:" + "'" + substring + "'   " + "Expected:" + num.ToString() + " Actual:" + RealNum.ToString());
                                            List_case.CheckedItems[i].BackColor = Color.Red;
                                            List_case.CheckedItems[i].ForeColor = Color.White;
                                        }
                                        else
                                        {
                                            List_case.CheckedItems[i].SubItems.Add("Failed");
                                            List_case.CheckedItems[i].BackColor = Color.Red;
                                            List_case.CheckedItems[i].ForeColor = Color.White;
                                        }
                                    }


                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }


                            //object b;
                            //object objTypeInstance;
                            //objTypeInstance = t.CreateInstance(FileName + "." + "TestCase" + "." + (strClass[j]));
                            //b = mt.Invoke(objTypeInstance, null);
                            //if (b.GetType() == typeof(bool))
                            //{
                            //    if (b.ToString() == "true")
                            //    {
                            //        List_case.CheckedItems[i].SubItems.Add("Pass");
                            //    }
                            //    else
                            //    {
                            //        List_case.CheckedItems[i].SubItems.Add("Failed");
                            //    }
                            //}
                            //if (b.GetType() == typeof(string))
                            //{
                            //    string[] sArray = b.ToString().Split('*');
                            //    string Snum = sArray[0];
                            //    string substring = sArray[1];
                            //    int num = Convert.ToInt32(Snum);
                            //    int RealNum = SubstringCount(richTextBox_Log.Text, substring);
                            //    if (RealNum == num)
                            //    {
                            //        List_case.CheckedItems[i].SubItems.Add("Pass");
                            //    }
                            //    else
                            //    {
                            //        List_case.CheckedItems[i].SubItems.Add("Failed");
                            //    }
                            //}
                            MethodInfo exit = a.GetMethod("TestCaseExit");//用GetMethod()方法来获取字符串的同名方法
                            exit.Invoke(obj, null);
                            if (i == count - 1)
                            {
                                toolStripProgressBar2.Value = 100;
                            }
                            else
                            {
                                toolStripProgressBar2.Value += (int)shares;
                            }

                            // }
                            //catch (Exception ex)
                            //{
                            //MessageBox.Show(ex.ToString());
                            //}
                        }
                    }
                }
                foreach (ListViewItem item in List_case.Items)
                {
                    item.Checked = false;
                }
                toolStripLabel1.Text = "全部Case执行完毕";

            }
            //MessageBox.Show(All_inform);
            //MessageBox.Show(richTextBox_Log.Text);

            closeThread();
        }
        /// <summary>
        /// 计算字符串中子串出现的次数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="substring">子串</param>
        /// <returns>出现的次数</returns>
        static int SubstringCount(string str, string substring)
        {
            if (str.Contains(substring))
            {
                string strReplaced = str.Replace(substring, "");
                return (str.Length - strReplaced.Length) / substring.Length;
            }

            return 0;
        }


        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            {
                foreach (ListViewItem item in List_case.Items)
                {
                    item.Checked = true;
                }

            }
        }

        private void selectInvertToolStripMenuItem_Click(object sender, EventArgs e)
        {

            {
                foreach (ListViewItem item in List_case.Items)
                {
                    item.Checked = !item.Checked;
                };

            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeThread();
            drawThread = new Thread(new ThreadStart(runTool));
            drawThread.IsBackground = true;
            drawThread.Start();
            //MessageBox.Show(All_inform);

        }
        private void closeThread()
        {
            if (drawThread != null)
            {
                if (drawThread.IsAlive)
                {
                    drawThread.Abort();
                }
            }
        }
        private void DTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeThread();
        }

        //private void List_case_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    ListViewHitTestInfo info = List_case.HitTest(e.X, e.Y);
        //    if (info.Item != null)
        //    {

        //            MessageBox.Show(info_);
        //    }
        //}

        private void R1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.Text = R1.Text;
        }

        private void loadSwVTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                List_case.Items.Clear();
                openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog1.Filter = "dll文件|*.dll";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Classstr = null;
                    path = openFileDialog1.FileName;
                    string FileName = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);//在私有变量里存储源文件名
                    //MessageBox.Show(FileName);
                    Assembly t = Assembly.LoadFile(path);
                    foreach (Type CN in t.GetTypes())
                    {
                        if (null != CN.BaseType)
                        {
                            if ("TestCaseBase" == CN.BaseType.Name)
                            {
                                Classstr = Classstr + CN.Name + ";";
                            }
                        }
                    }
                    strClass = Classstr.Split(';');
                    SstrClass = Classstr.Split(';');
                    ClassNum = strClass.Length;
                    #region 获取作者信息
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        string Step = null;
                        Type a = t.GetType(FileName + "." + "TestCase" + "." + strClass[j]);
                        MethodInfo[] miArr = a.GetMethods();
                        foreach (MethodInfo MN in miArr) //BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                        {
                            var methodAttribute = (CaseUnit.TestCaseAttribute)Attribute.GetCustomAttribute(MN, typeof(CaseUnit.TestCaseAttribute));
                            if (null != methodAttribute)
                            {
                                Step = methodAttribute.Author;
                            }
                        }
                        SstrClass[j] = strClass[j] + "   " + "Author" + ":" + Step;
                    }
                    //for (int j = 0; j < ClassNum - 1; j++)
                    //{
                    //    SstrClass[j] = SstrClass[j].Substring(0, SstrClass[j].IndexOf("*"));
                    //    //MessageBox.Show(SstrClass[j]);
                    //}
                    #endregion
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        Type a = t.GetType(FileName + "." + "TestCase" + "." + strClass[j]);
                        MethodInfo[] miArr = a.GetMethods();
                        ListViewGroup aaa = new ListViewGroup();
                        aaa.Header = SstrClass[j];
                        this.List_case.Groups.Add(aaa);
                        foreach (MethodInfo MN in miArr) //BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                        {
                            var methodAttribute = (CaseUnit.TestCaseAttribute)Attribute.GetCustomAttribute(MN, typeof(CaseUnit.TestCaseAttribute));
                            if (null != methodAttribute)
                            {

                                ParameterInfo[] list = MN.GetParameters();
                                if (0 != list.Count())
                                    foreach (ParameterInfo asd in list)
                                    {
                                        s += asd.ToString();
                                    }
                                ListViewItem itemCase = new ListViewItem();
                                string All_Name = methodAttribute.Describtion;
                                //string Step_name = methodAttribute.Step;
                                //MessageBox.Show(Step_name);
                                //string[] sArray = All_Name.Split('_');
                                //string Class_Name=sArray[0];
                                //Method_Name = sArray[2];
                                //string Step_Num = sArray[1];
                                //MessageBox.Show(Class_Name + Method_Name + Step_Num);
                                itemCase.Text = All_Name;
                                List_case.Items.Add(itemCase);
                                this.List_case.ShowGroups = true;
                                aaa.Items.Add(itemCase);
                                //mi.Invoke(null, null);
                            }
                        }

                    }


                }
                this.List_case.GridLines = true;
            }
        }
        private void loadSwITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List_case.Items.Clear();
                openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog1.Filter = "dll文件|*.dll";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //获取Class的个数和对应的名称
                    Classstr = null;
                    path = openFileDialog1.FileName;
                    string FileName = path.Substring(path.LastIndexOf('\\') + 1, path.LastIndexOf('.') - path.LastIndexOf('\\') - 1);//在私有变量里存储源文件名
                    //MessageBox.Show(FileName);
                    Assembly t = Assembly.LoadFile(path);
                    foreach (Type CN in t.GetTypes())
                    {
                        if (null != CN.BaseType)
                        {
                            if ("TestCaseBase" == CN.BaseType.Name)
                            {
                                Classstr = Classstr + CN.Name + ";";
                            }
                        }
                    }
                    //MessageBox.Show(Classstr);
                    strClass = Classstr.Split(';');
                    SstrClass = Classstr.Split(';');
                    ClassNum = strClass.Length;//函数的个数
                    //MessageBox.Show(CaseNum.ToString());
                    // MessageBox.Show(strArray[1]);
                    //MessageBox.Show(Classstr);
                    //Array.Sort(strClass);
                    #region <按照属性中的Step执行排序>
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        string Step = null;
                        Type a = t.GetType(FileName + "." + "TestCase" + "." + strClass[j]);
                        MethodInfo[] miArr = a.GetMethods();
                        foreach (MethodInfo MN in miArr) //BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                        {
                            var methodAttribute = (CaseUnit.TestCaseAttribute)Attribute.GetCustomAttribute(MN, typeof(CaseUnit.TestCaseAttribute));
                            if (null != methodAttribute)
                            {
                                Step = methodAttribute.Step;
                            }
                        }
                        string num = null;
                        foreach (char item in Step)
                        {
                            if (item >= 48 && item <= 58)
                            {
                                num += item;
                            }
                        }
                        strClass[j] = num + strClass[j];
                    }
                    Array.Sort(strClass);
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        SstrClass[j] = strClass[j + 1];
                    }
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        strClass[j] = SstrClass[j].Substring(1);
                        //MessageBox.Show(strClass[j]);
                    }
                    #endregion
                    //获得不同函数下对应的方法的名称和个数
                    for (int j = 0; j < ClassNum - 1; j++)
                    {
                        Type type = t.GetType(FileName);
                        string nameSpace = FileName;
                        Type a = t.GetType(FileName + "." + "TestCase" + "." + strClass[j]);
                        MethodInfo[] miArr = a.GetMethods();
                        //foreach (MethodInfo MN in miArr)
                        //{
                        //    Methodstr = Methodstr + MN.Name + ";";
                        //}

                        //strMethod = Methodstr.Split(';');
                        //MethodNum = strMethod.Length;

                        ListViewGroup aaa = new ListViewGroup();
                        aaa.Header = strClass[j];
                        //MessageBox.Show(aaa.Header.ToString());
                        this.List_case.Groups.Add(aaa);
                        foreach (MethodInfo MN in miArr) //BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                        {
                            var methodAttribute = (CaseUnit.TestCaseAttribute)Attribute.GetCustomAttribute(MN, typeof(CaseUnit.TestCaseAttribute));
                            if (null != methodAttribute)
                            {

                                ParameterInfo[] list = MN.GetParameters();
                                if (0 != list.Count())
                                    foreach (ParameterInfo asd in list)
                                    {
                                        s += asd.ToString();
                                    }
                                ListViewItem itemCase = new ListViewItem();
                                string All_Name = methodAttribute.Describtion;
                                //string Step_name = methodAttribute.Step;
                                //MessageBox.Show(Step_name);
                                //string[] sArray = All_Name.Split('_');
                                //string Class_Name=sArray[0];
                                //Method_Name = sArray[2];
                                //string Step_Num = sArray[1];
                                //MessageBox.Show(Class_Name + Method_Name + Step_Num);
                                itemCase.Text = All_Name;
                                List_case.Items.Add(itemCase);
                                this.List_case.ShowGroups = true;
                                aaa.Items.Add(itemCase);
                                //MessageBox.Show(List_case.Columns.ToString());
                                //mi.Invoke(null, null);
                            }
                        }


                    }
                    //if (s.Contains("RichTextBox"))
                    //{
                    //    flag = 1;
                    //}
                }
                this.List_case.GridLines = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionLength = 0;
            richTextBox1.Focus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
