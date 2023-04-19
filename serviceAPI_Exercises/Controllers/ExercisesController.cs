using serviceAPI_Exercises.Models;
using serviceAPI_Exercises.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace serviceAPI_Exercises.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class ExercisesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
        Random random = new Random();
        List<string> exercises = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            int operand1 = random.Next(1, 11);
            int operand2 = random.Next(1, 11);
            int result = 0;
            int wrongAnswer1 = random.Next(-21, 21);
            int wrongAnswer2 = random.Next(-21, 21);
            string operatorSymbol = "";

            int operatorValue = random.Next(1, 4);

            switch (operatorValue)
            {
                case 1: // Addition
                    result = operand1 + operand2;
                    operatorSymbol = "+";
                    break;
                case 2: // Subtraction
                    result = operand1 - operand2;
                    operatorSymbol = "-";
                    break;
                case 3: // Multiplication
                    result = operand1 * operand2;
                    operatorSymbol = "x";
                    break;
            }
            while(wrongAnswer1 == result || wrongAnswer2 == result || wrongAnswer1 == wrongAnswer2){
                 wrongAnswer1 = random.Next(-21, 21);
                 wrongAnswer2 = random.Next(-21, 21);
            }
            
            string exercise = $"{operand1} {operatorSymbol} {operand2} = ? & {result} & {wrongAnswer1} & {wrongAnswer2}";
            exercises.Add(exercise);
        }
        
        return exercises;
        }
    
}
