using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;
using NAudio.Wave;

namespace Tpotifiy
{
    // sasifre degistirme de hata aliniuyor degistirirken duzelt
    public static class Utilities
    {
        public static event EventHandler<string> SongTimeUpdated;

        private static string ConnectionString { get; } = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = Topotify; Integrated Security = True";
        private static readonly string smtpServer = "smtp.gmail.com";
        private static readonly int smtpPort = 587;
        private static readonly string smtpUsername = "denemedenemedeneme134@gmail.com";
        private static readonly string smtpPassword = "oqnblprcnjwkkcal";

        private static IWavePlayer waveOutDevice;
        private static AudioFileReader audioFileReader;
        private static Timer timer;
        private static long currentPosition;
        public static float Volume { get; set; } = 0.5f;

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool Login(string _username, string _password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                        SELECT UserID
                        FROM users
                        WHERE CAST(username AS NVARCHAR(MAX)) = @Username
                        AND CAST(password AS NVARCHAR(MAX)) = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", _username);
                command.Parameters.AddWithValue("@Password", _password);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result == null)
                {
                    connection.Close();
                    return false;
                }
                Session.UserID = result.ToString();

                connection.Close();
                return true;
            }
        }

        public static void SignUp(string _name, string _surname, string _username, string _mail, string _password,
            string _phonenumber, string _date, bool _gender)
        {
            string password = ComputeSha256Hash(_password);

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();

                Guid newID = Guid.NewGuid();

                string insertQuery = "INSERT INTO users (userID, Name, Surname, Username, Mail, Password, Phonenumber, BirthDate, Gender) VALUES (@ID, @Name, @Surname, @Username, @Mail, @Password, @Phone_number, @Date, @Gender)";
                SqlCommand command = new SqlCommand(insertQuery, connection);
                command.Parameters.AddWithValue("@ID", newID);
                command.Parameters.AddWithValue("@Name", _name);
                command.Parameters.AddWithValue("@Surname", _surname);
                command.Parameters.AddWithValue("@Username", _username);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@Mail", _mail);
                command.Parameters.AddWithValue("@Phone_number", _phonenumber);
                command.Parameters.AddWithValue("@Date", _date);
                command.Parameters.AddWithValue("@Gender", _gender);

                int rowsAffected = command.ExecuteNonQuery();
            }
        }

        public static bool SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(smtpUsername);
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;

                    using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                        smtpClient.EnableSsl = true;

                        smtpClient.Send(mail);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 6);
        }

        public static bool checkMail(string _mail)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                        SELECT UserID
                        FROM users
                        WHERE CAST(Mail AS NVARCHAR(MAX)) = @Mail";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Mail", _mail);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result == null)
                {
                    connection.Close();
                    return false;
                }

                Session.UserID = result.ToString();

                connection.Close();
                return true;
            }
        }

        public static bool checkPassword(string _password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = @"
                        SELECT UserID
                        FROM users
                        WHERE CAST(Password AS NVARCHAR(MAX)) = @Password";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Password", _password);

                connection.Open();

                object result = command.ExecuteScalar();
                string id = result.ToString();

                if (id == Session.UserID)
                {
                    connection.Close();
                    return false;
                }
                connection.Close();
                return true;
            }
        }

        public static void changePassword(string _newPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                string updateQuery = @"
                UPDATE users
                SET Password = @NewPassword
                WHERE UserID = @UserID";

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@NewPassword", _newPassword);
                updateCommand.Parameters.AddWithValue("@UserID", Session.UserID);

                connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();

                connection.Close();

                if (rowsAffected > 0)
                {
                    //PASSWORD CHANGED
                }
                else
                {
                    Console.WriteLine("Password change failed.");
                }
            }
        }

        public static void PlaySong(string filePath, ProgressBar progressBar, Label label)
        {
            waveOutDevice = new WaveOut();
            audioFileReader = new AudioFileReader(filePath)
            {
                Volume = Volume
            };
            audioFileReader.Position = currentPosition; // Resume from last position
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();

            // Initialize ProgressBar
            progressBar.Maximum = (int)audioFileReader.TotalTime.TotalSeconds;

            // Timer to update ProgressBar
            timer = new Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += (sender, e) => Timer_Tick(sender, e, progressBar, label);
            timer.Start();
        }

        public static void StopSong()
        {
            if (waveOutDevice != null)
            {
                currentPosition = audioFileReader.Position; // Şu anki pozisyonu kaydet
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
                audioFileReader.Dispose();
                audioFileReader = null;

                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                    timer = null;
                }
            }
        }

        private static void Timer_Tick(object sender, EventArgs e, ProgressBar progressBar,Label label1)
        {
            if (audioFileReader != null && waveOutDevice != null && waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                TimeSpan currentTime = audioFileReader.CurrentTime;
                string currentTimeString = currentTime.ToString(@"mm\:ss");

                // Update ProgressBar
                progressBar.Value = (int)currentTime.TotalSeconds;
                label1.Text = currentTimeString;

                // Notify subscribers about the current song time
                SongTimeUpdated?.Invoke(null, currentTimeString);
            }
        }
        public static void SetVolume(float volume)
        {
            if (audioFileReader != null)
            {
                audioFileReader.Volume = volume;
            }
            Volume = volume;
        }

        public static void SeekTo(int positionInSeconds)
        {
            if (audioFileReader != null && waveOutDevice != null)
            {
                // Set the new position in the audio file
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(positionInSeconds);

                // Update the current position
                currentPosition = audioFileReader.Position;
            }
        }
    }
}

