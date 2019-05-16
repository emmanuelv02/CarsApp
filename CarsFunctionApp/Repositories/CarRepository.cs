using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using CarsFunctionApp.Entities;

namespace CarsFunctionApp.Repositories
{
    public class CarRepository
    {
        public long AddCar(Car car)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Cars (IsNew,Brand,Model,Year,Price) values (?,?,?,?,?)";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int32, Value = (car.IsNew ? 1 : 0) });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.String, Value = car.Brand });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.String, Value = car.Model });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int32, Value = car.Year });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int32, Value = car.Price });
            command.ExecuteNonQuery();
            return conn.LastInsertRowId;
        }

        public void DeleteCar(Car car)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "DELETE FROM Cars WHERE Id = ?";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int64, Value = car.Id });
            command.ExecuteNonQuery();
        }

        public void UpdateCar(Car car)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "UPDATE Cars SET IsNew = ?, Brand = ?, Model = ?, Year = ?, Price = ? WHERE Id = ?";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int32, Value = (car.IsNew ? 1 : 0) });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.String, Value = car.Brand });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.String, Value = car.Model });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int32, Value = car.Year });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int32, Value = car.Price });
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int64, Value = car.Id });
            command.ExecuteNonQuery();
        }

        public List<Car> GetAll()
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Cars";

            var reader = command.ExecuteReader();
            var result = new List<Car>();
            while (reader.Read())
            {
                var car = new Car
                {
                    Id = reader.GetInt32(0),
                    IsNew = reader.GetBoolean(1),
                    Brand = reader.GetString(2),
                    Model = reader.GetString(3),
                    Year = reader.GetInt32(4),
                    Price = reader.GetFloat(5)
                };
                result.Add(car);
            }

            return result;
        }

        public List<string> GetAllBrands()
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT Distinct Brand FROM Cars";
            var reader = command.ExecuteReader();

            var result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }

        public List<string> GetModelsByBrand(string brand)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT Distinct Model FROM Cars WHERE Brand = ?";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.String, Value = brand });

            var reader = command.ExecuteReader();

            var result = new List<string>();
            while (reader.Read())
            {
                result.Add(reader.GetString(0));
            }

            return result;
        }

        public Car GetById(int id)
        {
            var conn = SqLiteConnectionManager.GetConnection();
            var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Cars WHERE Id = ?";
            command.Parameters.Add(new SQLiteParameter { DbType = DbType.Int64, Value = id });

            var reader = command.ExecuteReader();

            Car result = null;
            while (reader.Read())
            {
                result = new Car
                {
                    Id = reader.GetInt32(0),
                    IsNew = reader.GetBoolean(1),
                    Brand = reader.GetString(2),
                    Model = reader.GetString(3),
                    Year = reader.GetInt32(4),
                    Price = reader.GetFloat(5)
                };
                break;
            }

            return result;
        }
    }
}
