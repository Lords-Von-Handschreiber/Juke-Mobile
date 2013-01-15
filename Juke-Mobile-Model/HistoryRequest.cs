using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juke_Mobile_Model
{
    public class HistoryRequest : DTPlayRequest
    {
        public string Zeit { get; set; }

        public HistoryRequest(PlayRequest pr)
            : base(pr)
        {
            Zeit = pr.PlayedDateTime.ToString("dd.MM.yy HH:mm:ss");
        }
    }
}
