namespace AdminUserInterface
{
    partial class LogUserControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblLog = new System.Windows.Forms.Label();
            this.from = new System.Windows.Forms.DateTimePicker();
            this.to = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLog.Location = new System.Drawing.Point(319, 37);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(229, 32);
            this.lblLog.TabIndex = 0;
            this.lblLog.Text = "Consulta del Log";
            // 
            // from
            // 
            this.from.Location = new System.Drawing.Point(208, 132);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(200, 22);
            this.from.TabIndex = 1;
            // 
            // to
            // 
            this.to.Location = new System.Drawing.Point(547, 132);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(200, 22);
            this.to.TabIndex = 2;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(477, 134);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(64, 20);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "Hasta: ";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(134, 134);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(68, 20);
            this.lblFrom.TabIndex = 4;
            this.lblFrom.Text = "Desde: ";
            // 
            // lstLog
            // 
            this.lstLog.Location = new System.Drawing.Point(138, 175);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(609, 338);
            this.lstLog.TabIndex = 5;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            // 
            // LogUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.to);
            this.Controls.Add(this.from);
            this.Controls.Add(this.lblLog);
            this.Name = "LogUserControl";
            this.Size = new System.Drawing.Size(853, 604);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.DateTimePicker from;
        private System.Windows.Forms.DateTimePicker to;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.ListView lstLog;
    }
}
