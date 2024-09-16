namespace ChatRealTime.Helper
{
    public class Util
    {
        public static string UploadImage(IFormFile Hinh, string folder)
        {
            try
            {
                var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hinh", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullpath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }
                return Hinh.FileName;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
        public static string GenerateRandomNumber()
        {
            Random random = new Random();

            // Tạo ra số đầu tiên không thể là 0
            string firstDigit = random.Next(1, 10).ToString();

            // Tạo ra các chữ số tiếp theo từ 0-9
            string remainingDigits = "";
            for (int i = 0; i < 9; i++)
            {
                remainingDigits += random.Next(0, 10).ToString();
            }

            // Kết hợp số đầu và các số còn lại
            return firstDigit + remainingDigits;
        }

    }
}
