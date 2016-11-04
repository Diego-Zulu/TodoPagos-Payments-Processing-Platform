namespace AdminUserInterface
{
    partial class PointsManagementUserControl
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
            this.lblPoints = new System.Windows.Forms.Label();
            this.lblActualValue = new System.Windows.Forms.Label();
            this.lblActualValueLoad = new System.Windows.Forms.Label();
            this.lblNewValue = new System.Windows.Forms.Label();
            this.txtNewPointValue = new System.Windows.Forms.TextBox();
            this.btnNewPointValue = new System.Windows.Forms.Button();
            this.lblActualBlacklist = new System.Windows.Forms.Label();
            this.lstActualBlacklist = new System.Windows.Forms.ListView();
            this.lblAddToBlacklist = new System.Windows.Forms.Label();
            this.lstAddToBlacklist = new System.Windows.Forms.ListBox();
            this.btnAddToBlacklist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPoints
            // 
            this.lblPoints.AutoSize = true;
            this.lblPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoints.Location = new System.Drawing.Point(309, 34);
            this.lblPoints.Name = "lblPoints";
            this.lblPoints.Size = new System.Drawing.Size(253, 32);
            this.lblPoints.TabIndex = 0;
            this.lblPoints.Text = "Sistema de Puntos";
            // 
            // lblActualValue
            // 
            this.lblActualValue.AutoSize = true;
            this.lblActualValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualValue.Location = new System.Drawing.Point(167, 100);
            this.lblActualValue.Name = "lblActualValue";
            this.lblActualValue.Size = new System.Drawing.Size(181, 20);
            this.lblActualValue.TabIndex = 1;
            this.lblActualValue.Text = "Valor actual del punto: ";
            // 
            // lblActualValueLoad
            // 
            this.lblActualValueLoad.AutoSize = true;
            this.lblActualValueLoad.Location = new System.Drawing.Point(355, 102);
            this.lblActualValueLoad.Name = "lblActualValueLoad";
            this.lblActualValueLoad.Size = new System.Drawing.Size(0, 17);
            this.lblActualValueLoad.TabIndex = 2;
            // 
            // lblNewValue
            // 
            this.lblNewValue.AutoSize = true;
            this.lblNewValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewValue.Location = new System.Drawing.Point(168, 174);
            this.lblNewValue.Name = "lblNewValue";
            this.lblNewValue.Size = new System.Drawing.Size(180, 20);
            this.lblNewValue.TabIndex = 3;
            this.lblNewValue.Text = "Nuevo valor del punto: ";
            // 
            // txtNewPointValue
            // 
            this.txtNewPointValue.Location = new System.Drawing.Point(372, 174);
            this.txtNewPointValue.Name = "txtNewPointValue";
            this.txtNewPointValue.Size = new System.Drawing.Size(90, 22);
            this.txtNewPointValue.TabIndex = 4;
            // 
            // btnNewPointValue
            // 
            this.btnNewPointValue.Location = new System.Drawing.Point(473, 174);
            this.btnNewPointValue.Name = "btnNewPointValue";
            this.btnNewPointValue.Size = new System.Drawing.Size(98, 23);
            this.btnNewPointValue.TabIndex = 5;
            this.btnNewPointValue.Text = "Cambiar";
            this.btnNewPointValue.UseVisualStyleBackColor = true;
            // 
            // lblActualBlacklist
            // 
            this.lblActualBlacklist.AutoSize = true;
            this.lblActualBlacklist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualBlacklist.Location = new System.Drawing.Point(167, 225);
            this.lblActualBlacklist.Name = "lblActualBlacklist";
            this.lblActualBlacklist.Size = new System.Drawing.Size(275, 20);
            this.lblActualBlacklist.TabIndex = 6;
            this.lblActualBlacklist.Text = "Lista negra de Proveedores actual: ";
            // 
            // lstActualBlacklist
            // 
            this.lstActualBlacklist.Location = new System.Drawing.Point(171, 248);
            this.lstActualBlacklist.Name = "lstActualBlacklist";
            this.lstActualBlacklist.Size = new System.Drawing.Size(541, 116);
            this.lstActualBlacklist.TabIndex = 7;
            this.lstActualBlacklist.UseCompatibleStateImageBehavior = false;
            // 
            // lblAddToBlacklist
            // 
            this.lblAddToBlacklist.AutoSize = true;
            this.lblAddToBlacklist.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddToBlacklist.Location = new System.Drawing.Point(170, 378);
            this.lblAddToBlacklist.Name = "lblAddToBlacklist";
            this.lblAddToBlacklist.Size = new System.Drawing.Size(292, 20);
            this.lblAddToBlacklist.TabIndex = 8;
            this.lblAddToBlacklist.Text = "Agregar a lista negra de Proveedores:";
            // 
            // lstAddToBlacklist
            // 
            this.lstAddToBlacklist.FormattingEnabled = true;
            this.lstAddToBlacklist.ItemHeight = 16;
            this.lstAddToBlacklist.Location = new System.Drawing.Point(174, 402);
            this.lstAddToBlacklist.Name = "lstAddToBlacklist";
            this.lstAddToBlacklist.Size = new System.Drawing.Size(538, 100);
            this.lstAddToBlacklist.TabIndex = 9;
            // 
            // btnAddToBlacklist
            // 
            this.btnAddToBlacklist.Location = new System.Drawing.Point(599, 508);
            this.btnAddToBlacklist.Name = "btnAddToBlacklist";
            this.btnAddToBlacklist.Size = new System.Drawing.Size(113, 35);
            this.btnAddToBlacklist.TabIndex = 10;
            this.btnAddToBlacklist.Text = "Agregar";
            this.btnAddToBlacklist.UseVisualStyleBackColor = true;
            // 
            // PointsManagementUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddToBlacklist);
            this.Controls.Add(this.lstAddToBlacklist);
            this.Controls.Add(this.lblAddToBlacklist);
            this.Controls.Add(this.lstActualBlacklist);
            this.Controls.Add(this.lblActualBlacklist);
            this.Controls.Add(this.btnNewPointValue);
            this.Controls.Add(this.txtNewPointValue);
            this.Controls.Add(this.lblNewValue);
            this.Controls.Add(this.lblActualValueLoad);
            this.Controls.Add(this.lblActualValue);
            this.Controls.Add(this.lblPoints);
            this.Name = "PointsManagementUserControl";
            this.Size = new System.Drawing.Size(853, 604);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPoints;
        private System.Windows.Forms.Label lblActualValue;
        private System.Windows.Forms.Label lblActualValueLoad;
        private System.Windows.Forms.Label lblNewValue;
        private System.Windows.Forms.TextBox txtNewPointValue;
        private System.Windows.Forms.Button btnNewPointValue;
        private System.Windows.Forms.Label lblActualBlacklist;
        private System.Windows.Forms.ListView lstActualBlacklist;
        private System.Windows.Forms.Label lblAddToBlacklist;
        private System.Windows.Forms.ListBox lstAddToBlacklist;
        private System.Windows.Forms.Button btnAddToBlacklist;
    }
}
