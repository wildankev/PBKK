using System;
using System.Globalization;
using System.Windows.Forms;

namespace CalculatorApp;

public partial class Form1 : Form
{
    private double firstNumber = 0;
    private double secondNumber = 0;
    private double result = 0;
    private string operation = "";
    private bool startNewNumber = false;

    public Form1()
    {
        InitializeComponent();
    }

    private void NumberButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        if (txtDisplay.Text == "0" || startNewNumber)
        {
            txtDisplay.Text = button.Text;
            startNewNumber = false;
        }
        else
        {
            txtDisplay.Text += button.Text;
        }
    }

    private void OperatorButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button) return;

        // Jika operator ditekan berulang tanpa mengganti angka, ganti operatornya
        if (string.IsNullOrEmpty(txtDisplay.Text))
        {
            if (!string.IsNullOrEmpty(operation))
            {
                operation = button.Text;
                lblHistory.Text = $"{firstNumber} {operation}";
            }
            return;
        }

        if (!double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            return;

        firstNumber = value;
        operation = button.Text;
        lblHistory.Text = $"{firstNumber} {operation}";
        startNewNumber = false;

        txtDisplay.Clear();
    }

    private void btnEquals_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(operation)) return;

        try
        {
            if (!double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out secondNumber))
            {
                throw new FormatException("Masukkan angka kedua terlebih dahulu.");
            }

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;
                case "−":
                    result = firstNumber - secondNumber;
                    break;
                case "×":
                    result = firstNumber * secondNumber;
                    break;
                case "÷":
                    if (secondNumber == 0)
                    {
                        throw new DivideByZeroException("Tidak bisa membagi dengan angka nol.");
                    }
                    result = firstNumber / secondNumber;
                    break;
            }

            lblHistory.Text = $"{firstNumber} {operation} {secondNumber} =";
            txtDisplay.Text = result.ToString(CultureInfo.InvariantCulture);
            operation = "";
            startNewNumber = true;
        }
        catch (DivideByZeroException ex)
        {
            MessageBox.Show(ex.Message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ResetCalculator();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        ResetCalculator();
    }

    private void btnDecimal_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDisplay.Text) || startNewNumber)
        {
            txtDisplay.Text = "0.";
            startNewNumber = false;
        }
        else if (!txtDisplay.Text.Contains('.'))
        {
            txtDisplay.Text += ".";
        }
    }

    private void ResetCalculator()
    {
        firstNumber = 0;
        secondNumber = 0;
        result = 0;
        operation = "";
        startNewNumber = false;
        lblHistory.Text = "";
        txtDisplay.Text = "0";
    }
}
