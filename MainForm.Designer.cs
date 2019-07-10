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
            this.bwLoadConsoleCommand = new System.ComponentModel.BackgroundWorker();
            this.bwLoadGeoMaps = new System.ComponentModel.BackgroundWorker();
            this.myTreeView = new GeoMapConverter.MyTreeView();
            this.SuspendLayout();
            // 
            // btnOpenConsoleCommand
            // 
            this.btnOpenConsoleCommand.Location = new System.Drawing.Point(14, 13);
            this.btnOpenConsoleCommand.Name = "btnOpenConsoleCommand";
            this.btnOpenConsoleCommand.Size = new System.Drawing.Size(168, 23);
            this.btnOpenConsoleCommand.TabIndex = 1;
            this.btnOpenConsoleCommand.Text = "Open Console Command XML";
            this.btnOpenConsoleCommand.UseVisualStyleBackColor = true;
            this.btnOpenConsoleCommand.Click += new System.EventHandler(this.BtnOpenConsoleCommand_Click);
            // 
            // btnOpenGeoMaps
            // 
            this.btnOpenGeoMaps.Enabled = false;
            this.btnOpenGeoMaps.Location = new System.Drawing.Point(188, 13);
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
            this.btnExport.Location = new System.Drawing.Point(14, 298);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(303, 32);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "Export Checked GeoMaps";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // dlgConsoleCommand
            // 
            this.dlgConsoleCommand.FileName = "ConsoleCommandControl.xml";
            this.dlgConsoleCommand.Filter = "Console Command Config|*.xml";
            // 
            // dlgGeoMaps
            // 
            this.dlgGeoMaps.FileName = "GeoMaps.xml";
            this.dlgGeoMaps.Filter = "GeoMaps|*.xml";
            // 
            // dlgSaveMaps
            // 
            this.dlgSaveMaps.FileName = "XXX GeoMaps.xml";
            this.dlgSaveMaps.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            this.dlgSaveMaps.Title = "Save GeoMaps As...";
            // 
            // bwLoadConsoleCommand
            // 
            this.bwLoadConsoleCommand.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwLoadConsoleCommand_DoWork);
            this.bwLoadConsoleCommand.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwLoadConsoleCommand_RunWorkerCompleted);
            // 
            // bwLoadGeoMaps
            // 
            this.bwLoadGeoMaps.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwLoadGeoMaps_DoWork);
            this.bwLoadGeoMaps.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwLoadGeoMaps_RunWorkerCompleted);
            // 
            // myTreeView
            // 
            this.myTreeView.CheckBoxes = true;
            this.myTreeView.Location = new System.Drawing.Point(14, 42);
            this.myTreeView.Name = "myTreeView";
            this.myTreeView.Size = new System.Drawing.Size(303, 250);
            this.myTreeView.TabIndex = 5;
            this.myTreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterCheck);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 343);
            this.Controls.Add(this.myTreeView);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnOpenGeoMaps);
            this.Controls.Add(this.btnOpenConsoleCommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GeoMap Converter";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOpenConsoleCommand;
        private System.Windows.Forms.Button btnOpenGeoMaps;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.OpenFileDialog dlgConsoleCommand;
        private System.Windows.Forms.OpenFileDialog dlgGeoMaps;
        private System.Windows.Forms.SaveFileDialog dlgSaveMaps;
        private MyTreeView myTreeView;
        private System.ComponentModel.BackgroundWorker bwLoadConsoleCommand;
        private System.ComponentModel.BackgroundWorker bwLoadGeoMaps;
    }
}

