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
            this.lstDifferentDLLs = new System.Windows.Forms.ListBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblSelectWay = new System.Windows.Forms.Label();
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
            this.lblLoadFromDLL.Location = new System.Drawing.Point(230, 127);
            this.lblLoadFromDLL.Name = "lblLoadFromDLL";
            this.lblLoadFromDLL.Size = new System.Drawing.Size(220, 20);
            this.lblLoadFromDLL.TabIndex = 1;
            this.lblLoadFromDLL.Text = "Cargar productos desde .dll:";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(487, 127);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(144, 23);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Importar .dll";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // lstDifferentDLLs
            // 
            this.lstDifferentDLLs.FormattingEnabled = true;
            this.lstDifferentDLLs.ItemHeight = 16;
            this.lstDifferentDLLs.Location = new System.Drawing.Point(167, 226);
            this.lstDifferentDLLs.Name = "lstDifferentDLLs";
            this.lstDifferentDLLs.Size = new System.Drawing.Size(556, 228);
            this.lstDifferentDLLs.TabIndex = 3;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(613, 460);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(110, 30);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "Cargar";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // lblSelectWay
            // 
            this.lblSelectWay.AutoSize = true;
            this.lblSelectWay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectWay.Location = new System.Drawing.Point(164, 190);
            this.lblSelectWay.Name = "lblSelectWay";
            this.lblSelectWay.Size = new System.Drawing.Size(374, 20);
            this.lblSelectWay.TabIndex = 5;
            this.lblSelectWay.Text = "Seleccionar forma de importación de Productos: ";
            // 
            // LoadNewProductsUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSelectWay);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.lstDifferentDLLs);
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
        private System.Windows.Forms.ListBox lstDifferentDLLs;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblSelectWay;
    }
}
