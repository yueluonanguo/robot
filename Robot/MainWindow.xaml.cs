using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Net;
using System.Collections;
using System.Globalization;
using OPCAutomation;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using DataGrid = System.Windows.Controls.DataGrid;
using DataGridCell = System.Windows.Controls.DataGridCell;

namespace Robot
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private OpenFileDialog openFileDialog = null;    //打开文件
        private SaveFileDialog saveFileDialog1 = null;   //保存文件
        
        public MainWindow()
        {
            InitializeComponent();
            openFileDialog = new OpenFileDialog();
            openFileDialog.FileOk += openFileDialogFileOk;
            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.FileOk += saveFileDialog1FileOk;
            this.Reset();
            button3.IsEnabled = false;
           
        }
        OPCServer KepServer;
        OPCGroups KepGroups;
        OPCGroup KepGroup;
        OPCItems KepItems;
        OPCItem KepItem; 
        string strHostIp = "";//主机ip
        string strHostName = "";//主机名称
        bool opc_connected = false;//连接状态
        int itmHandleClient = 0;//客户端句柄
        int itmHandleServer = 0;//服务端句柄
      
        private void GetLocalServer()//枚举本地OPC服务器
        {
            //获取本地计算机IP，计算机名称
            //IPHostEntry IPHost = Dns.Resolve(Environment.MachineName);
            IPHostEntry IPHost = Dns.GetHostEntry(Environment.MachineName);
            
            if (IPHost.AddressList.Length > 0)
            {
                strHostIp = IPHost.AddressList[0].ToString();
            }
            else
            {
                return;
            }
            //通过IP来获取计算机名称，可用在局域网内

            IPHostEntry ipHostEntry = Dns.GetHostEntry(strHostIp);
            strHostName = ipHostEntry.HostName.ToString();

            //获取本地计算机上的OPCServerName
            try
            {
                KepServer = new OPCServer();
                object serverList = KepServer.GetOPCServers(strHostName);
                foreach (string turn in (Array)serverList)
                {
                    comboBox1.Items.Add(turn);

                }
                comboBox1.SelectedIndex = 0;
            
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("枚举本地OPC服务器出错：" + err.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
      
        //创建组
        private bool CreatGroup()
        {
            try
            {
                KepGroups = KepServer.OPCGroups;
                KepGroup = KepGroups.Add("OPCDOTNETGROUP");
                String();
                KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                KepItems = KepGroup.OPCItems;
               
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("创建组出现错误：" + err.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }


        public void String()                              //设置组属性
        {
            string a;
            string b;
            string c;
            string d;
            string e;
            Window1 m = new Window1();
            a = m.textBox1.Text;
            KepServer.OPCGroups.DefaultGroupIsActive = Convert.ToBoolean(a);
            b = m.textBox2.Text;
            KepServer.OPCGroups.DefaultGroupDeadband = Convert.ToInt32(b);
            c = m.textBox3.Text;
            KepGroup.UpdateRate = Convert.ToInt32(c);
            d = m.textBox4.Text;
            KepGroup.IsActive = Convert.ToBoolean(d);
            e = m.textBox5.Text;
            KepGroup.IsSubscribed = Convert.ToBoolean(e);
        }
        double dX=450;
        double dY=0;
        double dZ=-695;
       
        void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
           
            for (int i = 1; i <= NumItems; i++)
            {
                if (ClientHandles.GetValue(i).Equals(1000))
                {
                    this.label19.Content = ItemValues.GetValue(i).ToString();
                    x.Angle = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1001))
                {
                    this.label29.Content = ItemValues.GetValue(i).ToString();
                }
                if (ClientHandles.GetValue(i).Equals(1002))
                {
                    this.label21.Content = ItemValues.GetValue(i).ToString();
                    y.Angle = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1003))
                {
                    this.label31.Content = ItemValues.GetValue(i).ToString();
                }
                if (ClientHandles.GetValue(i).Equals(1004))
                {
                    this.label23.Content = ItemValues.GetValue(i).ToString();
                    z.Angle = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1005))
                {
                    this.label33.Content = ItemValues.GetValue(i).ToString();
                }
                if (ClientHandles.GetValue(i).Equals(1006))
                {
                    this.label6.Content = ItemValues.GetValue(i).ToString();
                    a.Angle = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1007))
                {
                    this.label41.Content = ItemValues.GetValue(i).ToString();
                }
                if (ClientHandles.GetValue(i).Equals(1008))
                {
                    this.label27.Content = ItemValues.GetValue(i).ToString();
                    b.Angle = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1009))
                {
                    this.label38.Content = ItemValues.GetValue(i).ToString();
                }
                if (ClientHandles.GetValue(i).Equals(1010))
                {
                    this.label25.Content = ItemValues.GetValue(i).ToString();
                    c.Angle = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1011))
                {
                    this.label36.Content = ItemValues.GetValue(i).ToString();
                }
                if (ClientHandles.GetValue(i).Equals(1012))
                {
                    
                    this.label44.Content = ItemValues.GetValue(i).ToString();
                    dX = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1013))
                {
                    
                    this.label46.Content = ItemValues.GetValue(i).ToString();
                    dY = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
                if (ClientHandles.GetValue(i).Equals(1014))
                {
                    
                    this.label48.Content = ItemValues.GetValue(i).ToString();
                    dZ = Convert.ToDouble(ItemValues.GetValue(i).ToString());
                }
              
                if (ClientHandles.GetValue(i).Equals(1234))
                {
                    if (textBox2.Text == "")
                    {
                        this.textBox3.Text = "";
                    }
                    else { this.textBox3.Text = ItemValues.GetValue(i).ToString(); }
                }
             
                    Point3D draw;
                    draw = new Point3D(-dX / 1000.0, -dZ / 1000.0 - 0.487, -dY / 1000.0);
                    e.Points.Add(draw);
                e.Points. 




            }
        }
        void Read(string u1,int u2)                                            //读
        {
            KepItem = KepItems.AddItem(u1, u2);
            itmHandleServer = KepItem.ServerHandle;
            OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);
            int[] temp = new int[2] { 0, bItem.ServerHandle };
            Array serverHandles = (Array)temp; 
            Array Errors;
            int cancelID;
            KepGroup.AsyncRead(1, ref serverHandles, out Errors, 2009, out cancelID);
            GC.Collect();  
        }
        private bool ConnectRemoteServer(string remoteServerIP, string remoteServerName)
        {
            try
            {
                KepServer.Connect(remoteServerName, remoteServerIP);
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    textBox1.Text = "已连接到-" + KepServer.ServerName + "   ";
                }
                else
                {
                    textBox1.Text = "状态：" + KepServer.ServerState.ToString() + "   ";
                }
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("连接远程服务器出现错误：" + err.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)   //运行窗口时处理的事情
        {
            GetLocalServer();
        }

        private void Window_Closing(object sender, CancelEventArgs e)  //关闭窗体时处理的事情
        {

            MessageBoxResult key = System.Windows.MessageBox.Show(
             "确定退出吗",
            "确定",
            MessageBoxButton.YesNo,
             MessageBoxImage.Question,
             MessageBoxResult.No);
            e.Cancel = (key == MessageBoxResult.No);
            if (!opc_connected)
            {
                return;
            }

            if (KepGroup != null)
            {
                KepGroup.DataChange -= new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
            }

            if (KepServer != null)
            {
                KepServer.Disconnect();
                KepServer = null;
            }
            
            opc_connected = false;
         
            App.Current.Shutdown();
        }
        private void RecurBrowse(OPCBrowser oPCBrowser)
        {
            //展开分支
            oPCBrowser.ShowBranches();
            //展开叶子
            oPCBrowser.ShowLeafs(true);
            foreach (object turn in oPCBrowser)
            {
                listBox1.Items.Add(turn.ToString());
            }
        }
        private void button1_Click(object sender, RoutedEventArgs e)          //连接
        {
            try
            {
                if (!ConnectRemoteServer("" ,comboBox1.Text))
               
                {
                    return;
                }
                opc_connected = true;
                RecurBrowse(KepServer.CreateBrowser());
                button3.IsEnabled = true;
                if (!CreatGroup())
                {
                    return;
                }
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("初始化出错：" + err.Message, "提示信息", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
       
        private void button3_Click(object sender, RoutedEventArgs e)              //设置
        {
            this.Hide();
            Window1 m = new Window1();
            m.ShowDialog();
        }
        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)  //列表
        {
            try
            {
                if (itmHandleClient != 0)
                {
                    this.textBox3.Text = "";
                    Array Errors;
                    OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);
                    //注： OPC中以1为数组的基数
                    int[] temp = new int[2] { 0, bItem.ServerHandle };
                    Array serverHandle = (Array)temp;
                    //移除上一次选择的项
                    KepItems.Remove(KepItems.Count, ref serverHandle, out Errors);

                }
                itmHandleClient = 1234;
                KepItem = KepItems.AddItem(listBox1.SelectedItem.ToString(), itmHandleClient);
                textBox2.Text = Convert.ToString(listBox1.SelectedItem);
                itmHandleServer = KepItem.ServerHandle;
            }
            catch (Exception err)
            {
                //没有任何权限的项，都是OPC服务器保留的系统项，此处可不做处理
                itmHandleClient = 0;
                textBox3.Text = "Error ox";
                System.Windows.MessageBox.Show("此项为系统保留项：" + err.Message, "提示信息");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)           //断开
        {
            KepServer.Disconnect();
            textBox1.Text = "断开";
            listBox1.Items.Clear();
            this.Reset1();
        }
        public void Reset1()
        { 
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
            textBox13.Text = string.Empty;
            textBox14.Text = string.Empty;
            textBox15.Text = string.Empty;
            label19.Content = string.Empty;
            label21.Content = string.Empty;
            label23.Content = string.Empty;
            label25.Content = string.Empty;
            label27.Content = string.Empty;
            label29.Content = string.Empty;
            label31.Content = string.Empty;
            label33.Content = string.Empty;
            label36.Content = string.Empty;
            label38.Content = string.Empty;
            label6.Content = string.Empty;
            label41.Content = string.Empty;
            textBox4.Text = string.Empty;
            label44.Content = string.Empty;
            label46.Content = string.Empty;
            label48.Content = string.Empty;
        }

        private void button5_Click(object sender, RoutedEventArgs e)                     //写值
        {
            Write(textBox2.Text, textBox6.Text);
          
        }

        private void button4_Click_1(object sender, RoutedEventArgs e)                   //读值
        {
            if (textBox2.Text!=null)
            {
                Read(textBox2.Text, 1234);
            }
        }
        public void Write(string m1,string m2)                                            // 写函数
        {
            itmHandleClient = 1234;
            KepItem = KepItems.AddItem(m1, itmHandleClient);
            itmHandleServer = KepItem.ServerHandle;
            OPCItem bItem = KepItems.GetOPCItem(itmHandleServer);
            int[] temp = new int[2] { 0, bItem.ServerHandle };
            Array serverHandles = (Array)temp;
            object[] valueTemp = new object[2] { "", m2 };
            Array values = (Array)valueTemp;
            Array Errors;
            int cancelID;
            KepGroup.AsyncWrite(1, ref serverHandles, ref values, out Errors, 2009, out cancelID);
            GC.Collect();
        }
      
        private void button6_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //X+
        {

            if (radioButton1.IsChecked == true)
            {   
                Write("PLC1.Application.XAXIS.mcj1.Velocity", textBox7.Text);
                Write("PLC1.Application.XAXIS.mcj1.JogForward", "true");
            }
            
        }
       
        
        private void button6_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)   //
        {
            
                 Write("PLC1.Application.XAXIS.mcj1.JogForward", "false"); 
        }

        private void button7_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)  //X-
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.XAXIS.mcj1.Velocity", textBox7.Text);
                Write("PLC1.Application.XAXIS.mcj1.JogBackward", "true");
            }
           
           
        }

        private void button7_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.XAXIS.mcj1.JogBackward", "false");
        }

        private void button8_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) //Y+
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.YAXIS.mcj2.Velocity", textBox8.Text);
                Write("PLC1.Application.YAXIS.mcj2.JogForward", "true");
            }
           
        }

        private void button8_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            Write("PLC1.Application.YAXIS.mcj2.JogForward", "false");
        }

        private void button9_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) //Y-
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.YAXIS.mcj2.Velocity", textBox8.Text);
                Write("PLC1.Application.YAXIS.mcj2.JogBackward", "true");
            }
           
        }

        private void button9_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { 
           Write("PLC1.Application.YAXIS.mcj2.JogBackward", "false");
        }

        private void button10_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)// Z+
        {
            if (radioButton1.IsChecked == true)
            {
                
                Write("PLC1.Application.ZAXIS.mcj3.Velocity", textBox9.Text);
                Write("PLC1.Application.ZAXIS.mcj3.JogForward", "true");
            }
           
        }

        private void button10_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Write("PLC1.Application.ZAXIS.mcj3.JogForward", "false");
        }

        private void button11_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)//Z-
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.ZAXIS.mcj3.Velocity", textBox9.Text);
                Write("PLC1.Application.ZAXIS.mcj3.JogBackward", "true");
            }
        }

        private void button11_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            Write("PLC1.Application.ZAXIS.mcj3.JogBackward", "false");
        }

        private void button12_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)//C+
        {
            if (radioButton1.IsChecked == true)
            {
               
                Write("PLC1.Application.CAXIS.mcj4.Velocity", textBox10.Text);
                Write("PLC1.Application.CAXIS.mcj4.JogForward", "true");
            }
           
        }

        private void button12_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
          
            Write("PLC1.Application.CAXIS.mcj4.JogForward", "false");
        }

        private void button13_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)//C-
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.CAXIS.mcj4.Velocity", textBox10.Text);
                Write("PLC1.Application.CAXIS.mcj4.JogBackward", "true");
            }
           
        }

        private void button13_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.CAXIS.mcj4.JogBackward", "false");
        }

        private void button14_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)//B+
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.BAXIS.mcj5.Velocity", textBox11.Text);
                Write("PLC1.Application.BAXIS.mcj5.JogForward", "true");
            }
            
        }

        private void button14_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.BAXIS.mcj5.JogForward", "false");
        }

        private void button15_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)   //B-
        {
            if (radioButton1.IsChecked != true) return;
            Write("PLC1.Application.BAXIS.mcj5.Velocity", textBox11.Text);
            Write("PLC1.Application.BAXIS.mcj5.JogBackward", "true");
        }

        private void button15_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
            Write("PLC1.Application.BAXIS.mcj5.JogBackward", "false");
        }

        private void button16_Click(object sender, RoutedEventArgs e)                             //使能开
        { 
            Write("PLC1.Application.XAXIS.mcp1.bRegulatorOn", "true");
            Write("PLC1.Application.YAXIS.mcp2.bRegulatorOn", "true");
            Write("PLC1.Application.ZAXIS.mcp3.bRegulatorOn", "true");
            Write("PLC1.Application.CAXIS.mcp4.bRegulatorOn", "true");
            Write("PLC1.Application.BAXIS.mcp5.bRegulatorOn", "true");
            Write("PLC1.Application.AAXIS.mcp6.bRegulatorOn", "true");
            Write("PLC1.Application.READGCODE.switch", "FALSE");
            Write("PLC1.Application.POU_X.FA", "true");
            Write("PLC1.Application.READGCODE.d_JOG.dB", "90");
            Write("PLC1.Application.READGCODE.d_JOG.dX", "450");
            Write("PLC1.Application.READGCODE.d_JOG.dZ", "-695");
            Write("PLC1.Application.TOOL_CONTROL.gg", "FALSE");
            Read("PLC1.Application.XAXIS.mcp01", 1000);
            Read("PLC1.Application.XAXIS.mcv01", 1001);
            Read("PLC1.Application.YAXIS.mcp02", 1002);
            Read("PLC1.Application.YAXIS.mcv02", 1003);
            Read("PLC1.Application.ZAXIS.mcp03", 1004);
            Read("PLC1.Application.ZAXIS.mcv03", 1005);
            Read("PLC1.Application.AAXIS.mcp06", 1006);
            Read("PLC1.Application.AAXIS.mcv06", 1007);
            Read("PLC1.Application.BAXIS.mcp05", 1008);
            Read("PLC1.Application.BAXIS.mcv05", 1009);
            Read("PLC1.Application.CAXIS.mcp04", 1010);
            Read("PLC1.Application.CAXIS.mcv04", 1011);
            Read("PLC1.Application.READGCODE.m1.dX", 1012);
            Read("PLC1.Application.READGCODE.m1.dY", 1013);
            Read("PLC1.Application.READGCODE.m1.dZ", 1014);
           
        }

        private void button17_Click(object sender, RoutedEventArgs e)                            //打开文件
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialogFileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string fullPathname = openFileDialog.FileName;
            FileInfo src = new FileInfo(fullPathname);
            textBox12.Text = src.FullName;
            TextReader reader = src.OpenText();
            displayData(reader);
        }
        private void displayData(TextReader reader)
        {
            textBox14.Text = "";
            string line = reader.ReadLine();
            while (line != null)
            {
                textBox14.Text += line + '\n';
                line = reader.ReadLine();
            }
            reader.Close();
        }

        private void button18_Click(object sender, RoutedEventArgs e)                              // 保存文件
        {
            saveFileDialog1.ShowDialog();
        }
        private void saveFileDialog1FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string fullPathname = saveFileDialog1.FileName;
            FileInfo src = new FileInfo(fullPathname);
            textBox13.Text = src.FullName;
            try
            {
                Stream stream = File.OpenWrite(textBox13.Text);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(textBox14.Text);
                }
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show("不能保存");
            }
        }

        private void button20_Click(object sender, RoutedEventArgs e)                                 //清屏
        {
            this.Reset();
        }
        public void Reset()
        { textBox14.Text = string.Empty; }
        private void radioButton9_Checked(object sender, RoutedEventArgs e)
        {
            checkBox1.IsEnabled = false;
        }
        private void radioButton10_Checked(object sender, RoutedEventArgs e)
        {
            checkBox1.IsEnabled = true;
        }
        private void button21_Click(object sender, RoutedEventArgs e)                                 //开始
        {
            if (radioButton10.IsChecked == true)
            {
                if (textBox15.Text == "")
                {
                    return;
                }
                else
                {
                    Write("PLC1.Application.POU_1.t", "FALSE");
                    Write("PLC1.Application.READGCODE.switch", "true");
                    Write("PLC1.Application.READGCODE.smcr1.sFileName", textBox15.Text);
                    Write("PLC1.Application.READGCODE.smcr1.bExecute", "true");
                }
            }
            if (radioButton9.IsChecked == true)
            {
                    Write("PLC1.Application.POU_1.t", "FALSE");
                    Stream stream = File.OpenWrite(@"D:\Program Files\3S CODESYS\CODESYS Control RTE3\n.cnc");
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.SetLength(0);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(textBox14.Text);
                    writer.Flush();
                    writer.Close();
                    Write("PLC1.Application.READGCODE.switch", "true");
                    Write("PLC1.Application.READGCODE.smcr1.sFileName", "n.cnc");
                    Write("PLC1.Application.READGCODE.smcr1.bExecute", "true");

            }
            if (radioButton12.IsChecked == true)
            {
                Write("PLC1.Application.POU_1.t", "TRUE");
                Write("PLC1.Application.READGCODE.smcr1.sFileName", "n.cnc");
                Write("PLC1.Application.READGCODE.smcr1.bExecute", "true");

            }
        }
        
        private void button22_Click(object sender, RoutedEventArgs e)                                 //使能关
        {
            Write("PLC1.Application.XAXIS.mcp1.bRegulatorOn", "false");
            Write("PLC1.Application.YAXIS.mcp2.bRegulatorOn", "false");
            Write("PLC1.Application.ZAXIS.mcp3.bRegulatorOn", "false");
            Write("PLC1.Application.CAXIS.mcp4.bRegulatorOn", "false");
            Write("PLC1.Application.BAXIS.mcp5.bRegulatorOn", "false");
            Write("PLC1.Application.POU_X.FA", "false");
            this.Reset2();
           
        }
        public void Reset2()
        {
            label19.Content = string.Empty;
            label21.Content = string.Empty;
            label23.Content = string.Empty;
            label25.Content = string.Empty;
            label27.Content = string.Empty;
            label29.Content = string.Empty;
            label31.Content = string.Empty;
            label33.Content = string.Empty;
            label36.Content = string.Empty;
            label38.Content = string.Empty;
            label6.Content = string.Empty;
            label41.Content = string.Empty;
            textBox4.Text = string.Empty;
            label44.Content = string.Empty;
            label46.Content = string.Empty;
            label48.Content = string.Empty;
        }

        private void button23_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.AAXIS.mcj6.Velocity", textBox4.Text);
                Write("PLC1.Application.AAXIS.mcj6.JogForward", "true");
            }
           
        }

        private void button23_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.AAXIS.mcj6.Velocity", textBox4.Text);
            Write("PLC1.Application.AAXIS.mcj6.JogForward", "false");
        }

        private void button24_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                Write("PLC1.Application.AAXIS.mcj6.Velocity", textBox4.Text);
                Write("PLC1.Application.AAXIS.mcj6.JogBackward", "true");
            }
        }

        private void button24_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.AAXIS.mcj6.Velocity", textBox4.Text);
            Write("PLC1.Application.AAXIS.mcj6.JogBackward", "false");
        }

        private void button25_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (radioButton2.IsChecked == true)
            {
                if (radioButton3.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpx2.Velocity",textBox5.Text);
                    Write("PLC1.Application.POU_X.XF", "TRUE");
                }
                if (radioButton4.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpy2.Velocity", textBox5.Text);
                    Write("PLC1.Application.POU_X.YF", "TRUE");
                }
                if (radioButton5.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpz2.Velocity", textBox5.Text);
                    Write("PLC1.Application.POU_X.ZF", "TRUE");
                }
            }
        }

        private void button25_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.POU_X.XF", "false");
            Write("PLC1.Application.POU_X.YF", "false");
            Write("PLC1.Application.POU_X.ZF", "false");
        }

        private void button26_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            if (radioButton2.IsChecked == true)
            {
                if (radioButton3.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpx2.Velocity", textBox5.Text);
                    Write("PLC1.Application.POU_X.XB", "TRUE");
                }
                if (radioButton4.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpy2.Velocity", textBox5.Text);
                    Write("PLC1.Application.POU_X.YB", "TRUE");
                }
                if (radioButton5.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpz2.Velocity", textBox5.Text);
                    Write("PLC1.Application.POU_X.ZB", "TRUE");
                }
            }
        }

        private void button26_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.POU_X.XB", "false");
            Write("PLC1.Application.POU_X.YB", "false");
            Write("PLC1.Application.POU_X.ZB", "false");
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)//关节坐标系点动
        { 
            Write("PLC1.Application.READGCODE.switch", "FALSE");
            Write("PLC1.Application.POU_1.m", "FALSE");
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)//世界坐标系点动
        {
            if (radioButton3.IsChecked == true || radioButton4.IsChecked == true || radioButton5.IsChecked == true)
            {
                Write("PLC1.Application.READGCODE.d_JOG.wAuxData", "63");  
                Write("PLC1.Application.READGCODE.switch", "FALSE");
                Write("PLC1.Application.POU_X.FB", "TRUE");
                Write("PLC1.Application.POU_1.m", "TRUE");
                Write("PLC1.Application.POU_X.FB", "FALSE");
            }
            if (radioButton6.IsChecked == true || radioButton7.IsChecked == true || radioButton8.IsChecked == true)
            {
                Write("PLC1.Application.READGCODE.d_JOG.wAuxData", "63");
                Write("PLC1.Application.READGCODE.switch", "FALSE");
                Write("PLC1.Application.POU_X.FB", "TRUE");
                Write("PLC1.Application.POU_1.m", "TRUE");
                Write("PLC1.Application.POU_X.FB", "FALSE");
            }
        }
        private void radioButton11_Checked(object sender, RoutedEventArgs e)//工具坐标系点动
        {
            Write("PLC1.Application.TOOL_CONTROL.control_tool", "TRUE");
            Write("PLC1.Application.TOOL_CONTROL.gg", "TRUE");
        }

      

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            radioButton6.IsChecked = false;
            radioButton7.IsChecked = false;
            radioButton8.IsChecked = false;
            Write("PLC1.APPlication.POU_1.SE", "FALSE");
            Write("PLC1.Application.POU_X.FB", "TRUE");
            Write("PLC1.Application.POU_1.m", "TRUE");
            Write("PLC1.Application.POU_X.FB", "FALSE");
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            radioButton6.IsChecked = false;
            radioButton7.IsChecked = false;
            radioButton8.IsChecked = false;
            Write("PLC1.APPlication.POU_1.SE", "FALSE");
            Write("PLC1.Application.POU_X.FB", "TRUE");
            Write("PLC1.Application.POU_1.m", "TRUE");
            Write("PLC1.Application.POU_X.FB", "FALSE");
        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            radioButton6.IsChecked = false;
            radioButton7.IsChecked = false;
            radioButton8.IsChecked = false;
            Write("PLC1.APPlication.POU_1.SE", "FALSE");
            Write("PLC1.Application.POU_X.FB", "TRUE");
            Write("PLC1.Application.POU_1.m", "TRUE");
            Write("PLC1.Application.POU_X.FB", "FALSE");
        }

        private void radioButton6_Checked(object sender, RoutedEventArgs e)
        {
            radioButton3.IsChecked = false;
            radioButton4.IsChecked = false;
            radioButton5.IsChecked = false;
            Write("PLC1.APPlication.POU_1.SE", "TRUE");
            Write("PLC1.Application.POU_X.FB", "TRUE");
            Write("PLC1.Application.POU_1.m", "TRUE");
            Write("PLC1.Application.POU_X.FB", "FALSE");
        }

        private void radioButton7_Checked(object sender, RoutedEventArgs e)
        {
            radioButton3.IsChecked = false;
            radioButton4.IsChecked = false;
            radioButton5.IsChecked = false;
            Write("PLC1.APPlication.POU_1.SE", "TRUE");
            Write("PLC1.Application.POU_X.FB", "TRUE");
            Write("PLC1.Application.POU_1.m", "TRUE");
            Write("PLC1.Application.POU_X.FB", "FALSE");
        }

        private void radioButton8_Checked(object sender, RoutedEventArgs e)
        {
            radioButton3.IsChecked = false;
            radioButton4.IsChecked = false;
            radioButton5.IsChecked = false;
            Write("PLC1.APPlication.POU_1.SE", "TRUE");
            Write("PLC1.Application.POU_X.FB", "TRUE");
            Write("PLC1.Application.POU_1.m", "TRUE");
            Write("PLC1.Application.POU_X.FB", "FALSE");
        }
       
        private void button27_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (radioButton2.IsChecked == true)
            {
                if (radioButton6.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpx2.Velocity", textBox16.Text);
                    Write("PLC1.Application.POU_X.XF", "TRUE");
                }
                if (radioButton7.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpy2.Velocity", textBox16.Text);
                    Write("PLC1.Application.POU_X.YF", "TRUE");
                }
                if (radioButton8.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpz2.Velocity", textBox16.Text);
                    Write("PLC1.Application.POU_X.ZF", "TRUE");
                }
            }
        }
        private void button27_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.POU_X.XF", "false");
            Write("PLC1.Application.POU_X.YF", "false");
            Write("PLC1.Application.POU_X.ZF", "false");
        }
        private void button28_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (radioButton2.IsChecked == true)
            {
                if (radioButton6.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpx2.Velocity", textBox16.Text);
                    Write("PLC1.Application.POU_X.XB", "TRUE");
                }
                if (radioButton7.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpy2.Velocity", textBox16.Text);
                    Write("PLC1.Application.POU_X.YB", "TRUE");
                }
                if (radioButton8.IsChecked == true)
                {
                    Write("PLC1.Application.POU_X.mcpz2.Velocity", textBox16.Text);
                    Write("PLC1.Application.POU_X.ZB", "TRUE");
                }
            }
        }
        private void button28_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Write("PLC1.Application.POU_X.XB", "false");
            Write("PLC1.Application.POU_X.YB", "false");
            Write("PLC1.Application.POU_X.ZB", "false");
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            Write("PLC1.Application.G_CODE.button","TRUE");
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            Write("PLC1.Application.G_CODE.button", "FALSE");
        }
        

        private void button29_Click(object sender, RoutedEventArgs e)  //添加
        {
            dataGrid1.LoadingRow += new EventHandler<DataGridRowEventArgs>(dataGrid1_LoadingRow);
            dataGrid1.Items.Add(new DataGridRow() { Item = new { G = comboBox2.SelectionBoxItem.ToString(), J0 = label19.Content, J1 = label21.Content, J2 = label23.Content, J3 = label6.Content, J4 = label27.Content, J5 = label25.Content, Ve = label35.Content } });
            dataGrid1.IsEnabled = false;
        }

        private void dataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
            
        }

        private void button30_Click(object sender, RoutedEventArgs e)
        {
             dataGrid1.IsEnabled = true;
            
        }

        private void button31_Click(object sender, RoutedEventArgs e)  
        {
            dataGrid1.CanUserDeleteRows = true;
            int currentRowIndex = dataGrid1.Items.IndexOf(this.dataGrid1.SelectedItem)+1;
            if (currentRowIndex == (dataGrid1.SelectedIndex+1))
            {
                dataGrid1.Items.Remove(dataGrid1.SelectedItem);
                dataGrid1.Items.Refresh();
            }
        }

        private void button32_Click(object sender, RoutedEventArgs e)//帮助
        {
            Process.Start(@"C:\Users\gu\Desktop\Robot\Robot\Help\core_SoftMotion.chm");
        }

        private void button33_Click(object sender, RoutedEventArgs e)
        {
           
            int number = dataGrid1.Items.Count;
            for( int i= 0;i<number;i++)
            {
                var dataGridRow = dataGrid1.Items[i] as DataGridRow;
                string str;
              
               // string G = comboBox2.SelectionBoxItem.ToString();
                if (dataGridRow != null)
                {
                    var r = dataGridRow.Item.ToString();
                    var f = r.Split();
                    var G = f[3].TrimEnd(',');
                    var c1 = f[6].TrimEnd(',');
                    var c2 = f[9].TrimEnd(',');
                    var c3 = f[12].TrimEnd(',');
                    var c4 = f[15].TrimEnd(',');
                    var c5 = f[18].TrimEnd(',');
                    var c6 = f[21].TrimEnd(',');
                    var ve = f[24].TrimEnd(',');
                    str = "N" +i + " "+G+ "  X" +c1 + " Y" +c2 + " Z" +c3 + " A" +c4+" B"+c5+" C"+c6+" F"+ve;
                    textBox14.Text += str + "\r\n";
                  
                }
                
            }
            Stream stream = File.OpenWrite(@"D:\Program Files\3S CODESYS\CODESYS Control RTE3\n.cnc");
            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(textBox14.Text);
            writer.Flush();
            writer.Close();

        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int v = comboBox2.SelectedIndex+1;
            Concent(v);
        }

        public void Concent(int cb)
        {
            switch (cb)
            {
                case 1: textBox17.Text = "直线运动不带刀" + "\r\n" + "具,定位";           
                    break;
                case 2: textBox17.Text = "带刀具的线性（" + "\r\n" + "直线）运动";
                    break;
                case 3: textBox17.Text = "顺时针圆周（圆"+ "\r\n" + "弧）运动";
                    break;
                case 4: textBox17.Text = "逆时针圆周（圆"+ "\r\n" + "弧）运动";
                    break;
                case 5: textBox17.Text = "延时时间";
                    break;
                case 6: textBox17.Text = "2D 技术样条点";
                    break;
                case 7: textBox17.Text = "抛物线";
                    break;
                case 8: textBox17.Text = "顺时针椭圆（圆" + "\r\n" + "弧）";
                    break;
                case 9: textBox17.Text = "逆时针椭圆（圆" + "\r\n" + "弧）";
                    break;
                case 10: textBox17.Text = "3D 基数样条点";
                    break;
                case 11: textBox17.Text = "结束圆路径/平" + "\r\n" + "滑路径功能";
                    break;
                case 12: textBox17.Text = "启动圆滑路径功" + "\r\n" + "能";
                    break;
                case 13: textBox17.Text = "启动圆弧路径功" + "\r\n" + "能";
                    break;
                case 14: textBox17.Text = "按照定义的绝对" + "\r\n" + "路径启动插补";
                    break;
                case 15: textBox17.Text = "按照定义的相对" + "\r\n" + "路径启动插补";
                    break;
                case 16: textBox17.Text = "设置位置不进行" + "\r\n" + "偏移";
                    break;
            }
           
            
        }
        double x2 = 0;
        private void button34_Click(object sender, RoutedEventArgs e)
        {
            
            if (radioButton13.IsChecked == true )
            { 
                x2 += 1;
                string y = Convert.ToString(x2);
                Write("PLC1.Application.TOOL_CONTROL.xx", y);
            }
            if (radioButton14.IsChecked == true)
            {
                
                x2 += 1;
                string y = Convert.ToString(x2);
                Write("PLC1.Application.TOOL_CONTROL.yy", y);
            }
            if (radioButton15.IsChecked == true)
            {
                
                x2 += 1;
                string y = Convert.ToString(x2);
                Write("PLC1.Application.TOOL_CONTROL.zz", y);
            }

        }
       
        private void button35_Click(object sender, RoutedEventArgs e)
        {
           
            if (radioButton13.IsChecked == true)
            {
               
                x2 -= 1;
                string y = Convert.ToString(x2);
                Write("PLC1.Application.TOOL_CONTROL.xx", y);
            }
            if (radioButton14.IsChecked == true)
            {
                
                x2 -= 1;
                string y = Convert.ToString(x2);
                Write("PLC1.Application.TOOL_CONTROL.yy", y);
            }
            if (radioButton15.IsChecked == true)
            {
             
                x2 -= 1;
                string y = Convert.ToString(x2);
                Write("PLC1.Application.TOOL_CONTROL.zz", y);
            }
        }
        private void radioButton13_Checked(object sender, RoutedEventArgs e)
        {
            x2 = 0;
            if (radioButton11.IsChecked == true)
            {
                
                radioButton14.IsChecked = false;
                radioButton15.IsChecked = false;
                Write("PLC1.Application.TOOL_CONTROL.gg", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.K1", "TRUE");
                Write("PLC1.Application.TOOL_CONTROL.K2", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.K3", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.f", "TRUE");
            }
        }

        private void radioButton14_Checked(object sender, RoutedEventArgs e)
        {
            x2 = 0;
            if (radioButton11.IsChecked == true)
            {
               
                radioButton13.IsChecked = false;
                radioButton15.IsChecked = false;
                Write("PLC1.Application.TOOL_CONTROL.gg", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.K2", "TRUE");             
                Write("PLC1.Application.TOOL_CONTROL.K1", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.K3", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.f", "TRUE");
            }
        }

        private void radioButton15_Checked(object sender, RoutedEventArgs e)
        {
            x2 = 0;
            if (radioButton11.IsChecked == true)
            {
                
                
                radioButton13.IsChecked = false;
                radioButton14.IsChecked = false;
                Write("PLC1.Application.TOOL_CONTROL.gg", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.K3", "TRUE");
                Write("PLC1.Application.TOOL_CONTROL.eee", "TRUE");
                Write("PLC1.Application.TOOL_CONTROL.K1", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.K2", "FALSE");
                Write("PLC1.Application.TOOL_CONTROL.f", "TRUE");
            }
        }

        

       

       

       

       

       

       

       
      
    }
       
    }
       
    

