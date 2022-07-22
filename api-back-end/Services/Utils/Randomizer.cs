namespace api_back_end.Services.Utils
{
    public static class Randomizer
    {
        private static readonly Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static string GenerateCode()
        {
            int charCount = random.Next(1, 4);
            int num = random.Next(1, 1000);
            string str = new string(Enumerable.Repeat(chars, charCount).Select(s => s[random.Next(chars.Length)]).ToArray());
            return $"{str}{num}";
        }

        public static int GeneratePrepTime()
        {
            return random.Next(5, 15);
        }
    }
}
