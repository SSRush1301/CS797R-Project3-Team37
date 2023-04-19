using System.Collections.Specialized;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;

namespace calcAppT37_REST;

public partial class SimpleExercisesPage : ContentPage
{
    private const string API_URL = "https://localhost:7234/Exercises";
    //private readonly HttpClient _httpClient;
    List<string> exercises = new List<string>();
    List<string> correctAnswers = new List<string>();
    List<string> incorrectAnswers = new List<string>();
    int exerciseCounter = 1;
    string buttonAnswer;
    bool firstButtonClicked = true;
    bool isQuestionAnswered = true;
    bool skipping = false;

    Color button1OrigColor;
    Color button2OrigColor;
    Color button3OrigColor;
    int delayMs = 1000;
    

    Style existingButtonStyle = (Style)App.Current.Resources["ButtonStyle"];

    public SimpleExercisesPage()
    {
        InitializeComponent();

    }


    private async void StartButtonClicked(object sender, EventArgs e)
    {
       

        //Console.WriteLine("Clicked");
        if (!isQuestionAnswered)
        {
            AnswerCheckLabel.Text = "Take A Guess First";
            return;
        }
        if (exerciseCounter == 11)
        {
            ExerciseButton.Text = "New 10 Exercises? \n Please Click!";
            exerciseCounter = 1;
            return;
        }
        if (skipping)
        {
            TryAgainOrSkippedClicked(sender, e);
        }

        ExerciseButton.Text = "Skip to Next";
        QuestionTracker.Text = "Question " + exerciseCounter;
        exerciseCounter++;
        AnswerCheckLabel.Text = "Choose Answer";

        if (firstButtonClicked)
        {
            HttpClient _httpClient = new HttpClient();
            HttpResponseMessage response = await _httpClient.GetAsync(API_URL);

            if (response.IsSuccessStatusCode)
            {

                // Read the response content as a string
                string responseContent = await response.Content.ReadAsStringAsync();
                char[] delimiters = { '&', ',' }; //Defined Delimiters for Parsing String
                string resultString = responseContent.Replace("[", "").Replace("]", "").Replace("\"", "");
                string[] result = resultString.Split(delimiters);

                for (int i = 0; i < 40; i++)
                {
                    if (i % 4 == 0)
                    {
                        exercises.Add(result[i]);
                    }
                    else if ((i % 4) == 1)
                    {
                        correctAnswers.Add(result[i]);
                    }
                    else
                    {
                        incorrectAnswers.Add(result[i]);
                    }
                }

                //Changes Exercises Display
                ExpressionDisplay.Text = exercises[0];

                //Randomizes Correct Answer Button Placement
                Random random = new Random();
                int operatorValue = random.Next(1, 4);
                switch (operatorValue)
                {
                    case 1: 
                        Answer1.Text = correctAnswers[0];
                        Answer2.Text = incorrectAnswers[0];
                        Answer3.Text = incorrectAnswers[1];
                        buttonAnswer = Answer1.Text;
                        break;
                    case 2: 
                        Answer2.Text = correctAnswers[0];
                        Answer1.Text = incorrectAnswers[0];
                        Answer3.Text = incorrectAnswers[1];
                        buttonAnswer = Answer2.Text;
                        break;
                    case 3: 
                        Answer3.Text = correctAnswers[0];
                        Answer1.Text = incorrectAnswers[0];
                        Answer2.Text = incorrectAnswers[1];
                        buttonAnswer = Answer3.Text;
                        break;
                }

                ResetButtonColor();
                
                //Answer1.BackgroundColor = button1OrigColor; Answer2.BackgroundColor = button2OrigColor; Answer3.BackgroundColor = button3OrigColor;

                firstButtonClicked = false;


                //Clear Lists for Next GET Request and Resets AnsweredCheck
                isQuestionAnswered = false;
                exercises.Remove(exercises[0]);
                correctAnswers.Remove(correctAnswers[0]);
                incorrectAnswers.Remove(incorrectAnswers[0]);
                incorrectAnswers.Remove(incorrectAnswers[0]);

                _httpClient.Dispose();

            }
        }
        else
        {
            //Resets for Next Set of Exercises
            if(exerciseCounter == 10)
            {
                firstButtonClicked=true;
            }
            ExpressionDisplay.Text = exercises[0];
            //Randomizes Correct Answer Button Placement
            Random random = new Random();
            int operatorValue = random.Next(1, 4);
            switch (operatorValue)
            {
                case 1:
                    Answer1.Text = correctAnswers[0];
                    Answer2.Text = incorrectAnswers[0];
                    Answer3.Text = incorrectAnswers[1];
                    buttonAnswer = Answer1.Text;
                    break;
                case 2:
                    Answer2.Text = correctAnswers[0];
                    Answer1.Text = incorrectAnswers[0];
                    Answer3.Text = incorrectAnswers[1];
                    buttonAnswer = Answer2.Text;
                    break;
                case 3:
                    Answer3.Text = correctAnswers[0];
                    Answer1.Text = incorrectAnswers[0];
                    Answer2.Text = incorrectAnswers[1];
                    buttonAnswer = Answer3.Text;
                    break;
            }
            
            ResetButtonColor();
            //Answer1.BackgroundColor = button1OrigColor; Answer2.BackgroundColor = button2OrigColor; Answer3.BackgroundColor = button3OrigColor;
            skipping = false;

            //Remove Already Used Expression & Answer Choices
            isQuestionAnswered = false;
            exercises.Remove(exercises[0]);
            correctAnswers.Remove(correctAnswers[0]);
            incorrectAnswers.Remove(incorrectAnswers[0]);
            incorrectAnswers.Remove(incorrectAnswers[0]);

        }

    }//End of Function

    private async void AnswerButtonClicked(object sender, EventArgs e)
    {
        //QuestionTracker.Text = sender.ToString();
        if (isQuestionAnswered == false)
        {
            Button button = (Button)sender;
            if (button.Text == buttonAnswer)
            {
                skipping = false;
                AnswerCheckLabel.Text = "Correct!";
                isQuestionAnswered = true;
                button.BackgroundColor = Color.FromRgb(144, 238, 144);
                await LoadNextProblemWithDelay(sender, e, delayMs);
                
            }
            else
            {
                LockAllAnswerButtons();
                AnswerCheckLabel.Text = "Incorrect!";
                button.BackgroundColor = Color.FromRgb(255, 204, 203);
                isQuestionAnswered = true;
                //LockAllAnswerButtons();
                //Create new row to add button
                var newRowDefinition = new RowDefinition
                {
                    Height = new GridLength(1, GridUnitType.Star)
                };
                GridLayout.RowDefinitions.Add(newRowDefinition);
                // Create a new button and set its properties
                var newButton = new Button()
                {
                    Text = "Try Again",
                    BackgroundColor = Color.FromRgb(255, 204, 203),

                };

                // Add the new button to the page's Grid
                Microsoft.Maui.Controls.Grid.SetRow(newButton, 3);
                Microsoft.Maui.Controls.Grid.SetColumn(newButton, 0);
                Microsoft.Maui.Controls.Grid.SetColumnSpan(newButton, 3);
                newButton.Clicked += TryAgainOrSkippedClicked;
                newButton.Style = existingButtonStyle;

                GridLayout.Children.Add(newButton);

                skipping = true;
                
            }
        }

    }//End of Function

    private async Task LoadNextProblemWithDelay(object sender, EventArgs e, int delayMs)
    {
        // Wait for the specified delay
        await Task.Delay(delayMs);
        // Load the next problem
        StartButtonClicked(sender, e);
    }//End of Function

    
    private void TryAgainOrSkippedClicked(object sender, EventArgs e)
    {
        //QuestionTracker.Text = "Trying Again";
        //UnlockAllAnswerButtons();
        //IsEnabledChanged(Answer1, e);
        //IsEnabledChanged(Answer2, e);
        //IsEnabledChanged(Answer3, e);
        UnlockAllAnswerButtons();
        isQuestionAnswered = false;
        //if (sender is Button button)
        //{
        //    if (button.Parent is Microsoft.Maui.Controls.Grid GridLayout)
        //    {
        //        GridLayout.RemoveAt(7);
        //        //GridLayout.Children.Remove(button);
        //        GridLayout.RowDefinitions.RemoveAt(3);
        //    }
        //}

        GridLayout.RemoveAt(7);
        //GridLayout.Children.Remove(button);
        GridLayout.RowDefinitions.RemoveAt(3);


    }//End of Function
    private void LockAllAnswerButtons()
    {
        Answer1.IsEnabled = false;
        Answer2.IsEnabled = false;
        Answer3.IsEnabled = false;
    }//End of Function
    private void UnlockAllAnswerButtons()
    {
        Answer1.IsEnabled = true;
        Answer2.IsEnabled = true;
        Answer3.IsEnabled = true;
    }//End of Function

   
    private void ResetButtonColor()
    {
        Answer1.SetDynamicResource(Button.BackgroundColorProperty, "EqualToColor");
        Answer2.SetDynamicResource(Button.BackgroundColorProperty, "EqualToColor");
        Answer3.SetDynamicResource(Button.BackgroundColorProperty, "EqualToColor");
    }


    //private void IsEnabledChanged(object sender, EventArgs e)
    //{
    //    Button button = (Button)sender;
    //    if (button.IsEnabled)
    //    {
    //        AnswerCheckLabel.Text = "Enabled";
    //    }
    //}
}