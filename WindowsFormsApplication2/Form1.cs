﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CVE_BID_tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            if (selector.SelectedIndex == 0)
            {
                List<CVE> our_cves = new List<CVE>();
                List<BID> our_bids = new List<BID>();
                char[] delimiterChars = { ',' };
                string[] cves = this.items_box.Text.Split(delimiterChars);
                string tmp = "";
                foreach (string c in cves)
                {
                    CVE t = new CVE(c);
                    our_cves.Add(t);
                }
                our_cves = our_cves.OrderBy(x => x.getID()).ToList();
                int i = 0;
                foreach (CVE c in our_cves)
                {
                    addTextBoxes(i, c.getID(), c.getDescription());
                    tmp += c.getID() + "\r\n\r\n";
                    tmp += c.getDescription() + "\r\n\r\n";
                    if (c.bids.Count > 0)
                        foreach (BID b in c.bids)
                        {
                            our_bids.Add(b);
                        }
                    i++;
                }
                our_bids = our_bids.GroupBy(x => x.id).Select(g => g.First()).ToList();
                our_bids = our_bids.OrderBy(x => x.id).ToList();
                foreach (BID b in our_bids)
                {
                    tmp += b.id + "\r\n\r\n";
                    tmp += b.description + "\r\n\r\n";
                }
               // copy_box.Text = tmp;
            }
            else if (selector.SelectedIndex == 1)
            {
                List<BID> our_bids = new List<BID>();
                string tmp = "";
                char[] delimiterChars = { ',' };
                string[] bids = this.items_box.Text.Split(delimiterChars);
                foreach (string b in bids)
                {
                    BID t = new BID(b);
                    our_bids.Add(t);
                }
                our_bids = our_bids.OrderBy(x => x.id).ToList();
                foreach (BID b in our_bids)
                {
                    tmp += b.id + "\r\n\r\n";
                    tmp += b.description + "\r\n\r\n";
                }
              //  copy_box.Text = tmp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void selector_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addTextBoxes(int i, string id, string description)
        {
       //     for (int i = 0; i < count; i++)
 //           {
                Label l = new Label();
                l.Location = new System.Drawing.Point(285, i * 100);
                l.Name = "LabelName" + i.ToString();
                l.Size = new System.Drawing.Size(200, 20);
                l.Text = id;
                panel1.Controls.Add(l);
                TextBox tb = new TextBox();
                tb.Location = new System.Drawing.Point(40, 20 + i * 100);
                tb.Name = "TextBoxName" + i.ToString();
                tb.Size = new System.Drawing.Size(600, 20);
                tb.TabIndex = i + 2;
                tb.Text = description;
                tb.Height = (tb.Text.Split('\n').Length + 4) * tb.Font.Height;
                tb.Multiline = true;
                tb.ReadOnly = true;
                tb.Click += new EventHandler(OnTBClick);
                panel1.Controls.Add(tb);
  //          }
        }

        private void OnTBClick(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            Clipboard.SetText(t.Text);          
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
