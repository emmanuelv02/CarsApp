using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace CarsFunctionApp.Repositories
{
    public class PhotosRepository
    {
        public long AddPhoto(string imageFile, long carId)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Photos (ImageFile,CarId) values (?,?)";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.String, Value = imageFile });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int64, Value = carId });
            command.ExecuteNonQuery();
            return conn.LastInsertRowId;
        }

        public void DeletePhotosByCarId(long carId)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "DELETE FROM Photos WHERE CarId = ?";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int64, Value = carId });
            command.ExecuteNonQuery();
        }

        public List<string> GetByCarId(long carId)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT ImageFile FROM Photos WHERE CarId = ?";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int64, Value = carId });

            var reader = command.ExecuteReader();
            var result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }
    }
}