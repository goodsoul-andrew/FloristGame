using System;
using System.Linq;
using System.Text;


public static class Extensions
{
	public static string FormatMatrix(this float[][] matrix)
	{
		return string.Join(Environment.NewLine, matrix.Select(row => string.Join("\t", row)));
	}

	public static void Print(this float[][] matrix)
	{
		Console.Write("[");
		for (int i = 0; i < matrix.Length; i++)
		{
			if (i > 0) Console.Write(" ");
			Console.Write("[ ");
			Console.Write(string.Join(", ", matrix[i]));
			Console.Write("]");
			if (i == matrix.Length - 1) Console.Write("]");
			Console.Write("\n");
		}
	}

	public static void Print(this float[] arr)
	{
		Console.WriteLine($"[{string.Join(", ", arr)}]");
	}
}

public class NoSolutionException : Exception
{
    public NoSolutionException(string message) : base(message)
    { }

    public NoSolutionException(float[][] initialMatrix, float[] freeMembers, float[][] matrixAfterSolve)
        : base(GetMessage(initialMatrix, freeMembers, matrixAfterSolve))
    { }

    private static string GetMessage(float[][] sourceMatrix, float[] freeMembers, float[][] solvedMatrix)
    {
        var builder = new StringBuilder();
        builder.Append("Initial matrix:" + Environment.NewLine + sourceMatrix.FormatMatrix() + Environment.NewLine);
        builder.Append("Free members: [" + string.Join(", ", freeMembers) + "]" + Environment.NewLine);
        builder.Append("Matrix after Solve:" + Environment.NewLine + solvedMatrix.FormatMatrix());
        return builder.ToString();
    }
}

public class Solver
{
    private static readonly float epsilon = 1e-6f;

    private static bool EqZero(float n) => Math.Abs(n) < epsilon;

    public static void PrintSystem(float[][] matrix, float[] freeMembers)
    {
        Console.Write("[");
        for (int i = 0; i < matrix.Length; i++)
        {
            if (i > 0) Console.Write(" ");
            Console.Write("[ ");
            Console.Write(string.Join(", ", matrix[i]) + $" ] {freeMembers[i]} ");
            if (i == matrix.Length - 1) Console.Write("]");
            Console.WriteLine();
        }
    }

    public static float[][] CreateFullMatrix(float[][] matrix, float[] freeMembers)
    {
        return matrix.Select((row, i) =>
        {
            var newRow = new float[row.Length + 1];
            Array.Copy(row, newRow, row.Length);
            newRow[^1] = -freeMembers[i];
            return newRow;
        }).ToArray();
    }

    public static float[][] ProcessSystem(float[][] _matrix, float[] _freeMembers)
    {
        var matrix = CreateFullMatrix(_matrix, _freeMembers);
        var used = new bool[matrix.Length];
        for (var col = 0; col < matrix[0].Length; col++)
        {
            var rows = Enumerable.Range(0, matrix.Length).Where(row => !used[row] && !EqZero(matrix[row][col])).ToArray();
            if (rows.Length == 0) continue;
            var r = rows[0];
            used[r] = true;
            for (var i = 0; i < matrix.Length; i++)
            {
                if (i != r && matrix[i][col] != 0)
                {
                    var k = matrix[i][col] / matrix[r][col];
                    for (var j = 0; j < matrix[i].Length; j++)
                    {
                        matrix[i][j] -= matrix[r][j] * k;
                        if (EqZero(matrix[i][j])) matrix[i][j] = 0;
                    }
                }
            }
        }
        return matrix;
    }

    public float[] Solve(float[][] matrix, float[] freeMembers)
    {
        var gaussMatrix = ProcessSystem(matrix, freeMembers);
        var x = new float[gaussMatrix[0].Length];
        x[^1] = 1;
        var found = new bool[matrix[0].Length - 1];
        for (var i = gaussMatrix.Length - 1; i > -1; i--)
        {
            if (gaussMatrix[i].Take(gaussMatrix[i].Length - 1).All(EqZero) && !EqZero(gaussMatrix[i][^1]))
            {
                throw new NoSolutionException("");
            }
            if (!gaussMatrix[i].All(EqZero))
            {
                var x_num = Array.FindIndex(gaussMatrix[i], j => !EqZero(j));
                for (var j = x_num + 1; j < gaussMatrix[i].Length; j++)
                {
                    x[x_num] -= x[j] * gaussMatrix[i][j];
                }
                x[x_num] /= gaussMatrix[i][x_num];
            }
        }
        return x.Take(x.Length - 1).ToArray();
    }
}