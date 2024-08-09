using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.CourseManagement.Common;
using System.CourseManagement.DataAccess.DataEntity;
using System.CourseManagement.Model;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace System.CourseManagement.DataAccess
{
    public class MySqlDataAccess
    {
        private static MySqlDataAccess instance;
        private MySqlDataAccess() { }
        public static MySqlDataAccess GetInstance()
        {
            return instance ?? (instance = new MySqlDataAccess());
        }

        MySqlConnection conn;
        MySqlCommand comm;
        MySqlDataAdapter adapter;

        private void Dispose()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
        }

        private bool DBConnection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            if (conn == null)
            {
                conn = new MySqlConnection(connStr);
            }
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserEntity CheckUserInfo(string userName, string pwd)
        {
            try
            {
                if (DBConnection())
                {
                    string userSql = "SELECT * FROM users WHERE user_name=@user_name AND password=@pwd AND is_validation=1";
                    adapter = new MySqlDataAdapter(userSql, conn);
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@user_name", MySqlDbType.VarChar)
                    {
                        Value = userName
                    });
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("@pwd", MySqlDbType.VarChar)
                    {
                        Value = MD5Provider.GetMD5String(pwd + "@" + userName)
                    });

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    if (count <= 0)
                        throw new Exception("用户名或密码不正确！");

                    DataRow dr = table.Rows[0];
                    if (dr.Field<int>("is_can_login") == 0)
                    {
                        throw new Exception("当前用户没有权限使用此平台");
                    }

                    UserEntity userInfo = new UserEntity();
                    userInfo.UserName = dr.Field<string>("user_name");
                    userInfo.RealName = dr.Field<string>("real_name");
                    userInfo.Password = dr.Field<string>("password");
                    userInfo.Avatar = dr.Field<string>("avatar");
                    userInfo.Gender = dr.Field<int>("gender");

                    return userInfo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }

        public List<CourseSeriesModel> GetCoursePlayRecord()
        {
            try
            {
                List<CourseSeriesModel> cModelList = new List<CourseSeriesModel>();
                if (DBConnection())
                {
                    string userSql = @"SELECT a.course_name, a.course_id, b.play_count, b.is_growing, b.growing_rate, 
                                       c.platform_name
                                       FROM courses a
                                       LEFT JOIN course_platform b 
                                       ON a.course_id = b.course_id
                                       LEFT JOIN platforms c
                                       ON b.platform_id = c.platform_id
                                       ORDER BY a.course_id, c.platform_id";
                    adapter = new MySqlDataAdapter(userSql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    int courseId = 0;
                    CourseSeriesModel cModel = null;

                    foreach(DataRow dr in table.AsEnumerable())
                    {
                        int tempId = dr.Field<int>("course_id");
                        if (tempId != courseId)
                        {
                            courseId = tempId;
                            cModel = new CourseSeriesModel();
                            cModelList.Add(cModel);

                            cModel.CourseName = dr.Field<string>("course_name");
                            cModel.SeriesCollection = new LiveCharts.SeriesCollection();
                            cModel.SeriesList = new Collections.ObjectModel.ObservableCollection<SeriesModel>();
                        }
                        if(cModel != null)
                        {
                            cModel.SeriesCollection.Add(new PieSeries
                            {
                                Title = dr.Field<string>("platform_name"),
                                Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<decimal>("play_count")) },
                                DataLabels=false
                            });
                            cModel.SeriesList.Add(new SeriesModel
                            {
                                SeriesName = dr.Field<string>("platform_name"),
                                CurrentValue = dr.Field<decimal>("play_count"),
                                IsGrowing = dr.Field<Int32>("is_growing") == 1,
                                ChangeRate = (int)dr.Field<decimal>("growing_rate")
                            });
                        }
                    }
                }
                return cModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        public List<string> GetPermanents()
        {
            try
            {
                List<string> result = new List<string>();
                if (this.DBConnection())
                {
                    string sql = "select real_name from users where is_permanent=1";
                    adapter = new MySqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)
                    {
                        result = table.AsEnumerable().Select(c => c.Field <string>("real_name")).ToList();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        public List<CourseModel> GetCourses()
        {
            try
            {
                List<CourseModel> result = new List<CourseModel>();
                if (this.DBConnection())
                {
                    string sql = @"select a.course_id, a.course_name, a.course_url, a.course_cover, a.description,
                           c.real_name from courses a
                           left join courses_permanent_relation b
                           on a.course_id = b.courses_id
                           left join users c
                           on b.permanent_id = c.user_id
                           order by a.course_id";
                    adapter = new MySqlDataAdapter(sql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);
                    if (count > 0)
                    {
                        int courseId = 0;
                        CourseModel model = null;

                        foreach (DataRow dr in table.AsEnumerable())
                        {
                            int tempId = dr.Field<int>("course_id");
                            if (courseId != tempId)
                            {
                                courseId = tempId;
                                model = new CourseModel
                                {
                                    CourseName = dr.Field<string>("course_name"),
                                    Cover = dr.Field<string>("course_cover"),
                                    Url = dr.Field<string>("course_url"),
                                    Description = dr.Field<string>("description"),
                                    Permanents = new List<string>()
                                };
                                result.Add(model);
                            }
                            if (model != null)
                            {
                                model.Permanents.Add(dr.Field<string>("real_name"));
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

    }
}
