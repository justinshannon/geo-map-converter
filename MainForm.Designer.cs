namespace GeoMapConverter
{
    partial class MainForm
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
            this.btnOpenConsoleCommand = new System.Windows.Forms.Button();
            this.btnOpenGeoMaps = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.dlgConsoleCommand = new System.Windows.Forms.OpenFileDialog();
            this.dlgGeoMaps = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveMaps = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.myTreeView = new GeoMapConverter.MyTreeView();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpenConsoleCommand
            // 
            this.btnOpenConsoleCommand.Enabled = false;
            this.btnOpenConsoleCommand.Location = new System.Drawing.Point(148, 12);
            this.btnOpenConsoleCommand.Name = "btnOpenConsoleCommand";
            this.btnOpenConsoleCommand.Size = new System.Drawing.Size(244, 23);
            this.btnOpenConsoleCommand.TabIndex = 1;
            this.btnOpenConsoleCommand.Text = "Open ConsoleCommandControl XML";
            this.btnOpenConsoleCommand.UseVisualStyleBackColor = true;
            this.btnOpenConsoleCommand.Click += new System.EventHandler(this.btnOpenConsoleCommand_Click);
            // 
            // btnOpenGeoMaps
            // 
            this.btnOpenGeoMaps.Location = new System.Drawing.Point(13, 12);
            this.btnOpenGeoMaps.Name = "btnOpenGeoMaps";
            this.btnOpenGeoMaps.Size = new System.Drawing.Size(129, 23);
            this.btnOpenGeoMaps.TabIndex = 2;
            this.btnOpenGeoMaps.Text = "Open GeoMaps XML";
            this.btnOpenGeoMaps.UseVisualStyleBackColor = true;
            this.btnOpenGeoMaps.Click += new System.EventHandler(this.BtnOpenGeoMaps_Click);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(12, 297);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(380, 32);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export Selected GeoMaps";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // dlgConsoleCommand
            // 
            this.dlgConsoleCommand.Filter = "XML Files|*.xml";
            this.dlgConsoleCommand.Title = "Open Console Command Control";
            // 
            // dlgGeoMaps
            // 
            this.dlgGeoMaps.FileName = "GeoMaps.xml";
            this.dlgGeoMaps.Filter = "GeoMaps|*.xml";
            this.dlgGeoMaps.Title = "Open GeoMaps";
            // 
            // dlgSaveMaps
            // 
            this.dlgSaveMaps.FileName = "XXX GeoMaps.xml";
            this.dlgSaveMaps.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.dlgSaveMaps.Title = "Save GeoMaps As...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(404, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(203, 17);
            this.txtStatus.Text = "Please open the GeoMap XML file...";
            // 
            // myTreeView
            // 
            this.myTreeView.CheckBoxes = true;
            this.myTreeView.Enabled = false;
            this.myTreeView.Location = new System.Drawing.Point(13, 41);
            this.myTreeView.Name = "myTreeView";
            this.myTreeView.Size = new System.Drawing.Size(379, 250);
            this.myTreeView.TabIndex = 5;
            this.myTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 361);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.myTreeView);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnOpenGeoMaps);
            this.Controls.Add(this.btnOpenConsoleCommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeoMap Converter";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpenConsoleCommand;
        private System.Windows.Forms.Button btnOpenGeoMaps;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.OpenFileDialog dlgConsoleCommand;
        private System.Windows.Forms.OpenFileDialog dlgGeoMaps;
        private System.Windows.Forms.SaveFileDialog dlgSaveMaps;
        private MyTreeView myTreeView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
    }
}

