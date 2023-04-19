using serviceAPI_Exercises.Models;

namespace serviceAPI_Exercises.Services;

public static class ExercisesService
{
    static List<Exercises> MathQuestion { get; }
    static int nextId = 3;
    static ExercisesService()
    {
        MathQuestion = new List<Exercises>
        {
            new Exercises { mathQuestion = 1},
            new Exercises { mathQuestion = 2}
        };
    }

    public static List<Exercises> GetAll() => MathQuestion;

    public static Exercises? Get(int id) => MathQuestion.FirstOrDefault(p => p.mathQuestion == id);

    
}