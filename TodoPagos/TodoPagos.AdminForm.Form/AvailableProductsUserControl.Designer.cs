namespace TodoPagos.AdminForm.Form
{
    partial class AvailableProductsUserControl
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
            this.lblProducts = new System.Windows.Forms.Label();
            this.lblActualAvailableProducts = new System.Windows.Forms.Label();
            this.lstActualAvailableProducts = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblProducts
            // 
            this.lblProducts.AutoSize = true;
            this.lblProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducts.Location = new System.Drawing.Point(363, 33);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(143, 32);
            this.lblProducts.TabIndex = 0;
            this.lblProducts.Text = "Productos";
            // 
            // lblActualAvailableProducts
            // 
            this.lblActualAvailableProducts.AutoSize = true;
            this.lblActualAvailableProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActualAvailableProducts.Location = new System.Drawing.Point(110, 102);
            this.lblActualAvailableProducts.Name = "lblActualAvailableProducts";
            this.lblActualAvailableProducts.Size = new System.Drawing.Size(280, 20);
            this.lblActualAvailableProducts.TabIndex = 1;
            this.lblActualAvailableProducts.Text = "Productos disponibles actualmente: ";
            // 
            // lstActualAvailableProducts
            // 
            this.lstActualAvailableProducts.FormattingEnabled = true;
            this.lstActualAvailableProducts.ItemHeight = 16;
            this.lstActualAvailableProducts.Location = new System.Drawing.Point(114, 126);
            this.lstActualAvailableProducts.Name = "lstActualAvailableProducts";
            this.lstActualAvailableProducts.Size = new System.Drawing.Size(634, 420);
            this.lstActualAvailableProducts.TabIndex = 2;
            // 
            // AvailableProductsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstActualAvailableProducts);
            this.Controls.Add(this.lblActualAvailableProducts);
            this.Controls.Add(this.lblProducts);
            this.Name = "AvailableProductsUserControl";
            this.Size = new System.Drawing.Size(853, 604);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.Label lblActualAvailableProducts;
        private System.Windows.Forms.ListBox lstActualAvailableProducts;
    }
}
