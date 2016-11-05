namespace TodoPagos.ProductImporterLogic.JSONLogic
{
    partial class JSONProductImporterControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.openFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(107, 244);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.ReadOnly = true;
            this.pathTextBox.Size = new System.Drawing.Size(206, 26);
            this.pathTextBox.TabIndex = 5;
            this.pathTextBox.Text = "-Sin seleccionar-";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(153, 164);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(365, 20);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "Seleccione su archivo TXT que contenga un JSON";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(402, 236);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(144, 42);
            this.openFileButton.TabIndex = 3;
            this.openFileButton.Text = "Seleccionar ...";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // JSONProductImporterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.openFileButton);
            this.Name = "JSONProductImporterControl";
            this.Size = new System.Drawing.Size(652, 447);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button openFileButton;
    }
}
