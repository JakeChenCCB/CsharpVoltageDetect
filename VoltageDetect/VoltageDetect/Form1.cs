using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoltageDetect
{
    public partial class Form1 : Form
    {
        int comNum = 0;
        int preComNum = 0;
        byte[] cdcBytes = new byte[512];
        int FIFOPtr = 0;
        byte[] cdc1Bytes = new byte[512];
        int FIFOPtr1 = 0;
        float mahSum = 0.0F;
        String filePath = String.Empty;
        String preDateTime = String.Empty;
        byte percent = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonUartSw_Click(object sender, EventArgs e)
        {
            if(buttonUartSw.Text == "Open") {
                try {
                    serialPortUart.PortName = comboBoxUart.SelectedItem.ToString();
                    serialPortUart.Open();
                    buttonUartSw.Text = "Close";
                    comboBoxUart.Enabled = false;
                } catch(Exception ) {
                    
                }
            } else {
                try {
                    serialPortUart.Close();
                } catch(Exception) {

                } finally {
                    buttonUartSw.Text = "Open";
                    comboBoxUart.Enabled = true;
                }

            }
        }

        private void timerComRefresh_Tick(object sender, EventArgs e)
        {
            ComRefresh();
        }
        private void ComRefresh()
        {

            RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            
            comNum = 0;
            if(keyCom != null) {
                string[] sSubKeys = keyCom.GetValueNames();
                foreach(string sName in sSubKeys) {
                    string sValue = (string)keyCom.GetValue(sName);
                    comNum++;
                }
                // If number of COM changes and there is no COM working,Refresh the Combobox
                if(preComNum != comNum ) {
                    if(comboBoxUart.Enabled == true) {
                        comboBoxUart.Items.Clear();
                        foreach(string sName in sSubKeys) {
                            string sValue = (string)keyCom.GetValue(sName);
                            comboBoxUart.Items.Add(sValue);
                        }
                        comboBoxUart.SelectedIndex = comboBoxUart.Items.Count - 1;
                    }
                    if(comboBoxUart1.Enabled == true) {
                        comboBoxUart1.Items.Clear();
                        foreach(string sName in sSubKeys) {
                            string sValue = (string)keyCom.GetValue(sName);
                            comboBoxUart1.Items.Add(sValue);
                        }
                        comboBoxUart1.SelectedIndex = comboBoxUart1.Items.Count - 1;
                    }
                    if(comboBoxUartUart.Enabled == true) {
                        comboBoxUartUart.Items.Clear();
                        foreach(string sName in sSubKeys) {
                            string sValue = (string)keyCom.GetValue(sName);
                            comboBoxUartUart.Items.Add(sValue);
                        }
                        comboBoxUartUart.SelectedIndex = comboBoxUartUart.Items.Count - 1;
                    }
                    preComNum = comNum;
                }
            }
        }
        private void serialPortUart2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] cdcBytes = new byte[512];
            int readLength = serialPortUart2.BytesToRead;

            serialPortUart2.Read(cdcBytes, 0, readLength);
            if(readLength == 14) {
                if(cdcBytes[0] == 0x7E && cdcBytes[4] == 0x46 && cdcBytes[5] == 0x00) {
                    percent = cdcBytes[10];
                }
            }
            this.textBoxDisplay2.Invoke(     // 在拥有此控件的基础窗口句柄的线程上执行委托Invoke(Delegate)
                                      // 即在textBox_ReceiveDate控件的父窗口form中执行委托.
                new MethodInvoker( // 表示一个委托，该委托可执行托管代码中声明为 void 且不接
                                   // 受任何参数的任何方法。在对控件的 Invoke 方法进行调用时
                                   // 或需要一个简单委托又不想自己定义时可以使用该委托。
                    delegate
                    {   // 匿名方法,C#2.0的新功能，这是一种允许程序员将一段完整
                        // 代码区块当成参数传递的程序代码编写技术，通过此种方法可以直接使用委托来设计事件响应程序
                        // 以下就是你要在主线程上实现的功能，但是有一点要注意，这里不适宜处理过多的方法，因为C#消息机
                        // 制是消息流水线响应机制，如果这里在主线程上处理语句的时间过长会导致主UI线程阻塞，停止响应或响
                        // 应不顺畅,这时你的主form界面会延迟或卡死

                        // 文本控件
                        String newStr = String.Empty;
                        for(int i = 0; i < readLength; i++) {
                            newStr += String.Format("{0:X2}", cdcBytes[i]);
                            newStr += " ";
                        }
                        textBoxDisplay2.AppendText(newStr + "   ".ToString());
                        textBoxDisplay2.AppendText(Environment.NewLine);
                        textBoxDisplay2.Select(textBoxDisplay2.TextLength, 0);
                        textBoxDisplay2.ScrollToCaret();
                    }
                )
            );
        }
        private void serialPortUart_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int readLength = serialPortUart.BytesToRead;

            serialPortUart.Read(cdcBytes, FIFOPtr, readLength);
            FIFOPtr += readLength;

            if((cdcBytes[0] == 0x2B) || (cdcBytes[0] == 0x2D)) {
                // 足够一个数据包时，开始解析
                if(FIFOPtr == 14) {
                    if((cdcBytes[12] == 0x0D) || (cdcBytes[13] == 0x0A)) {
                        // 用于tb_display另开线程显示
                        this.textBoxDisplay.Invoke(
                            new MethodInvoker(
                                delegate
                                {
                                    String newStr = String.Empty;

                                    for(int i = 0; i < 14; i++) {
                                        newStr += Convert.ToString(cdcBytes[i] / 16, 16);
                                        newStr += Convert.ToString(cdcBytes[i] % 16, 16);
                                        newStr += " ";
                                    }
                                    textBoxDisplay.AppendText(newStr);
                                    textBoxDisplay.AppendText(Environment.NewLine);
                                    textBoxDisplay.ScrollToCaret();
                                    textBoxDisplay.Select(textBoxDisplay.TextLength, 0);
                                    textBoxDisplay.ScrollToCaret();
                                }
                                )
                            );

                        // 用于显示实际电压值
                        this.labelValue.Invoke(
                            new MethodInvoker(
                                delegate
                                {
                                    String newStr = String.Empty;
                                    if((cdcBytes[11] & 0x80) == 0x80) {
                                        newStr += '-';
                                    }
                                    for(int i = 0; i < 4; i++) {
                                        if(cdcBytes[i + 1] == 0x3f) {
                                            newStr += ' ';
                                        } else if(cdcBytes[i + 1] == 0x3a) {
                                            newStr += 'L';
                                        } else {
                                            newStr += Convert.ToString(cdcBytes[i + 1] - 0x30);
                                        }
                                        if((cdcBytes[6] & (1 << i)) != 0) {
                                            newStr += '.';
                                        }
                                    }
                                    switch(cdcBytes[10]) {
                                        case 0x80:
                                            switch(cdcBytes[9]) {
                                                case 0x00:
                                                    labelUint.Text = "V";
                                                    break;
                                                case 0x04:
                                                    labelUint.Text = "D:" + " V";
                                                    break;
                                                case 0x40:
                                                    labelUint.Text = "mV";
                                                    break;
                                                default:
                                                    labelUint.Text = "Uint";
                                                    break;
                                            }
                                            break;
                                        case 0x40:
                                            switch(cdcBytes[9]) {
                                                case 0x00:
                                                    labelUint.Text = "A";
                                                    break;
                                                case 0x40:
                                                    labelUint.Text = "mA";
                                                    break;
                                                case 0x80:
                                                    labelUint.Text = "uA";
                                                    break;
                                                default:
                                                    labelUint.Text = "Uint";
                                                    break;
                                            }
                                            break;
                                        case 0x20:
                                            switch(cdcBytes[9]) {
                                                case 0x00:
                                                    labelUint.Text = "Ω";
                                                    break;
                                                case 0x20:
                                                    labelUint.Text = "kΩ";
                                                    break;
                                                case 0x10:
                                                    labelUint.Text = "MΩ";
                                                    break;
                                                default:
                                                    labelUint.Text = "Uint";
                                                    break;
                                            }
                                            break;
                                        case 0x02:
                                            labelUint.Text = "℃";
                                            break;
                                        case 0x08:
                                            labelUint.Text = "Hz";
                                            break;
                                        case 0x04:
                                            labelUint.Text = "nF";
                                            break;
                                        case 0x10:
                                            labelUint.Text = "hFE";
                                            break;
                                        default:
                                            labelUint.Text = "Uint";
                                            break;
                                    }
                                    labelValue.Text = newStr;
                                }
                                )
                                );
                        // 完结后清零
                        FIFOPtr = 0;
                    }
                }
            } else {
                // 如果其实头不对，抛弃数据直到真确
                labelUint.Text = "Uint";
                labelValue.Text = "Value";
                FIFOPtr = 0;
            }
        }
        private void serialPortUart1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int readLength = serialPortUart1.BytesToRead;

            serialPortUart1.Read(cdc1Bytes, FIFOPtr1, readLength);
            FIFOPtr1 += readLength;

            if((cdc1Bytes[0] == 0x2B) || (cdc1Bytes[0] == 0x2D)) {
                // 足够一个数据包时，开始解析
                if(FIFOPtr1 == 14) {
                    if((cdc1Bytes[12] == 0x0D) || (cdc1Bytes[13] == 0x0A)) {
                        // 用于tb_display另开线程显示
                        this.textBoxDisplay1.Invoke(
                            new MethodInvoker(
                                delegate
                                {
                                    String newStr = String.Empty;

                                    for(int i = 0; i < 14; i++) {
                                        newStr += Convert.ToString(cdc1Bytes[i] / 16, 16);
                                        newStr += Convert.ToString(cdc1Bytes[i] % 16, 16);
                                        newStr += " ";
                                    }
                                    textBoxDisplay1.AppendText(newStr);
                                    textBoxDisplay1.AppendText(Environment.NewLine);
                                    textBoxDisplay1.ScrollToCaret();
                                    textBoxDisplay1.Focus();
                                    textBoxDisplay1.Select(textBoxDisplay1.TextLength, 0);
                                    textBoxDisplay1.ScrollToCaret();
                                }
                                )
                            );

                        // 用于显示实际电压值
                        this.labelValue1.Invoke(
                            new MethodInvoker(
                                delegate
                                {
                                    String newStr = String.Empty;
                                    if((cdc1Bytes[11] & 0x80) == 0x80) {
                                        newStr += '-';
                                    }
                                    for(int i = 0; i < 4; i++) {
                                        if(cdc1Bytes[i+1] == 0x3f) {
                                            newStr += ' ';
                                        } else if(cdc1Bytes[i+1] == 0x3a) {
                                            newStr += 'L';
                                        } else {
                                            newStr += Convert.ToString(cdc1Bytes[i + 1] - 0x30);
                                        }
                                        if((cdc1Bytes[6] & (1 << i)) != 0) {
                                            newStr += '.';
                                        }
                                    }
                                    switch(cdc1Bytes[10]) {
                                        case 0x80:
                                            switch(cdc1Bytes[9]) {
                                                case 0x00:
                                                    labelUint1.Text = "V";
                                                    break;
                                                case 0x04:
                                                    labelUint1.Text = "D:" + " V";
                                                    break;
                                                case 0x40:
                                                    labelUint1.Text = "mV";
                                                    break;
                                                default:
                                                    labelUint1.Text = "Uint";
                                                    break;
                                            }
                                            break;
                                        case 0x40:
                                            switch(cdc1Bytes[9]) {
                                                case 0x00:
                                                    labelUint1.Text = "A";
                                                    break;
                                                case 0x40:
                                                    labelUint1.Text = "mA";
                                                    break;
                                                case 0x80:
                                                    labelUint1.Text = "uA";
                                                    break;
                                                default:
                                                    labelUint1.Text = "Uint";
                                                    break;
                                            }
                                            break;
                                        case 0x20:
                                            switch(cdc1Bytes[9]) {
                                                case 0x00:
                                                    labelUint1.Text = "Ω";
                                                    break;
                                                case 0x20:
                                                    labelUint1.Text = "kΩ";
                                                    break;
                                                case 0x10:
                                                    labelUint1.Text = "MΩ";
                                                    break;
                                                default:
                                                    labelUint1.Text = "Uint";
                                                    break;
                                            }
                                            break;
                                        case 0x02:
                                            labelUint1.Text = "℃";
                                            break;
                                        case 0x08:
                                            labelUint1.Text = "Hz";
                                            break;
                                        case 0x04:
                                            labelUint1.Text = "nF";
                                            break;
                                        case 0x10:
                                            labelUint1.Text = "hFE";
                                            break;
                                        default:
                                            labelUint1.Text = "Uint";
                                            break;
                                    }
                                   
                                    labelValue1.Text = newStr;
                                }
                                )
                                );
                        // 完结后清零
                        FIFOPtr1 = 0;
                    }
                }
            } else {
                // 如果其实头不对，抛弃数据直到真确
                labelUint1.Text = "Uint";
                labelValue1.Text = "Value";
                FIFOPtr1 = 0;
            }
        }
        private void buttonUart1Sw_Click(object sender, EventArgs e)
        {
            if(buttonUart1Sw.Text == "Open") {
                try {
                    serialPortUart1.PortName = comboBoxUart1.SelectedItem.ToString();
                    serialPortUart1.Open();
                    buttonUart1Sw.Text = "Close";
                    comboBoxUart1.Enabled = false;
                } catch(Exception) {

                }
            } else {
                try {
                    serialPortUart1.Close();
                } catch(Exception) {

                } finally {
                    buttonUart1Sw.Text = "Open";
                    comboBoxUart1.Enabled = true;
                }

            }
        }
        int count = 0;
        private void timerSecond_Tick(object sender, EventArgs e)
        {
            count++;
            if(labelUint1.Text == "mA") {
                mahSum += (float.Parse(labelValue1.Text))/3600.0F;
            }
            if(buttonUartSw.Text == "Close" && buttonUart1Sw.Text == "Close") {
                string str = count.ToString() + '\t' + labelValue.Text + '\t' + labelUint.Text + '\t' + labelValue1.Text + '\t' + labelUint1.Text + '\t' + mahSum.ToString() + '\t' + "mAh" + '\t' + percent.ToString();
                textBoxVolAndCrt.AppendText(str);
                textBoxVolAndCrt.AppendText(Environment.NewLine);
                textBoxVolAndCrt.ScrollToCaret();
                // 对TXT进行数据备份
                if(filePath != String.Empty) {
                    if(preDateTime != DateTime.Now.ToString()) {
                        StreamWriter sw = new StreamWriter(filePath, true);
                        preDateTime = DateTime.Now.ToString();
                        sw.WriteLine("" + DateTime.Now.ToString() + "\t" + str);
                        sw.Close();
                    }
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Filter = "文本文件(*.txt)|*.txt";
            if(saveFile.ShowDialog() == DialogResult.OK) {
                StreamWriter sw = new StreamWriter(saveFile.FileName, true);
                filePath = saveFile.FileName;
                sw.WriteLine("日期 时间 数据");
                sw.Close();
            }
        }

        private void buttonAutoSave_Click(object sender, EventArgs e)
        {
            if(buttonAutoSave.Text == "AutoSave") {
                StreamWriter sw = new StreamWriter(@"log.txt", true);
                filePath = @"log.txt";
                sw.WriteLine("日期 时间 数据");
                sw.Close();
                buttonAutoSave.Text = "Saving";
            } else {
                filePath = String.Empty;
                buttonAutoSave.Text = "AutoSave";
            }

        }

        private void buttonDeleteLog_Click(object sender, EventArgs e)
        {
            if(buttonAutoSave.Text == "Saving") {
                buttonAutoSave.Text = "AutoSave";
            } if(File.Exists(@"log.txt")) {
                File.Delete(@"log.txt");
            }
        }

        private void buttonUart_Click(object sender, EventArgs e)
        {
            if(buttonUart.Text == "Open") {
                try {
                    serialPortUart2.PortName = comboBoxUartUart.SelectedItem.ToString();
                    serialPortUart2.Open();
                    buttonUart.Text = "Close";
                    comboBoxUartUart.Enabled = false;
                } catch(Exception) {

                }
            } else {
                try {
                    serialPortUart2.Close();
                } catch(Exception) {

                } finally {
                    buttonUart.Text = "Open";
                    comboBoxUartUart.Enabled = true;
                }

            }
        }

        private void comboBoxUart1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
