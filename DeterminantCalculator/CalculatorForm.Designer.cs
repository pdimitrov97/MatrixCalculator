using System.Windows.Forms;
using System.Drawing;

namespace DeterminantCalculator
{
    partial class CalculatorForm
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
                components.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calculateTransposeMatrixButton = new RadioButton();
            this.calculateDeterminantButton = new RadioButton();
            this.calculateInverseMatrixButton = new RadioButton();
            this.matrixType = new ComboBox();
            this.clearButton = new Button();
            this.calculateButton = new Button();
            this.resultBox = new TextBox();
            
            this.SuspendLayout();
            // 
            // calculateTransposeMatrix checkbox
            // 
            this.calculateTransposeMatrixButton.AutoSize = true;
            this.calculateTransposeMatrixButton.Location = new Point(10, 8);
            this.calculateTransposeMatrixButton.Name = "calculate_transpose_matrix";
            this.calculateTransposeMatrixButton.Size = new Size(75, 17);
            this.calculateTransposeMatrixButton.TabIndex = 18;
            this.calculateTransposeMatrixButton.TabStop = true;
            this.calculateTransposeMatrixButton.Text = "Transpose";
            this.calculateTransposeMatrixButton.UseVisualStyleBackColor = true;
            this.calculateTransposeMatrixButton.CheckedChanged += new System.EventHandler(this.calculateTransposeMatrixCheckedChanged);
            // 
            // calculateDeterminant checkbox
            // 
            this.calculateDeterminantButton.AutoSize = true;
            this.calculateDeterminantButton.Location = new Point(100, 8);
            this.calculateDeterminantButton.Name = "calculate_determinant";
            this.calculateDeterminantButton.Size = new Size(82, 17);
            this.calculateDeterminantButton.TabIndex = 16;
            this.calculateDeterminantButton.TabStop = true;
            this.calculateDeterminantButton.Text = "Determinant";
            this.calculateDeterminantButton.UseVisualStyleBackColor = true;
            this.calculateDeterminantButton.CheckedChanged += new System.EventHandler(this.calculateDdeterminantCheckedChanged);
            // 
            // calculateInverseMatrix checkbox
            // 
            this.calculateInverseMatrixButton.AutoSize = true;
            this.calculateInverseMatrixButton.Location = new Point(199, 8);
            this.calculateInverseMatrixButton.Name = "calculate_inverse_matrix";
            this.calculateInverseMatrixButton.Size = new Size(91, 17);
            this.calculateInverseMatrixButton.TabIndex = 17;
            this.calculateInverseMatrixButton.TabStop = true;
            this.calculateInverseMatrixButton.Text = "Inverse Matrix";
            this.calculateInverseMatrixButton.UseVisualStyleBackColor = true;
            this.calculateInverseMatrixButton.CheckedChanged += new System.EventHandler(this.calculateInverseMatrixCheckedChanged);
            // 
            // matrixType dropdown
            // 
            this.matrixType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.matrixType.FormattingEnabled = true;
            this.matrixType.Items.AddRange(new object[] {"Select Matrix..."});
            this.matrixType.Location = new Point(10, 31);
            this.matrixType.Name = "matrix_type";
            this.matrixType.Size = new Size(102, 21);
            this.matrixType.TabIndex = 1;
            this.matrixType.SelectedIndexChanged += new System.EventHandler(this.matrixTypeSelectedIndexChanged);
            this.matrixType.SelectedIndex = 0;

            // 
            // clearButton button
            // 
            this.clearButton.Location = new Point(118, 30);
            this.clearButton.Name = "clear_button";
            this.clearButton.Size = new Size(71, 23);
            this.clearButton.TabIndex = 13;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButtonClick);
            // 
            // calculateButton button
            // 
            this.calculateButton.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            this.calculateButton.Location = new Point(195, 30);
            this.calculateButton.Name = "calculate_button";
            this.calculateButton.Size = new Size(95, 23);
            this.calculateButton.TabIndex = 12;
            this.calculateButton.Text = "Calculate";
            this.calculateButton.UseVisualStyleBackColor = true;
            this.calculateButton.Click += new System.EventHandler(this.calculateButtonClick);
            // 
            // result textbox
            // 
            this.resultBox.Location = new Point(10, 58);
            this.resultBox.Name = "result_textbox";
            this.resultBox.ReadOnly = true;
            this.resultBox.Size = new Size(280, 20);
            this.resultBox.TabIndex = 14;
            this.resultBox.TextAlign = HorizontalAlignment.Right;
            // 
            // Determinant Calculator Form
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(302, 87);
            this.Controls.Add(this.calculateTransposeMatrixButton);
            this.Controls.Add(this.calculateInverseMatrixButton);
            this.Controls.Add(this.calculateDeterminantButton);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.calculateButton);
            this.Controls.Add(this.matrixType);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Determinant Calculator Form";
            this.Text = "Determinant Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private RadioButton calculateTransposeMatrixButton;
        private RadioButton calculateDeterminantButton;
        private RadioButton calculateInverseMatrixButton;
        private ComboBox matrixType;
        private Button clearButton;
        private Button calculateButton;
        private TextBox resultBox;
    }
}