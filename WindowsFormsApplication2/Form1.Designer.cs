﻿namespace CVE_BID_tool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.submit_button = new System.Windows.Forms.Button();
            this.items_box = new System.Windows.Forms.TextBox();
            this.selector = new System.Windows.Forms.ComboBox();
            this.copy_box = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(287, 65);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(78, 23);
            this.submit_button.TabIndex = 0;
            this.submit_button.Text = "Submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // items_box
            // 
            this.items_box.Location = new System.Drawing.Point(138, 39);
            this.items_box.Name = "items_box";
            this.items_box.Size = new System.Drawing.Size(392, 20);
            this.items_box.TabIndex = 1;
            // 
            // selector
            // 
            this.selector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selector.FormattingEnabled = true;
            this.selector.Items.AddRange(new object[] {
            "CVE",
            "BID"});
            this.selector.Location = new System.Drawing.Point(263, 12);
            this.selector.Name = "selector";
            this.selector.Size = new System.Drawing.Size(121, 21);
            this.selector.TabIndex = 2;
            this.selector.SelectedIndexChanged += new System.EventHandler(this.selector_SelectedIndexChanged);
            // 
            // copy_box
            // 
            this.copy_box.Location = new System.Drawing.Point(2, 108);
            this.copy_box.Multiline = true;
            this.copy_box.Name = "copy_box";
            this.copy_box.ReadOnly = true;
            this.copy_box.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.copy_box.Size = new System.Drawing.Size(655, 391);
            this.copy_box.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 500);
            this.Controls.Add(this.copy_box);
            this.Controls.Add(this.selector);
            this.Controls.Add(this.items_box);
            this.Controls.Add(this.submit_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.TextBox items_box;
        private System.Windows.Forms.ComboBox selector;
        private System.Windows.Forms.TextBox copy_box;

    }
}
