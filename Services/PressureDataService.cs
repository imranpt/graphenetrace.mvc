using Project.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Project.Services
{
    public class PressureDataService
    {
        // THRESHOLD for "high pressure" pixels
        private const int PRESSURE_THRESHOLD = 50;

        public PressureFrame ProcessCsv(string filePath, int patientId)
        {
            // Read all lines of CSV file
            var lines = File.ReadAllLines(filePath);

            // MATRIX: 32×32
            int size = 32;
            int[,] matrix = new int[size, size];

            for (int row = 0; row < size; row++)
            {
                var values = lines[row].Split(',', StringSplitOptions.RemoveEmptyEntries);

                for (int col = 0; col < size; col++)
                {
                    matrix[row, col] = int.Parse(values[col]);
                }
            }

            // --- METRIC 1: Peak Pressure ---
            int peak = 0;
            foreach (var value in matrix)
                if (value > peak) peak = value;

            // --- METRIC 2: Contact Area % ---
            int totalPixels = size * size;
            int highPressurePixels = matrix.Cast<int>().Count(v => v >= PRESSURE_THRESHOLD);
            double contactArea = (highPressurePixels / (double)totalPixels) * 100;

            // Convert 32x32 int[,] into List<List<int>>
            var listMatrix = new List<List<int>>();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = new List<int>();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row.Add(matrix[i, j]);
                }
                listMatrix.Add(row);
            }

            string matrixJson = JsonSerializer.Serialize(listMatrix);


            // --- Create PressureFrame object ---
            var frame = new PressureFrame
            {
                PatientId = patientId,
                Timestamp = DateTime.Now,
                PeakPressure = peak,
                ContactAreaPercent = Math.Round(contactArea, 2),
                MatrixJson = matrixJson
            };

            return frame;
        }
    }
}


