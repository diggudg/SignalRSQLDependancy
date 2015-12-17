using SQLDependancySignalR.Hubs;
using SQLDependancySignalR.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SQLDependancySignalR
{
    public class JobInfoRepository
    {
        SignalREntities sr = new SignalREntities();
        //this comment 
        public IEnumerable<JobInfo> GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaulConnection"].ConnectionString))
            {
                connection.Open();
                string a = @"SELECT COUNT(*) as Votes,UserId,Band  FROM [dbo].[Vote] group by UserId,Band order by Votes desc";
                //using (SqlCommand command = new SqlCommand(@"SELECT COUNT(*) as Votes,UserId,Band  FROM [dbo].[Vote] group by UserId,Band order by Votes desc", connection))
                //using (SqlCommand command = new SqlCommand(@"SELECT [Id],[UserId],[Band] FROM [dbo].[Vote] ", connection))
                using (SqlCommand command = new SqlCommand(a, connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.using 
                    //(SqlCommand command = new SqlCommand(@"SELECT [JobID],[Name],[LastExecutionDate],[Status]
                   // FROM[dbo].[JobInfo]", connection))
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new JobInfo()
                            {
                                JobID = x.GetInt32(0),
                                Name = x.GetInt32(1),
                                LastExecutionDate = x.GetString(2),
                               // Status= x.GetString(3)
                            }).ToList();



                }
            }
        }
        public List<FinalVoting> GetJob()
        {
            List<FinalVoting> a = new List<FinalVoting>();
            FinalVoting b = new FinalVoting();
            var totalvoteE = sr.Votes.Where(v => v.Band == "E").ToList().GroupBy(g=>g.UserId);
            //var totalcount = 
            foreach(var total in totalvoteE)
            {
                var c = new FinalVoting
                {
                    TotalVotes= total.Count(),
                    EmpCode=total.Key.ToString()
                };
                a.Add(c);
            }
            return a;
        }
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            JobHub.Show();
        }


    }


    public class JobInfo
    {
        public int JobID { get; set; }
        public int Name { get; set; }
        public string LastExecutionDate { get; set; }
        public string Status { get; set; }
    }
    public class FinalVoting
    {
        public string Name  { get; set; }
        public int TotalVotes { get; set; }
        public string EmpCode { get; set; }
    }
}