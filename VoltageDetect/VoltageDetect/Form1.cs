using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                    preComNum = comNum;
                }
            }
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
                                    //                                    newStr += Convert.ToString(readLength);

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

                                    if((cdcBytes[9] == 0x00) && (cdcBytes[10] == 0x80)) {
                                        newStr += Convert.ToString(cdcBytes[1] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdcBytes[2] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[3] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[4] - 0x30);
                                        labelUint.Text = "   V";
                                    } else if((cdcBytes[9] == 0x40) && (cdcBytes[10] == 0x40)) {
                                        newStr += Convert.ToString(cdcBytes[1] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[2] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdcBytes[3] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[4] - 0x30);
                                        labelUint.Text = "  mA";
                                    } else if((cdcBytes[9] == 0x00) && (cdcBytes[10] == 0x40)) {
                                        newStr += Convert.ToString(cdcBytes[1] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdcBytes[2] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[3] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[4] - 0x30);
                                        labelUint.Text = "   A";
                                    } else if((cdcBytes[9] == 0x40) && (cdcBytes[10] == 0x80)) {
                                        newStr += Convert.ToString(cdcBytes[1] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[2] - 0x30);
                                        newStr += Convert.ToString(cdcBytes[3] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdcBytes[4] - 0x30);
                                        labelUint.Text = "  mV";
                                    }
                                    labelValue.Text = newStr;

                                    // 对TXT进行数据备份
                                    //if(filePath != String.Empty) {
                                    //    if(preDateTime != DateTime.Now.ToString()) {
                                    //        StreamWriter sw = new StreamWriter(filePath, true);
                                    //        preDateTime = DateTime.Now.ToString();
                                    //        sw.WriteLine("" + DateTime.Now.ToString() + " " + newStr);
                                    //        sw.Close();
                                    //    }
                                    //}
                                }
                                )
                                );
                        // 完结后清零
                        FIFOPtr = 0;
                    }
                }
            } else {
                // 如果其实头不对，抛弃数据直到真确
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
                                    //                                    newStr += Convert.ToString(readLength);

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
                                    if((cdc1Bytes[9] == 0x00) && (cdc1Bytes[10] == 0x80)) {
                                        newStr += Convert.ToString(cdc1Bytes[1] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdc1Bytes[2] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[3] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[4] - 0x30);
                                        labelUint1.Text = "   V";
                                    } else if((cdc1Bytes[9] == 0x40) && (cdc1Bytes[10] == 0x40)) {
                                        newStr += Convert.ToString(cdc1Bytes[1] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[2] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdc1Bytes[3] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[4] - 0x30);
                                        labelUint1.Text = "  mA";
                                    } else if((cdc1Bytes[9] == 0x00) && (cdc1Bytes[10] == 0x40)) {
                                        newStr += Convert.ToString(cdc1Bytes[1] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdc1Bytes[2] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[3] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[4] - 0x30);
                                        labelUint1.Text = "   A";
                                    } else if((cdc1Bytes[9] == 0x40) && (cdc1Bytes[10] == 0x80)) {
                                        newStr += Convert.ToString(cdc1Bytes[1] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[2] - 0x30);
                                        newStr += Convert.ToString(cdc1Bytes[3] - 0x30);
                                        newStr += '.';
                                        newStr += Convert.ToString(cdc1Bytes[4] - 0x30);
                                        labelUint1.Text = "  mV";
                                    }
                                    labelValue1.Text = newStr;

                                    // 对TXT进行数据备份
                                    //if(filePath != String.Empty) {
                                    //    if(preDateTime != DateTime.Now.ToString()) {
                                    //        StreamWriter sw = new StreamWriter(filePath, true);
                                    //        preDateTime = DateTime.Now.ToString();
                                    //        sw.WriteLine("" + DateTime.Now.ToString() + " " + newStr);
                                    //        sw.Close();
                                    //    }
                                    //}
                                }
                                )
                                );
                        // 完结后清零
                        FIFOPtr1 = 0;
                    }
                }
            } else {
                // 如果其实头不对，抛弃数据直到真确
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
            if(buttonUartSw.Text == "Close" && buttonUart1Sw.Text == "Close") {
                string str = count.ToString() + '\t' + labelValue.Text + '\t' + labelUint.Text + '\t' + labelValue1.Text + '\t' + labelUint1.Text;
                textBoxVolAndCrt.AppendText(str);
                textBoxVolAndCrt.AppendText(Environment.NewLine);
                textBoxVolAndCrt.ScrollToCaret();



                //textBoxVolAndCrt.Text += str;
                //textBoxDisplay1.AppendText(newStr);
                //textBoxDisplay1.AppendText(Environment.NewLine);
                //textBoxDisplay1.ScrollToCaret();
                //textBoxDisplay1.Focus();
                //textBoxDisplay1.Select(textBoxDisplay1.TextLength, 0);
                //textBoxDisplay1.ScrollToCaret();
            }
        }
    }
}
