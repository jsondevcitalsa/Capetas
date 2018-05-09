namespace Consulta
{
    partial class Partesvendidas
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
            this.dgVer = new System.Windows.Forms.DataGridView();
            this.btnCon = new System.Windows.Forms.Button();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.Desde = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgVer)).BeginInit();
            this.SuspendLayout();
            // 
            // dgVer
            // 
            this.dgVer.AllowUserToAddRows = false;
            this.dgVer.AllowUserToDeleteRows = false;
            this.dgVer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgVer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVer.Location = new System.Drawing.Point(12, 49);
            this.dgVer.Name = "dgVer";
            this.dgVer.ReadOnly = true;
            this.dgVer.Size = new System.Drawing.Size(753, 263);
            this.dgVer.TabIndex = 2;
            // 
            // btnCon
            // 
            this.btnCon.Location = new System.Drawing.Point(554, 18);
            this.btnCon.Name = "btnCon";
            this.btnCon.Size = new System.Drawing.Size(96, 23);
            this.btnCon.TabIndex = 3;
            this.btnCon.Text = "Consultar";
            this.btnCon.UseVisualStyleBackColor = true;
            this.btnCon.Click += new System.EventHandler(this.btnCon_Click);
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(56, 21);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(200, 20);
            this.dtpDesde.TabIndex = 4;
            this.dtpDesde.Value = new System.DateTime(2017, 1, 2, 0, 0, 0, 0);
            // 
            // Desde
            // 
            this.Desde.AutoSize = true;
            this.Desde.Location = new System.Drawing.Point(12, 28);
            this.Desde.Name = "Desde";
            this.Desde.Size = new System.Drawing.Size(38, 13);
            this.Desde.TabIndex = 5;
            this.Desde.Text = "Desde";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Haste";
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(332, 20);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(200, 20);
            this.dtpHasta.TabIndex = 7;
            this.dtpHasta.Value = new System.DateTime(2017, 1, 5, 0, 0, 0, 0);
            // 
            // Partesvendidas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 324);
            this.Controls.Add(this.dtpHasta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Desde);
            this.Controls.Add(this.dtpDesde);
            this.Controls.Add(this.btnCon);
            this.Controls.Add(this.dgVer);
            this.Name = "Partesvendidas";
            this.Text = "Partesvendidas";
            this.Load += new System.EventHandler(this.Partesvendidas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgVer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgVer;
        private System.Windows.Forms.Button btnCon;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.Label Desde;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpHasta;
    }
}