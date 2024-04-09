//-----------------------------------------------------------------------
// Form1.Designer.cs
// contains implementation of UI class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HW9
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            dataGridView1 = new DataGridView();
            menuStrip1 = new MenuStrip();
            editToolStripMenuItem = new ToolStripMenuItem();
            cellToolStripMenuItem = new ToolStripMenuItem();
            changeSelectedCellsBackgroundColorToolStripMenuItem = new ToolStripMenuItem();
            colorDialog1 = new ColorDialog();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Location = new Point(12, 36);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridView1.Size = new Size(1122, 585);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellBeginEdit += DataGridView1_CellBeginEdit;
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem, cellToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1146, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            undoToolStripMenuItem.Enabled = false;
            undoToolStripMenuItem.Text = "Undo Unavailable";
            redoToolStripMenuItem.Enabled = false;
            redoToolStripMenuItem.Text = "Redo Unavailable";
            // 
            // cellToolStripMenuItem
            // 
            cellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeSelectedCellsBackgroundColorToolStripMenuItem });
            cellToolStripMenuItem.Name = "cellToolStripMenuItem";
            cellToolStripMenuItem.Size = new Size(48, 24);
            cellToolStripMenuItem.Text = "Cell";
            // 
            // changeSelectedCellsBackgroundColorToolStripMenuItem
            // 
            changeSelectedCellsBackgroundColorToolStripMenuItem.Name = "changeSelectedCellsBackgroundColorToolStripMenuItem";
            changeSelectedCellsBackgroundColorToolStripMenuItem.Size = new Size(361, 26);
            changeSelectedCellsBackgroundColorToolStripMenuItem.Text = "Change Selected Cells Background Color";
            changeSelectedCellsBackgroundColorToolStripMenuItem.Click += changeSelectedCellsBackgroundColorToolStripMenuItem_Click;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(224, 26);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(224, 26);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1146, 631);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem cellToolStripMenuItem;
        private ToolStripMenuItem changeSelectedCellsBackgroundColorToolStripMenuItem;
        private ColorDialog colorDialog1;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;

        //public void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //    spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text = cell.Value.ToString();
        //}
    }
}
