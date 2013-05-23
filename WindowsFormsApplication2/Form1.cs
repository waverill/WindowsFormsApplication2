using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CVE_BID_tool
{
    public partial class Form1 : Form
    {
        private int buffer;
        private List<Label> form_labels;
        private List<TextBox> form_tbs;
        private List<CVE> cves;
        private List<BID> bids;
        private int count;

        delegate void showProgressDelegate(int cves, int cves_done, CVE c);
        delegate void showProgress2Delegate(int count, BID b, Boolean first);
        delegate void showProgress3Delegate(int done, Boolean hide);
        delegate void showBidsDelegate();

        public Form1()
        {
            InitializeComponent();
            this.form_labels = new List<Label>();
            this.form_tbs = new List<TextBox>();
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            clearControls();
            if (selector.SelectedIndex == 0)
            {
                Thread cveThread = new Thread(new ThreadStart(makeCVEs));
                cveThread.Start();
            }
            else if (selector.SelectedIndex == 1)
            {
                int j = 0;
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
                    addTextBoxes2(j, b.id, b.description, false);
                    tmp += b.id + "\r\n\r\n";
                    tmp += b.description + "\r\n\r\n";
                    j++;
                }
              //  copy_box.Text = tmp;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void selector_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.items_box.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void addTextBoxes(int i, string id, string description)
        {
                int offset = this.panel1.VerticalScroll.Value;
                Label l = new Label();
                l.Location = new System.Drawing.Point(285, (i-1) * 90 - offset);
                l.Name = "LabelName" + i.ToString();
                l.Size = new System.Drawing.Size(200, 20);
                l.Text = id;
                this.panel1.Controls.Add(l);
                this.form_labels.Add(l);
                TextBox tb = new TextBox();
                tb.Location = new System.Drawing.Point(40, ((i-1) * 90) + 20 - offset);
                tb.Name = "TextBoxName" + i.ToString();
                tb.Size = new System.Drawing.Size(600, 20);
                tb.TabIndex = i + 2;
                tb.Text = description;
                tb.Height = (tb.Text.Split('\n').Length + 4) * tb.Font.Height;
                tb.Multiline = true;
                tb.ReadOnly = true;
                tb.Click += new EventHandler(OnTBClick);
                this.panel1.Controls.Add(tb);
                this.form_tbs.Add(tb);
        }

        private void addTextBoxes2(int i, string id, string description, bool first)
        {
            if (first)
                this.buffer = (90 * i) + 5 - this.panel1.VerticalScroll.Value;
            else
                this.buffer += 50;
            Label l = new Label();
            l.Location = new System.Drawing.Point(305, buffer);
            l.Name = "LabelName" + i.ToString();
            l.Size = new System.Drawing.Size(200, 20);
            l.Text = id;
            l.Click += new EventHandler(OnBIDLabelClick);
            this.panel1.Controls.Add(l);
            this.form_labels.Add(l);
            TextBox tb = new TextBox();
            tb.Location = new System.Drawing.Point(40, 20 + buffer);
            tb.Name = "TextBoxName" + i.ToString();
            tb.Size = new System.Drawing.Size(600, 20);
            tb.TabIndex = i + 2;
            tb.Text = description;
            tb.Height = (tb.Text.Split('\n').Length + 4) * tb.Font.Height;
            tb.ReadOnly = true;
            tb.Click += new EventHandler(OnTBClick);
            this.panel1.Controls.Add(tb);
            this.form_tbs.Add(tb);
        }

        private void displayLabel(int done, Boolean hide)
        {
                this.label1.Visible = !hide;
                this.progressBar1.Visible = !hide;
                this.progressBar1.Maximum = this.cves.Count;
                if (this.progressBar1.Maximum == done)
                    this.progressBar1.Value = 0;
                else
                    this.progressBar1.Value = done;
        }

        private void OnBIDLabelClick(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            Clipboard.SetText(l.Text);
        }

        private void OnTBClick(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            Clipboard.SetText(t.Text);          
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void clearControls()
        {
            for (int x = panel1.Controls.Count - 1; x >= 0; x--)
                panel1.Controls[x].Dispose();
            panel1.Controls.Clear();
            this.buffer = 0;
        }


        void showProgress(int cves, int cves_done, CVE c)
        {
            if (progressBar1.InvokeRequired == false)
            {
                addTextBoxes(cves_done, c.getID(), c.getDescription());
            }
            else
            {
                showProgressDelegate ShowProgress = new showProgressDelegate(showProgress);
                this.BeginInvoke(ShowProgress, new object[] { cves, cves_done, c });
            }

        }


        void showProgress2(int count, BID b, Boolean first)
        {
            if (progressBar1.InvokeRequired == false)
            {
                addTextBoxes2(count, b.id, b.description, first);
            }
            else
            {
                showProgress2Delegate ShowProgress2 = new showProgress2Delegate(showProgress2);
                this.BeginInvoke(ShowProgress2, new object[] { count, b, first});
            }

        }

        void showProgress3(int done, Boolean hide)
        {
            if (progressBar1.InvokeRequired == false)
            {
                displayLabel(done, hide);
            }
            else
            {
                showProgress3Delegate ShowProgress3 = new showProgress3Delegate(showProgress3);
                this.BeginInvoke(ShowProgress3, new object[] { done, hide });
            }

        }

        void showBids()
        {
            if (textBox1.InvokeRequired == false)
            {
                all_da_bids();
            }
            else
            {
                showBidsDelegate ShowBids = new showBidsDelegate(showBids);
                this.BeginInvoke(ShowBids);
            }

        }

        void makeCVEs()
        {
            this.cves = new List<CVE>();
            this.bids = new List<BID>();
            char[] delimiterChars = { ',' };
            string[] cves = this.items_box.Text.Split(delimiterChars);
            int total = cves.Length;
            for (int x = 0; x < total; x++)
                cves[x] = cves[x].Trim();
            this.count = 0;
            Array.Sort(cves);
            foreach (string c in cves)
            {
                CVE t = new CVE(c);
                this.cves.Add(t);
                this.count++;
                showProgress(total, this.count, t);
            }
            Thread bidThread = new Thread(new ThreadStart(CVEsToBIDs));
            bidThread.Start();
        }

        void CVEsToBIDs()
        {
            int done = 0;
            showProgress3(done, false);
            foreach (CVE c in this.cves)
            {
                List<BID> bds = c.curl();
                foreach (BID b in bds)
                {
                    this.bids.Add(b);
                }
                done++;
                showProgress3(done,false);
            }
            showProgress3(0, true);
            this.bids = this.bids.GroupBy(x => x.id).Select(g => g.First()).ToList();
            this.bids = this.bids.OrderBy(x => x.id).ToList();
            bool first = true;
            foreach (BID b in this.bids)
            {
                showProgress2(this.count, b, first);
                this.count++;
                first = false;
            }
            showBids();
        }

        void makeBIDs()
        {

        }

        private void all_da_bids()
        {
            string bid_print = "";
            foreach (BID b in this.bids)
            {
                bid_print += b.id + ", ";
            }
            bid_print = bid_print.Substring(0, bid_print.Length - 1);
            this.textBox1.Text = bid_print;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
