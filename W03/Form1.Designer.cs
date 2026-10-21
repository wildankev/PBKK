namespace CalculatorApp;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.TextBox txtDisplay;
    private System.Windows.Forms.Label lblHistory;
    private System.Windows.Forms.Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9;
    private System.Windows.Forms.Button btnAdd, btnSub, btnMul, btnDiv;
    private System.Windows.Forms.Button btnEquals, btnClear, btnDecimal;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.txtDisplay = new System.Windows.Forms.TextBox();
        this.lblHistory = new System.Windows.Forms.Label();
        this.btn7 = new System.Windows.Forms.Button();
        this.btn8 = new System.Windows.Forms.Button();
        this.btn9 = new System.Windows.Forms.Button();
        this.btnDiv = new System.Windows.Forms.Button();
        this.btn4 = new System.Windows.Forms.Button();
        this.btn5 = new System.Windows.Forms.Button();
        this.btn6 = new System.Windows.Forms.Button();
        this.btnMul = new System.Windows.Forms.Button();
        this.btn1 = new System.Windows.Forms.Button();
        this.btn2 = new System.Windows.Forms.Button();
        this.btn3 = new System.Windows.Forms.Button();
        this.btnSub = new System.Windows.Forms.Button();
        this.btn0 = new System.Windows.Forms.Button();
        this.btnDecimal = new System.Windows.Forms.Button();
        this.btnClear = new System.Windows.Forms.Button();
        this.btnAdd = new System.Windows.Forms.Button();
        this.btnEquals = new System.Windows.Forms.Button();
        this.SuspendLayout();

        // lblHistory
        this.lblHistory.Location = new System.Drawing.Point(16, 12);
        this.lblHistory.Size = new System.Drawing.Size(292, 18);
        this.lblHistory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblHistory.ForeColor = System.Drawing.Color.Gray;
        this.lblHistory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
        this.lblHistory.Text = "";

        // txtDisplay
        this.txtDisplay.Location = new System.Drawing.Point(16, 32);
        this.txtDisplay.Size = new System.Drawing.Size(292, 45);
        this.txtDisplay.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.txtDisplay.ReadOnly = true;
        this.txtDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        this.txtDisplay.Text = "0";
        this.txtDisplay.BackColor = System.Drawing.Color.White;

        // Button Layout Helpers
        int startX = 16;
        int startY = 88;
        int btnW = 68;
        int btnH = 50;
        int gap = 7;
        var btnFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
        var opFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);

        // Row 1: 7, 8, 9, ÷
        SetupButton(this.btn7, "7", startX, startY, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btn8, "8", startX + (btnW + gap), startY, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btn9, "9", startX + 2 * (btnW + gap), startY, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btnDiv, "÷", startX + 3 * (btnW + gap), startY, btnW, btnH, opFont, OperatorButton_Click);

        // Row 2: 4, 5, 6, ×
        int row2Y = startY + (btnH + gap);
        SetupButton(this.btn4, "4", startX, row2Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btn5, "5", startX + (btnW + gap), row2Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btn6, "6", startX + 2 * (btnW + gap), row2Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btnMul, "×", startX + 3 * (btnW + gap), row2Y, btnW, btnH, opFont, OperatorButton_Click);

        // Row 3: 1, 2, 3, −
        int row3Y = startY + 2 * (btnH + gap);
        SetupButton(this.btn1, "1", startX, row3Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btn2, "2", startX + (btnW + gap), row3Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btn3, "3", startX + 2 * (btnW + gap), row3Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btnSub, "−", startX + 3 * (btnW + gap), row3Y, btnW, btnH, opFont, OperatorButton_Click);

        // Row 4: 0, ., C, +
        int row4Y = startY + 3 * (btnH + gap);
        SetupButton(this.btn0, "0", startX, row4Y, btnW, btnH, btnFont, NumberButton_Click);
        SetupButton(this.btnDecimal, ".", startX + (btnW + gap), row4Y, btnW, btnH, btnFont, btnDecimal_Click);
        SetupButton(this.btnClear, "C", startX + 2 * (btnW + gap), row4Y, btnW, btnH, opFont, btnClear_Click);
        SetupButton(this.btnAdd, "+", startX + 3 * (btnW + gap), row4Y, btnW, btnH, opFont, OperatorButton_Click);

        // Row 5: =
        int row5Y = startY + 4 * (btnH + gap);
        int eqW = 4 * btnW + 3 * gap;
        SetupButton(this.btnEquals, "=", startX, row5Y, eqW, btnH, opFont, btnEquals_Click);
        this.btnEquals.BackColor = System.Drawing.Color.CornflowerBlue;
        this.btnEquals.ForeColor = System.Drawing.Color.White;

        // Form1 Properties
        this.ClientSize = new System.Drawing.Size(325, 385);
        this.Controls.Add(this.lblHistory);
        this.Controls.Add(this.txtDisplay);
        this.Controls.Add(this.btn7);
        this.Controls.Add(this.btn8);
        this.Controls.Add(this.btn9);
        this.Controls.Add(this.btnDiv);
        this.Controls.Add(this.btn4);
        this.Controls.Add(this.btn5);
        this.Controls.Add(this.btn6);
        this.Controls.Add(this.btnMul);
        this.Controls.Add(this.btn1);
        this.Controls.Add(this.btn2);
        this.Controls.Add(this.btn3);
        this.Controls.Add(this.btnSub);
        this.Controls.Add(this.btn0);
        this.Controls.Add(this.btnDecimal);
        this.Controls.Add(this.btnClear);
        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.btnEquals);

        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Kalkulator - PBKK C";
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private void SetupButton(System.Windows.Forms.Button btn, string text, int x, int y, int w, int h, System.Drawing.Font font, System.EventHandler handler)
    {
        btn.Text = text;
        btn.Location = new System.Drawing.Point(x, y);
        btn.Size = new System.Drawing.Size(w, h);
        btn.Font = font;
        btn.UseVisualStyleBackColor = true;
        btn.Click += handler;
    }
}
