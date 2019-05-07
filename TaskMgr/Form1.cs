using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TaskMgr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            Process[] proList = Process.GetProcesses(".");//获得本机的进程
            lblNum.Text = proList.Length.ToString(); //当前进程数量
            foreach (Process p in proList)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = p.ProcessName;
                lvi.SubItems.AddRange(new string[] { p.Id.ToString(), p.PrivateMemorySize64.ToString() }); //进程ID  使用内存
                lvi.Tag = p;
                listView1.Items.Add(lvi);
            }
        }
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0].Tag != null)
                {
                    try
                    {
                        Process p = (Process)listView1.SelectedItems[0].Tag;
                        p.Kill();
                        listView1.Items.Remove(listView1.SelectedItems[0]);
                    }
                    catch(Exception ee)
                    {
                        MessageBox.Show("无法结束进程 ：" + ee.Message);
                    }
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEnd.Enabled = listView1.SelectedItems.Count > 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRefesh_Click(sender, e);
        }
    }
}
