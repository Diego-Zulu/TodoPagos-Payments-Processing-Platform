namespace AdminUserInterface
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
            this.lstActualAvailableProducts = new System.Windows.Forms.ListView();
            this.lblRemoveFromAvaiableProducts = new System.Windows.Forms.Label();
            this.lstRemoveFromAvailableProducts = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblAddToAvailableProducts = new System.Windows.Forms.Label();
            this.btnRemoveFromAvailableProducts = new System.Windows.Forms.Button();
            this.btnAddToAvailableProducts = new System.Windows.Forms.Button();
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
            this.lblActualAvailableProducts.Location = new System.Drawing.Point(125, 102);
            this.lblActualAvailableProducts.Name = "lblActualAvailableProducts";
            this.lblActualAvailableProducts.Size = new System.Drawing.Size(280, 20);
            this.lblActualAvailableProducts.TabIndex = 1;
            this.lblActualAvailableProducts.Text = "Productos disponibles actualmente: ";
            // 
            // lstActualAvailableProducts
            // 
            this.lstActualAvailableProducts.Location = new System.Drawing.Point(129, 125);
            this.lstActualAvailableProducts.Name = "lstActualAvailableProducts";
            this.lstActualAvailableProducts.Size = new System.Drawing.Size(620, 150);
            this.lstActualAvailableProducts.TabIndex = 2;
            this.lstActualAvailableProducts.UseCompatibleStateImageBehavior = false;
            // 
            // lblRemoveFromAvaiableProducts
            // 
            this.lblRemoveFromAvaiableProducts.AutoSize = true;
            this.lblRemoveFromAvaiableProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemoveFromAvaiableProducts.Location = new System.Drawing.Point(125, 291);
            this.lblRemoveFromAvaiableProducts.Name = "lblRemoveFromAvaiableProducts";
            this.lblRemoveFromAvaiableProducts.Size = new System.Drawing.Size(273, 20);
            this.lblRemoveFromAvaiableProducts.TabIndex = 3;
            this.lblRemoveFromAvaiableProducts.Text = "Quitar de disponibles actualmente: ";
            // 
            // lstRemoveFromAvailableProducts
            // 
            this.lstRemoveFromAvailableProducts.FormattingEnabled = true;
            this.lstRemoveFromAvailableProducts.ItemHeight = 16;
            this.lstRemoveFromAvailableProducts.Location = new System.Drawing.Point(129, 331);
            this.lstRemoveFromAvailableProducts.Name = "lstRemoveFromAvailableProducts";
            this.lstRemoveFromAvailableProducts.Size = new System.Drawing.Size(277, 164);
            this.lstRemoveFromAvailableProducts.TabIndex = 4;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(462, 331);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(287, 164);
            this.listBox1.TabIndex = 5;
            // 
            // lblAddToAvailableProducts
            // 
            this.lblAddToAvailableProducts.AutoSize = true;
            this.lblAddToAvailableProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddToAvailableProducts.Location = new System.Drawing.Point(443, 291);
            this.lblAddToAvailableProducts.Name = "lblAddToAvailableProducts";
            this.lblAddToAvailableProducts.Size = new System.Drawing.Size(277, 20);
            this.lblAddToAvailableProducts.TabIndex = 6;
            this.lblAddToAvailableProducts.Text = "Agregar a disponibles actualmente: ";
            // 
            // btnRemoveFromAvailableProducts
            // 
            this.btnRemoveFromAvailableProducts.Location = new System.Drawing.Point(319, 501);
            this.btnRemoveFromAvailableProducts.Name = "btnRemoveFromAvailableProducts";
            this.btnRemoveFromAvailableProducts.Size = new System.Drawing.Size(87, 38);
            this.btnRemoveFromAvailableProducts.TabIndex = 7;
            this.btnRemoveFromAvailableProducts.Text = "Quitar";
            this.btnRemoveFromAvailableProducts.UseVisualStyleBackColor = true;
            // 
            // btnAddToAvailableProducts
            // 
            this.btnAddToAvailableProducts.Location = new System.Drawing.Point(662, 501);
            this.btnAddToAvailableProducts.Name = "btnAddToAvailableProducts";
            this.btnAddToAvailableProducts.Size = new System.Drawing.Size(87, 38);
            this.btnAddToAvailableProducts.TabIndex = 8;
            this.btnAddToAvailableProducts.Text = "Agregar";
            this.btnAddToAvailableProducts.UseVisualStyleBackColor = true;
            // 
            // AvailableProductsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddToAvailableProducts);
            this.Controls.Add(this.btnRemoveFromAvailableProducts);
            this.Controls.Add(this.lblAddToAvailableProducts);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.lstRemoveFromAvailableProducts);
            this.Controls.Add(this.lblRemoveFromAvaiableProducts);
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
        private System.Windows.Forms.ListView lstActualAvailableProducts;
        private System.Windows.Forms.Label lblRemoveFromAvaiableProducts;
        private System.Windows.Forms.ListBox lstRemoveFromAvailableProducts;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblAddToAvailableProducts;
        private System.Windows.Forms.Button btnRemoveFromAvailableProducts;
        private System.Windows.Forms.Button btnAddToAvailableProducts;
    }
}
