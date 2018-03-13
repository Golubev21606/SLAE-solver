﻿namespace UI
{
    partial class FormatForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.далееToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.formatBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.далееToolStripMenuItem,
            this.forwardItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 65);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(257, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // далееToolStripMenuItem
            // 
            this.далееToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.далееToolStripMenuItem.Name = "далееToolStripMenuItem";
            this.далееToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.далееToolStripMenuItem.Text = "Назад";
            // 
            // forwardItem
            // 
            this.forwardItem.Enabled = false;
            this.forwardItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forwardItem.Name = "forwardItem";
            this.forwardItem.Size = new System.Drawing.Size(55, 20);
            this.forwardItem.Text = "Далее";
            this.forwardItem.Click += new System.EventHandler(this.forwardItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Выбрать формат хранения:";
            // 
            // formatBox
            // 
            this.formatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatBox.FormattingEnabled = true;
            this.formatBox.Items.AddRange(new object[] {
            "Плотный ",
            "Строчный разреженный без выделенной диагонали",
            "Строчно-столбцовый",
            "Координатный"});
            this.formatBox.Location = new System.Drawing.Point(15, 27);
            this.formatBox.Name = "formatBox";
            this.formatBox.Size = new System.Drawing.Size(197, 21);
            this.formatBox.TabIndex = 16;
            this.formatBox.SelectedIndexChanged += new System.EventHandler(this.formatBox_SelectedIndexChanged);
            // 
            // FormatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 89);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.formatBox);
            this.Controls.Add(this.menuStrip1);
            this.MaximumSize = new System.Drawing.Size(273, 128);
            this.MinimumSize = new System.Drawing.Size(273, 128);
            this.Name = "FormatForm";
            this.Text = "Конструктор СЛАУ";
            this.Load += new System.EventHandler(this.FormatForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem далееToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forwardItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox formatBox;
    }
}