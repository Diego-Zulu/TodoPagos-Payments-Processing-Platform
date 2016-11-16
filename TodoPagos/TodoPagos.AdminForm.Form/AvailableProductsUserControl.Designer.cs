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
            this.lblDeleteProduct = new System.Windows.Forms.Label();
            this.lstActualAvailableProducts = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblDataToModify = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblNeededPoints = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtNeededPoints = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProducts
            // 
            this.lblProducts.AutoSize = true;
            this.lblProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducts.Location = new System.Drawing.Point(272, 27);
            this.lblProducts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(110, 26);
            this.lblProducts.TabIndex = 0;
            this.lblProducts.Text = "Productos";
            // 
            // lblDeleteProduct
            // 
            this.lblDeleteProduct.AutoSize = true;
            this.lblDeleteProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeleteProduct.Location = new System.Drawing.Point(82, 83);
            this.lblDeleteProduct.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDeleteProduct.Name = "lblDeleteProduct";
            this.lblDeleteProduct.Size = new System.Drawing.Size(283, 16);
            this.lblDeleteProduct.TabIndex = 1;
            this.lblDeleteProduct.Text = "Seleccione el Producto a eliminar o modificar: ";
            // 
            // lstActualAvailableProducts
            // 
            this.lstActualAvailableProducts.FormattingEnabled = true;
            this.lstActualAvailableProducts.Location = new System.Drawing.Point(86, 102);
            this.lstActualAvailableProducts.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lstActualAvailableProducts.Name = "lstActualAvailableProducts";
            this.lstActualAvailableProducts.Size = new System.Drawing.Size(476, 134);
            this.lstActualAvailableProducts.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(462, 242);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Eliminar";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblDataToModify
            // 
            this.lblDataToModify.AutoSize = true;
            this.lblDataToModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataToModify.Location = new System.Drawing.Point(83, 296);
            this.lblDataToModify.Name = "lblDataToModify";
            this.lblDataToModify.Size = new System.Drawing.Size(126, 16);
            this.lblDataToModify.TabIndex = 4;
            this.lblDataToModify.Text = "Datos del Producto:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(83, 339);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(47, 13);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Nombre:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(83, 375);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(69, 13);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Descripción: ";
            // 
            // lblNeededPoints
            // 
            this.lblNeededPoints.AutoSize = true;
            this.lblNeededPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNeededPoints.Location = new System.Drawing.Point(390, 339);
            this.lblNeededPoints.Name = "lblNeededPoints";
            this.lblNeededPoints.Size = new System.Drawing.Size(97, 13);
            this.lblNeededPoints.TabIndex = 7;
            this.lblNeededPoints.Text = "Puntos necesarios:";
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStock.Location = new System.Drawing.Point(449, 375);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(38, 13);
            this.lblStock.TabIndex = 8;
            this.lblStock.Text = "Stock:";
            // 
            // txtNeededPoints
            // 
            this.txtNeededPoints.Location = new System.Drawing.Point(493, 336);
            this.txtNeededPoints.Name = "txtNeededPoints";
            this.txtNeededPoints.Size = new System.Drawing.Size(68, 20);
            this.txtNeededPoints.TabIndex = 9;
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(493, 372);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(68, 20);
            this.txtStock.TabIndex = 10;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(132, 336);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 11;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(149, 372);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(285, 20);
            this.txtDescription.TabIndex = 12;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(462, 418);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(100, 23);
            this.btnModify.TabIndex = 13;
            this.btnModify.Text = "Modificar";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // AvailableProductsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtNeededPoints);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblNeededPoints);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblDataToModify);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lstActualAvailableProducts);
            this.Controls.Add(this.lblDeleteProduct);
            this.Controls.Add(this.lblProducts);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "AvailableProductsUserControl";
            this.Size = new System.Drawing.Size(640, 491);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.Label lblDeleteProduct;
        private System.Windows.Forms.ListBox lstActualAvailableProducts;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblDataToModify;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblNeededPoints;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtNeededPoints;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnModify;
    }
}
