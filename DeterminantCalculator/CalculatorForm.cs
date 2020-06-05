using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DeterminantCalculator
{
    public partial class CalculatorForm : Form
    {
        private const int MAX_MATRIX_TYPE = 10;
        private const int DEFAULT_WIDTH = 318;
        private const int DEFAULT_HEIGHT = 126;
        private const int WINDOW_WIDTH_ONE_MATRIX_OFFSET = 52;
        private const int WINDOW_WIDTH_TWO_MATRICES_OFFSET = 88;
        private const int WINDOW_HEIGHT_OFFSET = 135;
        private const int TEXTBOX_WIDTH = 30;
        private const int TEXTBOX_WIDTH_WITH_PADDING = TEXTBOX_WIDTH + 10;
        private const int TEXTBOX_HEIGHT = 20;
        private const int TEXTBOX_HEIGHT_WITH_PADDING = TEXTBOX_HEIGHT + 20;
        private const int TEXTBOX_X_OFFSET = 23;
        private const int LABEL_ROW_WIDTH = 20;
        private const int LABEL_ROW_Y_OFFSET = 20;
        private const int LABEL_COLUMN_HEIGHT = 13;
        private const int LABEL_COLUMN_X_OFFSET = 15;
        
        private int selectedMatrixType = 0;

        public CalculatorForm()
        {
            InitializeComponent();

            object[] items = new object[MAX_MATRIX_TYPE - 1];

            for (int i = 0; i < (MAX_MATRIX_TYPE - 1); i++)
                items[i] = (i + 2).ToString() + " X " + (i + 2).ToString();

            this.matrixType.Items.AddRange(items);
        }

        private void deleteFields()
        {
            for (int i = this.Controls.Count - 1; i >= 0 ; i--)
            {
                if (this.Controls[i] is TextBox)
                {
                    if (this.Controls[i].Name == "result_textbox")
                        continue;

                    this.Controls[i].Dispose();
                }
                else if (this.Controls[i] is Label)
                    this.Controls[i].Dispose();
            }
        }

        private void promptForMatrixType()
        {
            resultBox.Text = "Select a matrix type!";
            this.Width = DEFAULT_WIDTH;
            this.Height = DEFAULT_HEIGHT;
        }

        private void resizeForDeterminantCalculation()
        {
            if (selectedMatrixType < 7)
                this.Width = DEFAULT_WIDTH;
            else
                this.Width = (selectedMatrixType * TEXTBOX_WIDTH_WITH_PADDING) + WINDOW_WIDTH_ONE_MATRIX_OFFSET;

            this.Height = (selectedMatrixType * TEXTBOX_HEIGHT_WITH_PADDING) + WINDOW_HEIGHT_OFFSET;
        }

        private void resizeForTransposeOrInverseCalculation()
        {
            if (selectedMatrixType < 3)
                this.Width = DEFAULT_WIDTH;
            else
                this.Width = (selectedMatrixType * TEXTBOX_WIDTH_WITH_PADDING * 2) + WINDOW_WIDTH_TWO_MATRICES_OFFSET;

            this.Height = (selectedMatrixType * TEXTBOX_HEIGHT_WITH_PADDING) + WINDOW_HEIGHT_OFFSET;
        }

        private void createLabelsAndFields(int x, int y, int matrixSize, bool isInput)
        {
            string name;
            int newX;
            int newY;
            int rowIndex;
            int columnIndex;

            // Create row labels
            Label[] labelRows = new Label[matrixSize];
            
            for (int i = 0; i < labelRows.Length; i++)
                labelRows[i] = new Label();

            rowIndex = 0;
            newY = y + LABEL_ROW_Y_OFFSET;

            foreach (Label label in labelRows)
            {
                name = "row_label_" + rowIndex.ToString();

                label.Name = name;
                label.Text = (rowIndex + 1).ToString();
                label.Width = LABEL_ROW_WIDTH;
                label.Height = TEXTBOX_HEIGHT;
                label.Location = new Point(x, newY);
                label.TextAlign = ContentAlignment.MiddleRight;
                label.Visible = true;
                this.Controls.Add(label);

                newY += TEXTBOX_HEIGHT_WITH_PADDING;
                rowIndex++;
            }

            // Create column labels
            Label[] labelColumns = new Label[matrixSize];

            for (int i = 0; i < labelColumns.Length; i++)
                labelColumns[i] = new Label();

            columnIndex = 0;
            newX = x + LABEL_COLUMN_X_OFFSET;

            foreach (Label label in labelColumns)
            {
                name = "column_label" + columnIndex.ToString();

                label.Name = name;
                label.Text = (columnIndex + 1).ToString();
                label.Width = TEXTBOX_WIDTH;
                label.Height = LABEL_COLUMN_HEIGHT;
                label.Location = new Point(newX, y);
                label.TextAlign = ContentAlignment.MiddleRight;
                label.Visible = true;
                this.Controls.Add(label);

                newX += TEXTBOX_WIDTH_WITH_PADDING;
                columnIndex++;
            }

            // Create textboxes
            TextBox[] matrixFields = new TextBox[matrixSize * matrixSize];

            for (int i = 0; i < matrixFields.Length; i++)
                matrixFields[i] = new TextBox();

            newX = x + TEXTBOX_X_OFFSET;
            newY = y + LABEL_ROW_Y_OFFSET;
            rowIndex = 0;
            columnIndex = 0;

            foreach (TextBox txt in matrixFields)
            {
                if (isInput)
                {
                    name = "input_field_" + rowIndex.ToString() + "_" + columnIndex.ToString();
                    txt.ReadOnly = false;
                }
                else
                {
                    name = "output_field_" + rowIndex.ToString() + "_" + columnIndex.ToString();
                    txt.ReadOnly = true; 
                }

                txt.Name = name;
                txt.Text = "0";
                txt.Width = TEXTBOX_WIDTH;
                txt.Height = TEXTBOX_HEIGHT;
                txt.Location = new Point(newX, newY);
                txt.TextAlign = HorizontalAlignment.Right;
                txt.Visible = true;

                this.Controls.Add(txt);

                columnIndex++;

                if (columnIndex >= matrixSize)
                {
                    newX = x + TEXTBOX_X_OFFSET;
                    newY += TEXTBOX_HEIGHT_WITH_PADDING;
                    columnIndex = 0;
                    rowIndex++;
                }
                else
                    newX += TEXTBOX_WIDTH_WITH_PADDING;
            }
        }

        private double[,] readMatrix(int size)
        {
            double[,] input = new double[size, size];
            int[] rowAndColumn;
            int row;
            int column;

            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    if (!control.Name.Contains("input_field_"))
                        continue;

                    rowAndColumn = new Regex(@"\d+").Matches(control.Name).Cast<Match>().Select(number => Int32.Parse(number.Value)).ToArray();
                    row = rowAndColumn[0];
                    column = rowAndColumn[1];

                    try
                    {
                        input[row, column] = double.Parse(control.Text);
                    }
                    catch (FormatException)
                    {
                        input[row, column] = 0.0;
                        resultBox.Text = "Error at row " + (row + 1).ToString() + " column " + (column + 1).ToString();
                        return null;
                    }
                }
            }

            return input;
        }

        private void printMatrix(double[,] result)
        {
            int[] rowAndColumn;
            int row;
            int column;

            foreach (Control control in this.Controls)
            {
                if (!control.Name.Contains("output_field_"))
                    continue;

                rowAndColumn = new Regex(@"\d+").Matches(control.Name).Cast<Match>().Select(number => Int32.Parse(number.Value)).ToArray();
                row = rowAndColumn[0];
                column = rowAndColumn[1];

                control.Text = result[row, column].ToString();
            }
        }

        private void clearOutputFields()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    if (control.Name.Contains("output_field_"))
                        control.Text = "0";
                }
            }
        }

        private double[,] calculateDivisonWithNumber(double[,] input, double number)
        {
            int size = input.GetLength(0);
            double[,] result = new double[size, size];

            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                    result[row, column] = input[row, column] / number;
            }

            return result;
        }

        private double[,] calculateTransposeMatrix(double[,] input)
        {
            double[,] result = new double[input.GetLength(0), input.GetLength(1)];

            for (int row = 0; row < input.GetLength(0); row++)
            {
                for (int column = 0; column < input.GetLength(1); column++)
                    result[column, row] = input[row, column];
            }

            return result;
        }

        private double calculateDeterminant(double[,] input)
        {
            int size = input.GetLength(0);
            double result = 0;

            if (size == 2)
            {
                result = (input[0, 0] * input[1, 1]) - (input[0, 1] * input[1, 0]);
                return result;
            }
            else
            {
                double[,] tempMatrix = new double[(size - 1), (size - 1)];
                int skipColumn;

                for (int group = 0; group < size; group++)
                {
                    for (int row = 0; row < size - 1; row++)
                    {
                        skipColumn = 0;

                        for (int column = 0; column < size; column++)
                        {
                            if (column == group)
                            {
                                skipColumn = 1;
                                continue;
                            }

                            tempMatrix[row, (column - skipColumn)] = input[(row + 1), column];
                        }
                    }

                    result += Math.Pow(-1, group) * input[0, group] * calculateDeterminant(tempMatrix);
                }
            }

            return result;
        }

        private double[,] calculateMatrixOfCofactors(double[,] input)
        {
            int size = input.GetLength(0);
            double[,] result = new double[size, size];

            if (size == 2)
            {
                result[0, 0] = input[1, 1];
                result[0, 1] = -input[1, 0];
                result[1, 0] = -input[0, 1];
                result[1, 1] = input[0, 0];
            }
            else
            {
                double[,] tempMatrix = new double[size - 1, size - 1];
                int skipRow;
                int skipColumn;

                for (int row = 0; row < size; row++)
                {
                    for (int column = 0; column < size; column++)
                    {
                        skipRow = 0;

                        for (int tempRow = 0; tempRow < size; tempRow++)
                        {
                            skipColumn = 0;

                            if (tempRow == row)
                            {
                                skipRow = 1;
                                continue;
                            }

                            for (int tempColumn = 0; tempColumn < size; tempColumn++)
                            {
                                if (tempColumn == column)
                                {
                                    skipColumn = 1;
                                    continue;
                                }

                                tempMatrix[(tempRow - skipRow), (tempColumn - skipColumn)] = input[tempRow, tempColumn];
                            }
                        }

                        result[row, column] = Math.Pow(-1, (row + column)) * calculateDeterminant(tempMatrix);
                    }
                }
            }

            return result;
        }

        private double[,] calculateInverseMatrix(double[,] input)
        {
            double determinantResult = calculateDeterminant(input);

            if (determinantResult == 0)
            {
                resultBox.Text = "Determinant = 0! There is no inverse matrix!";
                return null;
            }

            int size = input.GetLength(0);
            double[,] result = new double[size, size];

            resultBox.Text = "Determinant = " + determinantResult.ToString();
            result = calculateMatrixOfCofactors(input);
            result = calculateTransposeMatrix(result);
            result = calculateDivisonWithNumber(result, determinantResult);

            return result;
        }

        private void calculateTransposeMatrixCheckedChanged(object sender, EventArgs e)
        {
            if (!calculateTransposeMatrixButton.Checked)
                return;

            //Check if valid matrix type is selected
            if (selectedMatrixType < 2)
            {
                promptForMatrixType();
                return;
            }

            //deleteFields();

            resultBox.Text = "";
            clearOutputFields();
            /*resizeForTransposeOrInverseCalculation();
            createLabelsAndFields(10, 85, selectedMatrixType, true);
            createLabelsAndFields(selectedMatrixType * 40 + 47, 85, selectedMatrixType, false);*/
        }

        private void calculateDdeterminantCheckedChanged(object sender, EventArgs e)
        {
            if (!calculateDeterminantButton.Checked)
                return;

            //Check if valid matrix type is selected
            if (selectedMatrixType < 2)
            {
                promptForMatrixType();
                return;
            }

            //deleteFields();

            resultBox.Text = "";
            clearOutputFields();
            /*resizeForDeterminantCalculation();
            createLabelsAndFields(10, 85, selectedMatrixType, true);*/
        }

        private void calculateInverseMatrixCheckedChanged(object sender, EventArgs e)
        {
            if (!calculateInverseMatrixButton.Checked)
                return;

            //Check if valid matrix type is selected
            if (selectedMatrixType < 2)
            {
                promptForMatrixType();
                return;
            }

            //deleteFields();

            resultBox.Text = "";
            clearOutputFields();
            /*resizeForTransposeOrInverseCalculation();
            createLabelsAndFields(10, 85, selectedMatrixType, true);
            createLabelsAndFields(selectedMatrixType * 40 + 47, 85, selectedMatrixType, false);*/
        }

        private void matrixTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            // Clean everything
            deleteFields();

            selectedMatrixType = matrixType.SelectedIndex + 1;

            if (selectedMatrixType < 2)
            {
                promptForMatrixType();
                return;
            }

            // Check if operation is selected
            if (!calculateTransposeMatrixButton.Checked && !calculateDeterminantButton.Checked && !calculateInverseMatrixButton.Checked)
            {
                resultBox.Text = "Select what are you going to calculate!";
                //return;
            }
            else
                resultBox.Text = "";

            /*if (calculateDeterminantButton.Checked == true)
            {
                resultBox.Text = "";
                resizeForDeterminantCalculation();
                createLabelsAndFields(10, 85, selectedMatrixType, true);
            }
            else
            {
                resultBox.Text = "";
                resizeForTransposeOrInverseCalculation();
                createLabelsAndFields(10, 85, selectedMatrixType, true);
                createLabelsAndFields(selectedMatrixType * 40 + 47, 85, selectedMatrixType, false);
            }*/

            //resultBox.Text = "";
            resizeForTransposeOrInverseCalculation();
            createLabelsAndFields(10, 85, selectedMatrixType, true);
            createLabelsAndFields(selectedMatrixType * 40 + 47, 85, selectedMatrixType, false);
        }

        private void clearButtonClick(object sender, EventArgs e)
        {
            //Check if valid matrix type is selected
            if (selectedMatrixType < 2)
            {
                resultBox.Text = "Select matrix type!";
                return;
            }

            //Check if valid operation is selected
            if (!calculateTransposeMatrixButton.Checked && !calculateDeterminantButton.Checked && !calculateInverseMatrixButton.Checked)
            {
                resultBox.Text = "Select what are you going to calculate!";
                return;
            }

            // Clear all textbox fields
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    if (control.Name == "result_textbox")
                        control.Text = "";
                    else
                        control.Text = "0";
                }
            }
        }

        private void calculateButtonClick(object sender, EventArgs e)
        {
            if (selectedMatrixType < 2)
            {
                resultBox.Text = "Select matrix type!";
                return;
            }

            if (!calculateTransposeMatrixButton.Checked && !calculateDeterminantButton.Checked && !calculateInverseMatrixButton.Checked)
            {
                resultBox.Text = "Select what are you going to calculate!";
                return;
            }

            double[,] input = readMatrix(selectedMatrixType);
            double[,] result;

            if (input == null)
                return;

            if (calculateTransposeMatrixButton.Checked == true)
            {
                result = calculateTransposeMatrix(input);
                printMatrix(result);
            }
            else if (calculateDeterminantButton.Checked == true)
            {
                double determinantResult = calculateDeterminant(input);
                resultBox.Text = "Determinant = " + determinantResult;
            }
            else if (calculateInverseMatrixButton.Checked == true)
            {
                result = calculateInverseMatrix(input);

                if (result == null)
                    return;

                printMatrix(result);
            }
            else
                return;
        }
    }
}