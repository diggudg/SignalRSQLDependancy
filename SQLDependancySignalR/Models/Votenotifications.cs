using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQLDependancySignalR.Models
{
    public class Votenotifications
    {

        public List<Vote> getVoteCount()
        {
            SignalREntities sre = new SignalREntities();
            var count = sre.Votes.ToList();
            return count;
        }
    }
}