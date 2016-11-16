namespace TodoPagos.AdminForm.Form
{
    partial class PrincipalUserControl
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnProductLoad = new System.Windows.Forms.Button();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnPoints = new System.Windows.Forms.Button();
            this.lblWelcomeLoad = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.btnExit);
            this.splitContainer.Panel1.Controls.Add(this.btnProductLoad);
            this.splitContainer.Panel1.Controls.Add(this.btnLog);
            this.splitContainer.Panel1.Controls.Add(this.btnProducts);
            this.splitContainer.Panel1.Controls.Add(this.btnPoints);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.lblWelcomeLoad);
            this.splitContainer.Panel2.Controls.Add(this.lblWelcome);
            this.splitContainer.Size = new System.Drawing.Size(754, 490);
            this.splitContainer.SplitterDistance = 114;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 0;
            // 
            // btnProductLoad
            // 
            this.btnProductLoad.Location = new System.Drawing.Point(0, 184);
            this.btnProductLoad.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnProductLoad.Name = "btnProductLoad";
            this.btnProductLoad.Size = new System.Drawing.Size(115, 105);
            this.btnProductLoad.TabIndex = 3;
            this.btnProductLoad.Text = "Carga de Productos";
            this.btnProductLoad.UseVisualStyleBackColor = true;
            this.btnProductLoad.Click += new System.EventHandler(this.btnProductLoad_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(0, 286);
            this.btnLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(115, 102);
            this.btnLog.TabIndex = 1;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(0, 384);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(115, 103);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Salir";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.Location = new System.Drawing.Point(0, 90);
            this.btnProducts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(115, 99);
            this.btnProducts.TabIndex = 1;
            this.btnProducts.Text = "Productos";
            this.btnProducts.UseVisualStyleBackColor = true;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnPoints
            // 
            this.btnPoints.Location = new System.Drawing.Point(0, 0);
            this.btnPoints.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnPoints.Name = "btnPoints";
            this.btnPoints.Size = new System.Drawing.Size(115, 94);
            this.btnPoints.TabIndex = 0;
            this.btnPoints.Text = "Puntos";
            this.btnPoints.UseVisualStyleBackColor = true;
            this.btnPoints.Click += new System.EventHandler(this.btnPoints_Click);
            // 
            // lblWelcomeLoad
            // 
            this.lblWelcomeLoad.AutoSize = true;
            this.lblWelcomeLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeLoad.Location = new System.Drawing.Point(326, 219);
            this.lblWelcomeLoad.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcomeLoad.Name = "lblWelcomeLoad";
            this.lblWelcomeLoad.Size = new System.Drawing.Size(0, 26);
            this.lblWelcomeLoad.TabIndex = 1;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(202, 219);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(120, 26);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Bienvenido";
            // 
            // PrincipalUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PrincipalUserControl";
            this.Size = new System.Drawing.Size(754, 490);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button btnProductLoad;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnPoints;
        private System.Windows.Forms.Label lblWelcomeLoad;
        private System.Windows.Forms.Label lblWelcome;
    }
}
