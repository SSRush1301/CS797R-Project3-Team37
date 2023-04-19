
using System.Data;

namespace calcAppT37_REST;

public partial class AddedFuncCalc : ContentPage
{
    public AddedFuncCalc()
    {
        InitializeComponent();
        OnClear(this, null);
    }

    string currentEntry = "";
    int currentState = 1;
    string mathOperator;
    double firstNumber, secondNumber;
    string decimalFormat = "N0";



    void OnSelectNumber(object sender, EventArgs e)
    {

        Button button = (Button)sender;
        string pressed = button.Text;

        currentEntry += pressed;

        if ((this.resultText.Text == "0" && pressed == "0")
            || (currentEntry.Length <= 1 && pressed != "0")
            || currentState < 0)
        {
            this.resultText.Text = "";
            if (currentState < 0)
                currentState *= -1;
        }

        if (pressed == "." && decimalFormat != "N2")
        {
            decimalFormat = "N2";
        }
        this.resultText.Text += pressed;
        this.CurrentCalculation.Text += pressed;
    }

    void OnSelectOperator(object sender, EventArgs e)
    {
        LockNumberValue(resultText.Text);
        OnCalculate(this, null);

        currentState = -2;
        Button button = (Button)sender;
        string pressed = button.Text;
        //To Allow for Decimal Results in Division Operation
        if (pressed == "÷")
        {
            decimalFormat = "N2";
        }
        mathOperator = pressed;

        this.CurrentCalculation.Text += pressed;
    }

    private void LockNumberValue(string text)
    {
        double number;
        if (double.TryParse(text, out number))
        {
            if (currentState == 1)
            {
                firstNumber = number;
            }
            else
            {
                secondNumber = number;
            }

            currentEntry = string.Empty;
        }
    }

    void OnClear(object sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        currentState = 1;
        decimalFormat = "N0";
        this.resultText.Text = "0";
        currentEntry = string.Empty;
        CurrentCalculation.Text = "";
    }

    void OnCalculate(object sender, EventArgs e)
    {
        if (currentState == 2)
        {
            if (secondNumber == 0)
            {
                LockNumberValue(resultText.Text);
            }
            double result = CalcAppT37_REST.Calculate(firstNumber, secondNumber, mathOperator);

            this.resultText.Text = result.ToTrimmedString(decimalFormat);
            firstNumber = result;
            secondNumber = 0;
            currentState = 1;
            currentEntry = string.Empty;
        }
    }

    void OnCalculateResult(object sender, EventArgs e)
    {
        string fullExpression = CurrentCalculation.Text;
        if (fullExpression.Contains("("))
        {
            try
            {
                fullExpression = fullExpression.Replace("×", "*").Replace("÷", "/").Replace("Mod", "%");
                string result = new DataTable().Compute(fullExpression, null).ToString();
                resultText.Text = result;
                CurrentCalculation.Text = "";
            }
            catch
            {
                resultText.Text = "NAN";
                CurrentCalculation.Text = "";
            }

        }
        else
        {
            OnCalculate(sender, e);
            CurrentCalculation.Text = "";
        }
    }

    void OnNegative(object sender, EventArgs e)
    {
  
        double number;
        double.TryParse(resultText.Text, out number);
        resultText.Text = (number * (-1)).ToTrimmedString(decimalFormat);
    }

    void OnPercentage(object sender, EventArgs e)
    {
        if (currentState == 1)
        {
            LockNumberValue(resultText.Text);
            decimalFormat = "N2";
            secondNumber = 0.01;
            mathOperator = "x";
            currentState = 2;
            OnCalculate(this, null);
        }
    }

    void OnRoot(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string pressed = button.Text;
        double number;
        decimalFormat = "N2";
        double.TryParse(resultText.Text, out number);
        if (number >= 0)
        {
            try
            {
                CurrentCalculation.Text = CurrentCalculation.Text.Remove(CurrentCalculation.Text.Length - resultText.Text.Length) + (pressed + resultText.Text);
                resultText.Text = Math.Sqrt(number).ToTrimmedString(decimalFormat);
            }
            catch
            {
                resultText.Text = Math.Sqrt(number).ToTrimmedString(decimalFormat);
            }

        }
        else
        {
            resultText.Text = "Invalid";
        }
    }
}

