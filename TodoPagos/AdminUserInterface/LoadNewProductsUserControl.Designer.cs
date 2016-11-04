namespace AdminUserInterface
{
    partial class LoadNewProductsUserControl
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
            this.lblLoadProducts = new System.Windows.Forms.Label();
            this.lblLoadFromDLL = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLoadProducts
            // 
            this.lblLoadProducts.AutoSize = true;
            this.lblLoadProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadProducts.Location = new System.Drawing.Point(301, 35);
            this.lblLoadProducts.Name = "lblLoadProducts";
            this.lblLoadProducts.Size = new System.Drawing.Size(266, 32);
            this.lblLoadProducts.TabIndex = 0;
            this.lblLoadProducts.Text = "Carga de Productos";
            // 
            // lblLoadFromDLL
            // 
            this.lblLoadFromDLL.AutoSize = true;
            this.lblLoadFromDLL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadFromDLL.Location = new System.Drawing.Point(222, 210);
            this.lblLoadFromDLL.Name = "lblLoadFromDLL";
            this.lblLoadFromDLL.Size = new System.Drawing.Size(220, 20);
            this.lblLoadFromDLL.TabIndex = 1;
            this.lblLoadFromDLL.Text = "Cargar productos desde .dll:";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(479, 210);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(144, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Seleccionar .dll";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // LoadNewProductsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.lblLoadFromDLL);
            this.Controls.Add(this.lblLoadProducts);
            this.Name = "LoadNewProductsUserControl";
            this.Size = new System.Drawing.Size(853, 604);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLoadProducts;
        private System.Windows.Forms.Label lblLoadFromDLL;
        private System.Windows.Forms.Button btnSelectFile;
    }
}
