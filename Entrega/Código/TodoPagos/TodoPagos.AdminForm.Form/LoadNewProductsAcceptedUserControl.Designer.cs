namespace AdminUserInterface
{
    partial class LoadNewProductsAcceptedUserControl
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
            this.foreignDLLPanel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnLoadProducts = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // foreignDLLPanel
            // 
            this.foreignDLLPanel.Location = new System.Drawing.Point(137, 42);
            this.foreignDLLPanel.Name = "foreignDLLPanel";
            this.foreignDLLPanel.Size = new System.Drawing.Size(599, 220);
            this.foreignDLLPanel.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(137, 385);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(121, 38);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Atrás";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnLoadProducts
            // 
            this.btnLoadProducts.Location = new System.Drawing.Point(615, 385);
            this.btnLoadProducts.Name = "btnLoadProducts";
            this.btnLoadProducts.Size = new System.Drawing.Size(121, 38);
            this.btnLoadProducts.TabIndex = 2;
            this.btnLoadProducts.Text = "Cargar";
            this.btnLoadProducts.UseVisualStyleBackColor = true;
            this.btnLoadProducts.Click += new System.EventHandler(this.btnLoadProducts_Click);
            // 
            // LoadNewProductsAcceptedUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLoadProducts);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.foreignDLLPanel);
            this.Name = "LoadNewProductsAcceptedUserControl";
            this.Size = new System.Drawing.Size(853, 604);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel foreignDLLPanel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnLoadProducts;
    }
}
