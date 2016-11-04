namespace AdminUserInterface
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
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnAvailableProducts = new System.Windows.Forms.Button();
            this.btnPoints = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.btnProductLoad);
            this.splitContainer.Panel1.Controls.Add(this.btnLog);
            this.splitContainer.Panel1.Controls.Add(this.btnSettings);
            this.splitContainer.Panel1.Controls.Add(this.btnAvailableProducts);
            this.splitContainer.Panel1.Controls.Add(this.btnPoints);
            this.splitContainer.Size = new System.Drawing.Size(1006, 603);
            this.splitContainer.SplitterDistance = 153;
            this.splitContainer.TabIndex = 0;
            // 
            // btnProductLoad
            // 
            this.btnProductLoad.Location = new System.Drawing.Point(0, 227);
            this.btnProductLoad.Name = "btnProductLoad";
            this.btnProductLoad.Size = new System.Drawing.Size(153, 129);
            this.btnProductLoad.TabIndex = 3;
            this.btnProductLoad.Text = "Carga de Productos";
            this.btnProductLoad.UseVisualStyleBackColor = true;
            this.btnProductLoad.Click += new System.EventHandler(this.btnProductLoad_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(0, 352);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(153, 126);
            this.btnLog.TabIndex = 1;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(0, 473);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(153, 127);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "Configuración";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnAvailableProducts
            // 
            this.btnAvailableProducts.Location = new System.Drawing.Point(0, 111);
            this.btnAvailableProducts.Name = "btnAvailableProducts";
            this.btnAvailableProducts.Size = new System.Drawing.Size(153, 122);
            this.btnAvailableProducts.TabIndex = 1;
            this.btnAvailableProducts.Text = "Productos Disponibles";
            this.btnAvailableProducts.UseVisualStyleBackColor = true;
            this.btnAvailableProducts.Click += new System.EventHandler(this.btnAvailableProducts_Click);
            // 
            // btnPoints
            // 
            this.btnPoints.Location = new System.Drawing.Point(0, 0);
            this.btnPoints.Name = "btnPoints";
            this.btnPoints.Size = new System.Drawing.Size(153, 116);
            this.btnPoints.TabIndex = 0;
            this.btnPoints.Text = "Puntos";
            this.btnPoints.UseVisualStyleBackColor = true;
            this.btnPoints.Click += new System.EventHandler(this.btnPoints_Click);
            // 
            // PrincipalUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "PrincipalUserControl";
            this.Size = new System.Drawing.Size(1006, 603);
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button btnProductLoad;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnAvailableProducts;
        private System.Windows.Forms.Button btnPoints;
    }
}
