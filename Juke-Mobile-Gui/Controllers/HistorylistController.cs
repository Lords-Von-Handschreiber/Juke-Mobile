using Juke_Mobile_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Juke_Mobile_Gui.Controllers
{
    public class HistorylistController : RavenController
    {
        // GET api/<controller>
        public dynamic Get()
        {
            var pl = PlayRequestManager.GetPlayList(PlayRequest.PlayRequestTypeEnum.History);
            var aaData = pl.Select(pr => new HistoryRequest(pr));

            return new
            {
                sEcho = 1,
                iTotalRecords = aaData.Count(),
                iTotalDisplayRecords = aaData.Count(),
                aaData = aaData
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}